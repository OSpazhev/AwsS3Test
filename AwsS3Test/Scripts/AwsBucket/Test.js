$(document).ready(
    function () {
        var currentTime = new Date();
        currentTime.setMinutes(currentTime.getMinutes() - currentTime.getTimezoneOffset() + 60);
        document.getElementById("expiration-datetime").value = currentTime.toJSON().slice(0, 19) /*+ "00"*/;

        onChangeMethodSelect(document.getElementById("HttpRequestMethod"));
    }
)

function onChangeHttpRequestMethod(item) {
    changeFileUploadView(item);
    changeSubmitButtonText(item);
}

function changeFileUploadView(item) {
    if (item.options[item.selectedIndex].text === "PUT") {
        $(".file-upload").show();
    }
    else {
        $(".file-upload").hide();
    }
}

function changeSubmitButtonText(item) {
    $(".btn-default").val(item.options[item.selectedIndex].text)
}