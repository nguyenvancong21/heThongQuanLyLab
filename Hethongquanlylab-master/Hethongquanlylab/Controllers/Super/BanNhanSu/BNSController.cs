using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Hethongquanlylab.Models;
using Hethongquanlylab.DAO;
using Hethongquanlylab.Common;
using OfficeOpenXml;
using System.IO;
using System.Data;
using OfficeOpenXml.Table;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Hethongquanlylab.Models.Login;

namespace Hethongquanlylab.Controllers.Super.BanNhanSu
{
    public class BNSController: SuperController
    {
        public BNSController()
        {
            unit = "Ban Nhân Sự";
            unitVar = "BNS";
        }

        // Upload avt: trong Thêm thành viên

        public IActionResult AddMember_New()
        {
            var urlQuery = Request.HttpContext.Request.Query;
            String avt = urlQuery["avt"];
            String mess = urlQuery["mess"];
            avt = avt == null ? "default.jpg" : avt; // Đặt avt mặc định nếu không up avt lên
            mess = mess == null ? "0" : mess;
            return View(String.Format("./Views/{0}/Members/AddMember_New.cshtml", unitVar), new List<string>() {unit, avt, mess });
        }


        [HttpPost]
        public override IActionResult UploadAvt(string var, string key, IFormFile file, [FromServices] IWebHostEnvironment hostingEnvironment)
        {
            string fileName = $"{hostingEnvironment.WebRootPath}/img/avt/{file.FileName}";
            // Dẩy file vào thư mục
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            TempData["avt"] = file.FileName; // Lưu tên vào TempData => Lưu vào Excel
            if (var == "edit")
                return RedirectToAction("EditMember", new { avt = file.FileName, Key = key });
            else
            {
                return RedirectToAction("AddMember_New", new { avt = file.FileName });
            }
        }

        [HttpPost]
        public IActionResult AddMember_New(String sortOrder, String searchString, String searchField, string IsAdd, string MembersVar, String Key, String LabID, String Name, String Sex, String Birthday, String Gen, String Phone, String Email, String Address, String Specicalization, String University, String Unit, String Position, bool IsLT, bool IsPassPTBT)
        {
            String avt = TempData["avt"] == null ? "default.jpg" : TempData["avt"].ToString();
            var unit = Unit == null ? "Không" : Unit;
            var position = Position == null ? "Không" : Position;
            var phone = Phone == null ? "N/A" : Phone;
            var email = Email == null ? "N/A" : Email;
            var address = Address == null ? "N/A" : Address;
            var specializaion = Specicalization == null ? "N/A" : Specicalization;
            var university = University == null ? "N/A" : University;

            List<Member> members = UserDAO.Instance.GetListUser();

            foreach (var member in members)
            {
                if (member.LabID == LabID)
                {
                    return RedirectToAction("AddMember", new {mess = "1"});
                }
            }

            var newMember = new Member(LabID, avt, Name, Sex, Birthday, Gen, phone, email, address, specializaion, university, unit, position, IsLT, IsPassPTBT, "Không");
            UserDAO.Instance.AddMember(newMember);

            ItemDisplay<Member> memberList = new ItemDisplay<Member>();


            // Lấy danh sách items trong trang hiện tại
            memberList.Paging(members, 10);
            return RedirectToAction("Member", new { page = memberList.PageCount });
        }
        // End: thêm thành viên
    }
}
