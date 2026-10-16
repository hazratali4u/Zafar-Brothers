<%@ Page Title="Rack Defination" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="frmRack.aspx.cs" Inherits="Forms_frmRack" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
   
    <div id="right_data">
        <div style="z-index: 5000; left: 612px; width: 100px; position: absolute; top: 10px; height: 100px">
            &nbsp;<asp:Panel ID="Panel21" runat="server">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                            Width="31px" />
                        Wait....
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </asp:Panel>
        </div>
        <cc1:TabContainer ID="TabContainer1" runat="server" Height="400px" Width="750px"
            ActiveTabIndex="3">
            <cc1:TabPanel ID="TabPanel3" runat="server">
                <HeaderTemplate>
                    Aisle
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div>
                                <asp:Panel ID="pnl_dept" runat="server">
                                    <fieldset>
                                        <table>
                                            <tr>
                                                <td>

                                                    <table>
                                                        <tbody>
                                                            <tr>
                                                                <td style="height: 25px" align="left" colspan="4">
                                                                    <asp:Label ID="lblerrorAisle" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                                                    <asp:HiddenField ID="HdnAisleID" runat="server" />
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td style="height: 25px" align="left">
                                                                    <strong>
                                                                        <asp:Label ID="Label6" runat="server" Width="70px" Text="Aisle" CssClass="lblbox"></asp:Label></strong>
                                                                </td>
                                                                <td style="width: 200px">
                                                                    <asp:TextBox ID="txtAisle" runat="server" Width="200px" ToolTip="Aisle"></asp:TextBox>
                                                                </td>
                                                                <td style="height: 25px">
                                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="txtAisle"
                                                                        ErrorMessage="*" Font-Bold="true" Font-Size="Medium" ValidationGroup="vgAsile"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td style="height: 25px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 70px; height: 26px"></td>
                                                                <td style="height: 25px">
                                                                    <asp:CheckBox ID="ChbAisle" runat="server" Width="97px" Text="Is Active" Font-Bold="true"
                                                                        Checked="True"></asp:CheckBox>
                                                                </td>
                                                                <td style="height: 25px" align="left"></td>
                                                                <td style="height: 25px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td style="width: 1px; height: 32px">
                                                                    <asp:Button ID="BtnAisleSave" runat="server" Width="84px" Font-Size="8pt"
                                                                        Text="Save" ValidationGroup="vgAsile" CssClass="Button" OnClick="BtnAisleSave_Click" />
                                                                    <asp:Button ID="BtnAisleCancel" runat="server" Width="83px"
                                                                        Font-Size="8pt" Text="Cancel" CssClass="Button" CausesValidation="false" OnClick="BtnAisleCancel_Click" />
                                                                </td>
                                                                <td style="height: 32px"></td>
                                                                <td style="height: 25px"></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>

                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </asp:Panel>
                                <asp:Panel ID="Panel3" runat="server" ScrollBars="Vertical" Height="234" Width="99.7%"
                                    BorderStyle="Solid" BorderColor="Silver" BorderWidth="1px">
                                    <asp:GridView ID="GrdAsile" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="gridRow2"
                                        AutoGenerateColumns="False" BackColor="White" BorderColor="SteelBlue" OnRowEditing="GrdAsile_RowEditing">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <AlternatingRowStyle BackColor="#E0E0E0" />
                                        <Columns>
                                            <asp:BoundField DataField="rackId" HeaderText="RackNo">
                                                <ItemStyle CssClass="HidePanel" />
                                                <HeaderStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="rackNo" HeaderText="Aisle No">
                                                <ItemStyle CssClass="grdDetail"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="status" HeaderText="Status">
                                                <ItemStyle Width="65px" HorizontalAlign="Center" CssClass="grdDetail" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" ToolTip="Edit">
                                                        <img alt="" id="Img2" runat="server" src="~/images/edit.gif" />
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Center" Width="40px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel1" runat="server">
                <HeaderTemplate>
                    Rack 
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                        <ContentTemplate>
                            <div>
                                <table>
                                    <tr>
                                        <td>

                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td style="height: 25px" align="left" colspan="4">
                                                            <asp:Label ID="lblErrorMsgRack" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                                            <asp:HiddenField ID="HdnRackID" runat="server" />
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td style="height: 25px" align="left">
                                                            <strong>
                                                                <asp:Label ID="Label4" runat="server" Width="70px" Text="Aisle" CssClass="lblbox"></asp:Label></strong>
                                                        </td>
                                                        <td style="width: 200px">
                                                            <%--                                                             <telerik:RadComboBox ID="DrpAisleRack" AutoPostBack="true" OnSelectedIndexChanged="DrpAisleRack_SelectedIndexChanged" runat="server" Filter="Contains" OnClientKeyPressing="(function(sender, e){ if (!sender.get_dropDownVisible()) sender.showDropDown(); })"
                                                                  Width="220px">
                                                              </telerik:RadComboBox>--%>
                                                            <asp:DropDownList ID="DrpAisleRack" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DrpAisleRack_SelectedIndexChanged" Width="200px"></asp:DropDownList>
                                                        </td>
                                                        <td style="height: 25px"></td>
                                                        <td style="height: 25px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 25px" align="left">
                                                            <strong>
                                                                <asp:Label ID="Label1" runat="server" Width="70px" Text="Rack No" CssClass="lblbox"></asp:Label></strong>
                                                        </td>
                                                        <td style="width: 200px">
                                                            <asp:TextBox ID="txtRackNo" runat="server" Width="200px" ToolTip="Rack No"></asp:TextBox>
                                                        </td>
                                                        <td style="height: 25px">
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvRackNo" ControlToValidate="txtRackNo"
                                                                ErrorMessage="*" Font-Bold="true" Font-Size="Medium" ValidationGroup="vgRack"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="height: 25px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 70px; height: 26px"></td>
                                                        <td style="height: 25px">
                                                            <asp:CheckBox ID="ChbRack" runat="server" Width="97px" Text="Is Active" Font-Bold="true"
                                                                Checked="True"></asp:CheckBox>
                                                        </td>
                                                        <td style="height: 25px" align="left"></td>
                                                        <td style="height: 25px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td style="width: 1px; height: 32px">
                                                            <asp:Button ID="btnSaveRack" OnClick="btnSaveRack_Click" runat="server" Width="84px" Font-Size="8pt"
                                                                Text="Save" ValidationGroup="vgRack" CssClass="Button" />
                                                            <asp:Button ID="btnCancelRack" OnClick="btnCancelRack_Click" runat="server" Width="83px"
                                                                Font-Size="8pt" Text="Cancel" CssClass="Button" CausesValidation="false" />
                                                        </td>
                                                        <td style="height: 32px"></td>
                                                        <td style="height: 25px"></td>
                                                    </tr>
                                                </tbody>
                                            </table>

                                        </td>
                                    </tr>
                                </table>

                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="234" Width="99.7%"
                                    BorderStyle="Solid" BorderColor="Silver" BorderWidth="1px">
                                    <asp:GridView ID="grdRack" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="gridRow2"
                                        AutoGenerateColumns="False" BackColor="White" BorderColor="SteelBlue" OnRowEditing="grdRack_RowEditing">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <AlternatingRowStyle BackColor="#E0E0E0" />
                                        <Columns>
                                            <asp:BoundField DataField="rackId" HeaderText="RackNo">
                                                <ItemStyle CssClass="HidePanel" />
                                                <HeaderStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="rackNo" HeaderText="Rack No">
                                                <ItemStyle CssClass="grdDetail"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="status" HeaderText="Status">
                                                <ItemStyle Width="65px" HorizontalAlign="Center" CssClass="grdDetail" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>

                                            <asp:BoundField DataField="ParentType" HeaderText="Aisle">
                                                <ItemStyle CssClass="HidePanel" />
                                                <HeaderStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" ToolTip="Edit">
                                                        <img alt="" id="Img2" runat="server" src="~/images/edit.gif" />
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Center" Width="40px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel4" runat="server">
                <HeaderTemplate>
                    Shelf
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div>
                                <table>
                                    <tr>
                                        <td>

                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td style="height: 25px" align="left" colspan="4">
                                                            <asp:Label ID="lblErrorShelf" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                                            <asp:HiddenField ID="HdnShelfID" runat="server" />
                                                        </td>
                                                    </tr>



                                                    <tr>
                                                        <td style="height: 25px" align="left">
                                                            <strong>
                                                                <asp:Label ID="Label7" runat="server" Width="70px" Text="Aisle" CssClass="lblbox"></asp:Label></strong>
                                                        </td>
                                                        <td style="width: 200px">
                                                            <asp:DropDownList ID="DrpAisleShelf" Width="200px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DrpAisleShelf_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="height: 25px"></td>
                                                        <td style="height: 25px"></td>
                                                    </tr>


                                                    <tr>
                                                        <td style="height: 25px" align="left">
                                                            <strong>
                                                                <asp:Label ID="Label8" runat="server" Width="70px" Text="Rack" CssClass="lblbox"></asp:Label></strong>
                                                        </td>
                                                        <td style="width: 200px">
                                                            <asp:DropDownList Width="200px" ID="DrpRackShelf" AutoPostBack="true" runat="server"
                                                                OnSelectedIndexChanged="DrpRackShelf_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="height: 25px"></td>
                                                        <td style="height: 25px"></td>
                                                    </tr>

                                                    <tr>
                                                        <td style="height: 25px" align="left">
                                                            <strong>
                                                                <asp:Label ID="Label5" runat="server" Width="70px" Text="Shelf" CssClass="lblbox"></asp:Label></strong>
                                                        </td>
                                                        <td style="width: 200px">
                                                            <asp:TextBox ID="txtShelf" runat="server" Width="200px" ToolTip="Shelf"></asp:TextBox>
                                                        </td>
                                                        <td style="height: 25px">
                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtShelf"
                                                                ErrorMessage="*" Font-Bold="true" Font-Size="Medium" ValidationGroup="vgShelf"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="height: 25px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 70px; height: 26px"></td>
                                                        <td style="height: 25px">
                                                            <asp:CheckBox ID="chbShelf" runat="server" Width="97px" Text="Is Active" Font-Bold="true"
                                                                Checked="True"></asp:CheckBox>
                                                        </td>
                                                        <td style="height: 25px" align="left"></td>
                                                        <td style="height: 25px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td style="width: 1px; height: 32px">
                                                            <asp:Button ID="BtnSaveShelf" runat="server" Width="84px" Font-Size="8pt"
                                                                Text="Save" ValidationGroup="vgShelf" CssClass="Button" OnClick="btnSaveShelf_Click" />
                                                            <asp:Button ID="BtncancelShelf" runat="server" Width="83px"
                                                                Font-Size="8pt" Text="Cancel" CssClass="Button" CausesValidation="false" OnClick="BtncancelShelf_Click" />
                                                        </td>
                                                        <td style="height: 32px"></td>
                                                        <td style="height: 25px"></td>
                                                    </tr>
                                                </tbody>
                                            </table>

                                        </td>
                                    </tr>
                                </table>

                                <asp:Panel ID="Panel4" runat="server" ScrollBars="Vertical" Height="234" Width="99.7%"
                                    BorderStyle="Solid" BorderColor="Silver" BorderWidth="1px">
                                    <asp:GridView ID="GrdShelf" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="gridRow2"
                                        AutoGenerateColumns="False" BackColor="White" BorderColor="SteelBlue" OnRowEditing="GrdShelf_RowEditing">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <AlternatingRowStyle BackColor="#E0E0E0" />
                                        <Columns>
                                            <asp:BoundField DataField="rackId" HeaderText="RackNo">
                                                <ItemStyle CssClass="HidePanel" />
                                                <HeaderStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="rackNo" HeaderText="Shelf No">
                                                <ItemStyle CssClass="grdDetail"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="status" HeaderText="Status">
                                                <ItemStyle Width="65px" HorizontalAlign="Center" CssClass="grdDetail" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>

                                            <asp:BoundField DataField="Parenttype" HeaderText="RackNo">
                                                <ItemStyle CssClass="HidePanel" />
                                                <HeaderStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" ToolTip="Edit">
                                                        <img alt="" id="Img2" runat="server" src="~/images/edit.gif" />
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Center" Width="40px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </cc1:TabPanel>

            <cc1:TabPanel ID="TabPanel2" runat="server">
                <HeaderTemplate>
                    Item Placement
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td style="height: 25px" align="left" colspan="4">
                                        <asp:Label ID="lblErrorMsg2" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                        <asp:HiddenField ID="HdnrackPlacementID" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" width="70px">
                                        <strong>
                                            <asp:Label ID="Label2" runat="server" CssClass="lblbox" Text="Location" Width="61px"></asp:Label></strong>
                                    </td>
                                    <td style="height: 25px" align="left">

                                        <%-- <telerik:RadComboBox ID="drpDistributor" runat="server" Width="220px">
                                        </telerik:RadComboBox>--%>
                                        <asp:DropDownList ID="drpDistributor" runat="server" Width="200px"></asp:DropDownList>
                                    </td>
                                    <td style="height: 25px"></td>
                                    <td style="height: 25px"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Aisle</strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DrpAisle" runat="server" Width="200px" AutoPostBack ="true"
                                             OnSelectedIndexChanged="DrpAisle_SelectedIndexChanged"></asp:DropDownList>
                                        <%-- <telerik:RadComboBox ID="DrpAisle" runat="server" Filter="Contains" OnClientKeyPressing="(function(sender, e){ if (!sender.get_dropDownVisible()) sender.showDropDown(); })"
                                            Width="220px">
                                        </telerik:RadComboBox>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Rack No</strong>
                                    </td>
                                    <td>

                                        <%--   <telerik:RadComboBox ID="DrpRack" runat="server" Filter="Contains" OnClientKeyPressing="(function(sender, e){ if (!sender.get_dropDownVisible()) sender.showDropDown(); })"
                                            Width="220px">
                                        </telerik:RadComboBox>--%>
                                        <asp:DropDownList ID="DrpRack" runat="server" AutoPostBack="true" Width="200px" OnSelectedIndexChanged="DrpRack_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                </tr>


                                <tr>
                                    <td>
                                        <strong>Shelf</strong>
                                    </td>
                                    <td>

                                        <%--  <telerik:RadComboBox ID="DrpShelf" runat="server" Filter="Contains" OnClientKeyPressing="(function(sender, e){ if (!sender.get_dropDownVisible()) sender.showDropDown(); })"
                                            Width="220px">
                                        </telerik:RadComboBox>--%>
                                        <asp:DropDownList ID="DrpShelf" runat="server" AutoPostBack="true" Width="200px" OnSelectedIndexChanged="DrpShelf_SelectedIndexChanged"></asp:DropDownList>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 70px;">
                                        <strong>Item Name</strong>
                                    </td>
                                    <td>

                                        <%--                                        <telerik:RadComboBox ID="ddlSkus" runat="server" Filter="Contains" OnClientKeyPressing="(function(sender, e){ if (!sender.get_dropDownVisible()) sender.showDropDown(); })"
                                            Width="220px">
                                        </telerik:RadComboBox>--%>

                                        <asp:DropDownList ID="ddlSkus" runat="server" Width="200px"></asp:DropDownList>

                                    </td>
                                </tr>

                                <tr>
                                    <td style="width: 70px; height: 26px"></td>
                                    <td style="height: 25px">
                                        <asp:CheckBox ID="chkIsStatus" runat="server" Width="97px" Text="Is Active" Checked="True"></asp:CheckBox>
                                    </td>
                                    <td style="height: 25px" align="left"></td>
                                    <td style="height: 25px"></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td style="width: 1px; height: 32px">
                                        <asp:Button ID="btnSavePlacement" OnClick="btnSavePlacement_Click" runat="server"
                                            Width="84px" Font-Size="8pt" Text="Save" CssClass="Button" />
                                        <asp:Button ID="btnCancelPlacement" OnClick="btnCancelPlacement_Click" runat="server"
                                            Width="83px" Font-Size="8pt" Text="Cancel" CssClass="Button" CausesValidation="false" />
                                    </td>
                                    <td style="height: 32px"></td>
                                    <td style="height: 25px"></td>
                                </tr>
                            </table>
                            <table class="tblhead" style="width: 100%;">
                                <tbody>
                                    <tr>
                                        <td style="color: White; font-weight: bold;">
                                            <asp:Label ID="Label3" runat="server" Width="125px" Text="Select Search Type"></asp:Label>
                                        </td>
                                        <td style="width: 140px; height: 21px">
                                            <asp:DropDownList ID="ddSearchType2" runat="server" Width="158px" CssClass="DropList">
                                                <asp:ListItem Value="SKU_code">All Records</asp:ListItem>
                                                <asp:ListItem Value="rackNo">Rack No</asp:ListItem>
                                                <asp:ListItem Value="skuName">Sku Name</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 224px; height: 21px">
                                            <asp:TextBox ID="txtSearchPlacement" runat="server" Width="160px" CssClass="txtBox "></asp:TextBox>
                                        </td>
                                        <td style="height: 21px; width: 250px">
                                            <telerik:RadButton ID="btnFilterPlacement" runat="server" Width="70px" Text="Filter"
                                                OnClick="btnFilterPlacement_Click" CausesValidation="false" />
                                        </td>
                                        <td style="height: 21px; width: 120px"></td>
                                    </tr>
                                </tbody>
                            </table>
                            <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Height="100" Width="99.7%"
                                BorderStyle="Solid" BorderColor="Silver" BorderWidth="1px">
                                <asp:GridView ID="grdPlacement" runat="server" Width="100%" ForeColor="SteelBlue"
                                    CssClass="gridRow2" AutoGenerateColumns="False" BackColor="White" BorderColor="SteelBlue"
                                    OnRowEditing="grdPlacement_RowEditing">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <AlternatingRowStyle BackColor="#E0E0E0" />
                                    <Columns>
                                        <asp:BoundField DataField="distributorId" HeaderText="DISTRIBUTOR_ID">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="rackId" HeaderText="RackId">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="skuId" HeaderText="skuId">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        
                                        <asp:BoundField DataField="rackPlaceId" HeaderText="rackPlaceId">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="skuName" HeaderText="Item Description">
                                            <ItemStyle CssClass="grdDetail"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="rackNo" HeaderText="Rack No">
                                            <ItemStyle CssClass="grdDetail"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="status" HeaderText="Status">
                                            <ItemStyle Width="65px" HorizontalAlign="Center" CssClass="grdDetail"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ShelfId" HeaderText="RackId">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AisleId" HeaderText="RackId">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                           <asp:BoundField DataField="sku_code" HeaderText="sku_code">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" ToolTip="Edit">
                                                    <img alt="" id="Img2" runat="server" src="~/images/edit.gif" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="grdDetail" HorizontalAlign="Center" Width="40px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                </asp:GridView>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
    </div>
</asp:Content>
