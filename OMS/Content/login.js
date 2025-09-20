
var test = null;
var check = "";
var state = document.getElementById('content-capture');

var myVal = ""; // Drop down selected value of reader
var disabled = true;
var startEnroll = false;

var currentFormat = Fingerprint.SampleFormat.PngImage;
var deviceTechn = {
    0: "Unknown",
    1: "Optical",
    2: "Capacitive",
    3: "Thermal",
    4: "Pressure"
}

var deviceModality = {
    0: "Unknown",
    1: "Swipe",
    2: "Area",
    3: "AreaMultifinger"
}

var deviceUidType = {
    0: "Persistent",
    1: "Volatile"
}

var FingerprintSdkTest = (function () {
    function FingerprintSdkTest() {
        var _instance = this;
        debugger;
        this.operationToRestart = null;
        this.acquisitionStarted = false;
        this.sdk = new Fingerprint.WebApi;
        this.sdk.onDeviceConnected = function (e) {
            // Detects if the deveice is connected for which acquisition started
            showMessage("Scan your finger");
        };
        this.sdk.onDeviceDisconnected = function (e) {
            // Detects if device gets disconnected - provides deviceUid of disconnected device
            showMessage("Device disconnected");
        };
        this.sdk.onCommunicationFailed = function (e) {
            // Detects if there is a failure in communicating with U.R.U web SDK
            showMessage("Communinication Failed")
        };
        this.sdk.onSamplesAcquired = function (s) {
            // Sample acquired event triggers this function
            debugger;
            sampleAcquired(s);
        };
        this.sdk.onQualityReported = function (e) {
            // Quality of sample aquired - Function triggered on every sample acquired
            document.getElementById("qualityInputBox").value = Fingerprint.QualityCode[(e.quality)];
        }

    }

    FingerprintSdkTest.prototype.startCapture = function () {
        if (this.acquisitionStarted) // Monitoring if already started capturing
            return;
        var _instance = this;
        showMessage("");
        this.operationToRestart = this.startCapture;
        this.sdk.startAcquisition(currentFormat, myVal).then(function () {
            _instance.acquisitionStarted = true;

            //Disabling start once started

        }, function (error) {
            showMessage(error.message);
        });
    };
    FingerprintSdkTest.prototype.stopCapture = function () {
        if (!this.acquisitionStarted) //Monitor if already stopped capturing
            return;
        var _instance = this;
        showMessage("");
        this.sdk.stopAcquisition().then(function () {
            _instance.acquisitionStarted = false;

            //Disabling stop once stoped
            disableEnableStartStop();

        }, function (error) {
            showMessage(error.message);
        });
    };

    FingerprintSdkTest.prototype.getInfo = function () {
        var _instance = this;
        return this.sdk.enumerateDevices();
    };

    FingerprintSdkTest.prototype.getDeviceInfoWithID = function (uid) {
        var _instance = this;
        return this.sdk.getDeviceInfo(uid);
    };


    return FingerprintSdkTest;
})();

function showMessage(message) {

    $("#message").innerHTML = message;
}

window.onload = function () {
    localStorage.clear();
    test = new FingerprintSdkTest();
    //readersDropDownPopulate(true); //To populate readers for drop down selection
 //   enableDisableScanQualityDiv("content-reader"); // To enable disable scan quality div
   // disableEnableExport(true);
    onStart();
};


function onStart() {
    if (currentFormat == "") {
        alert("Please select a format.")
    } else {
        test.startCapture();
    }
}

function changeButtonText(bool) {
    if (bool) {
        $('#starScanning').html('Stop Scann');
        $('#starScanning').click(function () {
            onStop();
        });
    } else {
        $('#starScanning').html('Start Scan');
        $('#starScanning').click(function (event) {
            onStart(event);
        });
    }

}
function onStop() {
    changeButtonText(false);

    test.stopCapture();
}

function onGetInfo() {
    var allReaders = test.getInfo();
    allReaders.then(function (sucessObj) {
        populateReaders(sucessObj);
    }, function (error) {
        showMessage(error.message);
    });
}
function onDeviceInfo(id, element) {
    var myDeviceVal = test.getDeviceInfoWithID(id);
    myDeviceVal.then(function (sucessObj) {
        var deviceId = sucessObj.DeviceID;
        var uidTyp = deviceUidType[sucessObj.eUidType];
        var modality = deviceModality[sucessObj.eDeviceModality];
        var deviceTech = deviceTechn[sucessObj.eDeviceTech];
        //Another method to get Device technology directly from SDK
        //Uncomment the below logging messages to see it working, Similarly for DeviceUidType and DeviceModality
        //console.log(Fingerprint.DeviceTechnology[sucessObj.eDeviceTech]);
        //console.log(Fingerprint.DeviceModality[sucessObj.eDeviceModality]);
        //console.log(Fingerprint.DeviceUidType[sucessObj.eUidType]);
        var retutnVal = //"Device Info -"
            "Id : " + deviceId
            + "<br> Uid Type : " + uidTyp
            + "<br> Device Tech : " + deviceTech
            + "<br> Device Modality : " + modality;

        document.getElementById(element).innerHTML = retutnVal;

    }, function (error) {
        showMessage(error.message);
    });

}
function onClear() {
    var vDiv = document.getElementById('imagediv');
    vDiv.innerHTML = "";
    localStorage.setItem("imageSrc", "");
    localStorage.setItem("image", "");

    localStorage.setItem("wsq", "");
    localStorage.setItem("raw", "");
    localStorage.setItem("intermediate", "");

    disableEnableExport(true);
}

