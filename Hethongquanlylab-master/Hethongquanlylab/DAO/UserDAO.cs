using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Hethongquanlylab.Models;
using OfficeOpenXml;
using System.IO;
using System.Globalization;

namespace Hethongquanlylab.DAO
{
    public class UserDAO
    {
        private static UserDAO instance;
        public static UserDAO Instance
        {
            get { if (instance == null) instance = new UserDAO(); return UserDAO.instance; }
            private set { UserDAO.instance = value; }
        }

        private UserDAO() { }

        public List<Member> GetListUser()
        {
            List<Member> members = DataProvider<Member>.Instance.GetListItem();
            return members;
        }

        public List<Member> GetListUser(string UnitVar)
        {
            List<Member> members = new List<Member>();
            if (UnitVar == "PT")
            {
                List<Member> memberList = DataProvider<Member>.Instance.GetListItem();
                members.AddRange(memberList.Where(s => CultureInfo.CurrentCulture.CompareInfo.IndexOf(s.Unit, "PT", CompareOptions.IgnoreCase) >= 0).ToList());
                members.AddRange(memberList.Where(s => CultureInfo.CurrentCulture.CompareInfo.IndexOf(s.Unit, "PowerTeam", CompareOptions.IgnoreCase) >= 0).ToList());
                members.AddRange(memberList.Where(s => CultureInfo.CurrentCulture.CompareInfo.IndexOf(s.Unit, "Power Team", CompareOptions.IgnoreCase) >= 0).ToList());
            }
            else if (UnitVar == "LT") members = DataProvider<Member>.Instance.GetListItem("IsLT", "1");
            else if (UnitVar == "All") members = DataProvider<Member>.Instance.GetListItem();
            else members = DataProvider<Member>.Instance.GetListItem("Unit", UnitVar);
            return members;
        }

        public Member GetUserByID(string ID)
        {
            Member member = DataProvider<Member>.Instance.GetItem("Key", ID);
            return member;
        }

        public Member GetUserByLabID(string LabID)
        {
            Member member = DataProvider<Member>.Instance.GetItem("LabID", LabID);
            return member;
        }

        public void AddMember(Member member)
        {
            DataTable data = DataProvider<Member>.Instance.LoadData();
            DataRow newMember = data.NewRow();

            var allAttr = typeof(Member).GetProperties(); // Lấy danh sách attributes của class Member

            foreach (var attr in allAttr)
                newMember[attr.Name] = attr.GetValue(member);


            data.Rows.Add(newMember);

            DataProvider<Member>.Instance.UpdateData(data);
        }

        public void EditMember(Member member)
        {
            DataTable data = DataProvider<Member>.Instance.LoadData();
            DataRow newMember = data.Select("Key=" + member.Key).FirstOrDefault();

            if (newMember != null)
            {
                var allAttr = typeof(Member).GetProperties(); // Lấy danh sách attributes của class Member
                foreach (var attr in allAttr)
                    newMember[attr.Name] = attr.GetValue(member);
            }
           
            DataProvider<Member>.Instance.UpdateData(data);
        }

        public void DeleteMember(String Key)
        {
            DataProvider<Member>.Instance.DeleteItem("Key", Key);
        }

        public void DeleteMemberFromUnit(String ID, String Unit)
        {
            DataTable data = DataProvider<Member>.Instance.LoadData();
            DataRow newMember = data.Select("Key=" + ID).FirstOrDefault();
            try
            {
                var unit = newMember["Unit"].ToString();
                var units = unit.Split(",");
                var newUnits = new List<string>();
                foreach (var item in units)
                {
                    bool add = true;
                    List<string> unitDeleteVar = new List<string>();
                    switch (Unit)
                    {
                        case "PT Lập trình":
                            unitDeleteVar.AddRange(new List<string>() { "lập trình", "lap trinh", "lậptrình", "laptrinh" });
                            break;
                        case "PT PTBT":
                            unitDeleteVar.AddRange(new List<string>() { "ptbt", "phat trien ban than", "phát triển bản thân"});
                            break;
                        case "PT Quản trị doanh nghiệp & Marketing":
                            unitDeleteVar.AddRange(new List<string>() { "marketing", "quản trị doanh nghiệp", "quan tri doanh nghiep" });
                            break;
                        case "PT Cơ khí - Cơ điện tử":
                            unitDeleteVar.AddRange(new List<string>() { "co khi", "cokhi", "cơ khí", "cơkhí" });
                            break;
                        case "PT Ngoại ngữ":
                            unitDeleteVar.AddRange(new List<string>() { "noại ngữ", "ngoạingữ", "ngoai ngu", "ngoaingu" });
                            break;
                        case "PT Tự động hóa & IOM":
                            unitDeleteVar.AddRange(new List<string>() { "tư động hóa", "tu dong hoa"});
                            break;
                        case "Ban Điều Hành":
                            unitDeleteVar.AddRange(new List<string>() { "ban điều hành", "ban diều hành"});
                            break;
                        case "Ban Cố Vấn":
                            unitDeleteVar.AddRange(new List<string>() { "ban cố vấn", "bancovan"});
                            break;
                        case "Ban Nhân Sự":
                            unitDeleteVar.AddRange(new List<string>() { "ban nhân sự", "bannhansu"});
                            break;
                        case "Ban Truyền Thông":
                            unitDeleteVar.AddRange(new List<string>() { "bantruyenthong", "ban truyền thông"});
                            break;
                        case "Ban Sự Kiện":
                            unitDeleteVar.AddRange(new List<string>() { "ban sự kiện", "bansukien"});
                            break;
                        case "Ban Giám Sát":
                            unitDeleteVar.AddRange(new List<string>() { "ban giám sát", "ban giam sat"});
                            break;
                        case "Ban Đời Sống":
                            unitDeleteVar.AddRange(new List<string>() { "ban đời sống", "bandoisong"});
                            break;
                        case "Ban Đào Tạo":
                            unitDeleteVar.AddRange(new List<string>() { "ban đào tạo", "bandaotao"});
                            break;
                    }
                    foreach (var varr in unitDeleteVar)
                    {
                        if (CultureInfo.CurrentCulture.CompareInfo.IndexOf(item, varr, CompareOptions.IgnoreCase) >= 0)
                        {
                            add = false;
                            break;
                        }
                    }
                    if (add) newUnits.Add(item);
                    
                }

                if (newUnits.Count > 1)
                {
                    newMember["Unit"] = string.Join(",", newUnits);
                }
                else if (newUnits.Count == 1)
                {
                    newMember["Unit"] = newUnits[0];
                }
                else
                {
                    newMember["Unit"] = "Không";
                }
            }
            catch
            {
                newMember["Unit"] = "Không";
            }
            DataProvider<Member>.Instance.UpdateData(data);
        }
    }
}
