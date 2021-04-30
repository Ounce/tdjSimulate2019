using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace tdjWpfClassLibrary.Retarder
{
    /// <summary>
    /// 减速顶位置。有关减速顶位置，步长内通过数量等计算。
    /// </summary>
    public class Positions : NotifyPropertyChanged
    {
        /// <summary>
        /// 起点里程。
        /// </summary>
        public double Start { get; set; }

        /// <summary>
        /// 终点里程。
        /// </summary>
        public double End { get; set; }

        /// <summary>
        /// 各顶位置。
        /// </summary>
        private ObservableCollection<double> Items { get; set; }

        /// <summary>
        /// 计算startPosition与endPosition之间的数量。要求startPosition小于endPosition。
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="endPosition"></param>
        /// <returns></returns>
        public int PassQuantity(double startPosition, double endPosition)
        {
            if (Items.Count < 1 || startPosition == endPosition)
                return 0;
            if (startPosition > Items[Items.Count - 1] || endPosition < Items[0])
                return 0;
            int q = 0;
            foreach (double p in Items)
            {
                if (p >= startPosition && p < endPosition)
                    q++;
            }
            return q;
        }

        /// <summary>
        /// 设置Position。双侧twoTailed = true；
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="endPosition"></param>
        /// <param name="quantity"></param>
        /// <param name="twoTailed"></param>
        public void SetPositions(double startPosition, double endPosition, int quantity, bool twoTailed)
        {
            int i;
            double Step;
            double Pos = 0;
            bool S;
            double StepNumber;

            if (quantity < 1) return;
            if (twoTailed)
            {
                if (quantity % 2 > 0)
                    StepNumber = (int)(0.5 * quantity) + 1;
                else
                    StepNumber = (int)(0.5 * quantity);
                Step = (endPosition - startPosition) / StepNumber;
                S = true;
                i = 0;
                Items.Clear();
                while (i < quantity)
                {
                    if (!S) Pos += Step;
                    S = !S;
                    Items.Add(startPosition + Pos);
                    i++;
                }
            }
            else
            {
                StepNumber = quantity;
                Step = (endPosition - startPosition) / StepNumber;
                i = 0;
                while (i < quantity)
                {
                    Pos += Step;
                    Items.Add(startPosition + Pos);
                    i++;
                }
            }
            return;
        }
    }
}