function toggle_visibility(ids) {
    document.getElementById("qualityInputBox").value = "";
    onStop();
    enableDisableScanQualityDiv(ids[0]); // To enable disable scan quality div
    for (var i = 0; i < ids.length; i++) {
        var e = document.getElementById(ids[i]);
        if (i == 0) {
            e.style.display = 'block';
            state = e;
            disableEnable();
        }
        else {
            e.style.display = 'none';
        }
    }
}



function populateReaders(readersArray) {

};

function sampleAcquired(s) {
    debugger;
    localStorage.setItem("imageSrc", "");
    var samples = JSON.parse(s.samples);

    localStorage.setItem("imageSrc", "data:image/png;base64," + Fingerprint.b64UrlTo64(samples[0]));
    localStorage.setItem("image", Fingerprint.b64UrlTo64(samples[0]));

    var patientimage = document.getElementById('fingerImage');
    patientimage.src = localStorage.getItem("imageSrc");

    var image2 = localStorage.getItem("image");
    $('#fingerprint').val(image2);

   // onStop();
    $("#submitbutton").trigger('click');

}



function populatePopUpModal() {
    var modelWindowElement = document.getElementById("ReaderInformationFromDropDown");
    modelWindowElement.innerHTML = "";
    if (myVal != "") {
        onDeviceInfo(myVal, "ReaderInformationFromDropDown");
    } else {
        modelWindowElement.innerHTML = "Please select a reader";
    }
}






// For Download and formats starts

function onImageDownload() {
    if (currentFormat == Fingerprint.SampleFormat.PngImage) {
        if (localStorage.getItem("imageSrc") == "" || localStorage.getItem("imageSrc") == null || document.getElementById('imagediv').innerHTML == "") {
            alert("No image to download");
        } else {
            //alert(localStorage.getItem("imageSrc"));
            downloadURI(localStorage.getItem("imageSrc"), "sampleImage.png", "image/png");
        }
    }

    else if (currentFormat == Fingerprint.SampleFormat.Compressed) {
        if (localStorage.getItem("wsq") == "" || localStorage.getItem("wsq") == null || document.getElementById('imagediv').innerHTML == "") {
            alert("WSQ data not available.");
        } else {
            downloadURI(localStorage.getItem("wsq"), "compressed.wsq", "application/octet-stream");
        }
    }

    else if (currentFormat == Fingerprint.SampleFormat.Raw) {
        if (localStorage.getItem("raw") == "" || localStorage.getItem("raw") == null || document.getElementById('imagediv').innerHTML == "") {
            alert("RAW data not available.");
        } else {

            downloadURI("data:application/octet-stream;base64," + localStorage.getItem("raw"), "rawImage.raw", "application/octet-stream");
        }
    }

    else if (currentFormat == Fingerprint.SampleFormat.Intermediate) {
        if (localStorage.getItem("intermediate") == "" || localStorage.getItem("intermediate") == null || document.getElementById('imagediv').innerHTML == "") {
            alert("Intermediate data not available.");
        } else {

            downloadURI("data:application/octet-stream;base64," + localStorage.getItem("intermediate"), "FeatureSet.bin", "application/octet-stream");
        }
    }

    else {
        alert("Nothing to download.");
    }
}


function downloadURI(uri, name, dataURIType) {
    if (IeVersionInfo() > 0) {
        //alert("This is IE " + IeVersionInfo());
        var blob = dataURItoBlob(uri, dataURIType);
        window.navigator.msSaveOrOpenBlob(blob, name);

    } else {
        //alert("This is not IE.");
        var save = document.createElement('a');
        save.href = uri;
        save.download = name;
        var event = document.createEvent("MouseEvents");
        event.initMouseEvent(
            "click", true, false, window, 0, 0, 0, 0, 0
            , false, false, false, false, 0, null
        );
        save.dispatchEvent(event);
    }
}

dataURItoBlob = function (dataURI, dataURIType) {
    var binary = atob(dataURI.split(',')[1]);
    var array = [];
    for (var i = 0; i < binary.length; i++) {
        array.push(binary.charCodeAt(i));
    }
    return new Blob([new Uint8Array(array)], { type: dataURIType });
}


function IeVersionInfo() {
    var sAgent = window.navigator.userAgent;
    var IEVersion = sAgent.indexOf("MSIE");

    // If IE, return version number.
    if (IEVersion > 0)
        return parseInt(sAgent.substring(IEVersion + 5, sAgent.indexOf(".", IEVersion)));

    // If IE 11 then look for Updated user agent string.
    else if (!!navigator.userAgent.match(/Trident\/7\./))
        return 11;

    // Quick and dirty test for Microsoft Edge
    else if (document.documentMode || /Edge/.test(navigator.userAgent))
        return 12;

    else
        return 0; //If not IE return 0
}


$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});




function delayAnimate(id, visibility) {
    document.getElementById(id).style.display = visibility;
}

// For Download and formats ends

