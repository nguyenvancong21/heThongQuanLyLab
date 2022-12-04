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
    public class ProcedureDAO
    {
        private static ProcedureDAO instance;
        public static ProcedureDAO Instance
        {
            get { if (instance == null) instance = new ProcedureDAO(); return ProcedureDAO.instance; }
            private set { ProcedureDAO.instance = value; }
        }

        private ProcedureDAO() { }


        public List<Procedure> GetProcedureList(string col, string var, string tb = "Process") // Lấy danh sách quy trình
        {
            List<Procedure> items = new List<Procedure>();
            if (col == "ApprovalWaiting")
            {   
                if (var == "Ban Điều Hành")
                {
                    items.AddRange(DataProvider<Procedure>.Instance.GetListItem(col, "Ban Điều Hành", tb));
                    items.AddRange(DataProvider<Procedure>.Instance.GetListItem(col, "Ban Cố Vấn", tb));
                    items.AddRange(DataProvider<Procedure>.Instance.GetListItem(col, "Nhà Sáng Lập", tb));
                }
                else if (var == "Ban Cố Vấn")
                {
                    items.AddRange(DataProvider<Procedure>.Instance.GetListItem(col, "Ban Cố Vấn", tb));
                    items.AddRange(DataProvider<Procedure>.Instance.GetListItem(col, "Nhà Sáng Lập", tb));
                }
                else if ((var == "Nhà Sáng Lập") || (var == "Nhà Đồng Sáng Lập"))
                {
                    items.AddRange(DataProvider<Procedure>.Instance.GetListItem(col, "Nhà Sáng Lập", tb));
                }
            }
            else
            {
                items = DataProvider<Procedure>.Instance.GetListItem(col, var, tb);
            }
            
            items.Reverse();
            return items;
        }

        public Procedure GetProcedureModel(string ID, string tb = "Process") // Lấy thông tin quy trình có ID = procedureID
        {
            Procedure item = DataProvider<Procedure>.Instance.GetItem("ID", ID, tb);
            return item;
        }

        public void AddProcedure(Procedure procedure, string tableName = "Process") // Thêm mới quy trình vào sheetName
        {
            DataTable data = DataProvider<Procedure>.Instance.LoadData(tableName);
            DataRow newProcedure = data.NewRow();

            var allAttr = typeof(Procedure).GetProperties(); // Lấy danh sách attributes của class Procedure

            foreach (var attr in allAttr)
                newProcedure[attr.Name] = attr.GetValue(procedure);

            data.Rows.Add(newProcedure);
            DataProvider<Procedure>.Instance.UpdateData(data, tableName);
        }

        private void deleteProcedure(string col, string Key, string tableName = "Process")  // Xóa ở Sheet [sheetName]
        {
            DataProvider<Procedure>.Instance.DeleteItem(col, Key, tableName);
        }

        public void DeleteProcedure(string ID)
        {
            deleteProcedure("ID", ID);
            deleteProcedure("SubID", ID, "ProcedureApproval");

        } // Xóa ở tất cả các nhánh


        public void EditProcedure(Procedure procedure, string tableName = "Process")
        {
            DataTable data = DataProvider<Procedure>.Instance.LoadData(tableName);
            DataRow newProcedure = data.Select("ID=" + procedure.ID).FirstOrDefault();

            if (newProcedure != null)
            {
                var allAttr = typeof(Procedure).GetProperties(); // Lấy danh sách attributes của class Procedure
                foreach (var attr in allAttr)
                    newProcedure[attr.Name] = attr.GetValue(procedure);
            }
            DataProvider<Procedure>.Instance.UpdateData(data, tableName);
        }

        public void SendToApproval(Procedure procedure, string unit)
        {
            String SubID = (Convert.ToInt32(procedure.ID)).ToString() ; // Do edit = delete + add (Primary key ID sẽ tăng 1)
            deleteProcedure("SubID", procedure.ID, "ProcedureApproval");
            procedure.SubID = SubID;
            if (unit == "Ban Điều Hành")
            {
                procedure.ApprovalWaiting = "Ban Cố Vấn";
                AddProcedure(procedure, "ProcedureApproval"); 
            }
            else if (unit == "Ban Cố Vấn")
            {
                procedure.ApprovalWaiting = "Nhà Sáng Lập";
                AddProcedure(procedure, "ProcedureApproval");
            }
            else
            {
                procedure.ApprovalWaiting = "Ban Điều Hành";
                AddProcedure(procedure, "ProcedureApproval");
            }
        }      

        public void ReturnProcedure(string unit, Procedure procedure, string feedback) // Trả lại quy trình của đơn vị unit
        {
            procedure.Status = unit + " trả lại";
            procedure.V1 = false;
            procedure.V2 = false;
            procedure.V3 = false;
            if (unit == "Ban Điều Hành")
            {
                procedure.BdhReply = feedback;
            }
            else if (unit == "Ban Cố Vấn")
            {
                procedure.BcvReply = feedback;
            }
            else if (unit == "Nhà Sáng Lập")
            {
                procedure.NSLReply = feedback;
            }
            else if (unit == "Nhà Đồng Sáng Lập")
            {
                procedure.NDSLReply = feedback;
            }
            EditProcedure(procedure); // Đã có xóa ở sheet này
        }

        public void ApprovalProcedure(string unit, string ID, string Feedback)
        {
            Procedure procedureApproval = ProcedureDAO.Instance.GetProcedureModel(ID, "ProcedureApproval");
            Procedure procedure = ProcedureDAO.Instance.GetProcedureModel(procedureApproval.SubID);
            if (unit == "Ban Điều Hành")
            {
                procedure.V1 = true;
                procedure.BdhReply = Feedback;
                procedure.ApprovalWaiting = "Ban Cố Vấn";
            }
            else if (unit == "Ban Cố Vấn")
            {
                procedure.V2 = true;
                procedure.BcvReply = Feedback;
                procedure.ApprovalWaiting = "Nhà Sáng Lập";
            }
            else if (unit == "Nhà Sáng Lập")
            {
                procedure.V3 = true;
                procedure.NSLReply = Feedback;
            }
            else if (unit == "Nhà Đồng Sáng Lập")
            {
                procedure.V3 = true;
                procedure.NDSLReply = Feedback;
            }
            procedure.Status = unit + " đã duyệt";
            ProcedureDAO.Instance.EditProcedure(procedure);
            String OriginalID = procedure.ID;
            procedure.ID = ID; // Chuyển sang bảng duyệt 
            procedure.SubID = OriginalID;
            ProcedureDAO.Instance.EditProcedure(procedure, "ProcedureApproval");
        }

        public void ReturnProcedure(string unit, string ID, string Feedback)
        {
            Procedure procedureApproval = ProcedureDAO.Instance.GetProcedureModel(ID, "ProcedureApproval");
            Procedure procedure = ProcedureDAO.Instance.GetProcedureModel(procedureApproval.SubID);
            procedure.V1 = false;
            procedure.V2 = false;
            procedure.V3 = false;
            if (unit == "Ban Điều Hành")
            {
                procedure.BdhReply = Feedback;
            }
            else if (unit == "Ban Cố Vấn")
            {
                procedure.BcvReply = Feedback;
            }
            else if (unit == "Nhà Sáng Lập")
            {
                procedure.NSLReply = Feedback;
            }
            else if (unit == "Nhà Đồng Sáng Lập")
            {
                procedure.NDSLReply = Feedback;
            }
            procedure.Status = unit + " trả lại";
            ProcedureDAO.Instance.EditProcedure(procedure);
            deleteProcedure("ID", ID, "ProcedureApproval");
        }
    }
}
