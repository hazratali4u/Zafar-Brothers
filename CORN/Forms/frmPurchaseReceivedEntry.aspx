<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmPurchaseReceivedEntry.aspx.cs" Inherits="Forms_frmPurchaseReceivedEntry"
    Title="SAMS :: Goods Receipt Note [GRN]" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
   
    <script language="JavaScript" type="text/javascript">

    </script>
    <div id="right_data">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
             <asp:HiddenField ID="hfClosingDate" runat="server" Value=""/>
                <div>
                    <table width="100%">
                        <tr>
                            <td>
                                <div style="z-index: 101; left: 50%; width: 100px; position: absolute; top: 8%; height: 16%">
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
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
                                        <td style="width: 15%" align="right">
                                            <strong>
                                                <asp:Label ID="lblDocumentNo" runat="server" Text="P.O. Ref #:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 84%">
                                            <asp:DropDownList ID="drpDocumentNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged"
                                                Width="25%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%" align="right">
                                            <strong>
                                                <asp:Label ID="lblfromLocation" runat="server" Text="Purchase For:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 84%">
                                            <asp:DropDownList ID="drpDistributor" runat="server" Width="25%" Enabled="false">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%" align="right">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Text="Date Effected:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDate" runat="server" Width="24%" MaxLength="11" OnTextChanged="txtDate_OnTextChanged"
                                                AutoPostBack="true"></asp:TextBox>
                                            <asp:ImageButton ID="ibDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                Width="16px" />
                                            <ajaxToolkit:CalendarExtender ID="ceDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibDate"
                                                TargetControlID="txtDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%" align="right">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Text="Remarks:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 84%">
                                            <asp:TextBox ID="txtRemarks" runat="server" Width="50%" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <%-- </ContentTemplate>
           
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>--%>
                <div>
                    <table width="100%">
                        <tr>
                            <td>
                                <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>--%>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel2" runat="server" Width="980px" Height="250px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="GrdPurchase" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="White" ForeColor="SteelBlue" HorizontalAlign="Center" Width="99.9%"
                                                        OnRowDataBound="GrdPurchase_RowDataBound">
                                                        <RowStyle ForeColor="Black" />
                                                         <AlternatingRowStyle BackColor="#E0E0E0" />
                                                        <Columns>
                                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_CODE" HeaderText="Item Code">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Left" Width="50px" />
                                                                <HeaderStyle CssClass="grdHead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="BAR_CODE" HeaderText="BAR Code">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Left" Width="100px" />
                                                                <HeaderStyle CssClass="grdHead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" HeaderText="Item Name">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Left" Width="150px" />
                                                                <HeaderStyle CssClass="grdHead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Quantity" HeaderText="Ord Qty">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Left" Width="50px" />
                                                                <HeaderStyle CssClass="grdHead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Prev Rcd Qty">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRecQty" runat="server" Width="50px" Text='<%# String.Format("{0:0}", Eval("RcdQty"))%>'
                                                                        Enabled="false"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" />
                                                                <HeaderStyle CssClass="grdHead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Curr Rcd Qty">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtCurrentRecQty" runat="server" Width="50px" Text='<%# String.Format("{0:0}", Eval("CurrentRcdQty"))%>'></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                        ValidChars=".0123456789" TargetControlID="txtCurrentRecQty">
                                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" />
                                                                <HeaderStyle CssClass="grdHead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Pur Price">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtPurchasePriceNew" runat="server" Width="75px" Text='<%# String.Format("{0:0.00}", Eval("PURCHASE_PRICE"))%>'></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderNew" runat="server"
                                                                        ValidChars=".0123456789" TargetControlID="txtPurchasePriceNew">
                                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" />
                                                                <HeaderStyle CssClass="grdHead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Cost Price">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtPurchasePrice" runat="server" Width="75px" Text='<%# String.Format("{0:0.00}", Eval("DISTRIBUTOR_PRICE"))%>'></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                                        ValidChars=".0123456789" TargetControlID="txtPurchasePrice">
                                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" />
                                                                <HeaderStyle CssClass="grdHead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Selling Price">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtTradePrice" runat="server" Width="75px" Text='<%# String.Format("{0:0.00}", Eval("TRADE_PRICE"))%>'></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                                        ValidChars=".0123456789" TargetControlID="txtTradePrice" />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" />
                                                                <HeaderStyle CssClass="grdHead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rack No" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRackNo" runat="server" Width="75px" Text='<%# Eval("RACK_NO")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" />
                                                                <HeaderStyle CssClass="grdHead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                               <asp:BoundField DataField="Amount" HeaderText="Ord Amount"  DataFormatString="{0:f2}">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Left" Width="75px" />
                                                                <HeaderStyle CssClass="grdHead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Date Effected">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtMfgDate" runat="server" Width="75%" Text='<%# Eval("DATE_EFFECTED")%>'
                                                                        MaxLength="11"></asp:TextBox>
                                                                    <asp:ImageButton ID="ibMfgDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                                        Width="16px" />
                                                                    <ajaxToolkit:CalendarExtender ID="ceMfgDate" runat="server" Format="dd-MMM-yyyy"
                                                                        PopupButtonID="ibMfgDate" TargetControlID="txtMfgDate">
                                                                    </ajaxToolkit:CalendarExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="100"/>
                                                                <HeaderStyle CssClass="grdHead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="B. Code Print Qty">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtBCodeQty" runat="server" Width="50px" Text='<%# String.Format("{0:0}", Eval("CurrentRcdQty"))%>'></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftbeBCodeQty" runat="server"
                                                                        ValidChars=".0123456789" TargetControlID="txtBCodeQty">
                                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" />
                                                                <HeaderStyle CssClass="grdHead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="COLOR" HeaderText="COLOR">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="740px">
                        <tr>
                            <td width="40%">
                            </td>
                            <td>
                                <strong>
                                    <asp:Label ID="Label7" runat="server" Width="100px" Height="16px" Text="Total Order Qty"></asp:Label></strong>
                                <asp:TextBox ID="txtTotalQuantity" runat="server" Width="80px" CssClass="txtBox"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                              <td>
                                <strong>
                                    <asp:Label ID="Label2" runat="server" Width="150px" Height="16px" Text="Total Cur. Rec Qty"></asp:Label></strong>
                                <asp:TextBox ID="txtTotalCurRecQty" runat="server" Width="80px" CssClass="txtBox"
                                    ReadOnly="True"></asp:TextBox>
                            </td>

                             <td>
                                <strong>
                                    <asp:Label ID="Label3" runat="server" Width="150px" Height="16px" Text="Total Cur. Rec Amount"></asp:Label></strong>
                                <asp:TextBox ID="txtTotalCurRecAmount" runat="server" Width="80px" CssClass="txtBox"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
                    <table width="100%">
                        <tr>
                            <td>
                                <br />
                                <asp:Button AccessKey="S" ID="btnCalculate" runat="server" Width="119px" Font-Size="8pt"
                                    Text="Calculate" UseSubmitBehavior="False" OnClick="btnCalculate_Click"
                                    CssClass="Button" />
                                <asp:Button AccessKey="S" ID="btnSaveDocument" runat="server" Width="119px" Font-Size="8pt"
                                    Text="Save Document" UseSubmitBehavior="False" OnClick="btnSaveDocument_Click"
                                    CssClass="Button" />
                                <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="120px" Font-Size="8pt"
                                    Text="Cancel" UseSubmitBehavior="False" OnClick="btnCancel_Click" CssClass="Button" />
                                <asp:Button AccessKey="C" ID="btnGenerate" runat="server" Width="120px" Font-Size="8pt"
                                    Text="Generate Bar Code" UseSubmitBehavior="False" OnClick="btnGenerate_Click" CssClass="Button" />
                                <asp:Button AccessKey="C" ID="btnGenerateSingle" runat="server" Width="150px" Font-Size="8pt"
                                    Text="Generate Bar Code Single" UseSubmitBehavior="False" OnClick="btnGenerateSingle_Click" CssClass="Button" />
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="hf_ID" runat="server" />
                    <asp:HiddenField ID="hf_PID" runat="server" />
                    <asp:HiddenField ID="hfPrincipalName" runat="server" />
                    <asp:HiddenField ID="hf_CurrentRcdQty" runat="server" />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSaveDocument" />
                <asp:PostBackTrigger ControlID="btnGenerateSingle" />
                <asp:PostBackTrigger ControlID="btnGenerate" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
