<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmTransferOutIn.aspx.cs" Inherits="Forms_frmTransferOutIn" Title="CORN :: Transfer In" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <div id="right_data">
        <div style="z-index: 101; left: 597px; width: 100px; position: absolute; top: 109px;
            height: 100px">
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
                    <td align="left">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td width="5px">
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblfromLocation" runat="server" Width="94px" Text="Transfer From"
                                                        CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" AutoPostBack="true"
                                                    CssClass="DropList" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5px">
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <label>
                                                        Document No</label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpDocumentNo" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5px">
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label2" runat="server" Width="94px" Text="Transfer To" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="DrpTransferFor" runat="server" Width="200px" CssClass="DropList"
                                                    >
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <asp:Label ID="Label1" runat="server" Visible="false" Width="94px" Text="Supplier"
                                            CssClass="lblbox"></asp:Label></strong>
                                        <asp:DropDownList ID="drpPrincipal" runat="server" Width="200px" CssClass="DropList"
                                            AutoPostBack="True" Enabled="False" Visible="false">
                                        </asp:DropDownList>
                                        <tr>
                                            <td width="5px">
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <label>
                                                        Driver Name</label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:TextBox ID="txtDocumentNo" runat="server" Width="195px" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5px">
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <label>
                                                        Builty No<label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:TextBox ID="txtBuiltyNo" runat="server" Width="195px" CssClass="txtBox"></asp:TextBox>
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
        <div>
            <table width="100%">
                <tr>
                    <td width="5px">
                    </td>
                    <td align="left" style="height: 220px">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table width="50%">
                                    <tbody>
                                        <tr >
                                            <td>
                                                <asp:Label ID="lblskuCode" runat="server" Width="170px" Text="  Item Code" CssClass="label"></asp:Label>
                                            </td>
                                            <td style="height: 16px">
                                                <asp:Label ID="lblskuname" runat="server" Width="216px" Text="   Item Name" CssClass="label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblquantity" runat="server" Width="80px" Text="Quantity" CssClass="label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr >
                                            <td>
                                                <asp:TextBox ID="txtskuCode" Enabled="false" runat="server" Width="172px" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtskuName" runat="server" Width="218px" Font-Bold="True" CssClass="txtBox"
                                                    Enabled="False"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQuantity" runat="server" Width="82px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="100px"
                                                    Font-Size="8pt" Text="Add Item" ValidationGroup="vg" CssClass="Button" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="6">
                                                <asp:Panel ID="Panel2" runat="server" Width="590px" Height="200px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="GrdPurchase" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                        Width="570px" OnRowEditing="GrdPurchase_RowEditing">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous" />
                                                        <RowStyle ForeColor="Black" />
                                                        <AlternatingRowStyle BackColor="#E0E0E0" />
                                                        <Columns>
                                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_CODE" HeaderText="Item Code">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                                                                    Width="85px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" HeaderText="Item Name">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                                                                    Width="205px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
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
                                &nbsp;&nbsp;
                                <asp:Button AccessKey="S" ID="btnTransferIn" runat="server" Width="90px" Text="Transfer In"
                                    UseSubmitBehavior="False" OnClick="btnTransferIn_Click" Font-Size="8pt" CssClass="Button">
                                </asp:Button>&nbsp;
                                <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="90px" Text="Cancel"
                                    UseSubmitBehavior="False" Font-Size="8pt" CssClass="Button" OnClick="btnCancel_Click">
                                </asp:Button>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 100px;">
                    </td>
                </tr>
            </table>
            &nbsp;
        </div>
    </div>
</asp:Content>
