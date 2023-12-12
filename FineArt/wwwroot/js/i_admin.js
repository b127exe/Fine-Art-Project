$(document).ready(function () {

    // Manager approval
    $(".manager-approval").on("click", function () {

        let id = $(this).data("id");
        swal({
            title: "Approval Request!!",
            text: "Do you want approved the manager request !!",
            type: "info",
            showCancelButton: !0,
            closeOnConfirm: !1,
            showLoaderOnConfirm: !0,
            preConfirm: () => {
                $(".loading").removeClass("loading-hidden");
                $.ajax({
                    type: 'POST',
                    url: '/Admin/ManagerApproval',
                    data: { id: id },
                    success: function (res) {
                        if (res.status == 'success') {
                            $(".loading").addClass("loading-hidden");
                            toastr.success("Manager request is approved", "Approved!", {
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
                                window.location.reload();
                            }, 3000)
                        }
                    },
                    error: function () {
                        alert('Failed to receive the Data');
                        console.log('Failed ');
                    }
                });
            }
        }, function () {
            
        })
    });

    // teacher approval
    $(".approved-teacher").on("click", function () {

        let id = $(this).data("id");
        swal({
            title: "Approval Request!!",
            text: "Do you want approved the teacher request !!",
            type: "info",
            showCancelButton: !0,
            closeOnConfirm: !1,
            showLoaderOnConfirm: !0,
            preConfirm: () => {
                $(".loading").removeClass("loading-hidden");
                $.ajax({
                    type: 'POST',
                    url: '/Admin/TeacherApproval',
                    data: { id: id },
                    success: function (res) {
                        if (res.status == 'success') {
                            $(".loading").addClass("loading-hidden");
                            toastr.success("Teacher request is approved", "Approved!", {
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
                                window.location.reload();
                            }, 3000)
                        }
                    },
                    error: function () {
                        alert('Failed to receive the Data');
                        console.log('Failed ');
                    }
                });
            }
        }, function () {

        })
    });

    //Add Award Ajax
    $("#AddAwardForm").on("submit", function (e) {
        e.preventDefault();

        $(".loading").removeClass("loading-hidden");

        //var data = $("#AdminProfileSetting").serialize();
        var formData = new FormData(this);
        $.ajax({
            type: 'POST',
            url: '/Admin/AddAward',
            //contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            contentType: false,
            processData: false,
            data: formData,
            success: function (res) {
                if (res.status == 'success') {
                    $(".loading").addClass("loading-hidden");
                    toastr.success("Award added successfully", "Award Save", {
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
                        window.location.href = '/Admin/AwardsList';
                    }, 3000);
                }
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        })
    });

    // Delete Award Ajax
    $(".award-delete").on("click", function () {

        let id = $(this).data("id");
        swal({
            title: "Are you sure to delete ?",
            text: "After delete this award is no longer in list !",
            type: "warning",
            showCancelButton: !0,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete it !!",
            closeOnConfirm: !1,
            preConfirm: () => {
                $(".loading").removeClass("loading-hidden");
                $.ajax({
                    type: 'POST',
                    url: '/Admin/DeleteAward',
                    data: { id: id },
                    success: function (res) {
                        if (res.status == 'success') {
                            $(".loading").addClass("loading-hidden");
                            toastr.warning("Award delete successfully", "Deleted!", {
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
                                window.location.reload();
                            }, 3000)
                        }
                    },
                    error: function () {
                        alert('Failed to receive the Data');
                        console.log('Failed ');
                    }
                });
            }
        }, function () {

        })
    });

    // Add Competition Ajax
    $("#AddComprtitionForm").on("submit", function (e) {
        e.preventDefault();

        $(".loading").removeClass("loading-hidden");

        //var data = $("#AdminProfileSetting").serialize();
        var formData = new FormData(this);
        $.ajax({
            type: 'POST',
            url: '/Admin/AddCompetition',
            //contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            contentType: false,
            processData: false,
            data: formData,
            success: function (res) {
                if (res.status == 'success') {
                    $(".loading").addClass("loading-hidden");
                    toastr.success("Competition added successfully", "Competition Added", {
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
                        window.location.href = '/Admin/CompetitionsList';
                    }, 3000);
                }
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        })
    });

    // Edit Competition Ajax
    $("#EditComprtitionForm").on("submit", function (e) {
        e.preventDefault();

        $(".loading").removeClass("loading-hidden");

        //var data = $("#AdminProfileSetting").serialize();
        var formData = new FormData(this);
        $.ajax({
            type: 'POST',
            url: '/Admin/EditCompetition',
            //contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            contentType: false,
            processData: false,
            data: formData,
            success: function (res) {
                if (res.status == 'success') {
                    $(".loading").addClass("loading-hidden");
                    toastr.success("Competition edit successfully", "Competition Edited", {
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
                        window.location.href = '/Admin/CompetitionsList';
                    }, 3000);
                }
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        })
    });

    // Delete Competition Ajax
    $(".competition-delete").on("click", function () {

        let id = $(this).data("id");
        swal({
            title: "Are you sure to delete ?",
            text: "After delete this competition is no longer in list !",
            type: "warning",
            showCancelButton: !0,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete it !!",
            closeOnConfirm: !1,
            preConfirm: () => {
                $(".loading").removeClass("loading-hidden");
                $.ajax({
                    type: 'POST',
                    url: '/Admin/DeleteCompetition',
                    data: { id: id },
                    success: function (res) {
                        if (res.status == 'success') {
                            $(".loading").addClass("loading-hidden");
                            toastr.warning("Competition delete successfully", "Deleted!", {
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
                                window.location.reload();
                            }, 3000)
                        }
                    },
                    error: function () {
                        alert('Failed to receive the Data');
                        console.log('Failed ');
                    }
                });
            }
        }, function () {

        })
    });

    // check remarks button ajax and logic
    $(".check-remarks").on("click", function (event) {
        
        event.preventDefault();
        console.log("get remarks");
        var id = $(this).data("id");

        //$("#remark-product_" + id).html("");

        $.ajax({
            url: '/Admin/GetSubmissionRemarks/' + id,
            type: "GET",
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (res) {

                //console.log(res);
                $('#chechRemarksModal_' + res.submissionId).modal('show');
                var message = '';
                var mark = '';
                $.each(res.remarks, function (index, item) {
                    switch (item.marks) {
                        case 0:
                            mark = 'Best';
                            break;
                        case 1:
                            mark = 'Better';
                            break;
                        case 2:
                            mark = 'Good';
                            break;
                        case 3:
                            mark = 'Moderate';
                            break;
                        case 4:
                            mark = 'Normal';
                            break;
                        case 5:
                            mark = 'Disqualified';
                            break;
                    }
                    message += `
                        <div class="message-blue">
                                <p class="message-content">${item.comments}</p>
                                <div class="message-timestamp-left">Impression: ${mark}</div>
                            </div>
                    `;
                });
                $(".msg-box_" + id).html(message);

                // for mark statistics

                setTimeout(function () {
                    getStatisticsChart(res.marks, res.submissionId);
                }, 1000)
                

            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });

    });

    // submission approval
    $(".submission-approved").on("click", function () {

        let id = $(this).data("id");
        swal({
            title: "Announce Winner!!",
            text: "Do you want approved this posting submission!!",
            type: "info",
            showCancelButton: !0,
            closeOnConfirm: !1,
            showLoaderOnConfirm: !0,
            preConfirm: () => {
                $(".loading").removeClass("loading-hidden");
                $.ajax({
                    type: 'POST',
                    url: '/Admin/AnnounceCompetitionWinner',
                    data: { id: id },
                    success: function (res) {
                        console.log(res);
                        if (res.status == 'success') {
                            $(".loading").addClass("loading-hidden");
                            toastr.success(res.message, "Winner Annouced!", {
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
                });
            }
        }, function () {

        })
    });

    // add exhibition form
    $('#AddExhibitionForm').submit(function (event) {
        event.preventDefault();

        var form = $(this);
        // Perform form validation
        if (validateExhibitionForm(form)) {
            submitExhibitionForm(form);
        }
    });

    $('#EditExhibitionForm').submit(function (event) {
        event.preventDefault();

        var form = $(this);
        // Perform form validation
        if (validateExhibitionForm(form)) {
            submitEditExhibitionForm(form);
        }
    });


    $(".exhibition-delete").on("click", function () {

        let id = $(this).data("id");
        swal({
            title: "Are you sure to delete ?",
            text: "After delete this Exhibition is no longer in list !",
            type: "warning",
            showCancelButton: !0,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete it !!",
            closeOnConfirm: !1,
            preConfirm: () => {
                $(".loading").removeClass("loading-hidden");
                $.ajax({
                    type: 'POST',
                    url: '/Admin/DeleteExhibition',
                    data: { id: id },
                    success: function (res) {
                        if (res.status == 'success') {
                            $(".loading").addClass("loading-hidden");
                            toastr.warning("Exhibition delete successfully", "Deleted!", {
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
                                window.location.reload();
                            }, 3000)
                        }
                    },
                    error: function () {
                        alert('Failed to receive the Data');
                        console.log('Failed ');
                    }
                });
            }
        }, function () {

        })
    });


    // load home content with get method
    LoadHomeCountData();

    // load the country api function 
    fetchCountryApi();

});

function LoadHomeCountData() {
    $.ajax({
        url: "/Admin/GetUserCount",
        type: "GET",
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (response, status, xhr) {
            $("#TotalUsers").html(response.totalUser);
            $("#TotalStudent").html(response.totalStudent);
            $("#TotalTeacher").html(response.totalTeacher);
            $("#TotalManager").html(response.totalManager);
        },
        error: function () {
            alert("Data can't get...");
        }
    });
}

function getStatisticsChart(marks, id) {
    //console.log("statistics");
    var markLabel = '';
    var markData = [];
    var labelArr = [];

    $.each(marks, function (index, item) {

        switch (item.mark) {
            case 0:
                markLabel = 'Best';
                break;
            case 1:
                markLabel = 'Better';
                break;
            case 2:
                markLabel = 'Good';
                break;
            case 3:
                markLabel = 'Moderate';
                break;
            case 4:
                markLabel = 'Normal';
                break;
            case 5:
                markLabel = 'Disqualified';
                break;
        }

        markData.push(item.count);
        labelArr.push(markLabel);

    });

    return new Chart($("#remark-product_" + id), {
        type: 'pie',
        data: {
            defaultFontFamily: 'Poppins',
            datasets: [{
                data: markData,
                borderWidth: 0,
                backgroundColor: [
                    "#007BB6",
                    "#999",
                    "rgba(89, 59, 219, .9)",
                    "rgba(89, 59, 219, .7)",
                    "rgba(89, 59, 219, .5)",
                    "rgba(89, 59, 219, .07)",
                ],
                hoverBackgroundColor: [
                    "#007BB6",
                    "#999",
                    "rgba(89, 59, 219, .9)",
                    "rgba(89, 59, 219, .7)",
                    "rgba(89, 59, 219, .5)",
                    "rgba(89, 59, 219, .07)"
                ]

            }],
            labels: labelArr
        },
        options: {
            responsive: true,
            legend: false,
            maintainAspectRatio: false
        }
    });
    
}

function validateExhibitionForm(form) {
    var isValid = true;

    if (form.find('#ExTitle').val() === '') {
        toastr.error("Please fill up Exhibition Title...", "Title Required!", {
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
    } else if (form.find('#ExhibitionDate').val() === '') {
        toastr.error("Please select an exhibition date...", "Date Required!", {
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
    } else if (form.find('#ExCountry').val() === '') {
        toastr.error("Please select an country...", "Country Required!", {
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
    } else if (form.find('#ExDetails').val() === '') {
        toastr.error("Please fill up details...", "Details Required!", {
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
    } else if (form.find('#ExConditions').val() === '') {
        toastr.error("Please fill up conditions...", "Conditions Required!", {
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

function submitExhibitionForm(form) {
    var formData = new FormData(form[0]);
    $(".loading").removeClass("loading-hidden");
    $.ajax({
        type: 'POST',
        url: '/Admin/AddExhibition',
        contentType: false,
        processData: false,
        data: formData,
        success: function (response) {
            if (response.status == 'success') {
                $(".loading").addClass("loading-hidden");
                toastr.success(response.message, "Exhibition Created!", {
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

                setTimeout(function () {
                    window.location.href = "/Admin/ExhibitionsList";
                }, 3000);
            }
        },
        error: function (error) {
            alert('Failed to send the Data');
            console.log('Failed ');
        }
    });
}

function submitEditExhibitionForm(form) {
    var formData = new FormData(form[0]);
    $(".loading").removeClass("loading-hidden");
    $.ajax({
        type: 'POST',
        url: '/Admin/EditExhibition',
        contentType: false,
        processData: false,
        data: formData,
        success: function (response) {
            if (response.status == 'success') {
                $(".loading").addClass("loading-hidden");
                toastr.success(response.message, "Exhibition Edited!", {
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

                setTimeout(function () {
                    window.location.href = "/Admin/ExhibitionsList";
                }, 3000);
            }
        },
        error: function (error) {
            alert('Failed to send the Data');
            console.log('Failed ');
        }
    });
}

function fetchCountryApi() {
    fetch('https://restcountries.com/v3.1/all')
        .then(response => response.json())
        .then(data => {
            // Get the select element
            const selectElement = document.getElementById('ExCountry');

            // Loop through the data and populate the options
            data.forEach(country => {
                const option = document.createElement('option');
                option.value = country.name.common;
                option.text = country.name.common;
                selectElement.appendChild(option);
            });
        })
        .catch(error => {
            console.error('Error fetching data:', error);
        });
}