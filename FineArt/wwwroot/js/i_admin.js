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

    LoadHomeCountData();

});

function LoadHomeCountData() {
    $.ajax({
        url: "/Admin/GetUserCount",
        type: "GET",
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (response, status, xhr) {
            console.log(response);
            $("#TotalUsers").html(response.data);
        },
        error: function () {
            alert("Data can't get...");
        }
    });
}