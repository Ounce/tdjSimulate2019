using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using tdjWpfClassLibrary.Wagon;

namespace tdjWpfClassLibrary
{
    public static class XmlHelper
    {
        public static void WriteXML(string fileName, object model)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();            //Create our own namespaces for the output
            ns.Add("", ""); //Add an empty namespace and empty value
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(model.GetType());
            //var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//SerializationOverview.xml";
            System.IO.FileStream file = System.IO.File.Create(fileName);
            //writer.Serialize(file, model, ns);
            writer.Serialize(file, model, ns);
            file.Close();
        }

        /// <summary>
        /// 序列化方式读取数据model。typeof（WagonModelList），返回object，需要先是转换。
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static object ReadXML(string fileName, Type model)
        {
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(model);
            //System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            XmlReader file = new XmlTextReader(fileName) { Namespaces = false };
            object o = reader.Deserialize(file);
            file.Close();
            if (o == null)
                o = model.GetConstructor(Type.EmptyTypes);  //  默认构造函数。
            return o;
        }
    }
}
