// Боже прости, что я пишу этот код \\

// Берём из формы время, которое нужно отдать на решение вопроса
let sec = Number(document.getElementById("time-to-answer-input").value);
let timeoutsec = Number(document.getElementById("timeout-to-answer-input").value);
// Форма и инпут со временем
const ans_form = document.getElementById('form');
const timer_input = document.getElementById("time");

LocalStorageCheck();

SetTime(sec);

let intervalTime;        // переменная интервала обычного времени
let intervalTimeOut;     // переменная интервала таймаута
let intervalChangeColor; // переменная интервала зименения цвета
let color = "red";

//Добавляет обработчик события отправки формы
document.addEventListener('submit', function () {
    localStorage.clear();
});

intervalTime = setInterval(Time, 1000);
// Функция, когда обычное время
function Time() {
    if (sec <= 0) {
        console.log("Время кончилось");
        clearInterval(intervalTime);
        TimeOutInit();
        return;
    }
    sec--;
    SetTime(sec);
    localStorage.setItem("sec", sec);
}

// Функция, когда уже таймаут
function TimeOutInit() {
    SetTime(timeoutsec);
    intervalTimeOut = setInterval(TimeOut, 1000);
    intervalChangeColor = setInterval(ChangeColor, 500);
}

function TimeOut() {
    if (timeoutsec <= 0) {
        End();
        return;
    }
    timeoutsec--;
    SetTime(timeoutsec);
    localStorage.setItem("timeoutsec", timeoutsec);
}

function End() {
    clearInterval(intervalTimeOut);
    clearInterval(intervalChangeColor);
    localStorage.clear();

    const textinput = document.getElementById("answerinput");
    if (textinput.value === "") {
        textinput.value = "-";
    }

    document.getElementById("sub-button").click();
}

function ChangeColor() {
    let timer_panel = document.getElementById("time-panel");

    if (color === "red") {
        timer_panel.style.color = "black";
        color = "black";
    }
    else {
        timer_panel.style.color = "red";
        color = "red";
    }
}

function SetTime(seconds) {
    timer_input.textContent = String(seconds);
}

function LocalStorageCheck() {
    // если ничего нет в хранилище, говорим, что квиз начат
    if (localStorage.getItem("quiz") === null) {
        localStorage.setItem("quiz", true);
        localStorage.setItem("sec", sec);
        localStorage.setItem("timeoutsec", timeoutsec);
        return;
    }
    // иначе просто берём готовые данные
    sec = Number(localStorage.getItem("sec"))
    timeoutsec = Number(localStorage.getItem("timeoutsec"))
}
