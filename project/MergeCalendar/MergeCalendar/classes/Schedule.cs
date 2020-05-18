using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeCalendar.classes
{
    public class Schedule
    {
        private DateTime begin;
        private DateTime end;
        public int Id { get; set; }
        public DateTime Begin {
            //get{
            //    return begin.Year.ToString() + "-" + begin.Month.ToString() + "-" + begin.Day.ToString();
            //}
            //set {
            //    string[] dates = value.Split('-');
            //    DateTime dateTime = new DateTime(Convert.ToInt32(dates[0]), Convert.ToInt32(dates[1]), Convert.ToInt32(dates[2]));
            //    begin = dateTime;
            //}
            get; set;
        }
        public DateTime End {
            //get {
            //    return end.Year.ToString() + "-" + end.Month.ToString() + "-" + end.Day.ToString();
            //}
            //set {
            //    string[] dates = value.Split('-');
            //    DateTime dateTime = new DateTime(Convert.ToInt32(dates[0]), Convert.ToInt32(dates[1]), Convert.ToInt32(dates[2]));
            //    end = dateTime;
            //}
            get; set;
        }
        public string Description { get; set; }
        public string Source { get; set; }

        public override string ToString()
        {
            return Description; 
        }
    }
}
