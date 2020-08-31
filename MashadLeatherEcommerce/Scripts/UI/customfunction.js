
function LoadOrders() {
    var jsondata = get();
    if (jsondata !== null) {
        $('#shop-cart').css('display', 'block');
        $('#shop-cart-empty').css('display', 'none');
        var rows = '';
        $.ajax(
            {
                url: "/Orders/LoadShopCart",
                data: { jsonvar: jsondata },
                type: "GET"

            }).done(function (result) {



                for (var i = 0; i < result.ShopCartItems.length; i++) {
                    var rowItems = GetRemoveButton(result.ShopCartItems[i].Id);

                    rowItems = rowItems +
                        GetProductTitleAndImage(result.ShopCartItems[i].ImageUrl, result.ShopCartItems[i].Title, result.ShopCartItems[i].Id) +
                        GetProductColor(result.ShopCartItems[i].colorTitle) +
                        GetProductSize(result.ShopCartItems[i].SizeTitle) +
                        GetProductPrice(result.ShopCartItems[i].Price) +
                        GetProductQty(result.ShopCartItems[i].Qty, result.ShopCartItems[i].Id) +

                        "</tr>";

                    rows = rows + rowItems;
                }

                $('#rows').html(rows);

                $('#orderAmount').html(result.Amount);
                $('#shippmentAmount').html(result.ShippmentPrice);
                $('#DiscountAmount').html(result.Discount);
                $('#total').html(result.TotalPayment);

            });
    } else {
        $('#shop-cart').css('display', 'none');
        $('#shop-cart-empty').css('display', 'block');
    }
}

function addToBasket(id) {
    //EventSetup
    window.dataLayer = window.dataLayer || [];
    window.dataLayer.push({
        event: 'addtocart'
    });
    //EventSetup
    
    var currentBasket = get();
    var qty = $("#txtCount").val();
    //var color = $('#ddlColor')[0].title();
    var ddl = $("#ddlColor");

    var color = ddl[0];

    var size = $(".product-size input[name='product-size']:checked").val();

    if (color === null || color === undefined || color === "")
        color = "nocolor";
    else {
        color = color.title;
    }

    if (color === null || color === undefined || color === "") {
        showErrorMessageForNoColorSelect();
    } else {
        if (size === null || size === undefined) {
            size = "nosize";
            if (currentBasket === null) {
                currentBasket = '';
            }
            currentBasket = currentBasket + id + "." + qty + "." + color + "." + size + "/";

            localStorage.setItem("basket", currentBasket);
            successMessage();
            setBasketCount();
        } else {
            $.ajax(
                {
                    url: "/Products/CheckSizeAndColor",
                    data: { productId: id, colorId: color, sizeId: size },
                    type: "GET",
                    async: false,
                    dataType: 'json'
                }).done(function (result) {
                    console.log(result);
                    if (result.includes("true")) {
                        if (currentBasket === null) {
                            currentBasket = '';
                        }
                        currentBasket = currentBasket + id + "." + qty + "." + color + "." + size + "/";

                        localStorage.setItem("basket", currentBasket);
                        successMessage();
                        setBasketCount();

                     

                    } else {
                        showErrorMessage();
                    }
                });
        }
    }
}

function showErrorMessage() {
    $('#successMessage').css('display', 'none');
    $('#errorMessage').css('display', 'block');

    $('#errorMessage').html('متاسفانه در حال حاضر سایز و رنگ انتخابی شما موجود نمی باشد. لطفا سایز یا رنگ را تغییر دهید');
}

function showErrorMessageForNoColorSelect() {
    $('#successMessage').css('display', 'none');
    $('#errorMessage').css('display', 'block');

    $('#errorMessage').html('کاربر گرامی رنگ محصول را انتخاب نمایید');
}
function checkSizeAndColorOnServer(productId, sizeId, colorId) {
    //return "true";
    $.ajax(
        {
            url: "/Products/CheckSizeAndColor",
            data: { productId: productId, colorId: colorId, sizeId: sizeId },
            type: "GET",
            async: false,
            dataType: 'json'
        }).done(function (result) {
            console.log(result);
            if (result.includes("true")) {
                return "true";
            } else {
                return "false";
            }
        });


}

