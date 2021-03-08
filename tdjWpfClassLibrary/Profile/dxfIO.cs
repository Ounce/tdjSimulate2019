/*
 * 这个类调用 netDXF.dll 内的函数实现导出 纵断面 到 dxf 格式文件中。 netDxf是Github上的一个项目：https://github.com/haplokuon/netDxf
 * netDxf.dll的使用方法：
 *      - 在引用中，添加netDxf.dll。
 *      - using netDxf;
 * 
 * 采用函数导出，ProfileDrawing为参数。由于Canvas 与 CAD 显示的方式不同，导出的dxf，按照1:1设置参数，不再使用HorizontalScale，
 * 按照习惯高程与长度的比例保留，采用Option中的VerticalHorizontalScale（一般为50）。
 * 
 * 纵断面采用Line格式 ，便于后期修改。没有使用Polyline。
 */



using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using netDxf;
using netDxf.Entities;
using netDxf.Tables;
using tdjWpfClassLibrary;

namespace tdjWpfClassLibrary.Profile
{
    /// <summary>
    /// 将纵断面导出到 dxf 格式。
    /// </summary>
    public class DXFIO
    {
        public bool DesignProfile = true;   // 是否导出DesignProfile
        public bool DesignProfileGradeTable = true;
        public bool DesignProfileAltitude = true;
        public bool ExistProfile = true;
        public bool ExistProfileGradeTable = true;
        public bool ExistProfileAltitude = true;
        public bool AltitudeDifference = true;

        public double VerticalHorizontalScale = 50;
        public int GradeUnit = 1000;

        private double GradeLineBottom = 0;    // 坡度图的底的 Y 轴坐标。
        private double GradeLineBottomAltitude; // 坡度图底部高程。
        private double TableTop = 0;     // 表底 Y 轴坐标。

        public double TextLineSpace = 1;    // 文字与参考位置的间距。
        public double FontHeight = 12;  // Text.Height 文字的高度。

        public double GradeLineTableHeight = 40;
        public double AltitudeTableHeight = 84;

        public double TableHeaderWidth = 120;

        private double GradeLineLeft = 0;   // 坡度图第一段坡段的起点 X 轴坐标。
        private double TableLeft = 0;   // 坡段表中第一坡段的起点 X 轴坐标。表头则为负值。

        private double top;
        private double length;

        DxfDocument dxfDocument;

