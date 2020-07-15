function timedCount() {
    var i = 0;

    function timedCount() {
        i = i + 1;
        postMessage(i);
        setTimeout("timedCount()", 300);
    }

    timedCount();
}
function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}
timedCount();