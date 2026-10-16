<%@ Page Title="SAMS :: Sale Order" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSaleOrderNew.aspx.cs" Inherits="Forms_frmSaleOrderNew"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
     
    <script language="JavaScript" type="text/javascript">
           function DisableButton() {
            document.getElementById("<%=btnSaveOrder.ClientID %>").disabled = true;
            document.getElementById("<%=btnSaveOrder.ClientID %>").value='Wait..'
}
window.onbeforeunload = DisableButton;

        function pageLoad() {
            $("select").searchable();
            ScanCheck();
        }
    

        function ProductSelected(source, eventArgs) {
            var skuid=eventArgs.get_value();
           
            document.getElementById("<%=hfSKUID.ClientID %>").value = skuid;
            var assigndisId = document.getElementById("<%=drpAssignedDistributor.ClientID %>").value;
            var distId = document.getElementById("<%=ddlDistributor.ClientID %>").value;
            CallMethod(skuid,distId, assigndisId);
            document.getElementById("<%=txtCtn.ClientID %>").focus();
        }
        function CallMethod(skuid, disId, assigndisId) {
          
            PageMethods.getstock(skuid, disId, assigndisId, CallSuccess, CallError);
        }
 
        function CallSuccess(res) {
            document.getElementById("<%=lblstock.ClientID %>").innerHTML=res;
        }
 
        function CallError() {
            alert('stock Error');
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
            
            $find("<%=aceProduct.ClientID%>").set_contextKey(document.getElementById("<%=ddlDistributor.ClientID %>").value);

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
    </script>
    <div id="right_data">
        <div style="z-index: 101; left: 60%; width: 100px; position: absolute; top: 10%; height: 100px">
            &nbsp;<asp:Panel ID="Panel21" runat="server">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" >
                    <ProgressTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                            Width="31px" />
                        Wait Update
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </asp:Panel>
        </div>
        
        <asp:UpdatePanel ID="pnlMaster" runat="server">
            <ContentTemplate>
             
                <table width="100%" cellpadding="3">
                    <tr>
                        <td style="width: 70%" valign="top">
                            <table width="100%" cellpadding="3">
                                <tr>
                                    <td style="width: 15%">
                                        <strong>
                                            <label>Location</label>
                                        </strong>
                                    </td>
                                    <td style="width: 85%">
                                        <asp:DropDownList ID="ddlDistributor" runat="server" Width="220px" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlDistributor_SelectedIndexChanged" TabIndex="1">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%">
                                        <strong>
                                            <label>Stock Use By</label>
                                        </strong>
                                    </td>
                                    <td style="width: 85%">
                                        <asp:DropDownList ID="drpAssignedDistributor" runat="server" Width="220px"              >
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="width: 15%">
                                        <strong>
                                            <label>Customer</label>
                                        </strong>
                                    </td>
                                    <td style="width: 85%">
                                        <asp:DropDownList ID="ddlCustomer" runat="server" Width="220px" 
                                     AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged"   
                                                TabIndex="3">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="HdnSaleExectiveID" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%">
                                        <strong>
                                            <asp:Label ID="lblAdress" runat="server" Text="Address"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 85%">
                                        <strong>
                                            <asp:Label ID="lblCustomerAddress" runat="server"></asp:Label>
                                        </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%">
                                        <strong>
                                            <label>Remarks</label>
                                        </strong>
                                    </td>
                                    <td style="width: 85%">
                                        <asp:TextBox ID="txtRemarks" runat="server" TabIndex="4" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%">
                                        <strong>
                                            <asp:CheckBox ID="cbScan" runat="server" Text="Scan" onchange="ScanCheck();" />
                                        </strong>
                                    </td>
                                    <td style="width: 85%"></td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 30%" valign="top" >
                            <fieldset style="width: 80%;" >
                                <table>
                                    <tr>
                                        <td style="font-size: 16px; text-align: center">
                                            <strong>Ledger Balance</strong>
                                        </td>
                                        <tr >
                                            <td style="font-size: 16px; text-align: right">
                                                <asp:Label ID="lblLedgerBalance" runat="server"></asp:Label>
                                                <asp:Label ID="lblOpeningType" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="pnlDetail" runat="server">
            <ContentTemplate>
                <fieldset style="width: 200px;">
                    <table>
                        <tbody>
                            <tr>
                                <td style="height: 16px">
                                    <asp:Label ID="lblskuname" runat="server" Width="357px" Text="Product Name" CssClass="lblDetail">
                                    </asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="Label3" runat="server" Width="86px" Text="Color" CssClass="lblDetail"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Width="86px" Text="Pack" CssClass="lblDetail"></asp:Label>
                                </td>


                                <td>
                                    <asp:Label ID="lblquantity" runat="server" Width="86px" Text="Unit" CssClass="lblDetail"></asp:Label>
                                </td>
                                <td> <asp:Label ID="Label2" runat="server" Width="40px" Text="Stock" ></asp:Label>
                                     <asp:Label ID="lblstock" runat="server" Width="30px" Text="0-0" ></asp:Label>
                                </td>
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
                                      <asp:HiddenField ID="hfStock" runat="server" Value="0" />
                                </td>
                                 <td>
                                    <asp:TextBox ID="txtColor" runat="server" Width="88px" TabIndex="6"></asp:TextBox>
                                  
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCtn" runat="server" Width="88px" TabIndex="6" Text="0"></asp:TextBox>
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
                                <td align="left" colspan="5">
                                    <asp:Panel ID="Panel2" runat="server" Width="751px" Height="155px" ScrollBars="Vertical"
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
                                                <asp:BoundField DataField="skuName" ReadOnly="true" HeaderText="SKU Name">
                                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Left" Width="350px" />
                                                </asp:BoundField>
                                                  <asp:BoundField DataField="COLOR" ReadOnly="true" HeaderText="COLOR">
                                                   <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="82px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="price" ReadOnly="true" HeaderText="Price">
                                                    <HeaderStyle CssClass="HidePanel" />
                                                    <ItemStyle CssClass="HidePanel" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="Ctn" ReadOnly="true" HeaderText="Quantity">
                                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="82px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Unit" ReadOnly="true" HeaderText="Quantity">
                                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="82px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="qty" ReadOnly="true" HeaderText="Quantity">
                                                    <HeaderStyle CssClass="HidePanel" />
                                                    <ItemStyle CssClass="HidePanel" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="amount" ReadOnly="true" HeaderText="Amount">
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
                            <tr>
                                <td style="text-align: right;">
                                    <strong>Total Qty </strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotalQty" runat="server" Width="88px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotalQtyUnit" runat="server" Width="88px" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <strong>Total Amount</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotalAmount" runat="server" Width="88px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                        </tbody>
                    </table>
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:Button AccessKey="S" ID="btnSaveOrder" runat="server" Width="110px" Font-Size="8pt"
                                    TabIndex="8" Text="Save Order" OnClick="btnSaveOrder_Click"
                                    CssClass="Button" />
                                <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="110px" Font-Size="8pt"
                                    TabIndex="9" Text="Cancel" UseSubmitBehavior="False" OnClick="btnCancel_Click"
                                    CssClass="Button" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSaveOrder" />
            </Triggers>
        </asp:UpdatePanel>

    </div>
</asp:Content>
