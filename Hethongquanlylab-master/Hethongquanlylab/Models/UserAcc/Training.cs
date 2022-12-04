using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Hethongquanlylab.Models
{
    public class Training
    {
        private string id;
        private string subid;
        private string name;
        private string speaker;
        private string link;
        private string date;
        private string unit;
        private string content;

        public string ID { get => id; set => id = value; }
        public string SubID { get => subid; set => subid = value; }
        public string Name { get => name; set => name = value; }
        public string Speaker { get => speaker; set => speaker = value; }
        public string Link { get => link; set => link = value; }
        public string Date { get => date; set => date = value; }
        public string Unit { get => unit; set => unit = value; }
        public string Content { get => content; set => content = value; }

        public Training(string name, string speaker, string unit, string content, string link, string id = "1", string subid = "SubID") // Thêm mới + chỉnh sửa
        {
            this.ID = id;
            this.SubID = subid;
            this.Name = name;
            this.Speaker = speaker;
            this.Unit = unit;
            this.Link = link;
            this.Content = content;

            DateTime day = DateTime.Today;
            DateTimeFormatInfo fmt = (new CultureInfo("fr-FR")).DateTimeFormat;
            string senddate = day.ToString("d", fmt);

            this.Date = senddate;
        }

        public Training (string id, string subid, string name, string speaker, string link, string date, string unit, string content) // Load
        {
            this.ID = id;
            this.SubID = subid;
            this.Name = name;
            this.Speaker = speaker;
            this.Link = link;
            this.Date = date;
            this.Unit = unit;
            this.Content = content;
        }

        public Training(DataRow row)
        {
            this.ID = row["ID"].ToString();
            this.SubID = row.IsNull("SubID") ? "N/A" : row["SubID"].ToString();
            this.Name = row["Name"] == null ? "N/A" : row["Name"].ToString();
            this.Speaker = row["Speaker"] == null ? "N/A" : row["Speaker"].ToString();
            this.Unit = row.IsNull("Unit") ? "N/A" : row["Unit"].ToString();
            this.Date = row.IsNull("Date") ? "N/A" : row["Date"].ToString();
            this.Content = row.IsNull("Content") ? "N/A" : row["Content"].ToString();
            this.Link = row.IsNull("Link") ? "N/A" : row["Link"].ToString();
        }
    }
}
