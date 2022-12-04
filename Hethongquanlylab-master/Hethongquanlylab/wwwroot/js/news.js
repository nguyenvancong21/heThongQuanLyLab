$(document).ready(function () {
    var dataTinTucJS = new dataJS();
    dataTinTucJS.loadData()
})

class dataJS {
    constructor() {
    }

    loadData() {
        $.each(dataNews, function (index, item) {
            var trHTML = $(`<tr style="background-color: #dfdede">
                                <td> <img src="./img/logo-trắng.jpg" alt="" style="width: 90px;"></td>
                                <td style="font-size: 24px; font-weight:500">${item.trươngDuLieu1}</td>
                            </tr>`);

            $('.news tbody').append(trHTML);
        })
    }
}

var dataNews = [
    {
        trươngDuLieu1: "Thông tin chi tiết về sự kiện 31/8",
        trươngDuLieu2: "Thôhhônng báo 1hônng báo 1hônng g báo 1hhôhông báo 1hônng báo 1hônng báo 1hônng báo 1hônng báo 1hhôhông báo 1hônng báo 1hônng báo 1hônng báo 1hônng nng báo 1hông báo 1 1 ",
    },
    {
        trươngDuLieu1: "Lễ công bố thành lập Công ty Cổ phần Đầu tư và phát triển Lab Thầy Sinh",
        trươngDuLieu2: "Thông báo 2",
    },
    {
        trươngDuLieu1: "Thông báo về Cuộc thi Nghiên cứu khoa học Lab Thầy Sinh",
        trươngDuLieu2: "Thông báo 3",
    },
    {
        trươngDuLieu1: "Thông báo tuyển Gen 5.2 Lab Thầy Sinh",
        trươngDuLieu2: "Thông báo 4",
    },
]