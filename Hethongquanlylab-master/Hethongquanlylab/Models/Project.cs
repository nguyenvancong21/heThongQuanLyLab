using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace Hethongquanlylab.Models
{
    public class Project
    {
        private string id;
        private string name;
        private string startday;
        private string endday;
        private string projectType;
        private string content;
        private string status;
        private string unit;
        private string member;
        private string link;

        public string ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Startday { get => startday; set => startday = value; }
        public string Endday { get => endday; set => endday = value; }
        public string Type { get => projectType; set => projectType = value; }
        public string Content { get => content; set => content = value; }
        public string Status { get => status; set => status = value; }
        public string Unit { get => unit; set => unit = value; }
        public string Member { get => member; set => member = value; }
        public string Link { get => link; set => link = value; }

        public Project(string id, string name, string startday, string endday, string projectType, string content, string status, string unit, string member, string link)
        {
            this.ID = id;
            this.Name = name;
            this.Startday = startday;
            this.Endday = endday;
            this.Type = projectType;
            this.Content = content;
            this.Status = status;
            this.Unit = unit;
            this.Member = member;
            this.Link = link;
        }

        public Project(DataRow row)
        {
            this.ID = row["ID"].ToString();
            this.Name = row["Name"] == null ? "N/A" : row["Name"].ToString();
            this.Startday = row["StartDay"] == null ? "N/A" : row["StartDay"].ToString();
            this.Endday = row["EndDay"] == null ? "N/A" : row["EndDay"].ToString();
            this.Unit = row.IsNull("Unit") ? "N/A" : row["Unit"].ToString();
            this.Type = row.IsNull("Type") ? "N/A" : row["Type"].ToString();
            this.Content = row.IsNull("Content") ? "N/A" : row["Content"].ToString();
            this.Status = row.IsNull("Status") ? "N/A" : row["Status"].ToString();
            this.Member = row.IsNull("Member") ? "N/A" : row["Member"].ToString();
            this.Link = row.IsNull("Link") ? "N/A" : row["Link"].ToString();
        }
    }
}
