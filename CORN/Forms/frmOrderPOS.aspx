<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmOrderPOS.aspx.cs" Inherits="Forms_frmOrderPOS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>CORN :: Point Of Sales</title>
    <link rel="shortcut icon" href="../images/favicon.png" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Granite/Popup.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Granite/GridSorter.css" rel="stylesheet" type="text/css" />
    <link href="../css/POSstyle.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" language="JavaScript">      
        function SearchProduct() {
            if (document.getElementById('<%=txtskuCode.ClientID%>').value == '') {
                document.getElementById('<%=numtxtTotalExtraDiscnt.ClientID%>').focus();
            }
            else {
                var obj = jQuery.parseJSON($("#<%=hfProduct.ClientID %>").val());
               var skuDetail = document.getElementById("<%=txtskuCode.ClientID %>").value;
                var SCode = skuDetail.substring(0, skuDetail.length - 1);
                $('#<%=lblfound.ClientID%>').text('');
                for (var i = 0; i < obj.length; i++) {
                    var item = obj[i];
                    if (item.bar_code == SCode) {
                        document.getElementById("<%= txtskuID.ClientID %>").value = item.SKU_ID;
                        document.getElementById("<%= txtskuName.ClientID %>").value = item.SKU_NAME;
                        document.getElementById("<%= txtcolor.ClientID %>").value = item.COLOR;
                        document.getElementById("<%= txtsize.ClientID %>").value = item.PACKSIZE;
                        document.getElementById("<%= txtUnitRate.ClientID %>").value = item.TRADE_PRICE;
                        document.getElementById("<%= txtskuCode.ClientID %>").focus();
                        return true;
                    }
                    else if (item.bar_code == skuDetail) {

                        document.getElementById("<%= txtskuID.ClientID %>").value = item.SKU_ID;
                        document.getElementById("<%= txtskuName.ClientID %>").value = item.SKU_NAME;
                        document.getElementById("<%= txtcolor.ClientID %>").value = item.COLOR;
                        document.getElementById("<%= txtsize.ClientID %>").value = item.PACKSIZE;
                        document.getElementById("<%= txtUnitRate.ClientID %>").value = item.TRADE_PRICE;
                        document.getElementById("<%= txtskuCode.ClientID %>").focus();
                        return true;
                    }
                }
                $('#<%=lblfound.ClientID%>').text('Product not found.');
                document.getElementById("<%= txtskuCode.ClientID %>").focus();
                return false;
            }
        }

        function storeTblValues() {
            var tableData = new Array();
            $('#<%=dataTable.ClientID%> tr').each(function (row, tr) {
                tableData[row] = {
                    "SKU_Code": $(tr).find('td:eq(0)').text()
                    , "SKU_Name": $(tr).find('td:eq(1)').text()
                    , "QUANTITY_UNIT": $(tr).find('td:eq(4)').text()
                    , "STANDARD_DISCOUNT": $(tr).find('td:eq(5)').text()
                    , "UNIT_PRICE": $(tr).find('td:eq(6)').text()
                    , "NET_AMOUNT": $(tr).find('td:eq(7)').text()
                    , "SKU_ID": $(tr).find('td:eq(8)').text()
                    , "COLOR": $(tr).find('td:eq(2)').text()
                    , "PACKSIZE": $(tr).find('td:eq(3)').text()
                    , "AMOUNT": $(tr).find('td:eq(9)').text()
                    , "CHECK_DELETE": 0
                    , "GST_RATE": 0
                    , "GST_AMOUNT": 0
                    , "TST_AMOUNT": 0
                    , "STANDARD_DISCOUNT_TEMP": 0
                    , "STANDARD_DISCOUNT_PER": '0.0000'
                    , "BATCH_NO": 0
                }
            });
            return tableData;
        }
        function addRow() {
            if (SearchProduct()) {
                var flag = 0;
                var skuCode = $('#<%=txtskuCode.ClientID%>').val();
                var e = document.getElementById('<%=DrpDiscount.ClientID%>');
                var discType = e.options[e.selectedIndex].value;
                var table = document.getElementById('<%=dataTable.ClientID%>');
                var rowCount = table.rows.length;
                var b;
                var net;
                var disc;
                var i;
                var tableData;
                var row;
                var perDisc;
                var cell1;
                var cell2;
                var cell3;
                var cell4;
                var cell5;
                var cell6;
                var cell7;
                var cell8;
                var cell9;
                var cell10;
                var cell11;
                var mode = document.getElementById("<%=btnToggleMode.ClientID%>").value;

                if (table.rows.length == "0") {

                    row = table.insertRow(rowCount);
                    cell1 = row.insertCell(0);
                    cell1.innerHTML = document.getElementById('<%=txtskuCode.ClientID%>').value;
                    cell1.style.width = "240px";
                    cell2 = row.insertCell(1);
                    cell2.innerHTML = document.getElementById('<%=txtskuName.ClientID%>').value;
                    cell2.style.width = "280px";

                    cell3 = row.insertCell(2);
                    cell3.innerHTML = document.getElementById('<%=txtcolor.ClientID%>').value;
                    cell3.style.display = 'none';

                    cell4 = row.insertCell(3);
                    cell4.innerHTML = document.getElementById('<%=txtsize.ClientID%>').value;
                    cell4.style.width = "92px";

                    cell5 = row.insertCell(4);
                    cell5.innerHTML = document.getElementById('<%=txtQuantity.ClientID%>').value;
                    cell5.style.width = "85px";

                    cell6 = row.insertCell(5);
                    if (discType == 0) {

                        perDisc = (document.getElementById('<%=txtUnitRate.ClientID%>').value * document.getElementById('<%=txtQuantity.ClientID%>').value) * (document.getElementById('<%=txtDiscount.ClientID%>').value / 100);
                        cell6.innerHTML = perDisc;

                    } else {

                        if (mode == 'SALE MODE') {

                            perDisc = document.getElementById('<%=txtDiscount.ClientID%>').value;
                            cell6.innerHTML = perDisc;
                        } else {

                            perDisc = document.getElementById('<%=txtDiscount.ClientID%>').value * -1;
                            cell6.innerHTML = perDisc;
                        }


                    }
                    cell6.style.width = "89px";

                    cell7 = row.insertCell(6);
                    cell7.innerHTML = document.getElementById('<%=txtUnitRate.ClientID%>').value;
                    cell7.style.width = "85px";

                    cell8 = row.insertCell(7);
                    cell8.innerHTML = (document.getElementById('<%=txtUnitRate.ClientID%>').value * document.getElementById('<%=txtQuantity.ClientID%>').value) - perDisc;
                    cell8.style.width = "70px";
                    //hidden cell's
                    cell9 = row.insertCell(8);
                    cell9.innerHTML = document.getElementById('<%=txtskuID.ClientID%>').value;
                    cell9.style.display = 'none';
                    cell10 = row.insertCell(9);
                    cell10.innerHTML = (document.getElementById('<%=txtUnitRate.ClientID%>').value * document.getElementById('<%=txtQuantity.ClientID%>').value);
                    cell10.style.display = 'none';

                    cell11 = row.insertCell(10);
                    cell11.innerHTML = '<input type="button" value = "X" style="color:white;background-color:red;cursor:pointer" onClick="Javacsript:deleteRow(this)">';

                    //Calculation for loop getting cell values
                    b = 0;
                    net = 0;
                    disc = 0;
                    for (i = 0; i < table.rows.length; i++) {

                        b = table.rows[i].cells[9].innerHTML;
                       
                        disc = parseFloat(table.rows[i].cells[5].innerHTML).toFixed(2);
                        net = (parseFloat(net) + parseFloat(b)).toFixed(2);

                    }
                    document.getElementById('<%=txtGrossAmount.ClientID%>').value = net;
                    document.getElementById('<%=numTxtTotlAmnt.ClientID%>').value = (parseFloat(net) - parseFloat(disc)).toFixed(2);
                    //set data in json and store in hidden field through storeTblValues()
                    tableData = storeTblValues();
                    tableData = $.toJSON(tableData);
                    document.getElementById('<%=tab.ClientID%>').value = tableData;
                } else { //Work on duplication
                    var duplicrw;

                    $('#<%=dataTable.ClientID%>').find('tr').each(function () {

                        var td1 = $(this).find("td:eq(0)").text();
                        if (skuCode == td1) {
                            duplicrw = $(this);
                            flag = 1;

                        }
                    });

                    if (flag == "1") { 
                        var perDisc2;
                        var unitrate = duplicrw.find("td:eq(6)").text();
                        var discount = duplicrw.find("td:eq(5)").text();
                        var qty = duplicrw.find("td:eq(4)").text();
                        if (discType == 0) {

                            perDisc2 = (document.getElementById('<%=txtUnitRate.ClientID%>').value * document.getElementById('<%=txtQuantity.ClientID%>').value) * (document.getElementById('<%=txtDiscount.ClientID%>').value / 100);
                        } else {


                            perDisc2 = document.getElementById('<%=txtDiscount.ClientID%>').value;

                        }
                        duplicrw.find("td:eq(4)").text(parseInt(qty, 10) + parseInt(document.getElementById('<%=txtQuantity.ClientID%>').value, 10));
                        duplicrw.find("td:eq(5)").text((parseFloat(perDisc2) + parseFloat(discount)).toFixed(2));

                        duplicrw.find("td:eq(7)").text((parseInt(qty, 10) + parseInt(document.getElementById('<%=txtQuantity.ClientID%>').value, 10)) * parseInt(unitrate, 10) - ((parseFloat(perDisc2) + parseFloat(discount)).toFixed(2)));
                        duplicrw.find("td:eq(9)").text((parseInt(qty, 10) + parseInt(document.getElementById('<%=txtQuantity.ClientID%>').value, 10)) * parseInt(unitrate, 10));

                        
                        b = 0;
                        net = 0;
                        var disc3 = 0;

                        for (i = 0; i < table.rows.length; i++) {

                            b = table.rows[i].cells[9].innerHTML;
                            disc3 = (parseFloat(disc3) + parseFloat(table.rows[i].cells[5].innerHTML)).toFixed(2);
                            net = (parseFloat(net) + parseFloat(b)).toFixed(2);

                        }

                        document.getElementById('<%=txtGrossAmount.ClientID%>').value = net;
                        document.getElementById('<%=numTxtTotlAmnt.ClientID%>').value = (parseFloat(net) - parseFloat(disc3)).toFixed(2);
                        tableData = storeTblValues();
                        tableData = $.toJSON(tableData);

                        document.getElementById('<%=tab.ClientID%>').value = tableData;

                        ClearControls();

                    } else {
                        row = table.insertRow(rowCount);
                        cell1 = row.insertCell(0);
                        cell1.innerHTML = document.getElementById('<%=txtskuCode.ClientID%>').value;
                        cell1.style.width = "240px";

                        cell2 = row.insertCell(1);
                        cell2.innerHTML = document.getElementById('<%=txtskuName.ClientID%>').value;
                        cell2.style.width = "280px";

                        cell3 = row.insertCell(2);
                        cell3.innerHTML = document.getElementById('<%=txtcolor.ClientID%>').value;
                        cell3.style.display = 'none';

                        cell4 = row.insertCell(3);
                        cell4.innerHTML = document.getElementById('<%=txtsize.ClientID%>').value;
                        cell4.style.width = "92px";

                        cell5 = row.insertCell(4);
                        cell5.innerHTML = document.getElementById('<%=txtQuantity.ClientID%>').value;
                        cell5.style.width = "85px";

                        cell6 = row.insertCell(5);
                        if (discType == 0) {

                            perDisc = (document.getElementById('<%=txtUnitRate.ClientID%>').value * document.getElementById('<%=txtQuantity.ClientID%>').value) * (document.getElementById('<%=txtDiscount.ClientID%>').value / 100);
                            cell6.innerHTML = perDisc;
                        } else {
                            if (mode == 'SALE MODE') {

                                perDisc = document.getElementById('<%=txtDiscount.ClientID%>').value;
                                cell6.innerHTML = perDisc;
                            } else {

                                perDisc = document.getElementById('<%=txtDiscount.ClientID%>').value * -1;
                                cell6.innerHTML = perDisc;
                            }
                        }
                        cell6.style.width = "89px";

                        cell7 = row.insertCell(6);
                        cell7.innerHTML = document.getElementById('<%=txtUnitRate.ClientID%>').value;
                        cell7.style.width = "85px"; cell8 = row.insertCell(7);

                        cell8.innerHTML = (document.getElementById('<%=txtUnitRate.ClientID%>').value * document.getElementById('<%=txtQuantity.ClientID%>').value) - perDisc;
                        cell8.style.width = "70px";
                        //hidden cell's
                        cell9 = row.insertCell(8);
                        cell9.innerHTML = document.getElementById('<%=txtskuID.ClientID%>').value;
                        cell9.style.display = 'none';
                        cell10 = row.insertCell(9);
                        cell10.innerHTML = (document.getElementById('<%=txtUnitRate.ClientID%>').value * document.getElementById('<%=txtQuantity.ClientID%>').value);
                        cell10.style.display = 'none';
                        cell11 = row.insertCell(10);
                        cell11.innerHTML = '<input type="button" value = "X" style="color:white;background-color:red;cursor:pointer" onClick="Javacsript:deleteRow(this)">';

                        //Calculation for loop getting cell values
                        b = 0;
                        net = 0;
                        var disc2 = 0;

                        for (i = 0; i < table.rows.length; i++) {

                            b = table.rows[i].cells[9].innerHTML;

                            disc2 = (parseFloat(disc2) + parseFloat(table.rows[i].cells[5].innerHTML)).toFixed(2);

                            net = (parseFloat(net) + parseFloat(b)).toFixed(2);

                        }
                        document.getElementById('<%=txtGrossAmount.ClientID%>').value = net;
                        document.getElementById('<%=numTxtTotlAmnt.ClientID%>').value = (parseFloat(net) - parseFloat(disc2)).toFixed(2);
                        //set data in json and store in hidden field through storeTblValues()
                        tableData = storeTblValues();

                        tableData = $.toJSON(tableData);

                        document.getElementById('<%=tab.ClientID%>').value = tableData;
                        
                    }
                }
            }
        }
        function deleteRow(obj) {

            var index = obj.parentNode.parentNode.rowIndex;
            var table = document.getElementById("<%=dataTable.ClientID %>");

            table.deleteRow(index);

            var b = 0;

            var net = 0;
            var disc = 0;

            for (var i = 0; i < table.rows.length; i++) {

                b = table.rows[i].cells[9].innerHTML;

                disc = table.rows[i].cells[5].innerHTML;
                net = (parseFloat(net) + parseFloat(b)).toFixed(2);

            }
            document.getElementById('<%=txtGrossAmount.ClientID%>').value = (parseFloat(net)).toFixed(2);
            document.getElementById('<%=numTxtTotlAmnt.ClientID%>').value = (parseFloat(net) - parseFloat(disc)).toFixed(2);
            Calculate2();
            if (table.rows.length == "0") {
                document.getElementById('<%=txtCashRecieved2.ClientID%>').value = "";
                document.getElementById('<%=txtBalance.ClientID%>').value = "";
                document.getElementById("<%= btnToggleMode.ClientID %>").disabled = false;
            }
            document.getElementById('<%=txtskuCode.ClientID%>').focus();

             var tableData = storeTblValues();
             tableData = $.toJSON(tableData);
             document.getElementById('<%=tab.ClientID%>').value = tableData;
        }

        function ClearControls() {
            document.getElementById('<%=txtskuID.ClientID%>').value = "";
            document.getElementById('<%=txtskuCode.ClientID%>').value = "";
            document.getElementById('<%=txtskuName.ClientID%>').value = "";
            var mode = document.getElementById("<%= btnToggleMode.ClientID %>").value;
            if (mode == 'SALE MODE') {
                document.getElementById('<%=txtQuantity.ClientID%>').value = "1";
            }
            else {
                document.getElementById('<%=txtQuantity.ClientID%>').value = "-1";
            }
            document.getElementById('<%=txtDiscount.ClientID%>').value = "0";
            document.getElementById('<%=txtcolor.ClientID%>').value = "";
            document.getElementById('<%=txtsize.ClientID%>').value = "";
            document.getElementById('<%=txtUnitRate.ClientID%>').value = "";
            document.getElementById("<%=btnToggleMode.ClientID%>").disabled = true;
        }

        function Calculate(e) {

            var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
            if (key == 13) {
                e.preventDefault();
                if (document.getElementById('<%=txtskuCode.ClientID%>').value != "") {
                    addRow();
                        var Units = 0;
                        $('#<%=dataTable.ClientID%>').find('tr').each(function () {
                            Units += parseInt($(this).find("td:eq(4)").text());
                        });
                    document.getElementById("<%= txtTotalQty.ClientID %>").value = Units;
                    ClearControls();
                    document.getElementById('<%=txtskuCode.ClientID%>').focus();
                }
                else {
                    document.getElementById('<%=numtxtTotalExtraDiscnt.ClientID%>').focus();
                }

            }
        }
        function CalculateBalance(e) {
           
            var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
            if (key == 13) {
                e.preventDefault();

                var cashRcd = document.getElementById('<%=txtCashRecieved2.ClientID%>').value;
                if (cashRcd != "") {
                    var netAmount = document.getElementById('<%=numTxtTotlAmnt.ClientID%>').value;
                    var disc = document.getElementById('<%=numtxtTotalExtraDiscnt.ClientID%>').value;
                    if (disc == '') {
                        disc = 0;
                    }
                    var balce = 0;
                    var mode = document.getElementById("<%= btnToggleMode.ClientID %>").value;
                    if (mode == 'SALE MODE') {
                        document.getElementById('<%=txtBalance.ClientID%>').value = (parseFloat(cashRcd) - parseFloat(netAmount)).toFixed(2);
                    }
                    else {
                        if ((cashRcd > 0) && (netAmount < 0)) {
                            balce = (parseFloat(netAmount) + parseFloat(cashRcd)).toFixed(2);
                            document.getElementById('<%=txtBalance.ClientID%>').value = balce;
                        }
                        else if ((cashRcd > 0) && (netAmount > 0)) {
                            balce = (parseFloat(netAmount) - parseFloat(cashRcd)).toFixed(2);
                            document.getElementById('<%=txtBalance.ClientID%>').value = balce;
                        }
                    }
                    document.getElementById('<%=LinkButton2.ClientID%>').focus();
                }
            }
        }
        function Calculate2() {

            var cashRcd = document.getElementById('<%=txtCashRecieved2.ClientID%>').value;
            if (cashRcd != "") {
                var netAmount = document.getElementById('<%=numTxtTotlAmnt.ClientID%>').value;
                var disc = document.getElementById('<%=numtxtTotalExtraDiscnt.ClientID%>').value;
                if (disc == 0) {
                    disc = 0;
                }
                var balce = 0;
                var mode = document.getElementById("<%= btnToggleMode.ClientID %>").value;
                if (mode == 'SALE MODE') {

                    if (cashRcd > 0) {
                        document.getElementById('<%=txtBalance.ClientID%>').value = (parseFloat(cashRcd) - parseFloat(netAmount)).toFixed(2);
                    }
                    else {
                        document.getElementById('<%=txtBalance.ClientID%>').value = parseFloat(netAmount).toFixed(2);
                    }
                }
                else {
                    if ((cashRcd > 0) && (netAmount < 0)) {

                        balce = (parseFloat(netAmount) - parseFloat(disc) + parseFloat(cashRcd)).toFixed(2);
                        document.getElementById('<%=txtBalance.ClientID%>').value = balce;
                    }
                    else if ((cashRcd > 0) && (netAmount > 0)) {

                        balce = (parseFloat(netAmount) - parseFloat(disc) - parseFloat(cashRcd)).toFixed(2);
                        document.getElementById('<%=txtBalance.ClientID%>').value = balce;
                    }
                }
            }
        }


        function ValidateForm() {

            var str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Quantity');
                return false;
            }

            return true;
        }



        function SetFocusTocashRecived(e) {
            var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
            if (key == 13) {
                e.preventDefault();
                setTimeout(function () { document.getElementById("<%= numtxtTotalExtraDiscnt.ClientID %>").focus(); }, 10);
            }
        }

        function SetFocusTocode(e) {
            var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
            if (key == 13) {
                document.getElementById("<%= txtskuCode.ClientID %>").focus();

            }
            if (document.getElementById("<%= btnToggleMode.ClientID %>").value == 'SALE MODE') {
                if (document.getElementById("<%= txtQuantity.ClientID %>").value == '-') {

                    alert('- is not allowed in sale mode!');
                    document.getElementById("<%= txtQuantity.ClientID %>").value = '';
                }

            }
        }

        function FocusToCash(e) {
            var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
            if (key == 13) {
                e.preventDefault();
                setTimeout(function () { document.getElementById("<%= txtCashRecieved2.ClientID %>").focus(); }, 10);

            }
        }

            function FocusToAuthorized(e) {
                var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
                if (key == 13) {
                    e.preventDefault();
                    var gross = document.getElementById("<%=txtGrossAmount.ClientID %>").value;
                    var totaldiscount = document.getElementById("<%=numtxtTotalExtraDiscnt.ClientID %>").value;
                    var mode = document.getElementById("<%=btnToggleMode.ClientID%>").value;
                    if (totaldiscount == '') {
                        totaldiscount = 0;
                    }
                    if (mode == 'SALE MODE') {                        
                        document.getElementById("<%=numTxtTotlAmnt.ClientID %>").value = (parseFloat(gross) - parseFloat(totaldiscount)).toFixed(2);
                    }
                    else {
                        if (parseFloat(totaldiscount) > 0) {
                            totaldiscount = totaldiscount * -1;                            
                        }
                        document.getElementById("<%=numtxtTotalExtraDiscnt.ClientID %>").value = parseFloat(totaldiscount).toFixed(2);
                        document.getElementById("<%=numTxtTotlAmnt.ClientID %>").value = (parseFloat(gross) - parseFloat(totaldiscount)).toFixed(2);
                    }
                    setTimeout(function () { document.getElementById("<%= txtAuthorisedBy.ClientID %>").focus(); }, 10);
                }
            }

       
        function ProductSelected(source, eventArgs) {
            var skuDetail = eventArgs.get_text();
            var num = eventArgs.get_value();

            document.getElementById("<%=txtskuCode.ClientID %>").value = skuDetail.substring(0, skuDetail.indexOf('-'));
        }
        function toggle(t) {

            var mode = document.getElementById("<%=btnToggleMode.ClientID%>").value;

            if (mode == 'SALE MODE') {
                document.getElementById("<%=btnToggleMode.ClientID %>").value = "REFUND MODE";
                document.getElementById("<%=hfToggleMode.ClientID %>").value = "REFUND MODE";
                document.getElementById("<%=txtQuantity.ClientID %>").value = "-1";
                document.getElementById("<%=btnToggleMode.ClientID %>").setAttribute("CssClass", "BtnModereturn");
                document.getElementById("<%= btnCalculate.ClientID %>").style.visibility = 'hidden';                
                document.getElementById("<%=hfIsCalculate.ClientID %>").value = '1';
                document.getElementById("lblCash").innerHTML = 'Cash Return';
                document.getElementById("<%=txtskuCode.ClientID %>").focus();
            } else if (mode == 'REFUND MODE') {
                document.getElementById("<%= btnToggleMode.ClientID %>").value = "SALE MODE";
                document.getElementById("<%=hfToggleMode.ClientID %>").value = "SALE MODE";
                document.getElementById("<%= txtQuantity.ClientID %>").value = "1";
                document.getElementById("<%= btnToggleMode.ClientID %>").setAttribute("CssClass", "BtnModesale");
                document.getElementById("<%= btnCalculate.ClientID %>").style.visibility = '';
                document.getElementById("<%=txtskuCode.ClientID %>").focus();
                document.getElementById("<%=hfIsCalculate.ClientID %>").value = '0';
                document.getElementById("lblCash").innerHTML = 'Cash Received';
                document.getElementById("<%=txtskuCode.ClientID %>").focus();
            }
        }

        function CheckCreditLimit()
        {
           
             var e = document.getElementById("<%= DrpPayMode.ClientID %>");
             var payMode = e.options[e.selectedIndex].value;
            

             if (payMode == "218") {

                 var balanceCeiling = document.getElementById("lblAllowLimit").innerHTML;
                 var NetAmount = document.getElementById("<%= numTxtTotlAmnt.ClientID %>").value;

                 if (parseFloat(balanceCeiling) < parseFloat(NetAmount)) {

                     alert('Please Check Customer Balance Ceiling');

                     return false;
                 }
             }
             else if (payMode == "214") {
                 var mode = document.getElementById("<%=btnToggleMode.ClientID%>").value;
                 if (mode == 'SALE MODE') {
                     var NetAmount = document.getElementById("<%= txtBalance.ClientID %>").value;

                     if (NetAmount == null || NetAmount.length == 0) {
                         alert('Please enter Payment');
                         document.getElementById("<%= txtCashRecieved2.ClientID %>").focus();
                         return false;
                     }
                     else if (parseFloat(NetAmount) < 0) {

                         alert('Receive amount not match with Net Amount');
                         document.getElementById("<%= txtCashRecieved2.ClientID %>").focus();
                         return false;
                     }
                 }
             }
           return true;
        }
        
        <%-- //////////////////Print Invoice region\\\\\\\\\\\\\\\\\\\\\--%>
            function PrintSaleInvoice() {
                if (document.getElementById("<%=hfIsCalculate.ClientID %>").value == '1' || document.getElementById("<%=hfIsManual.ClientID %>").value == '1') {
                    if (CheckCreditLimit()) {
                        var mode = document.getElementById("<%=btnToggleMode.ClientID%>").value;
                        if (mode == 'REFUND MODE') {
                            $("#invoiceMode").text("Sale Return");
                        }
                        else {
                            $("#invoiceMode").text("Sale Invoice");
                        }
                        var payMode = document.getElementById("<%= DrpPayMode.ClientID %>");
                        $("#payMode").text(payMode.options[payMode.selectedIndex].text);
                        var saleMan = document.getElementById("<%= ddsalesForce.ClientID %>");
                        $("#saleMan").text(saleMan.options[saleMan.selectedIndex].text);
                        var CustomerName = document.getElementById("<%= ddlCustomer.ClientID %>");
                        $("#lblCustomerName").text(CustomerName.options[CustomerName.selectedIndex].text);
                        var Units = 0;
                        $('#<%=dataTable.ClientID%>').find('tr').each(function () {
                            Units += parseInt($(this).find("td:eq(4)").text());
                        });
                        $("#Units").text(Units);
                        var orderedProducts = document.getElementById('<%=tab.ClientID%>').value;
                        orderedProducts = eval(orderedProducts);
                        $('#invoiceDetailBody').empty(); // clear all skus  from invoice                        
                        for (var i = 0, len = orderedProducts.length; i < len; i++) {
                            var discount = 0;
                            var netamount = 0;
                            var discounttotal = document.getElementById("<%= numtxtTotalExtraDiscnt.ClientID %>").value;
                            if (discounttotal == '') {
                                discounttotal = 0;
                            }
                            if (mode == 'SALE MODE') {
                                if (document.getElementById("<%=hfIsManual.ClientID %>").value == '1') {
                                    discount = parseFloat(orderedProducts[i].NET_AMOUNT) / parseFloat(document.getElementById('<%=txtGrossAmount.ClientID%>').value) * parseFloat(discounttotal);
                                    netamount = parseFloat(orderedProducts[i].NET_AMOUNT) - parseFloat(discount);
                                }
                                else {
                                    var result = jQuery.parseJSON(document.getElementById('<%=hfTradeOffer.ClientID%>').value);
                                    for (var ii = 0; ii < result.length; ii++) {
                                        var item = result[ii];
                                        if (item.SKU_ID == orderedProducts[i].SKU_ID) {
                                            orderedProducts[i].STANDARD_DISCOUNT = parseFloat(item.STANDARD_DISCOUNT).toFixed(2);
                                            discount = parseFloat(item.STANDARD_DISCOUNT).toFixed(2);
                                            netamount = parseFloat(orderedProducts[i].NET_AMOUNT) - parseFloat(discount);
                                        }
                                    }
                                }
                            }
                            else {
                                if (discounttotal > 0) {
                                    discounttotal = discounttotal * -1;
                                }
                                discount = parseFloat(orderedProducts[i].NET_AMOUNT) / parseFloat(document.getElementById('<%=txtGrossAmount.ClientID%>').value) * parseFloat(discounttotal);                                
                                netamount = parseFloat(orderedProducts[i].NET_AMOUNT) - parseFloat(discount);                                
                            }
                            var row = $('<tr><td>' + orderedProducts[i].SKU_Code + '<br />' + orderedProducts[i].SKU_Name + '</td><td class="text-right">' + orderedProducts[i].QUANTITY_UNIT + '</td><td class="text-right">' + parseFloat(orderedProducts[i].UNIT_PRICE).toFixed(1) + '</td><td class="text-right">' + parseFloat(discount).toFixed(1) + '</td><td class="text-right">' + parseFloat(netamount).toFixed(1) + '</td></tr>');
                            $('#invoiceDetailBody').append(row);                            
                        }                        
                        var gross = document.getElementById('<%=txtGrossAmount.ClientID%>').value;
                        var discount = document.getElementById('<%=numtxtTotalExtraDiscnt.ClientID%>').value;
                        var amountDue = document.getElementById('<%=numTxtTotlAmnt.ClientID%>').value;
                        var paid = document.getElementById('<%=txtCashRecieved2.ClientID%>').value;
                        var balance = document.getElementById('<%=txtBalance.ClientID%>').value;
                        $("#TotalValue").text(gross);
                        $("#DiscountTotal").text(parseFloat(discount).toFixed(2));
                        $("#GrandTotal").text(parseFloat(amountDue).toFixed(2));
                        $("#Paid").text(parseFloat(paid).toFixed(2));
                        $("#Balance").text(parseFloat(balance).toFixed(2));
                        if ($("#invoiceDetailBody tr").length > 0) {
                            $.print("#dvSaleInvoice");
                        }
                        document.getElementById("<%=hfIsCalculate.ClientID %>").value = '0';
                        SaveInvoiceInDataBase();
                    }
                }
                else {
                    alert('Please first press calculate button.');
                    document.getElementById("<%=btnCalculate.ClientID %>").focus();
                }
            }

            function CalculateDiscount() {
                $.ajax
                     (
                         {
                             type: "POST", //HTTP method
                             url: "frmOrderPOS.aspx/GetDiscount", //page/method name
                             contentType: "application/json; charset=utf-8",
                             data: JSON.stringify({ orderedProducts: document.getElementById('<%=tab.ClientID%>').value}),
                             dataType: "json",
                             success: SetDiscount
                         }
                     );
            }

            function SetDiscount(Discount) {
                document.getElementById('<%=hfTradeOffer.ClientID%>').value = Discount.d;                
                var result = jQuery.parseJSON(document.getElementById('<%=hfTradeOffer.ClientID%>').value);
                var totalDiscount = 0;
                for (var i = 0; i < result.length; i++) {
                    var item = result[i];
                    totalDiscount += parseInt(item.STANDARD_DISCOUNT);
                }
                document.getElementById('<%=numtxtTotalExtraDiscnt.ClientID%>').value = parseFloat(totalDiscount).toFixed(2);
                var gross = document.getElementById("<%=txtGrossAmount.ClientID %>").value;
                document.getElementById("<%=numTxtTotlAmnt.ClientID %>").value = (parseFloat(gross) - parseFloat(totalDiscount)).toFixed(2);
                document.getElementById("<%=hfIsCalculate.ClientID %>").value = '1';
                document.getElementById("<%=txtCashRecieved2.ClientID %>").focus();
            }

        function GetMaxID() {
            $.ajax
                 (
                     {
                         type: "POST", //HTTP method
                         url: "frmOrderPOS.aspx/GetMaxID", //page/method name
                         contentType: "application/json; charset=utf-8",
                         dataType: "json",                         
                         success: SetMaxID
                     }
                 );
        }

        function SetMaxID(MaxID) {
            MaxID = JSON.stringify(MaxID);
            var result = jQuery.parseJSON(MaxID.replace(/&quot;/g, '"'));
            MaxID = eval(result.d);
            $("#CustomerType").text(MaxID);
    }

        function SaveInvoiceInDataBase() {
           
        $.ajax
            (
                {
                    type: "POST", //HTTP method
                    url: "frmOrderPOS.aspx/InsertInvoice", //page/method name
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ orderedProducts: document.getElementById('<%=tab.ClientID%>').value, amountDue: document.getElementById('<%=txtGrossAmount.ClientID%>').value, author: document.getElementById('<%=txtAuthorisedBy.ClientID%>').value, discount: $('#<%=numtxtTotalExtraDiscnt.ClientID%>').val(), netAmount: $('#<%=numTxtTotlAmnt.ClientID%>').val(), paidIn: $('#<%=txtCashRecieved2.ClientID%>').val(), payType: document.getElementById("<%= DrpPayMode.ClientID %>").value, Gst: document.getElementById("<%=numTxtTotalGST.ClientID %>").value, manualId: document.getElementById("<%= hfToggleMode.ClientID %>").value, customerId: document.getElementById("<%= ddlCustomer.ClientID %>").value, saleForce: document.getElementById("<%= ddsalesForce.ClientID %>").value, strcurrentWorkDate: document.getElementById("<%= hfCurrentWorkDate.ClientID %>").value, strUsserID: document.getElementById("<%= hfUserID.ClientID %>").value, strDistributorID: document.getElementById("<%= hfDistributorID.ClientID %>").value}),
                    success: invoiceSaved,
                    error: invoiceNotSaved
                }
            );
        }   

            function invoiceSaved(flag) {
                flag = JSON.stringify(flag);
                var result = jQuery.parseJSON(flag.replace(/&quot;/g, '"'));
                flag = eval(result.d);
                if (flag > 0) {
                    document.getElementById('<%=tab.ClientID%>').value = "";
                    $('#<%=dataTable.ClientID%> tr').empty();
                    Clear();
                    __doPostBack('UpdatePanel1', '');
                    UpdateCreditLimit();
                    document.getElementById("<%=hfIsCalculate.ClientID %>").value = '0';
                    GetMaxID();
                }
                else
                {
                    alert('Invoice not saved. Some error occurred');
                }
            }

      function invoiceNotSaved() {
        alert('Some error occurred');
    }

     // Update Limit After Insertion

    function UpdateCreditLimit(){ 
    var e = document.getElementById("<%= DrpPayMode.ClientID %>");
    var payMode = e.options[e.selectedIndex].value;

       if(payMode=="218"){
                document.getElementById('<%= btnUpdateLimit.ClientID %>').click();
        }
    }

            function Clear() {
                $('#<%=txtGrossAmount.ClientID%>').val('');
                $('#<%=txtAuthorisedBy.ClientID%>').val('');
                $('#<%=numtxtTotalExtraDiscnt.ClientID%>').val('0');
                $('#<%=numTxtTotlAmnt.ClientID%>').val('');
                $('#<%=txtCashRecieved2.ClientID%>').val('');
                $('#<%=txtBalance.ClientID%>').val('');
                $('#<%=numTxtTotalGST.ClientID %>').val('');
                $('#<%=txtTotalQty.ClientID %>').val('');
                document.getElementById("<%= txtQuantity.ClientID %>").value = "1";
            }
        function PaymentMode() {

            var e = document.getElementById("<%= DrpPayMode.ClientID %>");
            var payMode = e.options[e.selectedIndex].value;
            if (payMode == "215" || payMode == "218") {
                document.getElementById("<%= txtCashRecieved2.ClientID %>").value = "";                
                document.getElementById("<%= txtCashRecieved2.ClientID %>").readOnly = true;
            } else {
                document.getElementById("<%= txtCashRecieved2.ClientID %>").readOnly = false;
            }
            document.getElementById("<%=txtskuCode.ClientID %>").focus();
        }
     
    </script>
    <style type="text/css">
        input[type=text], textarea
        {
            border: 1px solid #ccc;
        }
        input[type=text]:focus, textarea:focus
        {
            background-color: #CEE3F6;
            border: 1px solid #ccc;
        }
    </style>
