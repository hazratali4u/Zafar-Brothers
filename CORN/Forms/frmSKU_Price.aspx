<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSKU_Price.aspx.cs" Inherits="SKU_Price" Title="CORN :: Item Price" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        function pageLoad() {
            $("select").searchable();
            CheckAll();
        }

        function ValidateForm() {
            var str;
            str = document.getElementById('<%=ddskuName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Select Item Name');
                return false;
            }
            str = document.getElementById('<%=txtFromdate.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Select Price Effective Date');
                return false;
            }
            str = document.getElementById('<%=txtTaxPrices.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Tax Price');
                return false;
            }

            str = document.getElementById('<%=txtRetailPrice.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Sale Price');
                return false;
            }
            str = document.getElementById('<%=txtDistributorPrice.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Cost Price');
                return false;
            }
            return true;
        }
        function CheckBoxListSelect() {
            var chkBoxList = document.getElementById('<%= ChbDistributorList.ClientID %>');
            var chkBox = document.getElementById('<%= ChbSelectAll.ClientID %>');
            var chkBoxCount;
            var i;
            if (chkBox.checked == true) {
                chkBoxCount = chkBoxList.getElementsByTagName("input");
                for (i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = true;
                }
            }
            else {
                chkBoxCount = chkBoxList.getElementsByTagName("input");
                for (i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = false;
                }
            }
        }
        function CheckAll() {
            var checkboxes = $(':checkbox');

            // Check all checkboxes
            checkboxes.prop('checked', true);
        }
    </script>
    <div id="right_data">
        <div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True" ></asp:Label>
                                </td>
                                <td colspan="1">
                                </td>
                                <td colspan="1">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 106px; height: 18px" align="left">
                                    <strong>
                                        <asp:Label ID="Label1" runat="server" Width="103px" Text="Item Name" CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td style="width: 298px; height: 18px" align="left">
                                    
                                    <asp:DropDownList ID="ddskuName" runat="server" Width="220px" 
                                         CssClass="DropList" OnSelectedIndexChanged="ddskuName_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 32%; border-right: darkgray 1px ridge; border-top: darkgray 1px ridge;
                                    border-left: darkgray 1px ridge; border-bottom: darkgray 1px ridge" align="left"
                                    rowspan="10">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td class="tblhead" width="200">
                                                    <strong>
                                                        <asp:Label ID="Label11" runat="server" Width="140px" ForeColor="White" Font-Bold="True"
                                                            Text="Location's Name"></asp:Label></strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="ChbSelectAll" onclick="CheckBoxListSelect()" runat="server" Width="75px"
                                                        Font-Size="8pt" Text="Select All" ></asp:CheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="300">
                                                    <asp:Panel ID="Panel1" runat="server" Width="100%" Height="220px" ScrollBars="Vertical"
                                                        BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver" BackColor="White">
                                                        <asp:CheckBoxList ID="ChbDistributorList" runat="server" Width="99%" Font-Size="8pt">
                                                        </asp:CheckBoxList>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td style="height: 18px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 106px" align="left">
                                    <strong>
                                        <asp:Label ID="Label8" runat="server" Width="100px" Text="Date Effected"></asp:Label></strong>
                                </td>
                                <td style="width: 298px" align="left">
                                    <asp:TextBox Style="text-align: justify" ID="txtFromdate" TabIndex="2" runat="server"
                                        Width="148px"  CssClass="txtBox" ValidationGroup="MKE"
                                        MaxLength="1"></asp:TextBox>
                                    <asp:ImageButton ID="ImgBntFromCalc" runat="server"  ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                        CausesValidation="False"></asp:ImageButton><cc1:CalendarExtender ID="CalendarExtender1"
                                            runat="server"  Format="dd-MMM-yyyy" PopupButtonID="ImgBntFromCalc"
                                            TargetControlID="txtFromdate">
                                        </cc1:CalendarExtender>
                                </td>
                                <td style="width: 201px" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <strong>
                                        <asp:Label ID="Label10" runat="server" Width="90px" Text="Cost Price" __designer:wfdid="w381"
                                            CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td style="width: 298px" align="left">
                                    <asp:TextBox ID="txtDistributorPrice" runat="server" Width="193px" CssClass="txtBox "
                                        MaxLength="8"></asp:TextBox>
                                </td>
                                <td style="width: 201px" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 106px" align="left">
                                    <strong>
                                        <asp:Label ID="Label4" runat="server" Width="103px" Text="Sale Price" CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtRetailPrice" runat="server" Width="192px" CssClass="txtBox "
                                        MaxLength="8"></asp:TextBox>
                                </td>
                                <td style="width: 201px" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 106px; height: 12px" align="left">
                                    <strong>
                                        <asp:Label ID="Label5" runat="server" Width="102px" Text="G.S.T (%)" CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtTaxPrices" runat="server" Width="192px" CssClass="txtBox " MaxLength="8"
                                        Text="0"></asp:TextBox>
                                </td>
                                <td style="width: 201px; height: 12px" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 106px; height: 12px" align="left">
                                    <strong>
                                        <asp:Label ID="Label14" runat="server" Width="102px" Text="S.E.D (%)" CssClass="lblbox"
                                            Visible="False"></asp:Label></strong>
                                </td>
                                <td style="width: 298px; height: 12px" align="left">
                                    <asp:TextBox ID="txtSADTax" runat="server" Width="192px" CssClass="txtBox " MaxLength="8"
                                        Visible="False"></asp:TextBox>
                                </td>
                                <td style="width: 201px; height: 12px" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 106px">
                                </td>
                                <td style="width: 298px" align="left">
                                    <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="90px" Font-Size="8pt"
                                        Text="Save" ValidationGroup="vg" CssClass="Button" />
                                    &nbsp;
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" __designer:wfdid="w392"
                                        TargetControlID="txtTaxPrices" ValidChars="0123456789." FilterType="Custom">
                                    </cc1:FilteredTextBoxExtender>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" __designer:wfdid="w393"
                                        TargetControlID="txtTradePrice" ValidChars=".0123456789" FilterType="Custom">
                                    </cc1:FilteredTextBoxExtender>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" __designer:wfdid="w394"
                                        TargetControlID="txtRetailPrice" ValidChars=".0123456789" FilterType="Custom">
                                    </cc1:FilteredTextBoxExtender>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" __designer:wfdid="w395"
                                        TargetControlID="txtDistributorPrice" ValidChars=".0123456789" FilterType="Custom">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td align="left">
                                </td>
                                <td align="left">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div style="z-index: 101; left: 530px; width: 100px; position: absolute; top: 440px;
                        height: 100px">
                        &nbsp;
                        <asp:Panel ID="Panel21" runat="server" __designer:wfdid="w396">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" __designer:wfdid="w397" AssociatedUpdatePanelID="UpdatePanel3">
                                <ProgressTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" Width="23px" Height="26px" __designer:wfdid="w398"
                                        ImageUrl="~/App_Themes/Granite/Images/image003.gif"></asp:ImageButton>
                                    Wait Update
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </asp:Panel>
                    </div>
                   
                    <asp:TextBox ID="txtTradePrice" runat="server" Width="192px" CssClass="txtBox " MaxLength="8"
                        Visible="false"></asp:TextBox>
                        <asp:DropDownList ID="ddskuPrincipal" runat="server" Visible="false" Width="220px"
                                        CssClass="DropList" OnSelectedIndexChanged="ddskuPrincipal_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                         <asp:DropDownList ID="ddskuDivision" Visible="false" runat="server" Width="220px"
                                 CssClass="DropList" OnSelectedIndexChanged="ddskuDivision_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
                           
                            <asp:DropDownList ID="ddskuCategory" runat="server" Visible="false" Width="220px"
                                CssClass="DropList" OnSelectedIndexChanged="ddskuCategory_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
                            
                            <asp:DropDownList ID="ddskuSubCategory" Visible="false" runat="server" Width="220px"
                                CssClass="DropList" OnSelectedIndexChanged="ddskuSubCategory_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
                          
                            <asp:DropDownList ID="ddskuBrand" runat="server" Width="220px" CssClass="DropList"
                                OnSelectedIndexChanged="ddskuBrand_SelectedIndexChanged" AutoPostBack="True"
                                Visible="False">
                            </asp:DropDownList>
                          
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel2" runat="server" Width="100%" Height="200px" ScrollBars="Vertical"
                        BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                        <asp:GridView ID="Grid_pricedetails" runat="server" Width="100%" ForeColor="SteelBlue"
                            CssClass="gridRow2" BorderColor="White" BackColor="White" AutoGenerateColumns="False"
                            HorizontalAlign="Center">
                            <AlternatingRowStyle BackColor="#E0E0E0" />
                            <Columns>
                                <asp:BoundField DataField="distributor_name" HeaderText="Location">
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="20%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SKU_CODE" HeaderText="Item Code">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="15%">
                                    </ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SKU_NAME" HeaderText="Item Name">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="20%">
                                    </ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="GST_ON" HeaderText="GST">
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DISTRIBUTOR_PRICE" HeaderText="Cost Price" DataFormatString="{0:F2}">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TRADE_PRICE" HeaderText="Sale Price" DataFormatString="{0:F2}"
                                    >
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RETAIL_PRICE" HeaderText="Sale Price" Visible="false" DataFormatString="{0:F2}">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TAX_PRICE" HeaderText="GST (%)" DataFormatString="{0:F2}">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle BorderColor="DarkGray" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SED_TAX" DataFormatString="{0:F2}" HeaderText="SED (%)">
                                    <ItemStyle CssClass="HidePanel" />
                                    <HeaderStyle CssClass="HidePanel" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DATE_EFFECTED" HeaderText="Date Effected">
                                    <ItemStyle BackColor="Transparent" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                    </ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="tblhead"></HeaderStyle>
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
