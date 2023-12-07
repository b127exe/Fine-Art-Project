$(document).ready(function () {

    //Custom input file js
    ImgUpload();

	// Fetch all the forms we want to apply custom Bootstrap validation styles to
	const forms = document.querySelectorAll('.needs-validation')

	// Loop over them and prevent submission
	Array.from(forms).forEach(form => {
		form.addEventListener('submit', event => {
			if (!form.checkValidity()) {
				event.preventDefault()
				event.stopPropagation()
			}

			form.classList.add('was-validated')
		}, false)
	});


    // Add Posting Ajax
    $("#AddPostingForm").on("submit", function (e) {
        e.preventDefault();

        $(".loading").removeClass("loading-hidden");

        //var data = $("#AdminProfileSetting").serialize();
        var formData = new FormData(this);
        $.ajax({
            type: 'POST',
            url: '/Web/AddPosting',
            //contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            contentType: false,
            processData: false,
            data: formData,
            success: function (res) {
                if (res.status == 'success') {
                    $(".loading").addClass("loading-hidden");
                    toastr.success("Design Posted Successfully", "Design Posted", {
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

                    $("#AddPostingForm")[0].reset();
                    $("#AddPostingModal").modal('hide');
                    //setTimeout(function () {
                    //    window.location.href = '/Admin/CompetitionsList';
                    //}, 3000);
                }
                else {
                    $(".loading").addClass("loading-hidden");
                    toastr.error("An error occured while posting your art..", "Error!", {
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
                }
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        })
    });

    // Competition Submission ajax 
    $('#CompetitionSubmissionForm').submit(function (event) {
        event.preventDefault(); 

        // Perform form validation
        if (validateSubmissionForm()) {
            // If validation passes, proceed with AJAX submission
            submitSubmissionForm();
        }
    });

    // Remarks Ajax
    $('.GiveRemarksForm').submit(function (event) {
        //console.log("clciked");
        event.preventDefault();

        var form = $(this);

        // Perform form validation
        if (validateRemarksForm(form)) {
            // If validation passes, proceed with AJAX submission
            submitRemarksForm(form);
        }
    });

    // competition search for teacher section
    $("#CompetitionTxtSearch").keyup(function () {
        var typeValue = $(this).val();
        $("#TCompetitionBody div").each(function () {
            if ($(this).text().search(new RegExp(typeValue, "i")) < 0) {
                $(this).fadeOut();
            }
            else {
                $(this).show();
            }
        });

    });

    $("#StudentTxtSearch").keyup(function () {
        var typeValue = $(this).val();
        $("#StudentForTeacherBody tr").each(function () {
            if ($(this).text().search(new RegExp(typeValue, "i")) < 0) {
                $(this).fadeOut();
            }
            else {
                $(this).show();
            }
        });
    })

});

//function ImgUpload() {
//    var imgWrap = "";
//    var imgArray = [];

//    $('.upload__inputfile').each(function () {
//        $(this).on('change', function (e) {
//            imgWrap = $(this).closest('.upload__box').find('.upload__img-wrap');
//            var maxLength = $(this).attr('data-max_length');

//            var files = e.target.files;
//            var filesArr = Array.prototype.slice.call(files);
//            var iterator = 0;
//            filesArr.forEach(function (f, index) {

//                if (!f.type.match('image.*')) {
//                    return;
//                }

//                if (imgArray.length > maxLength) {
//                    return false
//                } else {
//                    var len = 0;
//                    for (var i = 0; i < imgArray.length; i++) {
//                        if (imgArray[i] !== undefined) {
//                            len++;
//                        }
//                    }
//                    if (len > maxLength) {
//                        return false;
//                    } else {
//                        imgArray.push(f);

//                        var reader = new FileReader();
//                        reader.onload = function (e) {
//                            var html = "<div class='upload__img-box'><div style='background-image: url(" + e.target.result + ")' data-number='" + $(".upload__img-close").length + "' data-file='" + f.name + "' class='img-bg'><div class='upload__img-close'></div></div></div>";
//                            imgWrap.append(html);
//                            iterator++;
//                        }
//                        reader.readAsDataURL(f);
//                    }
//                }
//            });
//        });
//    });

//    $('body').on('click', ".upload__img-close", function (e) {
//        var file = $(this).parent().data("file");
//        for (var i = 0; i < imgArray.length; i++) {
//            if (imgArray[i].name === file) {
//                imgArray.splice(i, 1);
//                break;
//            }
//        }
//        $(this).parent().parent().remove();
//    });
//}

function ImgUpload() {
    var imgWrap = "";
    var imgArray = [];

    $('.upload__inputfile').each(function () {
        $(this).on('change', function (e) {
            imgWrap = $(this).closest('.upload__box').find('.upload__img-wrap');
            var maxLength = $(this).attr('data-max_length');

            var files = e.target.files;
            var filesArr = Array.prototype.slice.call(files);
            var iterator = 0;

            // Show loader while processing
            var loaderHtml = `<div class="w-100 d-flex justify-content-center">
                                  <div class="spinner-border" role="status">
                                    <span class="visually-hidden">Loading...</span>
                                  </div>
                                </div>`;
            imgWrap.html(loaderHtml);

            // Process files with a delay to simulate loading time
            setTimeout(function () {
                filesArr.forEach(function (f, index) {
                    if (!f.type.match('image.*')) {
                        return;
                    }

                    if (imgArray.length > maxLength) {
                        return false;
                    } else {
                        var len = 0;
                        for (var i = 0; i < imgArray.length; i++) {
                            if (imgArray[i] !== undefined) {
                                len++;
                            }
                        }
                        if (len > maxLength) {
                            return false;
                        } else {
                            imgArray.push(f);

                            var reader = new FileReader();
                            reader.onload = function (e) {
                                var html = "<div class='upload__img-box'><div style='background-image: url(" + e.target.result + ")' data-number='" + $(".upload__img-close").length + "' data-file='" + f.name + "' class='img-bg'><div class='upload__img-close'></div></div></div>";
                                imgWrap.html(html);
                                iterator++;
                            }
                            reader.readAsDataURL(f);
                        }
                    }
                });
            }, 1000); // Simulate loading time

        });
    });

    $('body').on('click', ".upload__img-close", function (e) {
        var file = $(this).parent().data("file");
        for (var i = 0; i < imgArray.length; i++) {
            if (imgArray[i].name === file) {
                imgArray.splice(i, 1);
                break;
            }
        }
        $(this).parent().parent().remove();
    });
}

function selectPosting(button, postingId) {
    // Deselect the previously selected button and hide its icon
    var previousButton = $('.select-posting.selected');
    if (previousButton.length) {
        previousButton.removeClass('selected');
        previousButton.html("SELECT");
        previousButton.closest('.card-pin').find('.selected-icon').hide();
    }

    // Select the clicked button and show its icon
    $(button).addClass('selected').html("SELECTED");
    $(button).closest('.card-pin').find('.selected-icon').show();
    getPostById(postingId);
    // Update the hidden input field with the selected postingId
    $('#selectedPostingId').val(postingId);
}

function getPostById(postingId) {
    var loaderHtml = `<div class="w-100 d-flex justify-content-center align-items-center">
                                  <div class="spinner-border" role="status">
                                    <span class="visually-hidden">Loading...</span>
                                  </div>
                                </div>`;
    $("#preview-design").html(loaderHtml);
    $.ajax({
        url: "/Web/GetPostById/" + postingId,
        type: "GET",
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (response, status, xhr) {
            var img = `<img src="/posting-uploads/${response.designImageUrl}" width="100%" />`;
            $("#preview-design").html(img);
        },
        error: function () {
            alert("Data can't get...");
        }
    });
}

// posting submission validation and ajax function

function validateSubmissionForm() {
    // Add your validation logic here
    // For example, check if the required fields are filled up
    var isValid = true;

    if ($('#selectedPostingId').val() === '') {
        toastr.error("Please Select the Posting Design", "Posting Required!", {
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
        isValid = false;
    } else if ($('#inputPostingSubmission_SubmissionQuote').val() === '') {
        toastr.error("Please Enter the Submission Quote", "Quote Required!", {
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
        isValid = false;
    }

    return isValid;
}
function submitSubmissionForm() {

    var formData = new FormData($("#CompetitionSubmissionForm")[0]);
    $(".loading").removeClass("loading-hidden");
    $.ajax({
        type: 'POST',
        url: '/Web/ParticipateInCompetition', 
        contentType: false,
        processData: false,
        data: formData, 
        success: function (response) {
            if (response.status == 'success') {
                $(".loading").addClass("loading-hidden");
                toastr.success(response.message, "Design Submitted!", {
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

                $("#CompetitionSubmissionForm")[0].reset();
                var img = `<img src="https://png.pngtree.com/png-clipart/20190925/original/pngtree-no-image-vector-illustration-isolated-png-image_4979075.jpg" width="100%" />`;
                $("#preview-design").html(img);
            }
        },
        error: function (error) {
            alert('Failed to send the Data');
            console.log('Failed ');
        }
    });
}

// give remarks submission functions and ajax

function validateRemarksForm(form) {
    var isValid = true;

    if (form.find('.Comments').val() === '') {
        toastr.error("Please fill up comments section...", "Comments Required!", {
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
        isValid = false;
    } else if (form.find('.Marks').val() === '') {
        toastr.error("Please select an impression", "Impression Required!", {
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
        isValid = false;
    }

    return isValid;
}

function submitRemarksForm(form) {
    var formData = new FormData(form[0]);
    $(".loading").removeClass("loading-hidden");
    $.ajax({
        type: 'POST',
        url: '/Web/GiveRemarksForDesign',
        contentType: false,
        processData: false,
        data: formData,
        success: function (response) {
            if (response.status == 'success') {
                $(".loading").addClass("loading-hidden");
                toastr.success(response.message, "Remarks Submitted!", {
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

                form[0].reset();
                $(".modal").modal("hide");
            }
        },
        error: function (error) {
            alert('Failed to send the Data');
            console.log('Failed ');
        }
    });
}