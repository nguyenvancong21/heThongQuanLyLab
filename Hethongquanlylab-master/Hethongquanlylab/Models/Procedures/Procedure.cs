using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace Hethongquanlylab.Models
{
    public class Procedure
    {
        private string id;
        private string subid;
        private string name;
        private string unit;
        private string senddate;
        private string content;
        private string appovalWaiting;
        private bool v1;
        private string bdhReply;
        private bool v2;
        private string bcvReply;
        private bool v3;
        private string nslReply;
        private string ndslReply;
        private string status;
        private string link;

        public string ID { get => id; set => id = value; }
        public string SubID { get => subid; set => subid = value; }
        public string Name { get => name; set => name = value; }
        public string Unit { get => unit; set => unit = value; }
        public string Senddate { get => senddate; set => senddate = value; }
        public string Content { get => content; set => content = value; }
        public string ApprovalWaiting { get => appovalWaiting; set => appovalWaiting = value; }
        public bool V1 { get => v1; set => v1 = value; }
        public bool V2 { get => v2; set => v2 = value; }
        public bool V3 { get => v3; set => v3 = value; }
        public string Status { get => status; set => status = value; }
        public string Link { get => link; set => link = value; }
        public string BdhReply { get => bdhReply; set => bdhReply = value; }
        public string BcvReply { get => bcvReply; set => bcvReply = value; }
        public string NSLReply { get => nslReply; set => nslReply = value; }
        public string NDSLReply { get => ndslReply; set => ndslReply = value; }



        public Procedure(string name, string unit, string content, string link, string id = "1", string subid = "SubID") // Thêm mới + chỉnh sửa
        {
            this.ID = id;
            this.SubID = subid;
            this.Name = name;
            this.Unit = unit;
            this.Link = link;
            this.Content = content;

            DateTime day = DateTime.Today;
            DateTimeFormatInfo fmt = (new CultureInfo("fr-FR")).DateTimeFormat;
            string senddate = day.ToString("d", fmt);

            this.Senddate = senddate;

            this.V1 = false;
            this.V2 = false;
            this.V3 = false;

            this.Status = "Chưa duyệt";
            this.BdhReply = "Chưa có phản hồi";
            this.BcvReply = "Chưa có phản hồi";
        }
        public Procedure(string id, string name, string unit, string content, string bdh, string bcv, string nsl, string ndsl, string link) // Phản hồi
        {
            this.ID = id;
            this.Name = name;
            this.Unit = unit;
            this.Link = link;
            this.Content = content;

            DateTime day = DateTime.Today;
            DateTimeFormatInfo fmt = (new CultureInfo("fr-FR")).DateTimeFormat;
            string senddate = day.ToString("d", fmt);

            this.Senddate = senddate;

            this.V1 = false;
            this.V2 = false;
            this.V3 = false;

            this.Status = "Chưa duyệt";
            this.BdhReply = bdh;
            this.BcvReply = bcv;
            this.NSLReply = nsl;
            this.NDSLReply = ndsl;
        }

        public Procedure(string id, string subid, string name, string unit, string senddate, string content, bool v1, string bdh,  bool v2, string bcv, bool v3, string nsl, string ndsl, string status, string link) // Load từ excel
        {
            this.ID = id;
            this.SubID = subid;
            this.Name = name;
            this.Unit = unit;
            this.Senddate = senddate;
            this.Link = link;
            this.Content = content;

            this.V1 = Convert.ToBoolean(v1);
            this.V2 = Convert.ToBoolean(v2);
            this.V3 = Convert.ToBoolean(v3);

            this.Status = status;
            this.BdhReply = bdh;
            this.BcvReply = bcv;
            this.NSLReply = nsl;
            this.NDSLReply = ndsl;
            this.Status = status;
        }

        public Procedure(DataRow row)
        {
            this.ID = row["ID"].ToString();
            this.SubID = row.IsNull("SubID") ? "N/A" : row["SubID"].ToString();
            this.Name = row["Name"] == null ? "N/A" : row["Name"].ToString();
            this.Unit = row.IsNull("Unit") ? "N/A" : row["Unit"].ToString();
            this.Senddate = row.IsNull("Senddate") ? "N/A" : row["Senddate"].ToString();
            this.Content = row.IsNull("Content") ? "N/A" : row["Content"].ToString();
            this.BdhReply = row.IsNull("BdhReply") ? "N/A" : row["BdhReply"].ToString();
            this.BcvReply = row.IsNull("BcvReply") ? "N/A" : row["BcvReply"].ToString();
            this.NSLReply = row.IsNull("NSLReply") ? "N/A" : row["NSLReply"].ToString();
            this.NDSLReply = row.IsNull("NDSLReply") ? "N/A" : row["NDSLReply"].ToString();
            this.Status = row.IsNull("Status") ? "N/A" : row["Status"].ToString();
            this.Link = row.IsNull("Link") ? "N/A" : row["Link"].ToString();

            this.ApprovalWaiting = row.IsNull("ApprovalWaiting") ? "N/A" : row["ApprovalWaiting"].ToString();
            this.V1 = Convert.ToBoolean(row.IsNull("V1") ? 0 : row["V1"]);
            this.V2 = Convert.ToBoolean(row.IsNull("V2") ? 0 : row["V2"]);
            this.V3 = Convert.ToBoolean(row.IsNull("V3") ? 1 : row["V3"]);
        }
    }
}