        public void ExportToDXF(ProfileViewModelCollection Profiles, string fileName, AltitudeDifferences altitudeDifferences)
        {
            dxfDocument = new DxfDocument();

            GradeLineBottomAltitude = Profiles.MinAltitude;
            VerticalHorizontalScale = Scale.VerticalHorizontalScale;
            for (int i = 0; i < Profiles.Count; i++)
            {
                foreach (SlopeViewModel slope in Profiles[i].Slopes)
                {
                    Line entity = new Line(new Vector2(slope.BeginMileage + GradeLineLeft, GetGradeLineY(slope.BeginAltitude)),
                                           new Vector2(slope.EndMileage + GradeLineLeft, GetGradeLineY(slope.EndAltitude)));
                    dxfDocument.AddEntity(entity);
                }
            }
            top = 0;
            length = Profiles.Length;
            if (DesignProfileGradeTable)
            {
                GradeTable(Profiles[0]);
            }
            if (AltitudeDifference)
            {
                double bottom1 = top - AltitudeTableHeight;
                double bottom2 = bottom1 - AltitudeTableHeight;
                double bottom3 = bottom2 - AltitudeTableHeight;
                foreach (AltitudeDifference item in altitudeDifferences.Items)
                {
                    Text text1 = new Text(item.DesignAltitude.ToString(), new Vector2(item.Mileage + TableLeft, bottom1 + TextLineSpace), FontHeight);
                    text1.Rotation = 90;
                    dxfDocument.AddEntity(text1);
                    Text text2 = new Text(NullableDoubleConvert.ToString(item.Difference, 3), new Vector2(item.Mileage + TableLeft, bottom2 + TextLineSpace), FontHeight);
                    text2.Rotation = 90;
                    dxfDocument.AddEntity(text2);
                    Text text3 = new Text(item.ExistAltitude.ToString(), new Vector2(item.Mileage + TableLeft, bottom3 + TextLineSpace), FontHeight);
                    text3.Rotation = 90;
                    dxfDocument.AddEntity(text3);
                }
                TableHeaderRow("设计高程", bottom1, AltitudeTableHeight);
                TableHeaderRow("高差", bottom2, AltitudeTableHeight);
                TableHeaderRow("既有高程", bottom3, AltitudeTableHeight);
                top = bottom3;
            }
            else
            {
                if (DesignProfileAltitude)
                {
                    AltitudeTable(Profiles[0], "设计高程");
                }
                if (ExistProfileAltitude)
                {
                    AltitudeTable(Profiles[1], "既有高程");
                }

            }
            if (ExistProfileGradeTable)
            {
                GradeTable(Profiles[1]);
            }
            
            bool result = true;
            while (result)
            {
                if (IsFileInUse(fileName))
                {
                    if (MessageBox.Show('\"' + fileName + "\"可能是被其他程序占用，请关闭后再试!", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel)
                    {
                        result = false;
                        MessageBox.Show("数据未导出！");
                    }
                }
                else
                {
                    dxfDocument.Save(fileName);
                    break;
                }
            }
        }

        private void AltitudeTable(ProfileViewModel profile, string headerName)
        {
            double bottom = top - AltitudeTableHeight;
            foreach (SlopeViewModel slope in profile.Slopes)
            {
                Text text = new Text(slope.EndAltitude.ToString(), new Vector2(slope.EndMileage + TableLeft, bottom + TextLineSpace), FontHeight);
                text.Rotation = 90;
                dxfDocument.AddEntity(text);
            }
            TableHeaderRow(headerName, bottom, GradeLineTableHeight);
            top = bottom;
        }

        private void GradeTable(ProfileViewModel profile)
        {
            double bottom = top - GradeLineTableHeight;
            LwPolyline polyline = new LwPolyline();
            polyline.Vertexes.Add(new LwPolylineVertex(TableLeft, top));
            polyline.Vertexes.Add(new LwPolylineVertex(length, top));
            polyline.Vertexes.Add(new LwPolylineVertex(length, bottom));
            polyline.Vertexes.Add(new LwPolylineVertex(TableLeft, bottom));
            polyline.Vertexes.Add(new LwPolylineVertex(TableLeft, top));
            dxfDocument.AddEntity(polyline);
            double y1, y2;
            double gradeTextPosition, lengthTextPosition;
            foreach (SlopeViewModel slope in profile.Slopes)
            {
                Text gradeText = new Text();
                Text lengthText = new Text();
                if (slope.Grade > 0.001)
                {
                    y1 = top;
                    y2 = bottom;
                    gradeTextPosition = slope.EndMileage - TextLineSpace;
                    gradeText.Alignment = netDxf.Entities.TextAlignment.BaselineRight;
                    lengthTextPosition = slope.BeginMileage + TextLineSpace;
                    lengthText.Alignment = netDxf.Entities.TextAlignment.BaselineLeft;
                }
                else if (slope.Grade < -0.001)
                {
                    y1 = bottom;
                    y2 = top;
                    gradeText.Alignment = netDxf.Entities.TextAlignment.BaselineLeft;
                    gradeTextPosition = slope.BeginMileage + TextLineSpace;
                    lengthTextPosition = slope.EndMileage - TextLineSpace;
                    lengthText.Alignment = netDxf.Entities.TextAlignment.BaselineRight;
                }
                else
                {
                    y1 = y2 = top - (0.5 * (top - bottom));
                    gradeText.Alignment = lengthText.Alignment = netDxf.Entities.TextAlignment.BaselineCenter;
                    gradeTextPosition = lengthTextPosition = 0.5 * (slope.BeginMileage + slope.EndMileage);
                }
                gradeText.Value = slope.Grade.ToString();
                gradeText.Position = new Vector3(gradeTextPosition, top - FontHeight - TextLineSpace, 0);
                gradeText.Height = FontHeight;
                dxfDocument.AddEntity(gradeText);
                lengthText.Value = slope.Length.ToString();
                lengthText.Position = new Vector3(lengthTextPosition, bottom + TextLineSpace, 0);
                lengthText.Height = FontHeight;
                dxfDocument.AddEntity(lengthText);
                Line gradeLine = new Line(new Vector2(slope.BeginMileage + TableLeft, y1), new Vector2(slope.EndMileage + TableLeft, y2));
                Line sepLine = new Line(new Vector2(slope.EndMileage + TableLeft, top), new Vector2(slope.EndMileage + TableLeft, bottom));
                dxfDocument.AddEntity(gradeLine);
                dxfDocument.AddEntity(sepLine);
            }
            TableHeaderRow(profile.Title, bottom, GradeLineTableHeight);
            top = bottom;
        }

        public void TableHeaderRow(string name, double bottom, double height)
        {
            LwPolyline polyline = new LwPolyline();
            polyline.Vertexes.Add(new LwPolylineVertex(TableLeft, top));
            polyline.Vertexes.Add(new LwPolylineVertex(-TableHeaderWidth, top));
            polyline.Vertexes.Add(new LwPolylineVertex(-TableHeaderWidth, bottom));
            polyline.Vertexes.Add(new LwPolylineVertex(TableLeft, bottom));
            polyline.Vertexes.Add(new LwPolylineVertex(TableLeft, top));
            dxfDocument.AddEntity(polyline);
            TextStyle textStyle = new TextStyle("Chinese text", "simsun.ttf");

            Text text = new Text(name, new Vector2(-0.5 * TableHeaderWidth, 0.5 * height + bottom), FontHeight, textStyle);
            text.Alignment = netDxf.Entities.TextAlignment.MiddleCenter;
            dxfDocument.AddEntity(text);
        }

        private double GetGradeLineY(double altitude)
        {
            return (altitude - GradeLineBottomAltitude) * VerticalHorizontalScale + GradeLineBottom;
        }

        /// <summary>
        /// 判断文件是否被占用，返回bool值，被占用返回True
        /// </summary>
        /// <param name="fileName">带文件名的路径，带后缀</param>
        /// <returns></returns>        
        public bool IsFileInUse(string fileName)
        {
            bool inUse = true;

            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read,
                FileShare.None);
                inUse = false;
            }
            catch
            {
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
            return inUse;//true表示正在使用,false没有使用  
        }
    }
}
