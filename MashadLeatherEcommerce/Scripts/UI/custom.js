$(document).ready(function () {
    localStorage.setItem("id", "");
});
function like(id) {
    var productId = id;
    var elementId = "#like_" + productId;

    $.ajax(
        {
            url: "/Products/SaveUserProductLike",
            data: { id: productId },
            type: "GET"
        }).done(function (result) {
            if (result === "true") {
                //like product
                $(elementId).addClass("likeList");
            }
            else if (result === "false") {
                //unlike product
                $(elementId).removeClass("likeList");
            }
            else if (result === "notAuthenticated") {
                //dont login
                $('#exampleModal').modal('show');
            }
        });
}
function LeaveComment(id) {
    localStorage.setItem("id", id);
    window.location.hash="respond";
}
function SubmitComment() {

    var parentId = localStorage.getItem("id");
    var url = window.location.pathname;
    var id = url.substring(url.lastIndexOf('/') + 1);
    var nameVal = $("#commentName").val();
    var emailVal = $("#commentEmail").val();
    var bodyVal = $("#commentBody").val();
    if (nameVal !== "" && emailVal !== "" && bodyVal !== "") {
        $.ajax(
            {
                url: "/Products/SubmitComment",
                data: { name: nameVal, email: emailVal, body: bodyVal, productGroupName: id },
                type: "GET"
            }).done(function (result) {
                if (result === "true") {
                    $("#errorDiv").css('display', 'none');
                    $("#SuccessDiv").css('display', 'block');
                    localStorage.setItem("id", "");
                }
                else if (result === "InvalidEmail") {
                    $("#errorDiv").html('ایمیل وارد شده صحیح نمی باشد.');
                    $("#errorDiv").css('display', 'block');
                    $("#SuccessDiv").css('display', 'none');
                    
                }
            });
    }
    else {
        $("#errorDiv").html('تمامی فیلد های زیر را تکمیل نمایید.');
        $("#errorDiv").css('display', 'block');
        $("#SuccessDiv").css('display', 'none');
        
    }
}

