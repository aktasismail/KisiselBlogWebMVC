const openmenuel = document.querySelector(".open-menu");
const addactiveel = document.querySelector(".ia_nav_link")
if(openmenuel && addactiveel) {
    openmenuel.addEventListener("click", () => 
    {
        addactiveel.classList.toggle("active");
        if(addactiveel.classList.contains("active")) {
            openmenuel.src = "/img/close.svg"
        }
        else {
            openmenuel.src = "/img/open.svg"
        }
    });
}