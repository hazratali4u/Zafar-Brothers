<%@ Page Language="C#" MasterPageFile="~/Forms/masterPOS.master" AutoEventWireup="true"
    CodeFile="frmNewCustomerPOS.aspx.cs" Inherits="Forms_frmNewCustomerPOS" Title="CORN :: New Customer" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../css/POSstyle.css" rel="stylesheet" type="text/css" />
    <asp:ScriptManager ID="MainScriptManager" runat="server" AsyncPostBackTimeout="300"
        EnablePartialRendering="true" />
    <script language="JavaScript" type="text/javascript">

<%-- //        function pageLoad() {

//            $("select").searchable();
//        } 

//        function NameValidation(event) {
//            // Allow: backspace, delete, tab and escape
//            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 32 ||
//            // Allow: Ctrl+A
//            (event.keyCode == 65 && event.ctrlKey === true) ||
//            // Allow: home, end, left, right
//            (event.keyCode >= 35 && event.keyCode <= 39) ||
//            // Allow: Dash, Underscoor
//            (event.keyCode == 189) ||
//            //Allow Comma,Period
//            ((event.keyCode == 190 || event.keyCode == 188) && event.shiftKey === false) ||
//            //Allow a-z
//            (event.keyCode >= 65 && event.keyCode <= 90)) {
//                // let it happen, don't do anything
//                return;
//            }
//            else {
//                // Ensure that it is a number and stop the keypress
//                event.preventDefault();
//            }
//        }

//        function AddressValidation(event) {
//            // Allow: backspace, delete, tab , escape and space bar
//            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 32 ||
//            // Allow: Ctrl+A
//            (event.keyCode == 65 && event.ctrlKey === true) ||
//            // Allow: home, end, left, right
//            (event.keyCode >= 35 && event.keyCode <= 39) ||
//            // Allow: Dash, Underscoor
//            (event.keyCode == 189) ||
//            // Allow: Open bracket, Close bracket
//            ((event.keyCode == 57 || event.keyCode == 48) && event.shiftKey === true) ||
//            //Allow Comma,Period
//            ((event.keyCode == 190 || event.keyCode == 188) && event.shiftKey === false) ||
//            //Allow 0-9
//            ((event.keyCode >= 48 && event.keyCode <= 57) && event.shiftKey === false) || //Standard Numbers
//            (event.keyCode >= 96 && event.keyCode <= 105) || //Keypad numbers
//            //Allow a-z
//            (event.keyCode >= 65 && event.keyCode <= 90)) {
//                // let it happen, don't do anything
//                return;
//            }
//            else {
//                // Ensure that it is a number and stop the keypress
//                event.preventDefault();
//            }
//        }

