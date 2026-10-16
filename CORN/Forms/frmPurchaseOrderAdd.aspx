<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmPurchaseOrderAdd.aspx.cs" Inherits="Forms_frmPurchaseOrderAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">

    <script src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        function ddlFocus(obj) {
            obj.className = "ddlFocus";
        }

        function ddlBlur(obj) {
            obj.className = "";
        }
        function pageLoad() {

            $("select").searchable();
        }

        function calendarShown(sender) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }


        function ValidateForm() {
            var str;

            str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Quantity');
                return false;
            }


            str = document.getElementById('<%=txtContainerNo.ClientID%>').value;

            if (str == null || str.length == 0) {

                alert('Must Enter Invoice #');

                return false;

            }

            return true;
        }
        
    </script>
    <div id="right_data">
        <div>
            <div style="z-index: 101; left: 597px; width: 100px; position: absolute; top: 109px;
                height: 100px">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div id='messagediv' style="text-align: center">
                            <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                Width="27px" />
                            Wait Update.......
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 10%" align="right">
                                            <strong>
                                                <asp:Label ID="lblfromLocation" runat="server" Text="Order For:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 89%">
                                            <asp:DropDownList ID="drpDistributor" runat="server" AutoPostBack="True" Width="200px"
                                                OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%" align="right">
                                            <strong>
                                                <asp:Label ID="lblPrincipal" runat="server" Text="Supplier:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 89%">
                                            <asp:DropDownList ID="drpPrincipal" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged"
                                                Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%" align="right">
                                            <strong>
                                                <asp:Label ID="lblRefNo" runat="server" Text="Invoice #:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 89%">
                                            <asp:TextBox ID="txtContainerNo" runat="server" Width="195px"></asp:TextBox>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td style="width: 10%" align="right">
                                            <strong>
                                                <asp:Label ID="lblCurrencty" runat="server" Text="Bag #:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 89%">
                                            <asp:TextBox ID="txtBag" runat="server" Width="195px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%" align="right">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Height="13px" Text="Expctd Arvl Date:" Width="102px"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 89%">
                                            <asp:TextBox ID="txtArrivalDate" runat="server" CssClass="txtBox" Width="180px"></asp:TextBox>
                                            <asp:ImageButton ID="ibtnDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                Width="16px" />
                                            <cc1:CalendarExtender ID="CEDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnDate"
                                                OnClientShown="calendarShown" TargetControlID="txtArrivalDate">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>

                                      <tr>
                                        <td style="width: 10%" align="right">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Height="13px" Text="Invoice Date:" Width="102px"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 89%">
                                            <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="txtBox" Width="180px"></asp:TextBox>
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                Width="16px" />
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnDate"
                                                OnClientShown="calendarShown" TargetControlID="txtInvoiceDate">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%" align="right">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Text="Remarks"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 89%">
                                            <asp:TextBox ID="txtRemarks" runat="server" Width="195px"></asp:TextBox>
                                        </td>
                                    </tr>
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
                                            <td >
                                                <asp:Label ID="lblskuname" runat="server" Width="299px"
                                                     Text=" Description" CssClass="label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblquantity" runat="server" Width="70px" 
                                                     Text="Quantity" CssClass="label" ></asp:Label>
                                            </td>
                                            
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlSKuCde" runat="server" Width="299px" onfocus="ddlFocus(this);"
                                                    onblur="ddlBlur(this);">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQuantity" runat="server" Width="70px"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" ValidChars=".0123456789"
                                                    TargetControlID="txtQuantity">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="94px"
                                                    Font-Size="8pt" Text="Add Item" ValidationGroup="vg" CssClass="Button" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="4">
                                                <asp:Panel ID="Panel2" runat="server" Width="480px" Height="210px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="GrdPurchase" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                        OnRowDeleting="GrdPurchase_RowDeleting" OnRowEditing="GrdPurchase_RowEditing"
                                                        ShowHeader="False" Width="463px">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous" />
                                                        <RowStyle ForeColor="Black" />
                                                         <AlternatingRowStyle BackColor="#E0E0E0" />
                                                        <Columns>
                                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                                                                 <ItemStyle CssClass="grdDetail" HorizontalAlign="Left"
                                                                    Width="50px" />
                                                            </asp:BoundField>
                                                              <asp:BoundField DataField="Bar_code" HeaderText="BAR Code">
                                                                 <ItemStyle CssClass="grdDetail" HorizontalAlign="Left"
                                                                    Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Left"
                                                                    Width="154px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right"
                                                                    Width="75px" />
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
                                                        <FooterStyle BackColor="White" />
                                                        <PagerStyle BackColor="Transparent" />
                                                        <HeaderStyle BackColor="#007395" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                            VerticalAlign="Middle" />
                                                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                       
                                        
                                        <td colspan="1" align="right" >
                                           <asp:Label ID="Label2" runat="server" Width="100px" 
                                                     Text="Total Quantity" CssClass="label" ></asp:Label>
                                        </td>
                                         
                                        <td>
                                        <asp:TextBox ID="txtTotalQty" runat="server" Width="70px"></asp:TextBox>
                                        </td>
                                         <td>
                                        
                                        </td>
                                        <td></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />
                                <asp:Button AccessKey="S" ID="btnSaveDocument" runat="server" Width="110px" Font-Size="8pt"
                                    Text="Save Document" UseSubmitBehavior="False" OnClick="btnSaveDocument_Click"
                                    CssClass="Button" />
                                <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="110px" Font-Size="8pt"
                                    Text="Cancel" UseSubmitBehavior="False" OnClick="btnCancel_Click" CssClass="Button" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSaveDocument" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
