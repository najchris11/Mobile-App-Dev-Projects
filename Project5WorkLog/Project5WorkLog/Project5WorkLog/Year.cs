using System;
using System.Collections.Generic;
using System.Text;

namespace Project5WorkLog
{
    public class Hours
    {
        //int year { get; set; }
        public int hoursWorked { get; set; }
        public int minsWorked { get; set; }
        //public bool Equals(int yearNum)
        //{
        //    return year == yearNum;
        //}
        public override string ToString()
        {
            return hoursWorked + ":" + minsWorked;
        }
    }
}