function get() {

    return localStorage.getItem("basket");
}
function successMessage() {
    $('#errorMessage').css('display', 'none');
    $('#successMessage').css('display', 'block');
    $('#successMessage').html('<a href="/Orders/ShopCart">این کالا با موفقیت به سبد خرید افزوده شد. جهت مشاهده سبد خرید و نهایی سازی خرید  <span class="underline-text">اینجا</span> را کلیک کنید.</a>');
}
function setBasketCount() {
    var basket = get();
    if (basket !== null) {
        var basketItem = basket.split('/');
        var idList = [];
        var list = { "id": "", "qty": "" };
        var j = -1;
        for (var i = 0; i < basketItem.length - 1; i++) {
            var orderDetail = basketItem[i].split('.');

            if (!idList.includes(orderDetail[0])) {
                j++;
                list[j] = {
                    "id": orderDetail[0],
                    "qty": orderDetail[1]
                };
                idList[j] = orderDetail[0];
            }
        }

        $('#basketItemCount').html(idList.length);
    } else {
        $('#basketItemCount').html('0');

    }
}
function removeOrderDetail(productId) {
    $('#' + productId).css('display', 'none');

    var basket = get();

    if (basket !== null) {

        var basketItem = basket.split('/');

        var newStorage = '';

        for (var i = 0; i < basketItem.length - 1; i++) {

            var orderDetail = basketItem[i].split('.');
            if (orderDetail[0].includes("undefined")) {
                var t = orderDetail[0].split("undefined");
                orderDetail[0] = t[1];
            }

            if (orderDetail[0] !== productId) {

                newStorage = newStorage + orderDetail[0] + '.' + orderDetail[1] + "." + orderDetail[2] + "." + orderDetail[3] + "/";

            }
        }
        localStorage.setItem("basket", newStorage);
        setBasketCount();
        LoadOrders();
    }


}
function UpdateBasket() {
    $('#updateLoadingImg').css('display', 'block');
    $('#btnUpdateBasket').css('display', 'none');
    var currentBasket;
    var basket = get();
    localStorage.clear();
    if (basket !== null) {
        var basketItem = basket.split('/');

        for (var i = 0; i < basketItem.length - 1; i++) {

            var orderDetail = basketItem[i].split('.');
            if (orderDetail[0].includes("undefined")) {
                var t = orderDetail[0].split("undefined");
                orderDetail[0] = t[1];
            }

            var id = "quantity_" + orderDetail[0];
            var input = document.getElementById(id);
            var value = input.value;
            if (value !== "0") {
                orderDetail[1] = value;
                currentBasket = currentBasket + orderDetail[0] + "." + orderDetail[1] + "." + orderDetail[2] + "." + orderDetail[3] + "/";
            }
            else {
                currentBasket = currentBasket;
            }

        }
        localStorage.setItem("basket", currentBasket);
    }
    $('#updateLoadingImg').css('display', 'none');
    $('#btnUpdateBasket').css('display', 'block');
    LoadOrders();
}
function GetRemoveButton(productId) {
    var product = "<tr id='" +
        productId +
        "'> <td class='cart-product-remove'>" +
        "<button type= 'button' onclick=\"removeOrderDetail('" +
        productId +
        "');\"> <i class='fa fa-close'></i></button>" +
        " </td>";

    return product;

}
function GetProductTitleAndImage(productImageUrl, productTitle, productId) {
    var product = " <td class='cart-product-thumbnail'><a href='/product/" + productId + "'>" +
        "<img src='" +
        productImageUrl +
        "' alt='" + productTitle + "'>" +
        //productTitle +
        //"'></a>" +
        "<div class='cart-product-thumbnail-name'>" +
        productTitle +
        "</div></a></td>";

    return product;

}
function GetProductPrice(productPrice) {

    var product = " <td class='cart-product-price'>" +
        "<span class='amount'>" +
        productPrice + " ريال" +
        "</span></td>";

    return product;

}
function GetProductColor(productColor) {

    var product = " <td class='cart-product-color'>" +
        "<span class='color'>" +
        productColor +
        "</span></td>";

    return product;

}
function GetProductSize(productSize) {

    var product = " <td class='cart-product-size'>" +
        "<span class='size'>" +
        productSize +
        "</span></td>";

    return product;

}
function GetProductQty(productQty, productId) {
    //var id = "quantity_" + productId
    //var input = document.getElementById(id);
    //var value = input.value();
    var product = " <td class='cart-product-quantity'>" +
        " <div class='quantity'>" +
        //"<input onchange=changevaluebasket('" + productId + "'," + productQty + "); type='number' style='width:80px;' value='" +
        //"<input onchange=changevaluebasket('" + productId + "'); type='number' style='width:80px;' value='" +
        "<input type='number' style='width:80px;' value='" +
        productQty +
        "' name='quantity' min='1' id='quantity_" + productId + "'>" +
        "   </div></td>";

    return product;
}
function GetRefIdFromResult(result) {
    var refId = result.split('-')[1];
    return refId;
}

