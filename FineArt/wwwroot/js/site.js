$(document).ready(function () {
    $("#imageUpload").change(function () {
        readURL(this);
    });      

    // Admin Profile setting validation
    //$("#AdminProfileSetting").validate({
    //    rules: {
    //        Age: "required",
    //        Gender: "required",
    //        Phone: "required",
    //        Extras: "required"
    //    },
    //    messages: {
    //        Age: {
    //            required: "Age field is required"
    //        },
    //        Gender: {
    //            required: "Gender field is required"
    //        },
    //        Phone: {
    //            required: "Phone field is required"
    //        },
    //        Extras: {
    //            required: "Extras field is required"
    //        },
    //    },
    //    submitHandler: function (form) {
    //        form.submit();
    //    }
    //});


    //Admin Profile Ajax
    $("#AdminProfileSetting").on("submit", function (e) {
        e.preventDefault();

        $(".loading").removeClass("loading-hidden");

        //var data = $("#AdminProfileSetting").serialize();
        var formData = new FormData(this);
        $.ajax({
            type: 'POST',
            url: '/Identify/AdminProfileCreate',
            //contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            contentType: false,
            processData: false,
            data: formData,
            success: function (res) {
                if (res.status == 'success') {
                    $(".loading").addClass("loading-hidden");
                    toastr.success("Your profile info save successfully", "Profile Save", {
                        positionClass: "toast-bottom-right",
                        timeOut: 5e3,
                        closeButton: !0,
                        debug: !1,
                        newestOnTop: !0,
                        progressBar: !0,
                        preventDuplicates: !0,
                        onclick: null,
                        showDuration: "300",
                        hideDuration: "1000",
                        extendedTimeOut: "1000",
                        showEasing: "swing",
                        hideEasing: "linear",
                        showMethod: "fadeIn",
                        hideMethod: "fadeOut",
                        tapToDismiss: !1
                    });
                    setTimeout(function () {
                        window.location.href = '/Admin/Index'; 
                    }, 3000);
                }
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        })
    });

    //Manager Profile Ajax
    $("#ManagerProfileSetting").on("submit", function (e) {
        e.preventDefault();

        $(".loading").removeClass("loading-hidden");

        //var data = $("#AdminProfileSetting").serialize();
        var formData = new FormData(this);
        $.ajax({
            type: 'POST',
            url: '/Identify/ManagerPage',
            //contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            contentType: false,
            processData: false,
            data: formData,
            success: function (res) {
                if (res.status == 'success') {
                    $(".loading").addClass("loading-hidden");                     
                    toastr.success("Your profile info save successfully", "Profile Save", {
                        positionClass: "toast-bottom-right",
                        timeOut: 5e3,
                        closeButton: !0,
                        debug: !1,
                        newestOnTop: !0,
                        progressBar: !0,
                        preventDuplicates: !0,
                        onclick: null,
                        showDuration: "300",
                        hideDuration: "1000",
                        extendedTimeOut: "1000",
                        showEasing: "swing",
                        hideEasing: "linear",
                        showMethod: "fadeIn",
                        hideMethod: "fadeOut",
                        tapToDismiss: !1
                    });
                    setTimeout(function () {
                        window.location.href = '/Admin/Index';
                    }, 3000);
                }
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        })
    });

    //Teacher (Staff) Profile Ajax
    $("#TeacherProfileSetting").on("submit", function (e) {
        e.preventDefault();

        $(".loading").removeClass("loading-hidden");

        //var data = $("#AdminProfileSetting").serialize();
        var formData = new FormData(this);
        $.ajax({
            type: 'POST',
            url: '/Identify/TeacherPage',
            //contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            contentType: false,
            processData: false,
            data: formData,
            success: function (res) {
                if (res.status == 'success') {
                    $(".loading").addClass("loading-hidden");
                    toastr.success("Your profile info save successfully", "Profile Save", {
                        positionClass: "toast-bottom-right",
                        timeOut: 5e3,
                        closeButton: !0,
                        debug: !1,
                        newestOnTop: !0,
                        progressBar: !0,
                        preventDuplicates: !0,
                        onclick: null,
                        showDuration: "300",
                        hideDuration: "1000",
                        extendedTimeOut: "1000",
                        showEasing: "swing",
                        hideEasing: "linear",
                        showMethod: "fadeIn",
                        hideMethod: "fadeOut",
                        tapToDismiss: !1
                    });
                    setTimeout(function () {
                        window.location.href = '/Web/Index';
                    }, 3000);
                }
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        })
    });

    //Student Profile Ajax
    $("#StudentProfileSetting").on("submit", function (e) {
        e.preventDefault();

        $(".loading").removeClass("loading-hidden");

        //var data = $("#AdminProfileSetting").serialize();
        var formData = new FormData(this);
        $.ajax({
            type: 'POST',
            url: '/Identify/StudentPage',
            //contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            contentType: false,
            processData: false,
            data: formData,
            success: function (res) {
                if (res.status == 'success') {
                    $(".loading").addClass("loading-hidden");
                    toastr.success("Your profile info save successfully", "Profile Save", {
                        positionClass: "toast-bottom-right",
                        timeOut: 5e3,
                        closeButton: !0,
                        debug: !1,
                        newestOnTop: !0,
                        progressBar: !0,
                        preventDuplicates: !0,
                        onclick: null,
                        showDuration: "300",
                        hideDuration: "1000",
                        extendedTimeOut: "1000",
                        showEasing: "swing",
                        hideEasing: "linear",
                        showMethod: "fadeIn",
                        hideMethod: "fadeOut",
                        tapToDismiss: !1
                    });
                    setTimeout(function () {
                        window.location.href = '/Web/Index';
                    }, 3000);
                }
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        })
    });

});

// custom file input JS
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imagePreview').css('background-image', 'url(' + e.target.result + ')');
            $('#imagePreview').hide();
            $('#imagePreview').fadeIn(650);
        }
        reader.readAsDataURL(input.files[0]);
    }
}

