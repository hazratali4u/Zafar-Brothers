<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmPurchaseOrderAdd2.aspx.cs" Inherits="Forms_frmPurchaseOrderAdd2" %>

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
            <div style="z-index: 101; left: 597px; width: 100px; position: absolute; top: 109px; height: 100px">
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
                                        <td style="width: 1%"></td>
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
                                        <td style="width: 1%"></td>
                                        <td style="width: 89%">
                                            <asp:DropDownList ID="drpPrincipal" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged"
                                                Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                        <tr>
                                        <td style="width: 10%" align="right">
                                            <strong>
                                                    <asp:Label ID="Label4" runat="server" Width="100px" Text="Gender"></asp:Label>
                                            </strong>
                                        </td>
                                            <td></td>
                                        <td >
                                            <asp:DropDownList ID="drpGender" 
                                                  runat="server" Width="200px" 
                                                CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="drpGender_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                        <tr>
                                        <td style="width: 10%" align="right">
                                            <strong>
                                                    <asp:Label ID="Label5" runat="server" Width="100px" Text="Category"></asp:Label>
                                            </strong>
                                        </td>
                                            <td></td>
                                        <td >
                                            <asp:DropDownList ID="drpcategory" 
                                              runat="server" Width="200px" OnSelectedIndexChanged="drpcategory_SelectedIndexChanged" AutoPostBack="True" >
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%" align="right">
                                            <strong>
                                                <asp:Label ID="lblRefNo" runat="server" Text="Invoice #:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%"></td>
                                        <td style="width: 89%">
                                            <asp:TextBox ID="txtContainerNo" runat="server" Width="195px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%" align="right">
                                            <strong>
                                                <asp:Label ID="lblCurrencty" runat="server" Text="Bag #:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%"></td>
                                        <td style="width: 89%">
                                            <asp:TextBox ID="txtBag" runat="server" Width="195px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%" align="right">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Height="13px" Text="Expctd Arvl Date:" Width="102px"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%"></td>
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
                                        <td style="width: 1%"></td>
                                        <td style="width: 89%">
                                            <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="txtBox" Width="180px"></asp:TextBox>
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                Width="16px" />
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton2"
                                                OnClientShown="calendarShown" TargetControlID="txtInvoiceDate">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%" align="right">
                                            <strong>
                                                <asp:Label ID="Label6" runat="server" Text="Remarks"></asp:Label>
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
                                            <td align="left" colspan="4">
                                                <asp:Panel ID="Panel2" runat="server" Width="550px" Height="210px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="GrdPurchase" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                        ShowHeader="true" Width="100%">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous" />
                                                        <RowStyle ForeColor="Black" />
                                                        <Columns>
                                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Left"
                                                                    Width="30px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Bar_code" HeaderText="BAR Code">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Left"
                                                                    Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Left"
                                                                    Width="194px" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Qty">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtQty" runat="server" Width="75px" Text='<%# String.Format("{0:0}", Eval("Quantity"))%>'></asp:TextBox>

                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Left"
                                                                    Width="54px" />
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


                                            <td colspan="1" align="right">
                                                <asp:Label ID="Label2" runat="server" Width="100px"
                                                    Text="Total Quantity" CssClass="label"></asp:Label>
                                            </td>

                                            <td>
                                                <asp:TextBox ID="txtTotalQty" runat="server" Width="70px"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />

                                <asp:Button AccessKey="S" ID="btnCalculate" runat="server" Width="119px" Font-Size="8pt"
                                    Text="Calculate" UseSubmitBehavior="False" OnClick="btnCalculate_Click"
                                    CssClass="Button" />
                                <asp:Button AccessKey="S" ID="btnSaveDocument" runat="server" Width="110px" Font-Size="8pt"
                                    Text="Save Document" UseSubmitBehavior="true" OnClick="btnSaveDocument_Click"
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
