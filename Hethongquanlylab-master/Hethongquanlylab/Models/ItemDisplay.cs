using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Hethongquanlylab.Models
{
    public class ItemDisplay<T>
    {
        private int pageCount;
        private int pageSize;
        private int currentPage;
        private List<T> items;
        private int itemCount;

        private String field;
        private String sortOrder;
        private String currentSearchString;
        private String currentSearchField;
        private List<String> searchFieldList;

        private string message;
        private bool isMessage;
        private List<String> link;
        private string sessionVar;

        private Dictionary<string, string> nameVar = new Dictionary<string, string>()
        {
            {"LabID", "LabID" },
            {"Name", "Tên" },
            {"Sex","Giới tính"},
            {"Birthday","Ngày sinh"},
            {"Gen","Thế hệ"},
            {"Unit","Đơn vị"},
            {"Position","Chức vụ"},
            {"Senddate","Ngày gửi"},
            {"Status","Tình trạng"},
            {"Content","Nội dung"},
            {"Type", "Loại" },
            {"Startday", "Ngày bắt đầu" },
            {"Endday", "Ngày kết thúc" }
        };

        private Dictionary<string, string> unitVar = new Dictionary<string, string>()
        {
            {"All", "Toàn bộ thành viên" },
            {"LT", "Thành viên Leader Team" },
            {"PT", "Thành viên Power Team" },
            {"BNS","Thành viên Ban Nhân Sự"},
            {"BDT","Thành viên Ban Đào Tạo"},
            {"BDH","Thành viên Ban Điều Hành"},
            {"BDS","Thành viên Ban Đời Sống"},
            {"BGS","Thành viên Ban Giám Sát"},
            {"BSK","Thành viên Ban Sự Kiện"},
            {"BTT","Thành viên Ban Truyền Thông"},
            {"BCV","Thành viên Ban Cố Vấn"},
        };

        private Dictionary<string, string> procedureVar = new Dictionary<string, string>()
        {
            {"All", "Toàn bộ quy trình" },
            {"BDH","Quy trình Ban Điều Hành"},
            {"BCV","Quy trình Ban Cố Vấn"},
            {"NSL","Nhà Sáng Lập"},
            {"NDSL","Nhà Đồng Sáng Lập"},
        };

        private Dictionary<string, string> procedureColorVar = new Dictionary<string, string>()
        {
            {"Chưa duyệt", "#4800ff"},
            {"Chờ duyệt", "#ff6a00"},
            {"Ban Điều Hành đã duyệt", "#0a0"},
            {"Ban Cố Vấn đã duyệt", "#0c0"},
            {"Nhà Sáng Lập đã duyệt", "#0f0"},
            {"Ban Điều Hành trả lại", "#a00"},
            {"Ban Cố Vấn trả lại", "#c00"},
            {"Nhà Sáng Lập trả lại", "#f00"},
            {"Đã duyệt", "#0f0"}
        };



        public int PageCount { get => pageCount; set => pageCount = value; }
        public int PageSize { get => pageSize; set => pageSize = value; }
        public int CurrentPage { get => currentPage; set => currentPage = value; }
        public List<T> Items { get => items; set => items = value; }
        public int ItemCount { get => itemCount; set => itemCount = value; }

        public String Field { get => field; set => field = value; }
        public String SortOrder { get => sortOrder; set => sortOrder = value; }
        public String CurrentSearchString { get => currentSearchString; set => currentSearchString = value; }
        public String CurrentSearchField { get => currentSearchField; set => currentSearchField = value; }
        public List<String> SearchFieldList { get => searchFieldList; set => searchFieldList = value; }

        public string Message { get => message; set => message = value; }
        public bool IsMessage { get => isMessage; set => isMessage = value; }
        public string SessionVar { get => sessionVar; set => sessionVar = value; }
        public List<String> Link { get => link; set => link = value; } // Biểu mẫu/Lịch làm việc/Báo lỗi

        public Dictionary<string, string> NameVar { get => nameVar; set => nameVar = value; }
        public Dictionary<string, string> UnitVar { get => unitVar; set => unitVar = value; }
        public Dictionary<string, string> ProcedureVar { get => procedureVar; set => procedureVar = value; }
        public Dictionary<string, string> ColorVar { get => procedureColorVar; set => procedureColorVar = value; }

        public static Boolean IsAddMember = false;

        public ItemDisplay()
        {
            this.pageSize = 10;
            this.currentPage = 1;
            this.searchFieldList = new List<String>();
            foreach (var attr in typeof(T).GetProperties().ToList())
            {
                this.searchFieldList.Add(attr.Name.ToString());
            }

            this.isMessage = false;
            this.message = "";
            this.sessionVar = "";
        }

        public ItemDisplay(string sessionVar)
        {
            this.sessionVar = sessionVar; // Session

            this.pageSize = 10;
            this.currentPage = 1;
            this.searchFieldList = new List<String>();

            foreach (var attr in typeof(T).GetProperties().ToList())
            {
                this.searchFieldList.Add(attr.Name.ToString());
            }

            this.isMessage = false;
            this.message = "";
        }

        public void Paging(List<T> members, int pageSize)
        {
            this.items = members;
            this.itemCount = members.Count;
            this.pageSize = pageSize;
            
            if ((double)((decimal)this.items.Count() % Convert.ToDecimal(this.pageSize)) == 0)
            {
                this.pageCount = this.items.Count() / this.pageSize;
            }
            else
            {
                double page_Count = (double)((decimal)this.items.Count() / Convert.ToDecimal(this.pageSize));
                this.pageCount = (int)Math.Ceiling(page_Count);
            }
        }
    }
}
