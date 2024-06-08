let form = document.forms["Form1"];
let TimeStartInput = form.elements["StartTime"];
let TimeEndInput = form.elements["EndTime"];
let WasSearchedInput = form.elements["WasSearched"];
let SearchButton = document.getElementById("SearchButton");

function SearchButtonClick() {
    WasSearchedInput.value = 1;
    window.open("https://ya.ru", "_black");
}
function SaveStartTime() {
    TimeStartInput.value = CreateDateTimeString(new Date(Date.now()));
}
function SaveEndTime() {
    TimeEndInput.value = CreateDateTimeString(new Date(Date.now()));
}
function CreateDateTimeString(date) {
    return `${date.getFullYear()}-${date.getMonth()+1}-${date.getDate()} ${date.getHours()}:${date.getMinutes()}:${date.getSeconds()},${date.getMilliseconds()}`;
}

SaveStartTime();

SearchButton.addEventListener("click", SearchButtonClick);
form.addEventListener("submit", SaveEndTime);