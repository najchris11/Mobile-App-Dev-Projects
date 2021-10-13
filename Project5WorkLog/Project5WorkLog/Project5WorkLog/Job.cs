using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Essentials;

namespace Project5WorkLog
{

    [Table("job")]
    public class Job
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public DateTime date { get; set; }
        public string jobName { get; set; }
        public bool oddHrs { get; set; }
        public int hours { get; set; }
        public int mins {get; set;}

        public override string ToString()
        {
            return string.Format("{0} for {1}:{2} " + (oddHrs ? "odd hours" : "regular hours") + " on " + date.ToShortDateString() , jobName, hours, mins);
        }
    }
}
