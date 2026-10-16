<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmBarcode.aspx.cs" Inherits="Forms_frmBarcode" Title="CORN :: Item Barcode" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <script src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function pageLoad() {
            $("select").searchable();
        }

    </script>
    <div id="right_data">
        <asp:UpdatePanel ID="up_pnl1" runat="server">
            <ContentTemplate>
                <div style="display: none;">
                    <table>
                        <tr>
                            <td>
                                <strong>
                                    <asp:Label ID="lbl_code" runat="server" Width="90px" Text="Item Code"></asp:Label></strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_code" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblskuCode" runat="server" Width="360px" CssClass="label" Text="Item Description"> </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblquantity" runat="server" Width="76px" Text="No of Row's" CssClass="label"></asp:Label>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:Label ID="Label41" runat="server" Width="92px" Text="Add Item" CssClass="label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlSKuCde" runat="server" Width="360px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQuantity" runat="server" Width="70px" Text="1" CssClass="txtBox "></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="ftbe_txtrow" runat="server" ValidChars="123456789"
                                                    TargetControlID="txtQuantity" />
                                            </td>
                                            <td style="width: 100px">
                                                <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="95px"
                                                    Font-Size="8pt" Text="Add Item" CssClass="Button" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="3">
                                                <asp:Panel ID="Panel2" runat="server" Width="550px" Height="200px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="GrdPurchase" runat="server" Width="533px" ForeColor="SteelBlue"
                                                        CssClass="gridRow2" BorderColor="White" BackColor="White" ShowHeader="False"
                                                        OnRowDeleting="GrdPurchase_RowDeleting" OnRowEditing="GrdPurchase_RowEditing"
                                                        HorizontalAlign="Center" AutoGenerateColumns="False">
                                                        <RowStyle ForeColor="Black" />
                                                          <AlternatingRowStyle BackColor="#E0E0E0" />
                                                         <Columns>
                                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="BAR_CODE" HeaderText="BAR Code">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                                    Width="85px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                                    Width="180px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PRICE" HeaderText="PRICE" DataFormatString="{0:f2}">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                                    Width="80px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                    Width="50px" />
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
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            &nbsp;
        </div>
        <div>
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btn_generate" runat="server" Text="Generate" OnClick="btn_generate_Click"
                            Width="80" CssClass="Button" CausesValidation="true" />
                        <asp:Button ID="btn_generateSingle" runat="server" Text="Generate Single" 
                            Width="120" CssClass="Button" CausesValidation="true" OnClick="btn_generateSingle_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
