using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hethongquanlylab.Models
{
    public class ItemDetail<T>
    {
        private T item;
        private string sessionVar;
        private string fieldVar;

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

        public T Item { get => item; set => item = value; }
        public string SessionVar { get => sessionVar; set => sessionVar = value; }
        public string FieldVar { get => fieldVar; set => fieldVar = value; }
        public List<string> Link { get; set; }
        public Dictionary<string, string> ColorVar { get => procedureColorVar; set => procedureColorVar = value; }

        public ItemDetail(T item, string sessionVar)
        {
            this.item = item;
            this.sessionVar = sessionVar;
            this.fieldVar = "";
        }
    }
}
