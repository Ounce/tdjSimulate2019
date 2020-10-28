using System;
using System.Collections.Generic;
using System.Text;

namespace tdjWpfClassLibrary.Profile
{
    public static class Grade
    {
        public static int Direction(double grade)
        {
            if (grade < -0.00001) return -1;
            else if (grade > 0.00001) return 1;
            else return 0;
        }
    }
}
