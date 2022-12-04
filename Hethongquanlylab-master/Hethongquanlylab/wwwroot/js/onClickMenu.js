const menu = document.querySelector(".navbar-list")
console.log(menu);
function showMenu(){
    menu.classList.add('show');
}

function outMenu(){
    menu.classList.remove('show');
}