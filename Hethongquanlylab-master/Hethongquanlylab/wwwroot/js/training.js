using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hethongquanlylab.wwwroot.js
{
    public class training
    {
        var idCourse = 1;
    function changeCourse1() {
        idCourse = 1
        document.getElementById("col-1").innerHTML = '<iframe width="100%" height="100%" src="https://www.youtube.com/embed/A-S_HdeAFiU" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>';
    }
    function changeCourse2() {
        idCourse = 2
        document.getElementById("col-1").innerHTML = '<iframe width="100%" height="100%" src="https://www.youtube.com/embed/D_a2hxqhYO8" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>';
    }
    function changeCourse3() {
        idCourse = 3
        document.getElementById("col-1").innerHTML = '<iframe width="100%" height="100%" src="https://www.youtube.com/embed/oUx8u5UNY1k" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>';
    }
    function changeCourse4() {
        idCourse = 4
        document.getElementById("col-1").innerHTML = '<iframe width="100%" height="100%" src="https://www.youtube.com/embed/mCJfE6WwWYs" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>';
    }
    function changeCourse5() {
        idCourse = 5
        document.getElementById("col-1").innerHTML = '<iframe width="100%" height="100%" src="https://www.youtube.com/embed/D_a2hxqhYO8" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>';
    }

    function nextCourse() {
        idCourse++;
        if (idCourse == 1) changeCourse1()
        if (idCourse == 2) changeCourse2()
        if (idCourse == 3) changeCourse3()
        if (idCourse == 4) changeCourse4()
        if (idCourse == 5) changeCourse5()
    }

    function prevCourse() {
        idCourse--;
        if (idCourse == 1) changeCourse1()
        if (idCourse == 2) changeCourse2()
        if (idCourse == 3) changeCourse3()
        if (idCourse == 4) changeCourse4()
        if (idCourse == 5) changeCourse5()
    }
    }
}