</head>
<body>
    <form id="form1" method="POST" runat="server">
    <asp:ScriptManager runat="server" ID="smPOs" AsyncPostBackTimeout="30000" EnablePartialRendering="true">
    </asp:ScriptManager>
    <script src="../AjaxLibrary/jquery-2.0.2.min.js" type="text/javascript"></script>
    <script src="../AjaxLibrary/jquery.json-2.4.min.js" type="text/javascript"></script>
    <script src="../AjaxLibrary/jQuery.print.js" type="text/javascript"></script>
    <div class="header">
        <div class="main">
            <div class="header-t">
                <div class="logo">
                </div>
                <div class="menu-top">
                    <ul>
                        <li>
                            <asp:LinkButton runat="server"  ID="btnNewCustomer" Enabled="true" OnClick="btnNewCustomer_Click"><img src="../images/icon-user.png" alt=""/><span>Customers<br /><b></b></span></asp:LinkButton>
                        </li>
                    </ul>
                    <div class="shadow-t-menu">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="mainPOS">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
             <asp:LinkButton ID="btnUpdateLimit" runat="server"  OnClick="btnUpdateLimit_Click"  />

                <asp:HiddenField ID="hfCompanyName" runat="server" />
                <asp:HiddenField ID="hfLocationName" runat="server" />
                <asp:HiddenField ID="hfContactNo" runat="server" />
                <asp:HiddenField ID="hfMaxId" runat="server" />
                <asp:HiddenField ID="hfProduct" runat="server" Value="0" />
                <asp:HiddenField ID="txtskuID" runat="server" />
                <asp:HiddenField ID="tab" runat="server" />
                <asp:HiddenField ID="hfCurrentWorkDate" runat="server" />
                <asp:HiddenField ID="hfUserID" runat="server" />
                <asp:HiddenField ID="hfDistributorID" runat="server" />
                <asp:HiddenField ID="hfIsCalculate" runat="server" Value="1"/>
                <asp:HiddenField ID="hfTradeOffer" runat="server" />
                <asp:HiddenField ID="hfIsManual" runat="server" Value="1"/>
                <div style="z-index: 101; left: 612px; width: 100px; position: absolute; top: 369px;
                    height: 100px">
                    &nbsp;<asp:Panel ID="Panel21" runat="server">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                    Width="31px" />
                                Wait Update
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </asp:Panel>
                </div>
                <div class="header-form">
                    <span style="width: 360px;"><strong>
                        <label style="font-size: 17px; font-weight: bold;">
                            Customer</label>
                    </strong>
                        <asp:DropDownList ID="ddlCustomer" runat="server" Height="30px" Width="265px" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                        </asp:DropDownList>
                    </span><span style="width: 271px"><strong>
                        <label style="font-size: 17px; font-weight: bold;">
                            Payment Mode</label>
                    </strong>
                        <asp:DropDownList ID="DrpPayMode" runat="server" onchange="PaymentMode()" Width="140px"
                            Height="30px">
                            <asp:ListItem Selected="True" Value="214">Cash</asp:ListItem>
                           <%-- <asp:ListItem Value="217">Cash & Credit Card</asp:ListItem>--%>
                            <asp:ListItem Value="215">Credit Card</asp:ListItem>
                            <asp:ListItem Value="218">Credit</asp:ListItem>
                        </asp:DropDownList>
                    </span><span style="width: 173px"><strong>
                        <label style="font-size: 17px; font-weight: bold;">
                            Discount</label>
                        <select id="DrpDiscount" runat="server" style="height: 30px; width: 90px">
                            <option value="0">% age</option>
                            <option value="1">Value</option>
                        </select>
                    </strong></span><span style="width: 475px;"><strong>
                        <asp:Label runat="server" ID="lblsaleforce" Text="Sales Person" Width="114px" Font-Bold="true"
                            Font-Size="17px"></asp:Label></strong>
                        <asp:DropDownList ID="ddsalesForce" runat="server" Width="140px" CssClass="DropList"
                            Height="30px">
                        </asp:DropDownList>
                        <input type="button" id="btnToggleMode" runat="server" value="SALE MODE" class="BtnModesale"
                            onclick="toggle(this);" />
                        <asp:HiddenField runat="server" ID="hfToggleMode" Value="SALE MODE" />
                    </span>
                    <div style="left: 530px; position: absolute; top: 50px;">
                        <label style="font-size: 17px; font-weight: bold;">Total Quantity</label>
                        <asp:TextBox ID="txtTotalQty" runat="server" class="u-prize-input2" Enabled="False"></asp:TextBox>
                    </div>
                </div>
                <div class="menu2">
                    <div class="main">
                        <ul>
                            <li class="sku-c">Item Code</li>
                            <li class="sperator"></li>
                            <li class="sku-name">Item Name</li>
                            <li class="sperator"></li>
                            <li class="size">Size</li>
                            <li class="sperator"></li>
                            <li class="qty">Quantity</li>
                            <li class="sperator"></li>
                            <li class="discount">Discount</li>
                            <li class="sperator"></li>
                            <li class="u-prize">Unit Price</li>
                            <li class="sperator"></li>
                            <li class="u-prize">Amount</li>
                            <li class="sperator"></li>
                            <li><span style="font-size: large; color: white; margin-left: 20px;">
                                <asp:Label Width="180px" Height="25px" Text="" runat="server" ID="lblLoacation"></asp:Label></span></li>
                        </ul>
                    </div>
                </div>
                <div class="main">
                    <div class="r-pannel">
                        <span>
                            <asp:TextBox ID="txtskuCode" runat="server" class="sku-c-input" onkeypress="Calculate(event);"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoComplete" runat="server" TargetControlID="txtskuCode"
                                ServicePath="wsProductList.asmx" MinimumPrefixLength="4" CompletionInterval="500"
                                UseContextKey="true" BehaviorID="AutoCompleteBehavior" CompletionSetCount="10"
                                CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                EnableCaching="true" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                OnClientItemSelected="ProductSelected" FirstRowSelected="true" ServiceMethod="GetPosProducts">
                            </cc1:AutoCompleteExtender>
                        </span><span>
                            <asp:TextBox ID="txtskuName" runat="server" class="sku-name-input" Enabled="False"
                                Font-Bold="True"></asp:TextBox>
                            <asp:TextBox ID="txtcolor" runat="server" class="color-input" Enabled="False"></asp:TextBox>
                        </span><span>
                            <asp:TextBox ID="txtsize" runat="server" class="size-input" Enabled="False"></asp:TextBox>
                        </span><span>
                            <asp:TextBox ID="txtQuantity" runat="server" onkeypress="SetFocusTocode(event)" class="qty-input"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom"
                                ValidChars="-0123456789." TargetControlID="txtQuantity" />
                        </span><span>
                            <asp:TextBox ID="txtDiscount" runat="server" class="discount-input"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom"
                                ValidChars="0123456789." TargetControlID="txtDiscount" />
                        </span><span>
                            <asp:TextBox ID="txtUnitRate" runat="server" class="u-prize-input" Enabled="False"></asp:TextBox>
                        </span><span>
                            <asp:TextBox ID="txtTotalPrice" runat="server" class="u-prize-input" Enabled="False"></asp:TextBox>
                        </span>
                        <div class="clr">
                        </div>
                        <div class="grid">
                            <asp:Panel ID="Panel2" runat="server" Height="305px" ScrollBars="Vertical" Width="99.3%"
                                BorderColor="Silver" BorderStyle="Groove" BorderWidth="3px">
                                <asp:Label ID="lblfound" ForeColor="Chocolate" runat="server"></asp:Label>
                                <asp:Table ID="dataTable" runat="server" CssClass="table table-striped table-bordered table-hover table-condensed">
                                </asp:Table>
                                <asp:GridView ID="GrdPurchase" runat="server" ForeColor="SteelBlue" RowStyle-Height="30px"
                                    Visible="false" BackColor="White" HorizontalAlign="Center" AutoGenerateColumns="False"
                                    BorderColor="White" ShowHeader="False" Width="100%" EnableModelValidation="True">
                                    <Columns>
                                        <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                            <FooterStyle VerticalAlign="Middle" />
                                            <HeaderStyle CssClass="HidePanel" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SKU_CODE" HeaderText="Item Code">
                                            <FooterStyle VerticalAlign="Middle" />
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" BorderColor="Silver" BorderWidth="2px"
                                                BorderStyle="Solid" Font-Bold="true" Font-Size="16px" Width="128px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SKU_NAME" HeaderText="Item Name">
                                            <FooterStyle VerticalAlign="Middle" />
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                VerticalAlign="Middle" Font-Bold="true" Font-Size="16px" Width="270px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="COLOR" HeaderText="COLOR">
                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" Font-Bold="true"
                                                VerticalAlign="Middle" HorizontalAlign="Left" Font-Size="16px" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PACKSIZE" HeaderText="PACKSIZE">
                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                VerticalAlign="Middle" Font-Bold="true" Font-Size="16px" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="QUANTITY_UNIT" HeaderText="QUANTITY_UNIT">
                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                VerticalAlign="Middle" Font-Bold="true" Font-Size="16px" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="STANDARD_DISCOUNT" HeaderText="DISCOUNT" DataFormatString="{0:F2}">
                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                VerticalAlign="Middle" Font-Bold="true" Font-Size="16px" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UNIT_PRICE" HeaderText="PRICE" DataFormatString="{0:F2}">
                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                VerticalAlign="Middle" Font-Bold="true" Font-Size="16px" Width="90px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NET_AMOUNT" HeaderText="Amount" DataFormatString="{0:F2}">
                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                VerticalAlign="Middle" Font-Bold="true" Font-Size="16px" Width="65px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Amount" HeaderText="Amount">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CHECK_DELETE" HeaderText="CHECK_DELETE">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" runat="server" BorderColor="Red" Text="Void" OnClientClick="javascript:return confirm('Are you sure you want to Void/Unvoid?');return false;"
                                                    CommandName="Delete"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle BorderColor="Silver" HorizontalAlign="Center" BorderWidth="2px" BorderStyle="Solid"
                                                Width="30px" Font-Bold="true" ForeColor="Red" Font-Overline="true" Font-Size="14px">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle Height="30px" />
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="l-pannel">
                        <ul>
                            <li><span></span>
                                <asp:Image runat="server" ID="imgSKU" ImageUrl="../images/cloth.png" AlternateText="No Image Found" />
                                <span></span></li>
                            <li>
                                <label>
                                    Gross Sale</label>
                                <asp:TextBox ID="txtGrossAmount" runat="server"></asp:TextBox>
                            </li>
                            <li><span></span>
                                <label>
                                    Discount &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp Authorised By</label>
                                <asp:TextBox ID="numtxtTotalExtraDiscnt" runat="server" Width="112" ValidationGroup="NumbersOnly" onkeypress="FocusToAuthorized(event)"></asp:TextBox>
                                <asp:TextBox ID="txtAuthorisedBy" runat="server" Width="112" TabIndex="20" onkeypress="FocusToCash(event)"></asp:TextBox>
                            </li>
                            <li><span></span>
                                <label>
                                    Sales Tax</label>
                                <asp:TextBox ID="numTxtTotalGST" runat="server"></asp:TextBox>
                            </li>
                            <li><span></span>
                                <label>
                                    Net Amount</label>
                                <asp:TextBox ID="numTxtTotlAmnt" runat="server"></asp:TextBox>
                            </li>
                            <li><span></span>
                                <label id="lblCash">Cash Received</label>                                
                                <asp:TextBox ID="txtCashRecieved2" runat="server" onkeypress="CalculateBalance(event);"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom"
                                    ValidChars="0123456789." TargetControlID="txtCashRecieved2" />
                            </li>
                            <li><span></span>
                                <label>
                                    Balance</label>
                                <asp:TextBox ID="txtBalance" runat="server"></asp:TextBox>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="footer" style="height: 200px;">
                    <div class="main">
                        <div class="address">
                            <h2>
                                Sales Report
                            </h2>
                            <p>
                                <b>Report Type:</b><span>
                                    <asp:DropDownList ID="ddlReportType" runat="server" Width="160px" CssClass="DropList">
                                        <asp:ListItem Text="Summary" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Detail Report" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </span>
                            </p>
                            <p>
                                <b>Sales Person:</b><span>
                                    <asp:DropDownList ID="ddl_saleforce2" runat="server" Width="160px" CssClass="DropList">
                                    </asp:DropDownList>
                                </span>
                            </p>
                            <p>
                                <b>From Date:&nbsp;&nbsp; </b><span>
                                    <asp:TextBox ID="txtstartDate" runat="server" Width="150px" MaxLength="11"></asp:TextBox>
                                    <%--<asp:ImageButton ID="ibnstartDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                    </asp:ImageButton>--%>
                                </span>
                            </p>
                            <p>
                                <b>To Date: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b><span>
                                    <asp:TextBox ID="txtEndDate" runat="server" Width="150px" MaxLength="11"></asp:TextBox>
                                    <%--<asp:ImageButton ID="ibnEndDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                    </asp:ImageButton>--%>
                                </span>
                                <%--
                            <cc1:CalendarExtender ID="CEEndDate" runat="server" TargetControlID="txtEndDate"
                            PopupButtonID="ibnEndDate" Format="dd-MMM-yyyy" PopupPosition="TopLeft" />--%>
                            </p>
                            <p>
                                <span style="margin-top: 2px;">&nbsp;&nbsp;
                                    <asp:Button runat="server" ID="btnViewSalesReport" CssClass="view" ToolTip="View"
                                        OnClick="btnViewSalesReport_Click" />
                                </span><span></span>
                            </p>
                        </div>
                        <div class="shadow-footer">
                        </div>
                        <div class="user-login">
                            <img src="../images/user-login.png" alt="" />
                            <span width="200px">
                                <h2>
                                    User Login</h2>
                                <h3>
                                    <asp:Label runat="server" ID="lbluserlogin" Text="Administrator"></asp:Label></h3>
                                <br />
                                <br />
                                <p>
                                </p>
                                <p>
                                    <b>Working Date:</b>
                                    <h3>
                                        <asp:Label runat="server" ID="lblCurrentWorkingDate" Text="Temp"></asp:Label>
                                    </h3>
                                    <br />
                                    <br />
                                    <p>
                                    </p>
                                    <p>
                                        <b>Loged in at</b><br />
                                        <asp:Label ID="lbllogintimedate" runat="server" Text="time"></asp:Label>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                </p>
                            </span>
                        </div>
                        <div class="shadow-footer">
                        </div>
                        <div class="free-sku">
                            <h2>
                               
                               
                            </h2>
                            <div style="width:350px;">
                                <table style="width:350px;">
                                   
                                    <tr>
                                        <td > <h2> Credit Ceiling :</h2></td>
                                        <td ><h2> <asp:Label ID="lblCreditLimit" runat="server"></asp:Label></h2> </td>
                                    </tr>
                                    <tr>
                                        <td><h2>Ledger Balance : </h2></td>
                                        <td><h2><asp:Label ID="lblLedgerBalance" runat="server"></asp:Label></h2></td>
                                    </tr>
                                     <tr>
                                        <td style="width:55%;"> <h2> Balance Ceiling :</h2></td>
                                        <td style="width:45%;"><h2> <asp:Label ID="lblAllowLimit" runat="server"></asp:Label></h2> </td>
                                    </tr>
                                </table>
                            </div>
                            <p>
                                <asp:LinkButton runat="server" ID="btnSaveOrder" OnClick="btnSaveOrder_Click" ToolTip="Save"
                                    CssClass="save-n-print" AccessKey="S" Visible="false"></asp:LinkButton>
                                <input type="button" id="LinkButton2" class="save-n-print" runat="server" onclick="PrintSaleInvoice();" />
                                <asp:LinkButton runat="server" ID="btnVoid" OnClick="btnVoid_Click" CssClass="void">
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnCancel" OnClick="btnCancel_Click" CssClass="exit"></asp:LinkButton>                                
                                <input type="button" id="btnCalculate" class="Button" onclick="CalculateDiscount();" value="Calculate" runat="server" visible="false" />
                            </p>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnViewSalesReport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div style="display: none; width: 2.6in;">
        <div id="dvSaleInvoice">
            <style type="text/css">
                #dvSaleInvoice
                {
                    width: 2.6in;
                }
                #SaleInvoice
                {
                    width: 2.6in;
                }
                #CompanyName
                {
                    font-size: 20px;
                    font-weight: bold;
                }
                #SaleInvoiceText
                {
                    font-size: 14px;
                }
                #InvoiceDate
                {
                    font-weight: bold;
                }
                #CustomerType
                {
                    font-weight: bold;
                }
                #phoneNo
                {
                    font-weight: bold;
                }
                #hrSaleInvoiceHead
                {
                    border: #333333 solid 1px;
                }
                #invoiceDetail
                {
                    width: 98%;
                    margin-top: 10px;
                }
                #invoiceDetailBody tr td
                {
                    border: #333333 solid 1px;
                    font-family: Sans-Serif;
                    font-size: 12px;
                    padding: 2px;
                }
                .text-right
                {
                    text-align: right;
                }
                #invoiceDetailFoot tr td
                {
                    font-family: Sans-Serif;
                    font-size: 14px;
                    font-weight: bold;
                }
            </style>
            <table id="SaleInvoice">
                <tr>
                    <td colspan="2" align="center">
                        <span id="CompanyName"><%=hfLocationName.Value%></span>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <span style="font-size: 10px;">
                            <%=hfContactNo.Value%></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 12px; font-family: Sans-Serif; font-style: italic;">
                        <label id="invoiceMode">
                            Sale Invoice</label>
                    </td>
                    <td>
                        MOP: &nbsp;<span id="Span1" style="font-style: italic; font-family: Sans-Serif; font-size: 12px;"><label
                            id="payMode">
                            Cash</label></span>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr style="border: 1px solid black; background-color: Black; margin-bottom: 2px;
                            margin-top: 1px;" />
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-style: italic; font-family: Sans-Serif; font-size: 12px;">
                        Date : <span style="font-style: normal; font-family: Sans-Serif; font-size: 12px;">
                            <%=lbllogintimedate.Text %></span>
                    </td>
                    <td style="font-style: italic; font-family: Sans-Serif; font-size: 12px;">
                        No of Units : &nbsp;<label style="font-style: normal; font-family: Sans-Serif; font-size: 12px;"
                            id="Units">
                        </label>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-style: italic; font-family: Sans-Serif; font-size: 12px;">
                        Inv #<label style="font-style: normal; font-family: Sans-Serif; font-size: 12px;"
                            id="CustomerType">
                            <%=hfMaxId.Value %>
                        </label>
                    </td>
                    <td style="font-style: italic; font-family: Sans-Serif; font-size: 12px;">
                        Saleman: <span style="font-style: normal; font-family: Sans-Serif; font-size: 12px;">
                            <label id="saleMan">
                                Cash</label></span>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr style="border: 1px solid black; background-color: Black; margin-bottom: 2px;
                            margin-top: 1px;" />
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="4" style="font-style: italic; font-size: 12px; margin-bottom: 5px;">
                        CUSTOMER: &nbsp;<span id="customerName" style="font-style: normal; font-size: 12px;"><label
                            id="lblCustomerName">Walk In Customer</label></span>
                        <hr style="border: 1px solid black; background-color: Black; margin-bottom: -8px;
                            margin-top: 2px;" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table id="invoiceDetail">
                            <thead id="invoiceDetailHead">
                                <tr>
                                    <td style="text-align: left; font-size: 12px; font-family: Sans-Serif; width: 32%">
                                        Item Name
                                    </td>
                                    <td align="center" style="font-size: 12px; font-family: Sans-Serif; width: 10%">
                                        Qty
                                    </td>
                                    <td align="center" style="font-size: 12px; font-family: Sans-Serif; width: 15%">
                                        Price
                                    </td>
                                    <td align="center" style="font-size: 12px; font-family: Sans-Serif; width: 10%">
                                        Disc
                                    </td>
                                    <td align="center" style="font-size: 12px; font-family: Sans-Serif; width: 15%">
                                        Amount
                                    </td>
                                </tr>
                            </thead>
                            <tbody id="invoiceDetailBody">
                            </tbody>
                            <tfoot id="invoiceDetailFoot">
                                <tr style="display: none;">
                                    <td colspan="3" align="right">
                                        <label id="TotalValue-text">
                                            GROSS AMOUNT :
                                        </label>
                                    </td>
                                    <td align="right">
                                        <label id="TotalValue">
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="3">
                                        <label id="GrandTotal-text">
                                            AMOUNT-DUE :
                                        </label>
                                    </td>
                                    <td align="right">
                                        <label id="DiscountTotal">
                                            0.0</label>
                                    </td>
                                    <td align="right">
                                        <label id="GrandTotal">
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="right">
                                        <label id="Paid-text">
                                            CASH-PAID-IN :
                                        </label>
                                    </td>
                                    <td align="right" colspan="2">
                                        <label id="Paid">
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="right">
                                        <label id="Balance-text">
                                            BALANCE :
                                        </label>
                                    </td>
                                    <td align="right" colspan="2">
                                        <label id="Balance">
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        CASHIER : <span style="font-style: normal; font-family: Sans-Serif; font-size: 12px;">
                                            <%=lbluserlogin.Text%>
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        .No warranty for imported garments.
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        .No claim for imported garments.
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        .Not refundable only change with in 5 days with complete accessories
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" align="center">
                                        THANKS FOR YOUR VISIT.
                                        <hr style="border: 1px solid black; background-color: Black; margin-bottom: 2px;
                                            margin-top: 1px;" />
                                        <span style="font-size: 10px;">Powered by:FastServices.pk</span>
                                    </td>
                                </tr>
                            </tfoot>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
    </div>
    </form>
</body>
</html>