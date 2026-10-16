<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmRollBackFormCheque.aspx.cs" Inherits="frmRollBackFormCheque" Title="CORN :: Rollback Transaction" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
   
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <div style="z-index: 101; left: 534px; width: 100px; position: absolute; top: 256px;
                            height: 100px">
                            <asp:Panel ID="Panel2" runat="server">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                    <ProgressTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                            Width="27px" />
                                        Wait Update.......
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </asp:Panel>
                        </div>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="Label2" runat="server" Width="104px" Text="Transaction Type" CssClass="lblbox"
                                    Visible="false"></asp:Label></strong>
                                <asp:DropDownList ID="DrpDocumentType" runat="server" Width="200px" CssClass="DropList"
                                    AutoPostBack="True" OnSelectedIndexChanged="DrpDocumentType_SelectedIndexChanged"
                                    Visible="false">
                                    <%--   <asp:ListItem Value="0">Order Entry</asp:ListItem>
                                 <asp:ListItem Value="1">Sale Invoice</asp:ListItem>
                                    <asp:ListItem Value="2">Sale Return</asp:ListItem>--%>
                                      <asp:ListItem Value="3">Realized Cheque</asp:ListItem>
                                </asp:DropDownList>
                                <table>
                                    <tr>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="lbltoLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList"
                                                AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                   
                                                <asp:Label ID="Label1" runat="server" Width="94px" Text="Supplier" CssClass="lblbox" Visible="false"></asp:Label>
                                            <asp:DropDownList ID="DrpPrincipal" runat="server" Width="200px" CssClass="DropList" Visible="false"
                                                AutoPostBack="True" OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged">
                                            </asp:DropDownList>
                                      
                                    <tr style="display:none;">
                                        <td align="left" style="height: 25px">
                                            <strong>
                                                <asp:Label ID="Label6" runat="server" Width="94px" Text="Sale Force" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="DrpOrderBooker" runat="server" Width="200px" CssClass="DropList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" CssClass="lblbox" Text="Legend" Width="94px"></asp:Label></strong>
                                        </td>
                                        <td align="left" style="height: 25px">
                                            <asp:DropDownList ID="DrpLenged" runat="server" Width="200px" CssClass="DropList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Button ID="btnGetOrder" OnClick="btnGetOrder_Click" runat="server" Width="100px"
                                                Font-Size="8pt" Text="Get Data" CssClass="Button" />
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <asp:Button ID="btnPost" runat="server" Font-Size="8pt" Text="Rollback" Width="128px"
                                                OnClick="btnPost_Click" CssClass="Button" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table style="border-right: silver thin inset; border-top: silver thin inset; border-left: silver thin inset;
                                    width: 650px; border-bottom: silver thin inset">
                                    <tbody>
                                        <tr>
                                            <td style="height: 21px" align="left" colspan="5">
                                                <asp:Panel ID="Panel1" runat="server" Width="740px" Height="250px" ScrollBars="Vertical">
                                                    <asp:GridView ID="GrdOrder" runat="server" Width="720px" ForeColor="SteelBlue" CssClass="gridRow2"
                                                        DataKeyNames="Document_ID" HorizontalAlign="Center" BorderColor="White" BackColor="White"
                                                        AutoGenerateColumns="False">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous" />
                                                         <AlternatingRowStyle BackColor="#E0E0E0" />
                                                        <Columns>
                                                            <asp:BoundField DataField="CUSTOMER_ID" HeaderText="Customer Id">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ChbInvoice" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"/>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="Code">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Name">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Document_Id" HeaderText="Document Id">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="Document Date">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="TOTAL_AMOUNT" DataFormatString="{0:F2}" HeaderText="Gross Amount">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                             <asp:BoundField DataField="SCHEME_AMOUNT" DataFormatString="{0:F2}" HeaderText="Discount">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DISCOUNT_AMOUNT" DataFormatString="{0:F2}" HeaderText="Extra Discount">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                           
                                                            <asp:BoundField DataField="GST_AMOUNT" DataFormatString="{0:F2}" HeaderText="Sales Tax">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="TOTAL_NET_AMOUNT" DataFormatString="{0:F2}" HeaderText="Net Amount">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="tblhead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:GridView>
                                                    <asp:GridView ID="GrdCheque" runat="server" Visible="False" Width="700px" ForeColor="SteelBlue"
                                                        CssClass="gridRow2" HorizontalAlign="Center" BorderColor="White" BackColor="White"
                                                        AutoGenerateColumns="False">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous"></PagerSettings>
                                                         <AlternatingRowStyle BackColor="#E0E0E0" />
                                                        <Columns>
                                                            <asp:BoundField DataField="CHEQUE_PROCESS_ID" HeaderText="CHEQUE_PROCESS_ID">
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CUSTOMER_ID" HeaderText="Customer Id">
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ChbInvoice" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="Code">
                                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                                </ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Name">
                                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                                </ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Voucher_No" HeaderText="Voucher No" Visible="False">
                                                                <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CHEQUE_NO" HeaderText="Cheque No">
                                                                <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                                </ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CHEQUE_DATE" HeaderText="Cheque Date">
                                                                <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                                </ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CHEQUE_AMOUNT" DataFormatString="{0:F2}" HeaderText="Cheque Amount">
                                                                <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                                </ItemStyle>
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="tblhead">
                                                        </HeaderStyle>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
