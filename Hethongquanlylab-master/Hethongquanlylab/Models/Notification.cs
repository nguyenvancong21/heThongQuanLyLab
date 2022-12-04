using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Hethongquanlylab.Models
{
    public class Notification
    {
        private int id;
        private string title;
        private string content;
        private string unit;
        private string date;
        private string link;
        private bool inner;

        public int ID { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Content { get => content; set => content = value; }
        public string Unit { get => unit; set => unit = value; }
        public string Date { get => date; set => date = value; }
        public string Link { get => link; set => link = value; }
        public bool Inner { get => inner; set => inner = value; }

        public Notification(string title, string content,string unit, string date, string link, bool inner, int ID = 1)
        {
            this.ID = ID;
            this.Title = title;
            this.Content = content;
            this.Unit = unit;
            this.Date = date;
            this.Link = link;
            this.Inner = inner;
        }

        public Notification(DataRow row)
        {
            this.ID = Convert.ToInt32(row["ID"]);
            this.Title = row["Title"] == null ? "N/A" : row["Title"].ToString();
            this.Unit = row.IsNull("Unit") ? "N/A" : row["Unit"].ToString();
            this.Date = row.IsNull("Date") ? "N/A" : row["Date"].ToString();
            this.Content = row.IsNull("Content") ? "N/A" : row["Content"].ToString();
            this.Link = row.IsNull("Link") ? "N/A" : row["Link"].ToString();
            this.Inner = Convert.ToBoolean(row.IsNull("Inner") ? true : row["Inner"]);
        }
    }
}
