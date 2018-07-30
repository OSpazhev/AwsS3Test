$(document).ready(
    function () {
        var currentTime = new Date();
        currentTime.setMinutes(currentTime.getMinutes() - currentTime.getTimezoneOffset() + 60);

        //document.getElementById("expiration-datetime").value = currentTime.toJSON().slice(0, 19) /*+ "00"*/;

        //onChangeMethodSelect(document.getElementById("HttpRequestMethod"));
    }
)

function sendFileToTheBucket() {
    var url = $("#GeneratePreSignedUrl").data("request-url");

    $.ajax({
        url: url,
        data: {
            'ObjectKey': $('#object-key').text().trim(),
            'BucketRegion': $('#bucket-region').text().trim(),
            'BucketName': $('#bucket-name').text().trim()
        },
        type: "POST",
        beforeSend: function () {
        },
        error: function (xhr) {
            alert('Wrong data for generate object key');
        },
        success: function (preSignedUrl) {
            sendFile(preSignedUrl);
        }
    })
}

function sendFile(preSignedUrl) {
    var files = $('#File').get()[0].files;
    if (!files.length) {
        return alert('Please choose a file to upload first.');
    }

    var request = $.ajax({
        url: preSignedUrl,
        data: files[0],
        type: 'PUT',
        contentType: 'binary/octet-stream',
        processData: false,
        beforeSend: function () {
        },
        error: function (xhr) {
            return alert('Uploading failed');
        },
        success: function (data) {
            return alert('Uploading success');
        }
    })
}
