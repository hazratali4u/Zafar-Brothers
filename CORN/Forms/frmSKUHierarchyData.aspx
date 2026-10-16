<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSKUHierarchyData.aspx.cs" Inherits="frmSKUHierarchyData" Title="CORN :: Item Hierarchy" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {

            $('#<%=txtNTN.ClientID %>').mask("9999999-9");
            $('#<%=txtSTRN.ClientID %>').mask("99-99-9999-999-99");
            $("select").searchable();
        }

    </script>
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <cc1:TabContainer ID="TabContainer1" runat="server" Height="420px" Width="650px"
                        ActiveTabIndex="4">
                        <cc1:TabPanel ID="TabPanel1" runat="server">
                            <HeaderTemplate>
                                Supplier/Vendor
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 100px"></td>
                                        <td style="width: 100px">
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                    <asp:Panel ID="pnl_dept" runat="server">
                                                        <fieldset>
                                                            <table width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 100px"></td>
                                                                        <td colspan="2">
                                                                            <asp:Label ID="lblErrorMsgPrincipal" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label><br />
                                                                        </td>
                                                                        <td style="width: 100px">
                                                                            <asp:HiddenField ID="HFPrincipalID" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 100px; height: 28px"></td>
                                                                        <td style="width: 49px; height: 28px">
                                                                            <strong>
                                                                                <asp:Label ID="Label3" runat="server" Width="90px" Text="Country"></asp:Label></strong>
                                                                        </td>
                                                                        <td style="width: 100px; height: 28px">
                                                                            <asp:DropDownList ID="drpCountry" runat="server" Width="200px" CssClass="DropList">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td style="width: 100px; height: 28px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 100px; height: 28px"></td>
                                                                        <%--<td style="width: 49px; height: 28px">--%>
                                                                        <strong>
                                                                            <asp:Label ID="Label1" runat="server" Width="82px" Text="Supplier Code" Visible="False"></asp:Label></strong>
                                                                        <%--  </td>
                                                                <td style="width: 100px; height: 28px">--%>
                                                                        <asp:TextBox ID="txtPrincipalCode" runat="server" Width="175px" Enabled="False" CssClass="txtBox"
                                                                            Visible="False"></asp:TextBox>
                                                                        <%-- </td>--%>
                                                                        <td style="width: 49px; height: 29px">
                                                                            <strong>
                                                                                <asp:Label ID="Label2" runat="server" Width="96px" Text="Supplier Name"></asp:Label></strong>
                                                                        </td>
                                                                        <td style="width: 100px; height: 29px">
                                                                            <asp:TextBox ID="txtPrincipalName" runat="server" Width="200px" Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 100px; height: 28px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 100px; height: 28px"></td>
                                                                        <td style="width: 49px; height: 28px">
                                                                            <strong>
                                                                                <asp:Label ID="Label4" runat="server" Width="82px" Text="Email"></asp:Label></strong>
                                                                        </td>
                                                                        <td style="width: 100px; height: 28px">
                                                                            <asp:TextBox ID="txtEmail" runat="server" Width="175px" Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 49px; height: 29px">
                                                                            <strong>
                                                                                <asp:Label ID="Label5" runat="server" Width="96px" Text="Phone No"></asp:Label></strong>
                                                                        </td>
                                                                        <td style="width: 100px; height: 29px">
                                                                            <asp:TextBox ID="txtPhoneNo" runat="server" Width="175px" Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 100px; height: 28px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 100px; height: 28px"></td>
                                                                        <td style="width: 49px; height: 28px">
                                                                            <strong>
                                                                                <asp:Label ID="Label6" runat="server" Width="82px" Text="Skype"></asp:Label></strong>
                                                                        </td>
                                                                        <td style="width: 100px; height: 28px">
                                                                            <asp:TextBox ID="txtSkype" runat="server" Width="175px" Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 49px; height: 29px">
                                                                            <strong>
                                                                                <asp:Label ID="Label9" runat="server" Width="96px" Text="Contact Person"></asp:Label></strong>
                                                                        </td>
                                                                        <td style="width: 100px; height: 29px">
                                                                            <asp:TextBox ID="txtContactPerson" runat="server" Width="175px" Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 100px; height: 28px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 100px; height: 29px"></td>
                                                                        <td style="width: 49px; height: 29px">
                                                                            <strong>
                                                                                <asp:Label ID="Label13" runat="server" Width="96px" Text="NTN" __designer:wfdid="w192"></asp:Label></strong>
                                                                        </td>
                                                                        <td style="width: 100px; height: 29px">
                                                                            <asp:TextBox ID="txtNTN" runat="server" Width="175px" __designer:wfdid="w193" Enabled="False"
                                                                                CssClass="txtBox "></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 49px; height: 29px">
                                                                            <strong>
                                                                                <asp:Label ID="Label14" runat="server" Width="96px" Text="STRN" __designer:wfdid="w194"></asp:Label></strong>
                                                                        </td>
                                                                        <td style="width: 100px; height: 29px">
                                                                            <asp:TextBox ID="txtSTRN" runat="server" Width="175px" Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 100px; height: 29px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 100px; height: 29px"></td>
                                                                        <td style="width: 49px; height: 29px">
                                                                            <strong>
                                                                                <asp:Label ID="Label11" runat="server" Width="96px" Text="Address"></asp:Label></strong>
                                                                        </td>
                                                                        <td style="width: 100px; height: 29px" colspan="3">
                                                                            <asp:TextBox ID="txtAddress" runat="server" Width="481px" Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 100px; height: 29px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 100px; height: 37px"></td>
                                                                        <td style="width: 49px; height: 37px" align="left"></td>
                                                                        <td style="width: 100px; height: 37px">
                                                                            <asp:CheckBox ID="ChIsMunalDiscount" runat="server" Visible="false" Checked="true"
                                                                                Width="190px" Text="Is Manual Dicount"></asp:CheckBox>
                                                                        </td>
                                                                        <td style="width: 100px">
                                                                            <asp:Button ID="btnSavePrincipal" OnClick="btnSavePrincipal_Click" runat="server"
                                                                                Width="85px" Font-Size="8pt" Text="New" CssClass="Button" />
                                                                        </td>
                                                                        <td style="width: 100px; height: 37px"></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </fieldset>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div style="z-index: 101; top: 59%; left: 50%; position: absolute;">
                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel6">
                                                    <ProgressTemplate>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" Width="33px" Height="31px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"></asp:ImageButton>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </div>
                                        </td>
                                        <td style="width: 100px"></td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">
                                            <asp:Panel ID="Panel1" runat="server" Height="127px" ScrollBars="Vertical" Width="100%">
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="GrdPrincipal" runat="server" Width="99%" ForeColor="SteelBlue"
                                                            BorderWidth="1px" Font-Size="9pt" CssClass="gridRow2" BorderColor="White" BackColor="White"
                                                            HorizontalAlign="Center" AutoGenerateColumns="False" OnRowEditing="GrdPrincipal_RowEditing"
                                                            OnRowDeleting="GrdPrincipal_RowDeleting" OnPageIndexChanging="GrdPrincipal_PageIndexChanging">
                                                            <PagerSettings PreviousPageText="Previous" Mode="NextPrevious" LastPageText="" FirstPageText=""
                                                                NextPageText="Next"></PagerSettings>
                                                              <AlternatingRowStyle BackColor="#E0E0E0" />
                                                             <Columns>
                                                                <asp:BoundField DataField="SKU_HIE_ID" HeaderText="ID">
                                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left"
                                                                        Width="50px"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_HIE_CODE" HeaderText="Code">
                                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Supplier">
                                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left"
                                                                        Width="200px"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="COUNTRY" HeaderText="Country">
                                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left"
                                                                        Width="150px"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ADDRESS" HeaderText="ADDRESS">
                                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="NTN" HeaderText="NTN">
                                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="STRN" HeaderText="STRN">
                                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="COUNTRY_ID" HeaderText="COUNTRY_ID">
                                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="EMAIL" HeaderText="Email">
                                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left"
                                                                        Width="100px"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PHONE_NO" HeaderText="PHONE_NO">
                                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKYPE_ADDRESS" HeaderText="SKYPE_ADDRESS">
                                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CONTACT_PERSON" HeaderText="CONTACT_PERSON">
                                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IS_MANUALDISCOUNT" HeaderText="Manual Discount">
                                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Edit">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Delete">
                                                                    <img src="../images/edit.gif" />
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="45px" HorizontalAlign="Center" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDelete" ToolTip="Delete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                            CommandName="Delete">
                                                                                    <img src="../images/delete.gif" width="16" height="16">
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="45px" HorizontalAlign="Center"></ItemStyle>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                                            <AlternatingRowStyle CssClass="GridAlternateRowStyle"></AlternatingRowStyle>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel2" runat="server">
                            <HeaderTemplate>
                                Gender
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 100px;"></td>
                                        <td align="center">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel6" runat="server">
                                                        <fieldset>
                                                            <table width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 100px; height: 8px"></td>
                                                                        <td style="height: 8px" align="left" colspan="2">
                                                                            <asp:Label ID="lblErrorMsgGender" runat="server" ForeColor="Red" Font-Bold="True"
                                                                                __designer:wfdid="w16"></asp:Label><br />
                                                                        </td>
                                                                        <td style="width: 100px; height: 8px">
                                                                            <asp:HiddenField ID="HFGenderID" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 100px; height: 8px"></td>
                                                                        <td style="width: 159px; height: 8px"></td>
                                                                        <td style="width: 100px; height: 8px"></td>
                                                                        <td style="width: 100px; height: 8px"></td>
                                                                    </tr>
                                                                    <strong>
                                                                        <asp:Label ID="Label12" runat="server" Width="80px" Text="Gender Code" Visible="False"></asp:Label></strong>
                                                                    <asp:TextBox ID="txtGenderCode" runat="server" Width="194px" Enabled="False" CssClass="txtBox"
                                                                        Visible="False"></asp:TextBox>
                                                                    <tr>
                                                                        <td style="width: 100px; height: 29px"></td>
                                                                        <td style="width: 159px; height: 29px" align="left">
                                                                            <strong>
                                                                                <asp:Label ID="Label21" runat="server" Width="85px" Text="Gender Name" __designer:wfdid="w21"></asp:Label></strong>
                                                                        </td>
                                                                        <td style="width: 100px; height: 29px">
                                                                            <asp:TextBox ID="txtGenderName" runat="server" Width="194px" __designer:wfdid="w22"
                                                                                Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 100px; height: 29px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 100px; height: 37px"></td>
                                                                        <td style="width: 159px; height: 37px" align="right">&nbsp;
                                                                        </td>
                                                                        <td style="width: 100px; height: 37px" align="left">
                                                                            <asp:Button ID="btnSaveGender" OnClick="btnSaveGender_Click" runat="server" Width="85px"
                                                                                Font-Size="8pt" Text="New" CssClass="Button" />
                                                                        </td>
                                                                        <td style="width: 100px; height: 37px"></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </fieldset>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                                <ProgressTemplate>
                                                    <asp:Image ID="Image2" runat="server" Width="30px" Height="28px" __designer:wfdid="w25"
                                                        ImageUrl="~/App_Themes/Granite/Images/image003.gif"></asp:Image>&nbsp; Loading
                                                    .........
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                        <td style="width: 100px;"></td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">
                                            <asp:Panel ID="Panel2" runat="server" Height="245px" ScrollBars="Vertical" Width="100%">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="GrdGender" runat="server" Width="99%" ForeColor="SteelBlue" Font-Size="9pt"
                                                            __designer:wfdid="w28" CssClass="gridRow2" BorderColor="White" BackColor="White"
                                                            HorizontalAlign="Center" AutoGenerateColumns="False" OnRowEditing="GrdGender_RowEditing"
                                                            OnRowDeleting="GrdGender_RowDeleting" OnPageIndexChanging="GrdGender_PageIndexChanging">
                                                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                                PreviousPageText="Previous"></PagerSettings>
                                                              <AlternatingRowStyle BackColor="#E0E0E0" />
                                                             <Columns>
                                                                <asp:BoundField DataField="SKU_HIE_ID" HeaderText="ID">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" Width="50px" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_HIE_CODE" HeaderText="Code">
                                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Gender Name">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Edit">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Delete">
                                                                    <img src="../images/edit.gif" />
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="45px" HorizontalAlign="Center" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDelete" ToolTip="Delete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                            CommandName="Delete">
                                                                                    <img src="../images/delete.gif" width="16" height="16">
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="45px" HorizontalAlign="Center"></ItemStyle>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel3" runat="server">
                            <HeaderTemplate>
                                Category
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 100px"></td>
                                        <td align="center" style="width: 100px">
                                            <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel7" runat="server">
                                                        <fieldset>
                                                            <table width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 100px; height: 16px"></td>
                                                                        <td style="height: 16px" align="left" colspan="2">
                                                                            <br />
                                                                            <asp:Label ID="lblErrorMsgCategory" runat="server" ForeColor="Red" Font-Bold="True"
                                                                                __designer:wfdid="w114"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 100px; height: 16px">
                                                                            <asp:HiddenField ID="HFCategoryID" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 100px; height: 8px"></td>
                                                                        <td style="width: 69px; height: 8px"></td>
                                                                        <td style="width: 100px; height: 8px"></td>
                                                                        <td style="width: 100px; height: 8px"></td>
                                                                    </tr>
                                                                    <asp:Label ID="Label121" runat="server" Width="90px" Text="Category Code" Visible="False"></asp:Label>
                                                                    <asp:TextBox ID="txtCategoryCode" runat="server" Width="194px" Enabled="False" CssClass="txtBox "
                                                                        Visible="False"></asp:TextBox>
                                                                    <tr>
                                                                        <td style="width: 100px; height: 29px"></td>
                                                                        <td style="width: 69px; height: 29px" align="left">
                                                                            <strong>
                                                                                <asp:Label ID="Label211" runat="server" Width="90px" Text="Category Name" __designer:wfdid="w121"></asp:Label></strong>
                                                                        </td>
                                                                        <td style="width: 100px; height: 29px">
                                                                            <asp:TextBox ID="txtCategoryName" runat="server" Width="194px" __designer:wfdid="w122"
                                                                                Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 100px; height: 29px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 100px; height: 37px"></td>
                                                                        <td style="width: 69px; height: 37px" align="right">&nbsp;
                                                                        </td>
                                                                        <td style="width: 100px; height: 37px" align="left">
                                                                            <asp:Button ID="btnSaveCategory" OnClick="btnSaveCategory_Click" runat="server" Width="85px"
                                                                                Font-Size="8pt" Text="New" CssClass="Button" />
                                                                        </td>
                                                                        <td style="width: 100px; height: 37px"></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </fieldset>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel21">
                                                <ProgressTemplate>
                                                    <asp:Image ID="Image3" runat="server" Width="30px" Height="28px" __designer:wfdid="w125"
                                                        ImageUrl="~/App_Themes/Granite/Images/image003.gif"></asp:Image>&nbsp; Loading
                                                    ......
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                        <td style="width: 100px"></td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">
                                            <asp:Panel ID="Panel3" runat="server" Height="200px" ScrollBars="Vertical" Width="100%">
                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="GrdCategory" runat="server" Width="99%" ForeColor="SteelBlue" Font-Size="9pt"
                                                            __designer:wfdid="w128" CssClass="gridRow2" BorderColor="White" BackColor="White"
                                                            HorizontalAlign="Center" AutoGenerateColumns="False" OnRowEditing="GrdCategory_RowEditing"
                                                            OnRowDeleting="GrdCategory_RowDeleting" OnPageIndexChanging="GrdCategory_PageIndexChanging">
                                                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                                PreviousPageText="Previous"></PagerSettings>
                                                              <AlternatingRowStyle BackColor="#E0E0E0" />
                                                             <Columns>
                                                                <asp:BoundField DataField="SKU_HIE_ID" HeaderText="ID">
                                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left"
                                                                        Width="50px"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_HIE_CODE" HeaderText="Code">
                                                                    <ItemStyle CssClass="HidePanel" />
                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Name">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PARENT_SKU_HIE_ID" HeaderText="PARENT_SKU_HIE_ID">
                                                                    <ItemStyle CssClass="HidePanel" />
                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Edit">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Delete">
                                                                    <img src="../images/edit.gif" />
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="45px" HorizontalAlign="Center" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDelete" ToolTip="Delete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                            CommandName="Delete">
                                                                                    <img src="../images/delete.gif" width="16" height="16">
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="45px" HorizontalAlign="Center"></ItemStyle>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel6" runat="server">
                            <HeaderTemplate>
                                Type
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 100px"></td>
                                        <td align="center" style="width: 100px">
                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel10" runat="server">
                                                        <fieldset>
                                                            <table width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 100px; height: 16px"></td>
                                                                        <td style="height: 16px" align="left" colspan="2">
                                                                            <br />
                                                                            <asp:Label ID="lblErrorMsgType" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 100px; height: 16px">
                                                                            <asp:HiddenField ID="HFTypeID" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 100px; height: 8px"></td>
                                                                        <td style="width: 69px; height: 8px"></td>
                                                                        <td style="width: 100px; height: 8px"></td>
                                                                        <td style="width: 100px; height: 8px"></td>
                                                                    </tr>
                                                                    <asp:Label ID="Label16" runat="server" Width="90px" Text="Type Code" Visible="False"></asp:Label>
                                                                    <asp:TextBox ID="txtTypeCode" runat="server" Width="194px" Enabled="False" CssClass="txtBox "
                                                                        Visible="False"></asp:TextBox>
                                                                    <tr>
                                                                        <td style="width: 100px; height: 29px"></td>
                                                                        <td style="width: 69px; height: 29px" align="left">
                                                                            <strong>
                                                                                <asp:Label ID="Label17" runat="server" Width="90px" Text="Type"></asp:Label></strong>
                                                                        </td>
                                                                        <td style="width: 100px; height: 29px">
                                                                            <asp:TextBox ID="txtTypeName" runat="server" Width="194px" Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 100px; height: 29px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 100px; height: 37px"></td>
                                                                        <td style="width: 69px; height: 37px" align="right">&nbsp;
                                                                        </td>
                                                                        <td style="width: 100px; height: 37px" align="left">
                                                                            <asp:Button ID="btnSaveType" OnClick="btnSaveType_Click" runat="server" Width="85px"
                                                                                Font-Size="8pt" Text="New" CssClass="Button" />
                                                                        </td>
                                                                        <td style="width: 100px; height: 37px"></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </fieldset>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="UpdatePanel10">
                                                <ProgressTemplate>
                                                    <asp:Image ID="Image3" runat="server" Width="30px" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"></asp:Image>&nbsp; Loading ......
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                        <td style="width: 100px"></td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">
                                            <asp:Panel ID="Panel11" runat="server" Height="200px" ScrollBars="Vertical" Width="100%">
                                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="GrdType" runat="server" Width="99%" ForeColor="SteelBlue" Font-Size="9pt"
                                                            CssClass="gridRow2" BorderColor="White" BackColor="White" HorizontalAlign="Center"
                                                            AutoGenerateColumns="False" OnRowEditing="GrdType_RowEditing" OnRowDeleting="GrdType_RowDeleting">
                                                              <AlternatingRowStyle BackColor="#E0E0E0" />
                                                             <Columns>
                                                                <asp:BoundField DataField="SKU_HIE_ID" HeaderText="ID">
                                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left"
                                                                        Width="50px"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_HIE_CODE" HeaderText="Code">
                                                                    <ItemStyle CssClass="HidePanel" />
                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Type Name">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PARENT_SKU_HIE_ID" HeaderText="PARENT_SKU_HIE_ID">
                                                                    <ItemStyle CssClass="HidePanel" />
                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Edit">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Delete">
                                                                    <img src="../images/edit.gif" />
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="45px" HorizontalAlign="Center" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDelete" ToolTip="Delete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                            CommandName="Delete">
                                                                                    <img src="../images/delete.gif" width="16" height="16">
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="45px" HorizontalAlign="Center"></ItemStyle>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel4" runat="server">
                            <HeaderTemplate>
                                Country
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 100px"></td>
                                        <td align="center" style="width: 100px">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel8" runat="server">
                                                        <fieldset>
                                                            <table width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:Label ID="lblErrorMsgCountry" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label><br />
                                                                        </td>
                                                                        <td style="width: 100px">
                                                                            <asp:HiddenField ID="HFCountryID" runat="server" />
                                                                            <asp:HiddenField ID="HFCountryTypeID" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <strong>
                                                                        <asp:Label ID="Label7" runat="server" Width="90px" Text="Country Code" Visible="False"></asp:Label></strong>
                                                                    <asp:TextBox ID="txtCountryCode" runat="server" Width="194px" Enabled="False" CssClass="txtBox "
                                                                        Visible="False"></asp:TextBox>
                                                                    <tr>
                                                                        <td style="width: 100px; height: 29px" align="left">
                                                                            <strong>
                                                                                <asp:Label ID="Label8" runat="server" Width="90px" Text="Country Name"></asp:Label></strong>
                                                                        </td>
                                                                        <td style="width: 100px; height: 29px">
                                                                            <asp:TextBox ID="txtCountryName" runat="server" Width="194px" Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 100px; height: 29px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 100px; height: 37px"></td>
                                                                        <td style="width: 100px; height: 37px" align="left">
                                                                            <asp:Button AccessKey="B" ID="btnSaveCountry" OnClick="btnSaveCountry_Click" runat="server"
                                                                                Width="85px" Font-Size="8pt" Text="New" CssClass="Button" />
                                                                        </td>
                                                                        <td style="width: 100px; height: 37px"></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </fieldset>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel4">
                                                <ProgressTemplate>
                                                    <asp:Image ID="Image4" runat="server" Width="24px" Height="23px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"></asp:Image>&nbsp; Loading.....
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                        <td style="width: 100px"></td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">
                                            <asp:Panel ID="Panel4" runat="server" Height="285px" ScrollBars="Vertical" Width="100%">
                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="GrdCountry" runat="server" Width="99%" ForeColor="SteelBlue" Font-Size="9pt"
                                                            __designer:wfdid="w164" CssClass="gridRow2" BorderColor="White" BackColor="White"
                                                            HorizontalAlign="Center" AutoGenerateColumns="False" OnRowEditing="GrdCountry_RowEditing"
                                                            OnRowDeleting="GrdCountry_RowDeleting" OnPageIndexChanging="GrdCountry_PageIndexChanging">
                                                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                                PreviousPageText="Previous"></PagerSettings>
                                                               <AlternatingRowStyle BackColor="#E0E0E0" />
                                                            <Columns>
                                                                <asp:BoundField DataField="SKU_HIE_ID" HeaderText="ID">
                                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                </asp:BoundField>

                                                                <asp:BoundField DataField="TYPE_ID" HeaderText="ID">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver"
                                                                        BorderWidth="1px" BorderStyle="Solid" Width="50px"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" />


                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_HIE_CODE" HeaderText="Code">
                                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Name">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>

                                                                <asp:BoundField DataField="TYPE_ID" HeaderText="TYPE_ID">
                                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Edit">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Delete">
                                                                    <img src="../images/edit.gif" />
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="45px" HorizontalAlign="Center" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDelete" ToolTip="Delete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                            CommandName="Delete">
                                                                                    <img src="../images/delete.gif" width="16" height="16">
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="45px" HorizontalAlign="Center"></ItemStyle>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel5" runat="server">
                            <HeaderTemplate>
                                Color
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 100px"></td>
                                        <td align="center" style="width: 100px">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel9" runat="server">
                                                        <fieldset>
                                                            <table width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:Label ID="lblErrorMsgColor" runat="server" ForeColor="Red" Font-Bold="True"
                                                                                __designer:wfdid="w148"></asp:Label><br />
                                                                        </td>
                                                                        <td style="width: 100px">
                                                                            <asp:HiddenField ID="HFColorID" runat="server" />
                                                                            <asp:HiddenField ID="HfColorTypeID" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <asp:Label ID="Label19" runat="server" Width="90px" Text="Color Code" Visible="False"></asp:Label>
                                                                        <asp:TextBox ID="txtColorCode" runat="server" Width="194px" Enabled="False" CssClass="txtBox "
                                                                            Visible="False"></asp:TextBox>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 29px" align="left">
                                                                                <strong>
                                                                                    <asp:Label ID="Label20" runat="server" Width="90px" Text="Color Name"></asp:Label></strong>
                                                                            </td>
                                                                            <td style="width: 100px; height: 29px">
                                                                                <asp:TextBox ID="txtColorName" runat="server" Width="194px" Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 100px; height: 29px"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 37px"></td>
                                                                            <td style="width: 100px; height: 37px" align="left">
                                                                                <asp:Button AccessKey="B" ID="btnSaveColor" OnClick="btnSaveColor_Click" runat="server"
                                                                                    Width="85px" Font-Size="8pt" Text="New" CssClass="Button" />
                                                                            </td>
                                                                            <td style="width: 100px; height: 37px"></td>
                                                                        </tr>
                                                                </tbody>
                                                            </table>
                                                        </fieldset>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                                <ProgressTemplate>
                                                    <asp:Image ID="Image4" runat="server" Width="24px" Height="23px" __designer:wfdid="w161"
                                                        ImageUrl="~/App_Themes/Granite/Images/image003.gif"></asp:Image>&nbsp; Loading.....
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                        <td style="width: 100px"></td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">
                                            <asp:Panel ID="Panel5" runat="server" Height="285px" ScrollBars="Vertical" Width="100%">
                                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="grdColor" runat="server" Width="99%" ForeColor="SteelBlue" Font-Size="9pt"
                                                            __designer:wfdid="w164" CssClass="gridRow2" BorderColor="White" BackColor="White"
                                                            HorizontalAlign="Center" AutoGenerateColumns="False" OnRowEditing="grdColor_RowEditing"
                                                            OnRowDeleting="grdColor_RowDeleting" OnPageIndexChanging="grdColor_PageIndexChanging">
                                                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                                PreviousPageText="Previous"></PagerSettings>
                                                               <AlternatingRowStyle BackColor="#E0E0E0" />
                                                            <Columns>
                                                                <asp:BoundField DataField="SKU_HIE_ID" HeaderText="ID">
                                                                    <ItemStyle CssClass="HidePanel" />
                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="TYPE_ID" HeaderText="ID">
                                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left"
                                                                        Width="50px"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_HIE_CODE" HeaderText="Code">
                                                                    <ItemStyle CssClass="HidePanel" />
                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Name">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>

                                                                <asp:BoundField DataField="TYPE_ID" HeaderText="TYPE_ID">
                                                                    <ItemStyle CssClass="HidePanel" />
                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Edit">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Delete">
                                                                    <img src="../images/edit.gif" />
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="45px" HorizontalAlign="Center" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDelete" ToolTip="Delete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                            CommandName="Delete">
                                                                                    <img src="../images/delete.gif" width="16" height="16">
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="45px" HorizontalAlign="Center"></ItemStyle>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>


                        <cc1:TabPanel ID="TabPanel9" runat="server">
                            <HeaderTemplate>
                                Season
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 100px"></td>
                                        <td align="center" style="width: 100px">
                                            <asp:UpdatePanel ID="UpSeason" runat="server">
                                                <ContentTemplate>
                                                    <asp:Panel ID="PnlSeason" runat="server">
                                                        <fieldset>
                                                            <table width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:Label ID="lblErrorMsgSeason" runat="server" ForeColor="Red" Font-Bold="True"
                                                                                __designer:wfdid="w148"></asp:Label><br />
                                                                        </td>
                                                                        <td style="width: 100px">
                                                                            <asp:HiddenField ID="HFSeasonID" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <%-- <td style="width: 100px; height: 28px" align="left">
                                                                    <strong>--%>
                                                                        <asp:Label ID="Label26" runat="server" Width="90px" Text="Season Code" Visible="False"></asp:Label><%--</strong>
                                                                </td>
                                                                <td style="width: 100px; height: 28px">--%>
                                                                        <asp:TextBox ID="txtSeasonCode" runat="server" Width="194px" Enabled="False" CssClass="txtBox "
                                                                            Visible="False"></asp:TextBox>
                                                                        <%--</td>
                                                                <td style="width: 100px; height: 28px">
                                                                </td>
                                                            </tr>--%>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 29px" align="left">
                                                                                <strong>
                                                                                    <asp:Label ID="Label27" runat="server" Width="90px" Text="Season Name"></asp:Label></strong>
                                                                            </td>
                                                                            <td style="width: 100px; height: 29px">
                                                                                <asp:TextBox ID="txtSeasonName" runat="server" Width="194px" Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 100px; height: 29px"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 37px"></td>
                                                                            <td style="width: 100px; height: 37px" align="left">
                                                                                <asp:Button AccessKey="B" ID="BtnSaveSeason" runat="server" OnClick="BtnSaveSeason_Click"
                                                                                    Width="85px" Font-Size="8pt" Text="New" CssClass="Button" />
                                                                            </td>
                                                                            <td style="width: 100px; height: 37px"></td>
                                                                        </tr>
                                                                </tbody>
                                                            </table>
                                                        </fieldset>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress9" runat="server" AssociatedUpdatePanelID="UpSeason">
                                                <ProgressTemplate>
                                                    <asp:Image ID="Image4" runat="server" Width="24px" Height="23px" __designer:wfdid="w161"
                                                        ImageUrl="~/App_Themes/Granite/Images/image003.gif"></asp:Image>&nbsp; Loading.....
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                        <td style="width: 100px"></td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">
                                            <asp:Panel ID="Panel15" runat="server" Height="285px" ScrollBars="Vertical" Width="100%">
                                                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="GrdSeason" runat="server" Width="99%" ForeColor="SteelBlue" Font-Size="9pt"
                                                            __designer:wfdid="w164" CssClass="gridRow2" BorderColor="White" BackColor="White"
                                                            HorizontalAlign="Center" AutoGenerateColumns="False" OnRowEditing="GrdSeason_RowEditing"
                                                            OnRowDeleting="GrdSeason_RowDeleting" OnPageIndexChanging="GrdSeason_PageIndexChanging">
                                                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                                PreviousPageText="Previous"></PagerSettings>
                                                              <AlternatingRowStyle BackColor="#E0E0E0" />
                                                             <Columns>
                                                                <asp:BoundField DataField="SKU_HIE_ID" HeaderText="ID">
                                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left"
                                                                        Width="50px"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_HIE_CODE" HeaderText="Code">
                                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Name">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Edit">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Delete">
                                                                    <img src="../images/edit.gif" />
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="45px" HorizontalAlign="Center" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDelete" ToolTip="Delete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                            CommandName="Delete">
                                                                                    <img src="../images/delete.gif" width="16" height="16">
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="45px" HorizontalAlign="Center"></ItemStyle>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel10" runat="server">
                            <HeaderTemplate>
                                Size
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 100px"></td>
                                        <td align="center" style="width: 100px">
                                            <asp:UpdatePanel ID="UpSize" runat="server">
                                                <ContentTemplate>
                                                    <asp:Panel ID="PnlSize" runat="server">
                                                        <fieldset>
                                                            <table width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:Label ID="lblErrorMsgSize" runat="server" ForeColor="Red" Font-Bold="True" __designer:wfdid="w148"></asp:Label><br />
                                                                        </td>
                                                                        <td style="width: 100px">
                                                                            <asp:HiddenField ID="HFSizeID" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <%-- <td style="width: 100px; height: 28px" align="left">
                                                                    <strong>--%>
                                                                        <asp:Label ID="Label29" runat="server" Width="90px" Text="Size Code" Visible="False"></asp:Label><%--</strong>
                                                                </td>
                                                                <td style="width: 100px; height: 28px">--%>
                                                                        <asp:TextBox ID="txtSizeCode" runat="server" Width="194px" Enabled="False" CssClass="txtBox "
                                                                            Visible="False"></asp:TextBox>
                                                                        <%--</td>
                                                                <td style="width: 100px; height: 28px">
                                                                </td>
                                                            </tr>--%>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 29px" align="left">
                                                                                <strong>
                                                                                    <asp:Label ID="Label30" runat="server" Width="90px" Text="Size Name"></asp:Label></strong>
                                                                            </td>
                                                                            <td style="width: 100px; height: 29px">
                                                                                <asp:TextBox ID="txtSizeName" runat="server" Width="194px" Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 100px; height: 29px"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 37px"></td>
                                                                            <td style="width: 100px; height: 37px" align="left">
                                                                                <asp:Button AccessKey="B" ID="BtnSaveSize" runat="server" OnClick="BtnSaveSize_Click"
                                                                                    Width="85px" Font-Size="8pt" Text="New" CssClass="Button" />
                                                                            </td>
                                                                            <td style="width: 100px; height: 37px"></td>
                                                                        </tr>
                                                                </tbody>
                                                            </table>
                                                        </fieldset>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress10" runat="server" AssociatedUpdatePanelID="UpSize">
                                                <ProgressTemplate>
                                                    <asp:Image ID="Image4" runat="server" Width="24px" Height="23px" __designer:wfdid="w161"
                                                        ImageUrl="~/App_Themes/Granite/Images/image003.gif"></asp:Image>&nbsp; Loading.....
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                        <td style="width: 100px"></td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">
                                            <asp:Panel ID="Panel16" runat="server" Height="285px" ScrollBars="Vertical" Width="100%">
                                                <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="GrdSize" runat="server" Width="99%" ForeColor="SteelBlue" Font-Size="9pt"
                                                            __designer:wfdid="w164" CssClass="gridRow2" BorderColor="White" BackColor="White"
                                                            HorizontalAlign="Center" AutoGenerateColumns="False" OnRowEditing="GrdSize_RowEditing"
                                                            OnRowDeleting="GrdSize_RowDeleting" OnPageIndexChanging="GrdSize_PageIndexChanging">
                                                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                                PreviousPageText="Previous"></PagerSettings>
                                                              <AlternatingRowStyle BackColor="#E0E0E0" />
                                                             <Columns>
                                                                <asp:BoundField DataField="SKU_HIE_ID" HeaderText="ID">
                                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left"
                                                                        Width="50px"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_HIE_CODE" HeaderText="Code">
                                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Name">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Edit">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Delete">
                                                                    <img src="../images/edit.gif" />
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="45px" HorizontalAlign="Center" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDelete" ToolTip="Delete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                            CommandName="Delete">
                                                                                    <img src="../images/delete.gif" width="16" height="16">
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="45px" HorizontalAlign="Center"></ItemStyle>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
