using System;
using System.Collections.Generic;
using System.Linq;
using Hethongquanlylab.Models;
using Hethongquanlylab.DAO;
using OfficeOpenXml;
using System.IO;
using System.Data;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Hethongquanlylab.Models.Login;

namespace Hethongquanlylab.Common
{
    public class Function
    {
        private static Function instance;
        public static Function Instance
        {
            get { if (instance == null) instance = new Function(); return Function.instance; }
            private set { Function.instance = value; }
        }
        
        public List<String> getLinks()
        {
            List<string> links = new List<string>() {LinkDAO.Instance.GetLink("Biểu mẫu"),
                                                LinkDAO.Instance.GetLink("Lịch làm việc"),
                                                LinkDAO.Instance.GetLink("Báo lỗi")};
            return links;
        }

        public List<T> sortItems<T>(List<T> items, String sortOrder)
        {
            var attrs = typeof(T).GetProperties();
            var result = items.OrderBy(s => attrs.First().GetValue(s, null));
            foreach (var attr in attrs)
            {
                if (sortOrder == attr.Name.ToString())
                {
                    if (attr.Name.ToString() == "Name") result = items.OrderBy(s => attr.GetValue(s, null).ToString().Split(" ").Last());
                    else if (attr.Name.ToString() == "Birthday") result = items.OrderBy(s => attr.GetValue(s, null).ToString().Split("/").Last());
                    else if (attr.Name.ToString() == "ID")
                    {
                        try
                        {
                            result = items.OrderBy(s => Convert.ToInt32(attr.GetValue(s, null)));
                        }
                        catch
                        {
                            result = items.OrderBy(s => attr.GetValue(s, null));
                        }
                    }
                    else
                        result = items.OrderBy(s => attr.GetValue(s, null));
                }
                else if (sortOrder == attr.Name.ToString() + "_desc")
                {
                    if (attr.Name.ToString() == "Name") result = items.OrderByDescending(s => attr.GetValue(s, null).ToString().Split(" ").Last());
                    else if (attr.Name.ToString() == "Birthday") result = items.OrderByDescending(s => attr.GetValue(s, null).ToString().Split("/").Last());
                    else if (attr.Name.ToString() == "ID")
                    {
                        try
                        {
                            result = items.OrderByDescending(s => Convert.ToInt32(attr.GetValue(s, null)));
                        }
                        catch
                        {
                            result = items.OrderByDescending(s => attr.GetValue(s, null));
                        }
                    }
                    else
                        result = items.OrderByDescending(s => attr.GetValue(s, null));
                }
            }

            return result.ToList();
        }

        public List<T> searchItems<T>(List<T> items, ItemDisplay<T> itemDisplay)
        {
            if (!String.IsNullOrEmpty(itemDisplay.CurrentSearchField))
            {
                if (!String.IsNullOrEmpty(itemDisplay.CurrentSearchString))
                {
                    var attrs = typeof(T).GetProperties();
                    foreach (var attr in attrs)
                    {
                        if (itemDisplay.CurrentSearchField == attr.Name.ToString())
                        {
                            items = items.Where(s => attr.GetValue(s, null).ToString().Contains(itemDisplay.CurrentSearchString)).ToList();
                        }
                    }
                }
            }
            return items;
        }


        public MemoryStream ExportToExcel<T>(List<T> items)
        {
            var memoryStream = new MemoryStream();
            using (var excelPackage = new ExcelPackage(memoryStream))
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add("Danh sách thành viên");
                var currentRow = 1;
                // trỏ đến dòng 1 và cột 1 thay giá trị bằng LabID các dòng dưới cx tương tự

                var allAttr = typeof(T).GetProperties(); // Lấy danh sách attributes của class Member
                int col = 1;


                foreach (var attr in allAttr)
                    if (attr.Name != "Avt")
                        worksheet.Cells[currentRow, col++].Value = attr.Name;

                // Lấy tất cả dữ liệu trong database theo thứ tự tăng dần labID
                
                foreach (var item in items)
                {
                    // Dòng thứ 2 trở đi sẽ đổ dữ liệu từ database vào
                    currentRow += 1;
                    col = 1;
                    foreach (var attr in allAttr)
                    {
                        if (attr.Name != "Avt")
                        {
                            object value = attr.GetValue(item);
                            worksheet.Cells[currentRow, col++].Value = value == null?"N/A": value.ToString();
                        }
                    }

                }
                // Trả về dữ liệu dạng xlsx
                using (var stream = new MemoryStream())
                {
                    excelPackage.SaveAs(stream);
                    return stream;
                }
            }
        }

        public string SendEmail(string subject, string message)
        {
            try
            {
                string receiver = "ngoxuanhinhad4@gmail.com";
                var senderEmail = new MailAddress("ngoxuanhinham123@gmail.com", "Hinh");
                var receiverEmail = new MailAddress(receiver, "Receiver");
                var password = "xivkcaxctodstpag";
                var sub = subject;
                var body = message;
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }
                return "Gửi mail thàhh công";
            }
            catch
            {
                return "Gửi mail thất bại";
            }

        }

        public ItemDisplay<Notification> getNotifications(string page)
        {
            page = page == null ? "1" : page;
            int currentPage = Convert.ToInt32(page);
            ItemDisplay<Notification> notificationList = new ItemDisplay<Notification>();
            notificationList.CurrentPage = currentPage;

            List<Notification> notifications = NotificationDAO.Instance.GetNotificationList_Excel();
            notifications.Reverse();

            notificationList.Paging(notifications, 5);
            

            if (notificationList.PageCount > 0)
            {
                if (notificationList.CurrentPage > notificationList.PageCount) notificationList.CurrentPage = notificationList.PageCount;
                if (notificationList.CurrentPage < 1) notificationList.CurrentPage = 1;
                if (notificationList.CurrentPage != notificationList.PageCount)
                    try
                    {
                        notificationList.Items = notificationList.Items.GetRange((notificationList.CurrentPage - 1) * notificationList.PageSize, notificationList.PageSize);
                    }
                    catch { }

                else
                    notificationList.Items = notificationList.Items.GetRange((notificationList.CurrentPage - 1) * notificationList.PageSize, notificationList.Items.Count % notificationList.PageSize == 0 ? notificationList.PageSize : notificationList.Items.Count % notificationList.PageSize);
            }
            

            return notificationList;
        }

    }
}
