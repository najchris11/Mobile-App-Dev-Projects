using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FinalProject
{
    [Table("tasks")]
    class Task
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public DateTime date { get; set; }
        public string taskName { get; set; }
        public string taskCategory { get; set; }
        public bool isActivity { get; set; }
        public bool isComplete { get; set; }
        public int hours { get; set; }
        public int mins { get; set; }
        public int priority { get; set; }
        public TimeSpan timeElapsed { get; set; }


        public override string ToString()
        {
            return string.Format("{0} for {1}:{2} " + " on " + date.ToShortDateString(), taskName, hours, mins);
        }
    }
}