//        function PhoneValidation(event) {
//            // Allow: backspace, delete, tab , escape
//            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 ||
//            // Allow: Ctrl+A
//            (event.keyCode == 65 && event.ctrlKey === true) ||
//            // Allow: home, end, left, right
//            (event.keyCode >= 35 && event.keyCode <= 39) ||
//            // Allow: Dash
//            (event.keyCode == 189 && event.shiftKey === false) ||
//            //Allow 0-9
//            ((event.keyCode >= 48 && event.keyCode <= 57) && event.shiftKey === false) || //Standard Numbers
//            //Keypad numbers
//            (event.keyCode >= 96 && event.keyCode <= 105)) {
//                // let it happen, don't do anything
//                return;
//            }
//            else {
//                // Ensure that it is a number and stop the keypress
//                event.preventDefault();
//            }
//        }
        --%>
        
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
        <div style="z-index: 101; left: 20%; width: 100px; position: absolute; top: 220px;
            height: 100px">
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
                <div class="menu2" style="width: 60.66%; margin-left: 300px">
                    <div class="main">
                        <ul>
                            <li>Customer Information</li>
                        </ul>
                    </div>
                </div>
                <div style="margin-left: 300px;">
                    <table>
                        <tbody>
                            <tr>
                                <td style="width: 143px" align="left">
                                </td>
                                <td style="width: 175px">
                                    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                </td>
                                <td style="width: 1px">
                                </td>
                                <td align="left">
                                </td>
                                <td style="width: 219px">
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 25px" align="left">
                                    <strong>
                                        <asp:Label ID="Label7" runat="server" Width="110px" Text="Customer Group" CssClass="lblbox" ></asp:Label></strong>
                                </td>
                                <td style="width: 175px">
                                    <asp:DropDownList ID="DrpDistributor" runat="server" Width="205px" CssClass="DropList"
                                        OnSelectedIndexChanged="DrpDistributor_SelectedIndexChanged" AutoPostBack="True" Visible="false">
                                    </asp:DropDownList>
                                     <asp:DropDownList ID="DrpVolumeClass" runat="server" Width="205px" CssClass="DropList">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 30px">
                                </td>
                                <td align="left">
                                  
                                     
                                </td>
                                <td style="width: 219px">
                                   
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
                                    <asp:ImageButton ID="ibndob" Visible="false" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                    </asp:ImageButton>
                                    <cc1:CalendarExtender ID="CEStartDate" runat="server" TargetControlID="txtRegdate"
                                        PopupButtonID="ibndob" Format="dd-MMM-yyyy">
                                    </cc1:CalendarExtender>
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
                                <td style="width: 1px; height: 26px" valign="top">
                                </td>
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
                                </td>
                                <td style="width: 175px; height: 26px">
                                </td>
                                <td style="width: 1px; height: 26px" valign="top">
                                </td>
                                <td align="left">
                                </td>
                                <td style="width: 219px; height: 26px">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="5">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 21px">
                                </td>
                                <td style="width: 175px" colspan="3">
                                    <telerik:RadButton ID="btnSave" runat="server" Skin="WebBlue" OnClick="btnSave_Click"
                                        Text="Save" Width="80px" ValidationGroup="req" CausesValidation="true" />
                                    &nbsp;
                                    <telerik:RadButton ID="btnCancel" runat="server" Skin="WebBlue" CausesValidation="false"
                                        OnClick="btnCancel_Click" Text="Cancel" Width="80px" />
                                    &nbsp;
                                    <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" Text="IsActive" Width="93px" />
                                    
                                </td>
                                <td style="width: 219px">
                                    <asp:Button ID="btnback" CssClass="ButtonPOS" runat="server" CausesValidation="false"
                                        Text="<< Back" Width="90px" OnClick="btnback_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 21px">
                                </td>
                                <td align="left" colspan="5">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    &nbsp;
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div style="margin-left: 300px; width: 60.66%">
                    <table class="tblhead">
                        <tbody>
                            <tr>
                                <td style="font-weight: bold; padding: 10px">
                                    <asp:Label ID="Label10" runat="server" ForeColor="White" Width="125px" Text="Select Search Type"></asp:Label>
                                </td>
                                <td style="width: 170px; height: 22px" align="left">
                                    <asp:DropDownList ID="ddSearchType" runat="server" Width="208px" CssClass="DropList">
                                        <asp:ListItem Value="CUSTOMER_CODE">Customer Code</asp:ListItem>
                                        <asp:ListItem Value="CUSTOMER_NAME" Selected="True">Customer Name</asp:ListItem>
                                        <asp:ListItem Value="CONTACT_NUMBER">Contact Number</asp:ListItem>
                                        <asp:ListItem Value="ADDRESS">Address</asp:ListItem>
                                        <asp:ListItem Value="EMAIL_ADDRESS">Email Address</asp:ListItem>
                                        <asp:ListItem>CNIC</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 224px; height: 22px" align="center">
                                    <asp:TextBox ID="txtSeach" runat="server" Width="200px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvSearch" runat="server" Display="Dynamic" ErrorMessage="*"
                                        ForeColor="White" Font-Bold="true" Font-Size="Medium" ValidationGroup="filter"
                                        ControlToValidate="txtSeach"></asp:RequiredFieldValidator>
                                </td>
                                <td style="height: 22px" align="left" width="250">
                                    <telerik:RadButton ID="btnSearch" runat="server" Width="85px"  Text="Filter"
                                        OnClick="btnSearch_Click" ValidationGroup="filter" CausesValidation="true" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <asp:Panel ID="pnlCustomer" ScrollBars="Vertical" Width="100%" Height="200px" BorderColor="LightGray"
                        BorderStyle="Solid" BorderWidth="1px" runat="server">
                        <asp:GridView ID="Grid_users" runat="server" Width="99.9%" ForeColor="SteelBlue"
                            CssClass="gridRow2" HorizontalAlign="Center" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="White" OnRowEditing="Grid_users_RowEditing">
                            <Columns>
                                <asp:BoundField DataField="CUSTOMER_ID" HeaderText="Customer Id">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DISTRIBUTOR_ID" HeaderText="DISTRIBUTOR_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="BUSINESS_TYPE_ID" HeaderText="BUSINESS_TYPE_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PROMOTION_CLASS" HeaderText="PROMOTION_CLASS">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CHANNEL_TYPE_ID" HeaderText="CHANNEL_TYPE_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TOWN_ID" HeaderText="TOWN_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="AREA_ID" HeaderText="AREA_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ROUTE_ID" HeaderText="ROUTE_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="Code">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Name">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CONTACT_PERSON" HeaderText="Contact Person">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CONTACT_NUMBER" HeaderText="Contact Number">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EMAIL_ADDRESS" HeaderText="Email">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ADDRESS" HeaderText="Address">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="GST_NUMBER" HeaderText="Gst No">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ChannelType" HeaderText="Channel Type">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="GEO_NAME" HeaderText="Town">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="AREA_NAME" HeaderText="Route">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ROUTE_NAME" HeaderText="Market">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IS_ACTIVE" HeaderText="Status">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="REGDATE" DataFormatString="{0:dd-MMM-yyy}">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IS_STAND" HeaderText="Stand">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IS_COOLER" HeaderText="Cooler">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CNIC" HeaderText="CNIC">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="NTN" HeaderText="NTN">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CREDIT_LIMIT" HeaderText="CREDIT_LIMIT" DataFormatString="{0:f2}">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:CommandField ShowEditButton="True" HeaderText="Action">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="Center"></ItemStyle>
                                </asp:CommandField>
                            </Columns>
                            <FooterStyle BackColor="White"></FooterStyle>
                            <PagerStyle BackColor="Transparent"></PagerStyle>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="tblhead" Height="20px" />
                        </asp:GridView>
                        <asp:Label ID="lblNoRecords" Text="No records to display" runat="server" Visible="false" ForeColor="red" ></asp:Label>
                    </asp:Panel>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
