using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Hethongquanlylab.Models
{
    public class Member
    {
        private string key;
        private string labID;
        private string avt;
        private string name;
        private string sex;
        private string birthday;
        private string gen;
        private string specialization;
        private string university;
        private string phone;
        private string email;
        private string address;
        private string unit;
        private string position;
        private bool isLT;
        private bool isPassPTBT;
        private int status;
        private string assessment;

        public string Key { get => key; set => key = value; }
        public string LabID { get => labID; set => labID = value; }
        public string Avt { get => avt; set => avt = value; }
        public string Name { get => name; set => name = value; }
        public string Sex { get => sex; set => sex = value; }
        public string Birthday { get => birthday; set => birthday = value; }
        public string Gen { get => gen; set => gen = value; }
        public string Specialization { get => specialization; set => specialization = value; }
        public string University { get => university; set => university = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Email { get => email; set => email = value; }
        public string Address { get => address; set => address = value; }
        public string Unit { get => unit; set => unit = value; }
        public string Position { get => position; set => position = value; }
        public bool IsLT { get => isLT; set => isLT = value; }
        public bool IsPassPTBT { get => isPassPTBT; set => isPassPTBT = value; }
        public int Status { get => status; set => status = value; }
        public string Assessment { get => assessment; set => assessment = value; }

        public Member(string labid, string avt, string name, string sex, string birthday, string gen, string phone, string email, string address, string specilization, string university, string unit, string position, bool isLT, bool isPassPTBT, string assessment, string key = "1", int status = 1)
        {
            this.Key = key;
            this.LabID = labid;
            this.Avt = avt;
            this.Name = name;
            this.Sex = sex;
            this.Birthday = birthday;
            this.Gen = gen;
            this.Phone = phone;
            this.Email = email;
            this.Address = address;
            this.Specialization = specilization;
            this.University = university;
            this.Unit = unit;
            this.Position = position;
            this.IsLT = isLT;
            this.isPassPTBT = isPassPTBT;
            this.Status = status;
            this.Assessment = assessment;
        }
        public Member( string avt, string name, string sex, string birthday, string phone, string email, string address, string specialization, string university, string assessment, string key = "1")
        {
            this.Key = key;
            this.Avt = avt;
            this.Name = name;
            this.Sex = sex;
            this.Birthday = birthday;
            this.Gen = gen;
            this.Phone = phone;
            this.Email = email;
            this.Address = address;
            this.Specialization = specialization;
            this.University = university;
            this.Unit = unit;
            this.Position = position;
            this.IsLT = isLT;
            this.IsPassPTBT = isPassPTBT;
            this.Assessment = assessment;
        }
        public Member(DataRow row)
        {
            this.Key = row["Key"].ToString();
            this.LabID = row.IsNull("LabID")? "N/A" : row["LabID"].ToString();
            this.Avt = row.IsNull("Avt") ? "defaulf.jpg" : row["Avt"].ToString();
            this.Name = row["Name"] == null ? "N/A" : row["Name"].ToString();
            this.Sex = row.IsNull("Sex")? "N/A" : row["Sex"].ToString();
            this.Birthday = row.IsNull("Birthday")? "N/A" : row["Birthday"].ToString();
            this.Gen = row.IsNull("Gen") ? "N/A" : row["Gen"].ToString();
            this.Phone = row.IsNull("Phone") ? "N/A" : row["Phone"].ToString();
            this.Email = row.IsNull("Email") ? "N/A" : row["Email"].ToString();
            this.Address = row.IsNull("Address") ? "N/A" : row["Address"].ToString();
            this.Specialization = row.IsNull("Specialization") ? "N/A" : row["Specialization"].ToString();
            this.University = row.IsNull("University") ? "N/A" : row["University"].ToString();
            this.Unit = row.IsNull("Unit") ? "Chưa có" : row["Unit"].ToString();
            this.Position = row.IsNull("Position") ? "Chưa có" : row["Position"].ToString();
            this.Assessment = row.IsNull("Assessment") ? "Chưa có" : row["Assessment"].ToString();

            this.IsLT = Convert.ToBoolean(row.IsNull("IsLT") ? 0 : row["IsLT"]);
            this.IsPassPTBT = Convert.ToBoolean(row.IsNull("IsPassPTBT")? 0 : row["IsPassPTBT"]);
            this.Status = Convert.ToInt32(row.IsNull("Status") ? 1 : row["Status"]);
        }
    }
}