function postRefId(refIdValue) {
    var form = document.createElement("form");
    form.setAttribute("method", "POST");
    form.setAttribute("action", "https://bpm.shaparak.ir/pgwchannel/startpay.mellat");
    form.setAttribute("target", "_self");
    var hiddenField = document.createElement("input");
    hiddenField.setAttribute("name", "RefId");
    hiddenField.setAttribute("value", refIdValue);
    form.appendChild(hiddenField);
    document.body.appendChild(form);
    form.submit();
    document.body.removeChild(form);
}
var refffid = 1;



function postSamanRefId() {
    //var form = document.createElement("form");
    //form.setAttribute("method", "POST");
    //form.setAttribute("action", "https://sep.shaparak.ir/payment.aspx");
    //form.setAttribute("target", "_self");
    //var hiddenField = document.createElement("input");
    //hiddenField.setAttribute("name", "Amount");
    //hiddenField.setAttribute("value", 1000);

    //var hiddenField2 = document.createElement("input");
    //hiddenField2.setAttribute("name", "ResNum");
    //hiddenField2.setAttribute("value", 3);

    //var hiddenField3 = document.createElement("input");
    //hiddenField3.setAttribute("name", "MID");
    //hiddenField3.setAttribute("value", "21284935");

    //var hiddenField4 = document.createElement("input");
    //hiddenField4.setAttribute("name", "RedirectURL");
    //hiddenField4.setAttribute("value", "https://google.com");

    //form.appendChild(hiddenField, hiddenField2, hiddenField3, hiddenField4);
    //document.body.appendChild(form);
    //form.submit();
    //document.body.removeChild(form);

    //$.ajax(
    //    {
    //        url: "https://sep.shaparak.ir/payment.aspx",
    //        data: {
    //            MID: "21284935",
    //            Amount: 10000,
    //            ResNum: "3",
    //            RedirectURL: "https://google.com"
    //        },
    //        type: "POST"
    //    }).done(function (result) {
    //    alert(result);
    //}).fail(function (err) {
    //    alert(err);
    //    });

    
    $.ajax({
        type: "POST",
        url: "https://sep.shaparak.ir/payment.aspx",
        data: {
            MID: "21284935",
            Amount: 10000,
            ResNum: "3",
            RedirectURL: "https://google.com"
        },
        contentType: "application/json; charset=utf-8",
        crossDomain: true,
        dataType: "json",
        success: function (data) {

            alert("success");// write success in " "
        },

        error: function (jqXHR, status) {
            // error handler
            console.log(jqXHR);
            alert('fail' + status);
        }
    });
}


