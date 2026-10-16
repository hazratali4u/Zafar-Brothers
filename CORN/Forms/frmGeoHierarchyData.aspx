<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmGeoHierarchyData.aspx.cs" Inherits="Forms_frmGeoHierarchyData" Title="CORN :: Geo Hierarchy" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script language="JavaScript" type="text/javascript">
        function ValidatRegion() {
            var str;

            str = document.getElementById('<%=txtRegionName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Region Name');
                return false;
            }
            return true;
        }


        function ValidateTerritory() {
            var str;
            str = document.getElementById('<%=txtTerritoryName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Territory Name');
                return false;
            }
            return true;
        }

    </script>
    <div id="right_data">
        <table>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <cc1:TabContainer ID="TabContainer1" runat="server" Height="400px" Width="650px"
                                    ActiveTabIndex="0">
                                    <cc1:TabPanel ID="TabPanel1" runat="server">
                                        <HeaderTemplate>
                                            Region&nbsp;
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 100px"></td>
                                                    <td style="width: 100px"></td>
                                                    <td style="width: 100px"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100px"></td>
                                                    <td style="width: 100px">
                                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                            <ContentTemplate>
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 100px"></td>
                                                                            <td style="width: 49px"></td>
                                                                            <td style="width: 100px"></td>
                                                                            <td style="width: 100px"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px"></td>
                                                                            <td colspan="2">
                                                                                <asp:Label ID="lblErrorMsgRegion" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                                                                <br />
                                                                            </td>
                                                                            <td style="width: 100px">
                                                                                <asp:HiddenField ID="HdnRegionID" runat="server" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 28px"></td>
                                                                            <td style="width: 49px; height: 28px">
                                                                                <strong>
                                                                                    <asp:Label ID="Label2" runat="server" Text="Name" Width="65px" Height="14px"></asp:Label>
                                                                                </strong>
                                                                            </td>
                                                                            <td style="width: 100px; height: 28px">
                                                                                <asp:TextBox ID="txtRegionName" runat="server" CssClass="txtBox " Width="192px" Height="17px"></asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 100px; height: 28px"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 29px"></td>
                                                                            <td style="width: 49px; height: 29px">&nbsp;
                                                                            </td>
                                                                            <td style="width: 100px; height: 29px">&nbsp;<asp:CheckBox ID="ChIsActive" runat="server" Text="Is Active" Checked="True"></asp:CheckBox>
                                                                            </td>
                                                                            <td style="width: 100px; height: 29px"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 37px"></td>
                                                                            <td style="width: 49px; height: 37px" align="right">
                                                                                <asp:Label ID="Label3" runat="server" Text="Code" Width="65px" Visible="False"></asp:Label>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td style="width: 100px; height: 37px">
                                                                                <asp:Button ID="btnRegionSave" OnClick="btnRegionSave_Click" runat="server" Width="85px"
                                                                                    Font-Size="8pt" Text="Save" CssClass="Button" />
                                                                                <asp:TextBox ID="txtRegionCode" runat="server" CssClass="txtBox " Width="77px" Visible="False">N/A</asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 100px; height: 37px"></td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td style="width: 100px"></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="3">
                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="grdRegionData" runat="server" Width="90%" Font-Size="9pt" AutoGenerateColumns="False"
                                                                    HorizontalAlign="Center" OnRowEditing="grdRegionData_RowEditing">
                                                                    <AlternatingRowStyle BackColor="#E0E0E0" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="GEO_ID" HeaderText="GEO_ID">
                                                                            <HeaderStyle CssClass="HidePanel" />
                                                                            <ItemStyle CssClass="HidePanel" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="GEO_CODE" HeaderText="Region Code">
                                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel"></ItemStyle>
                                                                            <HeaderStyle HorizontalAlign="Left" CssClass="HidePanel" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="GEO_NAME" HeaderText="Region Name">
                                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="ISDELETED" HeaderText="Status">
                                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <%--  <asp:CommandField ShowEditButton="True" HeaderText="Edit"></asp:CommandField>--%>
                                                                        <asp:TemplateField HeaderText="Edit">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Delete">
                                                                    <img src="../images/edit.gif" />
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="45px" HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
                                        <HeaderTemplate>
                                            Territory
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 100px"></td>
                                                    <td align="center" style="width: 100px">
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 8px"></td>
                                                                            <td style="height: 8px" align="left" colspan="2">
                                                                                <asp:Label ID="lblErrorTerritory" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label><br />
                                                                            </td>
                                                                            <td style="width: 100px; height: 8px">
                                                                                <asp:HiddenField ID="HdnTerritoryID" runat="server" />

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 16px"></td>
                                                                            <td style="width: 49px; height: 28px" align="left">
                                                                                <strong>
                                                                                    <asp:Label ID="Label5" runat="server" Text="Region" Width="47px"></asp:Label></strong>
                                                                            </td>
                                                                            <td style="height: 16px" align="left">
                                                                                <asp:DropDownList ID="drpRegionTerritory" runat="server" Height="17px" Width="200px" AutoPostBack="True"
                                                                                    OnSelectedIndexChanged="drpRegionTerritory_SelectedIndexChanged" CssClass="DropList">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td style="width: 100px; height: 16px"></td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td style="width: 100px; height: 8px"></td>
                                                                            <td style="width: 49px; height: 28px" align="left">
                                                                                <strong>
                                                                                    <asp:Label ID="Label211" runat="server" Text="Name" Width="43px"></asp:Label></strong>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtTerritoryName" runat="server" Height="17px" CssClass="txtBox " Width="194px"></asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 100px; height: 8px"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 37px"></td>
                                                                            <td align="right"></td>
                                                                            <td align="left">
                                                                                <asp:CheckBox ID="chbIsTerritoryActive" runat="server" Checked="True" Text="Is Active" />
                                                                            </td>
                                                                            <td style="width: 100px; height: 37px"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 37px"></td>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label41" runat="server" Text="Code" Width="43px" Visible="False"></asp:Label>&nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Button ID="btnSaveTerritory" runat="server" Font-Size="8pt" OnClick="btnSaveTerritory_Click"
                                                                                    CssClass="Button" Text="Save" Width="85px" />
                                                                                <asp:TextBox ID="txtTerritoryCode" runat="server" CssClass="txtBox " Width="100px" Height="17px"
                                                                                    Visible="False">N/A</asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 100px; height: 37px"></td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td style="width: 100px"></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="3">
                                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="grdTerritoryData" runat="server" Width="100%" Font-Size="9pt" ForeColor="SteelBlue"
                                                                    CssClass="gridRow2" BorderColor="White" BackColor="White" HorizontalAlign="Center"
                                                                    AutoGenerateColumns="False" OnRowEditing="grdTerritoryData_RowEditing" AllowPaging="True"
                                                                    OnPageIndexChanging="grdTerritoryData_PageIndexChanging">
                                                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                                        PreviousPageText="Previous" />
                                                                    <AlternatingRowStyle BackColor="#E0E0E0" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="PARENT_GEO_ID" HeaderText="PARENT_GEO_ID">
                                                                            <HeaderStyle CssClass="HidePanel" />
                                                                            <ItemStyle CssClass="HidePanel" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="GEO_ID" HeaderText="GEO_ID">
                                                                            <HeaderStyle CssClass="HidePanel" />
                                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CssClass="HidePanel"
                                                                                HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="GEO_CODE" HeaderText="Territory Code">
                                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CssClass="HidePanel" />
                                                                            <HeaderStyle HorizontalAlign="Left" CssClass="HidePanel" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="GEO_NAME" HeaderText="Territory Name">
                                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="ISDELETED" HeaderText="Status">
                                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <%--  <asp:CommandField ShowEditButton="True" HeaderText="Edit"></asp:CommandField>--%>
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
                                                                    <HeaderStyle CssClass="tblhead" />

                                                                </asp:GridView>
                                                                &nbsp;
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel3">
                                        <HeaderTemplate>
                                            Zone
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 100px"></td>
                                                    <td align="center" style="width: 100px">
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 8px"></td>
                                                                            <td style="height: 8px" align="left" colspan="2">
                                                                                <asp:Label ID="lblZoneError" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label><br />
                                                                            </td>
                                                                            <td style="width: 100px; height: 8px">
                                                                                <asp:HiddenField ID="hdnZoneID" runat="server" />

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 16px"></td>
                                                                            <td style="width: 49px; height: 28px" align="left">
                                                                                <strong>
                                                                                    <asp:Label ID="Label4" runat="server" Text="Region" Width="47px"></asp:Label></strong>
                                                                            </td>
                                                                            <td style="height: 16px" align="left">
                                                                                <asp:DropDownList ID="DrpZoneRegion" runat="server" Width="200px" AutoPostBack="True" Height="17px"
                                                                                    OnSelectedIndexChanged="DrpZoneRegion_SelectedIndexChanged" CssClass="DropList">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td style="width: 100px; height: 16px"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 16px"></td>
                                                                            <td style="width: 49px; height: 28px" align="left">
                                                                                <strong>
                                                                                    <asp:Label ID="Label8" runat="server" Text="Territory" Width="47px"></asp:Label></strong>
                                                                            </td>
                                                                            <td style="height: 16px" align="left">
                                                                                <asp:DropDownList ID="drpZoneTerrotory" runat="server" Width="200px" AutoPostBack="True" Height="17px"
                                                                                    OnSelectedIndexChanged="drpZoneTerrotory_SelectedIndexChanged" CssClass="DropList">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td style="width: 100px; height: 16px"></td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td style="width: 100px; height: 8px"></td>
                                                                            <td style="width: 49px; height: 28px" align="left">
                                                                                <strong>
                                                                                    <asp:Label ID="Label6" runat="server" Text="Name" Width="43px"></asp:Label></strong>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtZoneName" runat="server" CssClass="txtBox " Width="194px"></asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 100px; height: 8px"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 37px"></td>
                                                                            <td align="right"></td>
                                                                            <td align="left">
                                                                                <asp:CheckBox ID="chbzoneActive" runat="server" Checked="True" Text="Is Active" />
                                                                            </td>
                                                                            <td style="width: 100px; height: 37px"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px; height: 37px"></td>
                                                                            <td align="right">
                                                                                <asp:Label ID="Label7" runat="server" Text="Code" Width="43px" Visible="False"></asp:Label>&nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Button ID="btnSaveZone" runat="server" Font-Size="8pt" OnClick="btnSaveZone_Click"
                                                                                    CssClass="Button" Text="Save" Width="85px" />
                                                                                <asp:TextBox ID="txtZoneCode" runat="server" CssClass="txtBox " Width="100px"
                                                                                    Visible="False">N/A</asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 100px; height: 37px"></td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td style="width: 100px"></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="3">
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="GrdZoneData" runat="server" Width="100%" Font-Size="9pt" ForeColor="SteelBlue"
                                                                    CssClass="gridRow2" BorderColor="White" BackColor="White" HorizontalAlign="Center"
                                                                    AutoGenerateColumns="False" OnRowEditing="GrdZoneData_RowEditing" AllowPaging="True"
                                                                    OnPageIndexChanging="GrdZoneData_PageIndexChanging">
                                                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                                        PreviousPageText="Previous" />
                                                                    <AlternatingRowStyle BackColor="#E0E0E0" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="PARENT_GEO_ID" HeaderText="PARENT_GEO_ID">
                                                                            <HeaderStyle CssClass="HidePanel" />
                                                                            <ItemStyle CssClass="HidePanel" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="GEO_ID" HeaderText="GEO_ID">
                                                                            <HeaderStyle CssClass="HidePanel" />
                                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CssClass="HidePanel"
                                                                                HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="GEO_CODE" HeaderText="Zone Code">
                                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CssClass="HidePanel" />
                                                                            <HeaderStyle HorizontalAlign="Left" CssClass="HidePanel" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="GEO_NAME" HeaderText="Zone Name">
                                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="ISDELETED" HeaderText="Status">
                                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
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
                                                                    </Columns>
                                                                    <FooterStyle BackColor="White" />
                                                                    <PagerStyle BackColor="Transparent" />
                                                                    <HeaderStyle CssClass="tblhead" />

                                                                </asp:GridView>
                                                                &nbsp;
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                </cc1:TabContainer>
                            </td>
                        </tr>
                    </table>
                </td>
                <td></td>
            </tr>
        </table>
    </div>
</asp:Content>
