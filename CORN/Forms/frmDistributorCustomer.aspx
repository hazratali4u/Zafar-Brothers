<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmDistributorCustomer.aspx.cs" Inherits="Forms_frmDistributorCustomer"
    Title="CORN :: New Customer" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

        function startRequest(sender, e) {
            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            document.getElementById('<%=btnSearch.ClientID%>').disabled = true;
            document.getElementById('<%=btnCancel.ClientID%>').disabled = true;

        }

        function endRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
            document.getElementById('<%=btnSearch.ClientID%>').disabled = false;
            document.getElementById('<%=btnCancel.ClientID%>').disabled = false;
        }




    </script>
    <div id="right_data">
        <div style="z-index: 101; left: 75%; width: 100px; position: absolute; top: 150px; height: 100px">
            &nbsp;<asp:Panel ID="Panel21" runat="server">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                    <ProgressTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                            Width="23px" />
                        Wait Update
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </asp:Panel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div>
                    <table>
                        <tbody>
                            <tr>
                                <td style="width: 143px" align="left"></td>
                                <td style="width: 175px">
                                    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                </td>
                                <td style="width: 1px"></td>
                                <td align="left"></td>
                                <td style="width: 219px"></td>
                            </tr>
                            <tr>
                                <td style="height: 25px" align="left">
                                    <strong>
                                        <asp:Label ID="Label7" runat="server" Width="110px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td style="width: 175px">
                                    <asp:DropDownList ID="DrpDistributor" runat="server" Width="205px" CssClass="DropList"
                                        OnSelectedIndexChanged="DrpDistributor_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>

                                </td>
                                <td style="width: 30px"></td>
                                <td align="left"><strong>
                                    <asp:Label ID="Label9" runat="server" Width="110px" Text="Customer Group" CssClass="lblbox"></asp:Label>
                                </strong>
                                </td>
                                <td style="width: 219px">
                                    <asp:DropDownList ID="DrpVolumeClass" runat="server" Width="205px" CssClass="DropList">
                                    </asp:DropDownList>
                                </td>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 26px" align="left">
                                    <strong>
                                        <asp:Label ID="Label2" runat="server" Width="77px" Text="Region" CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td style="height: 16px" align="left">
                                    <asp:DropDownList ID="drpRegion" runat="server" Width="205px" AutoPostBack="True"
                                        OnSelectedIndexChanged="drpRegion_SelectedIndexChanged" CssClass="DropList">
                                    </asp:DropDownList>
                                </td>
                                <td></td>
                                <td style="height: 26px" align="left">
                                    <strong>
                                        <asp:Label ID="Label3" runat="server" Width="77px" Text="Territory" CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td style="height: 16px" align="left">
                                    <asp:DropDownList ID="drpTerritory" runat="server" Width="205px" AutoPostBack="true" OnSelectedIndexChanged="drpTerritory_SelectedIndexChanged"
                                        CssClass="DropList">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 26px" align="left">
                                    <strong>
                                        <asp:Label ID="Label15" runat="server" Width="77px" Text="Zone" CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td style="height: 16px" align="left">
                                    <asp:DropDownList ID="DrpZone" runat="server" Width="205px"
                                        CssClass="DropList">
                                    </asp:DropDownList>
                                </td>
                                <td></td>
                                <td align="left">
                                    <strong>
                                        <asp:Label ID="Label14" runat="server" Text="Sale Person"></asp:Label></strong>
                                </td>
                                <td style="width: 219px; height: 26px">
                                    <asp:DropDownList ID="DrpsalePerson" runat="server" Width="205px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 26px" align="left">
                                    <strong>
                                        <asp:Label ID="Label1" runat="server" Width="77px" Text="Name" CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td style="width: 175px; height: 26px">
                                    <asp:TextBox ID="txtCustomerName" runat="server" Width="205px" CssClass="txtBox"
                                        MaxLength="100"></asp:TextBox>
                                </td>
                                <td style="width: 1px; height: 26px" valign="top">
                                    <asp:RequiredFieldValidator ID="rfvCustomerName" ControlToValidate="txtCustomerName"
                                        runat="server" ForeColor="Red" ErrorMessage="*" ValidationGroup="req" Font-Bold="true"
                                        Font-Size="Medium">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td align="left">
                                    <strong>
                                        <asp:Label ID="lblAddress1" runat="server" Width="77px" Text="Address " CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td style="width: 219px; height: 26px">
                                    <asp:TextBox ID="txtAddress" runat="server" Width="205px" CssClass="txtBox " MaxLength="250"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAddress" ValidationGroup="req" Font-Bold="true"
                                        Font-Size="Medium" ControlToValidate="txtAddress" runat="server" ForeColor="Red"
                                        ErrorMessage="*">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 26px" align="left">
                                    <strong>
                                        <asp:Label ID="lblPhNo" runat="server" Width="77px" Text="Phone No" CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td style="width: 175px; height: 26px">
                                    <asp:TextBox ID="txtPhoneNo" runat="server" Width="205px" CssClass="txtBox" MaxLength="15"></asp:TextBox>
                                </td>
                                <td style="width: 1px; height: 26px" valign="top">
                                    <asp:RequiredFieldValidator ID="rfvPhoneNo" ValidationGroup="req" ControlToValidate="txtPhoneNo"
                                        runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Medium" ErrorMessage="*">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td align="left">
                                    <strong>
                                        <asp:Label ID="lbldesignationID" runat="server" Width="77px" Text="Email" CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td style="width: 219px; height: 26px">
                                    <asp:TextBox ID="txtEmailAddress" runat="server" Width="205px" CssClass="txtBox"></asp:TextBox>
                                    <asp:TextBox ID="txtRegdate" runat="server" Visible="false" Width="185px" CssClass="txtBox"
                                        Enabled="false" MaxLength="11"></asp:TextBox>
                                    <asp:ImageButton ID="ibndob" Visible="false" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton>
                                    <cc1:CalendarExtender ID="CEStartDate" runat="server" TargetControlID="txtRegdate"
                                        PopupButtonID="ibndob" Format="dd-MMM-yyyy">
                                    </cc1:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 26px" align="left">
                                    <strong>
                                        <asp:Label ID="Label4" runat="server" Width="77px" Text="Mobile No" CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td style="width: 175px; height: 26px">
                                    <asp:TextBox ID="txtMobileNo" runat="server" Width="205px" CssClass="txtBox" MaxLength="15"></asp:TextBox>
                                </td>
                                <td style="width: 1px; height: 26px" valign="top"></td>
                                <td align="left">
                                    <strong>
                                        <asp:Label ID="Label5" runat="server" Width="100px" Text="Contact Person" CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td style="width: 219px; height: 26px">
                                    <asp:TextBox ID="txtContactperson" runat="server" Width="205px" CssClass="txtBox"></asp:TextBox>

                                </td>
                            </tr>
                            <tr>
                                <td style="height: 26px" align="left">
                                    <strong>
                                        <asp:Label ID="Label8" runat="server" Width="77px" Text="CNIC #" CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td style="width: 175px; height: 26px">
                                    <asp:TextBox ID="txtCNIC" runat="server" Width="205px" CssClass="txtBox"></asp:TextBox>
                                </td>
                                <td style="width: 1px; height: 26px" valign="top"></td>
                                <td align="left">
                                    <strong>
                                        <asp:Label ID="Label12" runat="server" Text="Credit Limit" CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td style="width: 219px; height: 26px">
                                    <asp:TextBox ID="txtCreditLimit" runat="server" Width="205px" CssClass="txtBox "></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="ftbCreditLimit" TargetControlID="txtCreditLimit"
                                        ValidChars=".0123456789" runat="server">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 26px" align="left">
                                    <strong>
                                        <asp:Label ID="Label6" runat="server" Text="Credit Days" CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td style="width: 175px; height: 26px">
                                    <asp:TextBox ID="txtCreditDays" runat="server" Width="205px" CssClass="txtBox "></asp:TextBox>
                                </td>
                                <td></td>
                                <td align="left">
                                    <strong>
                                        <asp:Label ID="Label11" runat="server" Text="Classification"></asp:Label></strong>
                                </td>
                                <td style="width: 219px; height: 26px">
                                    <asp:DropDownList ID="DrpClassification" runat="server" Width="205px">
                                        <asp:ListItem Value="0" Text="A"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="B"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="C"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="D"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="E"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>




                            <tr>
                                <td style="height: 26px" align="left">
                                    <strong>
                                        <asp:Label ID="Label13" runat="server" Text="Customer Type" CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td style="width: 175px; height: 26px">
                                    <asp:DropDownList ID="DrpCustomerType" runat="server" Width="205px">
                                        <asp:ListItem Value="0" Text="Men"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Women"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Kids"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Mix"></asp:ListItem>

                                    </asp:DropDownList></td>
                                <td></td>

                            </tr>

                            <tr>
                                <td align="left" colspan="5"></td>
                            </tr>
                            <tr>
                                <td style="width: 21px"></td>
                                <td style="width: 175px" colspan="3">
                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Width="80px"
                                        ValidationGroup="req" CssClass="Button" CausesValidation="true" />
                                    &nbsp;
                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="false" OnClick="btnCancel_Click"
                                        CssClass="Button" Text="Cancel" Width="80px" />
                                    &nbsp; &nbsp; &nbsp;
                                    <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" Font-Bold="true" Text="IsActive"
                                        Width="93px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 21px"></td>
                                <td align="left" colspan="5"></td>
                            </tr>
                            <tr>
                                <td colspan="6">&nbsp;
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div>
                    <table class="tblhead" width="100%">
                        <tbody>
                            <tr>
                                <td style="font-weight: bold; padding: 5px; width: 138px">
                                    <asp:Label ID="Label10" runat="server" ForeColor="White" Width="125px" Text="Select Search Type"></asp:Label>
                                </td>
                                <td style="width: 170px; height: 22px" align="left">
                                    <asp:DropDownList ID="ddSearchType" runat="server" Width="208px" CssClass="ddlist">
                                        <asp:ListItem Value="CUSTOMER_CODE">Customer Code</asp:ListItem>
                                        <asp:ListItem Value="CUSTOMER_NAME" Selected="True">Customer Name</asp:ListItem>
                                        <asp:ListItem Value="CONTACT_NUMBER">Contact Number</asp:ListItem>
                                        <asp:ListItem Value="ADDRESS">Address</asp:ListItem>
                                        <asp:ListItem Value="EMAIL_ADDRESS">Email Address</asp:ListItem>
                                        <asp:ListItem Value="CNIC">CNIC</asp:ListItem>
                                        <asp:ListItem Value="user_name">Sale Person</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 224px; height: 22px" align="center">
                                    <asp:TextBox ID="txtSeach" runat="server" Width="200px" CssClass="txtDetail"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvSearch" runat="server" Display="Dynamic" ErrorMessage="*"
                                        ForeColor="White" Font-Bold="true" Font-Size="Medium" ValidationGroup="filter"
                                        ControlToValidate="txtSeach"></asp:RequiredFieldValidator>
                                </td>
                                <td style="height: 22px" align="left" width="250">
                                    <telerik:RadButton ID="btnSearch" runat="server" Width="85px" Text="Filter" OnClick="btnSearch_Click"
                                        ValidationGroup="filter" CausesValidation="true" />
                                </td>
                                <td width="250px"></td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:Panel ID="pnlCustomer" ScrollBars="Vertical" Width="100%" Height="200px" BorderColor="LightGray"
                        BorderStyle="Solid" BorderWidth="1px" runat="server">
                        <asp:GridView ID="Grid_users" runat="server" Width="99.9%" ForeColor="SteelBlue"
                            CssClass="gridRow2" HorizontalAlign="Center" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="White" OnRowEditing="Grid_users_RowEditing">
                             <AlternatingRowStyle BackColor="#E0E0E0" />
                            <Columns>
                                <asp:BoundField DataField="CUSTOMER_ID" ReadOnly="true" HeaderText="Customer Id">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DISTRIBUTOR_ID" ReadOnly="true" HeaderText="DISTRIBUTOR_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="BUSINESS_TYPE_ID" ReadOnly="true" HeaderText="BUSINESS_TYPE_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PROMOTION_CLASS" ReadOnly="true" HeaderText="PROMOTION_CLASS">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CHANNEL_TYPE_ID" ReadOnly="true" HeaderText="CHANNEL_TYPE_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TOWN_ID" ReadOnly="true" HeaderText="TOWN_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="AREA_ID" ReadOnly="true" HeaderText="AREA_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ROUTE_ID" ReadOnly="true" HeaderText="ROUTE_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CUSTOMER_CODE" ReadOnly="true" HeaderText="Code">
                                    <ItemStyle CssClass="grdDetail"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CUSTOMER_NAME" ReadOnly="true" HeaderText="Name">
                                    <ItemStyle CssClass="grdDetail"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CONTACT_PERSON" ReadOnly="true" HeaderText="Contact Person">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CONTACT_NUMBER" ReadOnly="true" HeaderText="Contact Number">
                                    <ItemStyle CssClass="grdDetail"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EMAIL_ADDRESS" ReadOnly="true" HeaderText="Email">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ADDRESS" ReadOnly="true" HeaderText="Address">
                                    <ItemStyle CssClass="grdDetail"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="GST_NUMBER" ReadOnly="true" HeaderText="Gst No">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ChannelType" ReadOnly="true" HeaderText="Channel Type">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="GEO_NAME" ReadOnly="true" HeaderText="Town">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="AREA_NAME" ReadOnly="true" HeaderText="Route">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ROUTE_NAME" ReadOnly="true" HeaderText="Market">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IS_ACTIVE" ReadOnly="true" HeaderText="Status">
                                    <ItemStyle CssClass="grdDetail"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="REGDATE" ReadOnly="true" DataFormatString="{0:dd-MMM-yyy}">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IS_STAND" ReadOnly="true" HeaderText="Stand">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IS_COOLER" ReadOnly="true" HeaderText="Cooler">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CNIC" ReadOnly="true" HeaderText="CNIC">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="NTN" ReadOnly="true" HeaderText="NTN">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CREDIT_LIMIT" ReadOnly="true" HeaderText="CREDIT_LIMIT" DataFormatString="{0:f2}">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CREDIT_DAYS" ReadOnly="true" HeaderText="CREDIT_DAYS">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="MOBILE_NO" ReadOnly="true" HeaderText="MOBILE_NO">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TERRITORY_ID" ReadOnly="true" HeaderText="TERRITORY_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="REGION_ID" ReadOnly="true" HeaderText="REGION_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CLASSIFICATION_ID" ReadOnly="true" HeaderText="CLASSIFICATION_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Customer_type" ReadOnly="true" HeaderText="Customer_type">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ASSIGNED_USER_ID" ReadOnly="true" HeaderText="ASSIGNED_USER_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>

                                <asp:BoundField DataField="ZONE_ID" ReadOnly="true" HeaderText="ZONE_ID">
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
                                <%--  <asp:CommandField ShowEditButton="True"  HeaderText="Action">
                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Center"></ItemStyle>
                                </asp:CommandField>--%>
                            </Columns>
                            <FooterStyle BackColor="White"></FooterStyle>
                            <PagerStyle BackColor="Transparent"></PagerStyle>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="tblhead" Height="20px" />

                        </asp:GridView>
                        <asp:Label ID="lblNoRecords" Text="No records to display" runat="server" Visible="false"
                            ForeColor="red"></asp:Label>
                    </asp:Panel>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