function FinalizeOrder() {
    var gateway = $('#payment-gateway').val();
  
        
    var jsondata = get();
    var t = $('#total').html();
    if (jsondata !== "" && t !== "0") {
        if ($('#chkPolicy').is(':checked')) {
            $('#loadingImg').css('display', 'block');
            $('#btnFinalizeOrder').css('display', 'none');

            var txtFirstName = $("#txtFirstName").val();
            var txtLastName = $("#txtLastName").val();
            var txtCellNumber = $("#txtCellNumber").val();
            var txtEmail = $("#txtEmail").val();
            var txtProvince = $('#ddlProvince option:selected').val();
            var txtCity = $('#ddlCity option:selected').val();
            var txtAddress = $("#txtAddress").val();
            var txtPhone = $("#txtPhone").val();
            var txtPostalCode = $("#txtPostalCode").val();

            if (txtCity === undefined || txtCity === "0") {
                $('#errorOrder').css('display', 'block');
                $('#errorOrder').html('استان و شهر خود را انتخاب نمایید');
                $('#successOrder').css('display', 'none');
                $('#loadingImg').css('display', 'none');
                $('#btnFinalizeOrder').css('display', 'block');
            }

            else if (txtPostalCode.length > 10) {
                $('#errorOrder').css('display', 'block');
                $('#errorOrder').html('کد پستی وارد شده صحیح نمی باشد');
                $('#successOrder').css('display', 'none');
                $('#loadingImg').css('display', 'none');
                $('#btnFinalizeOrder').css('display', 'block');
            }

            else if (txtFirstName !== "" && txtLastName !== "" && txtAddress !== "" && txtCellNumber !== "" &&
                txtEmail !== "" && txtCity !== "" && txtProvince !== "" && txtCity !== "undefined") {
                $.ajax(
                    {
                        url: "/Orders/Finalize",
                        data: {
                            jsonvar: jsondata,
                            firstName: txtFirstName,
                            lastName: txtLastName,
                            cellNumber: txtCellNumber,
                            email: txtEmail,
                            province: txtProvince,
                            city: txtCity,
                            address: txtAddress,
                            phone: txtPhone,
                            postalCode: txtPostalCode,
                            bank:gateway
                        },
                        type: "GET"
                    }).done(function (result) {
                        if (result.includes('true')) {
                            $("#myModal").modal();
                            
                                refffid = GetRefIdFromResult(result);
                           

                            //var refId = GetRefIdFromResult(result);
                            //postRefId(refId);
                            //$('#successOrder').css('display', 'block');
                            //$('#errorOrder').css('display', 'none');
                            //localStorage.clear();
                            //setBasketCount();

                        } else if (result === 'invalidCellNumber') {

                            $('#errorOrder').css('display', 'block');
                            $('#errorOrder').html('شماره موبایل وارد شده صحیح نمی باشد');
                            $('#successOrder').css('display', 'none');

                        } else if (result === 'invalidEmail') {

                            $('#errorOrder').css('display', 'block');
                            $('#errorOrder').html('ایمیل وارد شده صحیح نمی باشد');
                            $('#successOrder').css('display', 'none');

                        } else if (result.includes('invalidQty')) {
                            var message = result.split('|')[1];
                            $('#errorOrder').css('display', 'block');
                            $('#errorOrder').html(message);
                            $('#successOrder').css('display', 'none');

                        }
                        else if (result.includes('bankerror')) {
                            var omessage = result.split('|')[1];
                            $('#errorOrder').css('display', 'block');
                            $('#errorOrder').html(omessage);
                            $('#successOrder').css('display', 'none');
                         
                        }
                        else {
                            console.log(result);
                            $('#errorOrder').css('display', 'block');
                            $('#errorOrder')
                                .html('متاسفانه هنگام پردازش درخواست شما خطایی رخ داده است. لطفا مجددا تلاش کنید');
                            $('#successOrder').css('display', 'none');

                        }
                        $('#loadingImg').css('display', 'none');
                        $('#btnFinalizeOrder').css('display', 'block');
                    });
            }

            else {
                $('#errorOrder').css('display', 'block');
                $('#errorOrder').html('فیلد های ستاره دار اجباری می باشند');
                $('#successOrder').css('display', 'none');
                $('#loadingImg').css('display', 'none');
                $('#btnFinalizeOrder').css('display', 'block');
            }
        }
        else {
            $('#errorOrder').css('display', 'block');
            $('#errorOrder').html('قوانین و مقررات را تایید بفرمایید');
            $('#successOrder').css('display', 'none');
            $('#loadingImg').css('display', 'none');
            $('#btnFinalizeOrder').css('display', 'block');
        }
    }
    else {
        $('#errorOrder').css('display', 'block');
        $('#errorOrder').html('سبد خرید شما خالی است.');
        $('#successOrder').css('display', 'none');

    }
}

function redirectToBank() {
   // alert(refffid);
    var gateway = $('#payment-gateway').val();
    if (gateway === 'mellat') {
        postRefId(refffid);
    } else {
        window.location.href = "/saman?refId=" + refffid;
    }
    
    $('#successOrder').css('display', 'block');
    $('#errorOrder').css('display', 'none');
    localStorage.clear();
    setBasketCount();
}
$('#ddlProvince').on('change',
    function () {
        var SelectedValue = $(this).val(); // < added test values
        if (SelectedValue !== "") {
            var procemessage = "<option value='0'> صبرکنید...</option>";
            $("#ddlCity").html(procemessage).show();
            $.ajax(
                {
                    url: "/Orders/FillCities",
                    data: { id: SelectedValue },
                    cache: false,
                    type: "POST",
                    success: function (data) {
                        var markup = "<option value='0'>انتخاب شهر</option>";
                        for (var x = 0; x < data.length; x++) {
                            markup += "<option value=" + data[x].Value + ">" + data[x].Text + "</option>";
                        }
                        $("#ddlCity").html(markup).show();
                    },
                    error: function (reponse) {
                        alert("error : " + reponse);
                    }
                });
        }
    });

function changeColor(color, id, proId) {
    var ddl = $("#ddlColor");
    ddl[0].innerText = color;
    ddl[0].title = id;



    $.ajax(
        {
            url: "/Products/ChangePriceByColor",
            data: {
                id: proId,
                colorId: id
            },
            cache: false,
            type: "POST",
            success: function (data) {
                if (!data.includes("error")) {
                    if (data !== "") {
                        $('.product-price ins').html(data);
                    }
                }
            },
            error: function (reponse) {
                alert("error : " + reponse);
            }
        });
}






