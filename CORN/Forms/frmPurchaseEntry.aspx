<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmPurchaseEntry.aspx.cs" Inherits="Forms_frmPurchaseEntry" Title="CORN :: Stock Register" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">

    <script src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">


        function pageLoad() {
            $("select").searchable();
            ScanCheck();
        }

        function ProductSelected(source, eventArgs) {

            document.getElementById("<%=hfSKUID.ClientID %>").value = eventArgs.get_value();
            document.getElementById("<%=txtCtn.ClientID %>").focus();
        }

        function ScanCheck() {
            if (document.getElementById("<%=btnSave.ClientID %>").value != "Update") {
                document.getElementById("<%=txtSKU.ClientID %>").value = "";
                document.getElementById("<%=txtSKUScan.ClientID %>").value = "";
            }
            if (document.getElementById("<%=cbScan.ClientID %>").checked) {
                document.getElementById("<%=btnSave.ClientID %>").style.visibility = "hidden";
                document.getElementById("<%=txtSKU.ClientID %>").style.display = "none";
                document.getElementById("<%=txtSKUScan.ClientID %>").style.display = "block";
            }
            else {
                document.getElementById("<%=btnSave.ClientID %>").style.visibility = "visible";
                document.getElementById("<%=txtSKU.ClientID %>").style.display = "block";
                document.getElementById("<%=txtSKUScan.ClientID %>").style.display = "none";
            }
        }


        function SetContextKey() {

            $find("<%=aceProduct.ClientID%>").set_contextKey(document.getElementById("<%=drpDistributor.ClientID %>").value);

        }
        function AddQuantity(e) {
            var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
            if (key == 13) {
                e.preventDefault();
                if (document.getElementById('<%=txtSKUScan.ClientID%>').value != "") {
                    document.getElementById("<%=btnSave.ClientID %>").click();
                }
            }
        }


        function ValidateForm() {
            var str;

           
            str = document.getElementById('<%=txtDocumentNo.ClientID%>').value;
            var lblInvoice = document.getElementById('<%=lblInvoice.ClientID%>').innerHTML;
            if (str == null || str.length == 0) {
                if (lblInvoice == 'Driver Name') {
                    alert('Must Enter Driver Name');
                } else {
                    alert('Must Enter Invoice/DC No');
                }
                return false;
            }
            str = document.getElementById('<%=txtBuiltyNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Builty No');
                return false;
            }

            return true;
        }



    </script>
    <div id="right_data">
        <div style="z-index: 101; left: 597px; width: 100px; position: absolute; top: 109px; height: 100px">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div id='messagediv' style="text-align: center">
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                            Width="27px" />
                        Wait....
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hfClosingDate" runat="server" Value="" />
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="height: 24px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label2" runat="server" CssClass="lblbox" Height="14px" Text="Transaction Type"
                                                        Width="102px"></asp:Label></strong>
                                            </td>
                                            <td style="height: 24px">
                                                <asp:DropDownList ID="DrpDocumentType" runat="server" AutoPostBack="True" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpDocumentType_SelectedIndexChanged" Width="200px">
                                                    <%-- <asp:ListItem Value="2">Purchase</asp:ListItem>--%>
                                                    <asp:ListItem Value="5">Transfer Out</asp:ListItem>
                                                    <asp:ListItem Value="3">Purchase Return</asp:ListItem>
                                                    <%-- <asp:ListItem Value="4">Transfer In</asp:ListItem>--%>
                                                    <asp:ListItem Value="6">Shop Damage</asp:ListItem>
                                                    <asp:ListItem Value="10">Import Damage</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 24px">
                                                <strong>
                                                    <asp:Label ID="Label5" runat="server" Width="5px"></asp:Label></strong>
                                            </td>
                                            <td style="width: 316px;" align="center" colspan="1" rowspan="8" valign="middle"></td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 25px">
                                                <strong>
                                                    <asp:Label ID="lblDocumentNo" runat="server" Text="Document No" Width="94px"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpDocumentNo" runat="server" AutoPostBack="True" CssClass="DropList"
                                                    OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px"></td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 25px">
                                                <strong>
                                                    <asp:Label ID="lbltoLocation" runat="server" CssClass="lblbox" Text="Supplier" Width="94px"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpPrincipal" runat="server" AutoPostBack="True" CssClass="DropList"
                                                    OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px"></td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 25px">
                                                <strong>
                                                    <asp:Label ID="lblfromLocation" runat="server" CssClass="lblbox" Text="Purchase For"
                                                        Width="94px"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpDistributor" runat="server" AutoPostBack="True" CssClass="DropList"
                                                    Width="200px" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label4" runat="server" CssClass="lblbox" Text="Transfer To" Visible="False"
                                                        Width="82px"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="DrpTransferFor" runat="server" CssClass="DropList" Visible="False"
                                                    Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px;" align="left">
                                                <strong>
                                                    <asp:Label ID="lblInvoice" runat="server" CssClass="lblbox" Text="INV/DC  No" Width="94px"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="txtBox" Width="195px"></asp:TextBox>
                                            </td>
                                            <td style="width: 1px" valign="top"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px;" align="left">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" CssClass="lblbox" Text="Builty No" Width="94px"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBuiltyNo" runat="server" CssClass="txtBox" Width="195px"></asp:TextBox>
                                            </td>
                                            <td style="width: 1px" valign="top"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                <strong>
                                                    <asp:CheckBox ID="cbScan" runat="server" Text="Scan" onchange="ScanCheck();" />
                                                </strong>
                                            </td>
                                            <td style="width: 85%"></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="height: 16px">
                                                <asp:Label ID="lblskuname" runat="server" Width="357px" Text="Product Name" CssClass="lblDetail">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Width="86px" Text="Pack" CssClass="lblDetail"></asp:Label>
                                            </td>


                                            <td>
                                                <asp:Label ID="lblquantity" runat="server" Width="86px" Text="Unit" CssClass="lblDetail"></asp:Label>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:TextBox ID="txtSKU" runat="server" Width="357px" onkeyup="SetContextKey()"></asp:TextBox>
                                                <ajaxToolkit:AutoCompleteExtender ID="aceProduct" runat="server" TargetControlID="txtSKU"
                                                    ServicePath="wsProductList.asmx" MinimumPrefixLength="3" CompletionInterval="500"
                                                    CompletionSetCount="10" CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListItemCssClass="autocomplete_listItem" EnableCaching="true"
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                    OnClientItemSelected="ProductSelected" FirstRowSelected="true" ServiceMethod="GetProducts3">
                                                </ajaxToolkit:AutoCompleteExtender>
                                                <asp:TextBox ID="txtSKUScan" runat="server" Width="357px" onkeypress="AddQuantity(event);" Style="display: none"></asp:TextBox>

                                                <asp:HiddenField ID="hfSKUID" runat="server" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCtn" runat="server" Width="88px" TabIndex="6" Text="1"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                    FilterType="Custom" ValidChars="0123456789" TargetControlID="txtCtn">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQuantity" runat="server" Width="88px" TabIndex="6"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                    FilterType="Custom" ValidChars="0123456789" TargetControlID="txtQuantity">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="92px"
                                                    Font-Size="8pt" Text="Add" ValidationGroup="vg" CssClass="Button" TabIndex="7" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="4">
                                               <asp:Panel ID="Panel2" runat="server" Width="641px" Height="155px" ScrollBars="Vertical"
                                        BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                        <asp:GridView ID="grdOrder" runat="server" AutoGenerateColumns="False" BackColor="White"
                                            BorderColor="White" ForeColor="SteelBlue" HorizontalAlign="Center" OnRowDeleting="grdOrder_RowDeleting"
                                            OnRowEditing="grdOrder_RowEditing" ShowHeader="False" Width="100%">
                                            <RowStyle ForeColor="Black" />
                                             <AlternatingRowStyle BackColor="#E0E0E0" />
                                            <Columns>
                                                <asp:BoundField DataField="SKU_ID" ReadOnly="true" HeaderText="SKU_ID">
                                                    <HeaderStyle CssClass="HidePanel" />
                                                    <ItemStyle CssClass="HidePanel" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SKU_Name" ReadOnly="true" HeaderText="SKU Name">
                                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Left" Width="350px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PRICE" ReadOnly="true" HeaderText="Price">
                                                    <HeaderStyle CssClass="HidePanel" />
                                                    <ItemStyle CssClass="HidePanel" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="Ctn" ReadOnly="true" HeaderText="Quantity">
                                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="82px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Unit" ReadOnly="true" HeaderText="Quantity">
                                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="82px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="QUANTITY" ReadOnly="true" HeaderText="Quantity">
                                                    <HeaderStyle CssClass="HidePanel" />
                                                    <ItemStyle CssClass="HidePanel" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AMOUNT" ReadOnly="true" HeaderText="Amount">
                                                    <HeaderStyle CssClass="HidePanel" />
                                                    <ItemStyle CssClass="HidePanel" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Edit">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Delete">
                                                                    <img src="../images/edit.gif" />
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="45px" HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                           
                                                                 <asp:TemplateField HeaderText="Delete" >
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnDelete" ToolTip="Delete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                                    CommandName="Delete">
                                                                                    <img src="../images/delete.gif" width="16" height="16">
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="45px" HorizontalAlign="Center">
                                                                            </ItemStyle>
                                                                        </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#007395" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                                          
                                        </asp:GridView>
                                    </asp:Panel>
                                 </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <strong>
                                    <asp:Label ID="Label7" runat="server" Width="103px" Height="16px" Text="Total Quantity"></asp:Label></strong>
                                <asp:TextBox ID="txtTotalQuantity"  runat="server" Width="88px"
                                    CssClass="txtBox" ReadOnly="True"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        <asp:Button AccessKey="S" ID="btnSaveDocument" runat="server" Width="119px" Font-Size="8pt"
                            Text="Save Document" UseSubmitBehavior="False" OnClick="btnSaveDocument_Click"
                            CssClass="Button" />
                        <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="120px" Font-Size="8pt"
                            Text="Cancel" UseSubmitBehavior="False" OnClick="btnCancel_Click" CssClass="Button" />

                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
