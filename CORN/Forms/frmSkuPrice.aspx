<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSkuPrice.aspx.cs" Inherits="SkuPrice" Title="CORN :: Item Price" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
 
    <script language="JavaScript" type="text/javascript">

       

        function ValidateForm() {
            var str;

            str = document.getElementById('<%=txtFromdate.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Select Price Effective Date');
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
    </script>
    <div id="right_data">
        <div>
            <div style="z-index: 101; left: 530px; width: 100px; position: absolute; top: 440px; height: 100px">
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

            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                </td>
                                <td colspan="1"></td>
                                <td colspan="1"></td>
                            </tr>

                            <tr>
                                <td style="width: 95px">
                                    <strong>
                                        <asp:Label ID="Label1" runat="server" Width="100px" Text="Location"></asp:Label>
                                    </strong>
                                </td>
                                <td style="width: 123px">
                                    <asp:DropDownList ID="DrpDistributor"
                                        AutoPostBack="true" runat="server" Width="220px" OnSelectedIndexChanged="DrpDistributor_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 32%; border-right: darkgray 1px ridge; border-top: darkgray 1px ridge;
                                    border-left: darkgray 1px ridge; border-bottom: darkgray 1px ridge" align="left"
                                    rowspan="6">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td class="tblhead" width="200">
                                                    <strong>
                                                        <asp:Label ID="Label11" runat="server" Width="140px" ForeColor="White" Font-Bold="True"
                                                            Text="Price For Location"></asp:Label></strong>
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
                                                    <asp:Panel ID="Panel1" runat="server" Width="100%" Height="150px" ScrollBars="Vertical"
                                                        BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver" BackColor="White">
                                                        <asp:CheckBoxList ID="ChbDistributorList" runat="server" Width="99%" Font-Size="8pt">
                                                        </asp:CheckBoxList>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 95px">
                                    <strong>
                                        <asp:Label ID="Label2" runat="server" Width="100px" Text="Supplier"></asp:Label>
                                    </strong>
                                </td>
                                <td style="width: 123px">
                                    <asp:DropDownList ID="drpPrincipal" runat="server" Width="220px"
                                        AutoPostBack="true" CssClass="DropList" OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 95px">
                                    <strong>
                                        <asp:Label ID="Label3" runat="server" Width="100px" Text="Gender"></asp:Label>
                                    </strong>
                                </td>
                                <td style="width: 123px">
                                    <asp:DropDownList ID="drpGender"
                                        AutoPostBack="true" runat="server" Width="220px" CssClass="DropList" OnSelectedIndexChanged="drpGender_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 95px">
                                    <strong>
                                        <asp:Label ID="Label6" runat="server" Width="100px" Text="Category"></asp:Label>
                                    </strong>
                                </td>
                                <td style="width: 123px">
                                    <asp:DropDownList ID="drpcategory"
                                        AutoPostBack="true" runat="server" Width="220px" OnSelectedIndexChanged="drpcategory_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>




                            <tr>
                                <td style="width: 95px" align="left">
                                    <strong>
                                        <asp:Label ID="Label8" runat="server" Width="100px" Text="Date Effected"></asp:Label></strong>
                                </td>
                                <td style="width: 123px" align="left">
                                    <asp:TextBox Style="text-align: justify" ID="txtFromdate" TabIndex="2" runat="server"
                                        Width="148px" CssClass="txtBox" ValidationGroup="MKE"
                                      AutoPostBack="true"  MaxLength="1" OnTextChanged="txtFromdate_TextChanged"></asp:TextBox>
                                    <asp:ImageButton ID="ImgBntFromCalc" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                        CausesValidation="False"></asp:ImageButton><cc1:CalendarExtender ID="CalendarExtender1"
                                            runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImgBntFromCalc"
                                            TargetControlID="txtFromdate">
                                        </cc1:CalendarExtender>
                                </td>
                            </tr>



                            <tr>
                                <td style="width: 95px"></td>
                                <td style="width: 123px" align="left">
                                    <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="90px" Font-Size="8pt"
                                        Text="Save" ValidationGroup="vg" CssClass="Button" />
                                    &nbsp;
                                </td>
                            </tr>
                        </tbody>
                    </table>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel2" runat="server" Width="95%" Height="300px" ScrollBars="Vertical"
                        BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                        <asp:GridView ID="Grid_pricedetails" runat="server" Width="100%" ForeColor="SteelBlue"
                            CssClass="gridRow2" BorderColor="White" BackColor="White" AutoGenerateColumns="False"
                            HorizontalAlign="Center">
                               <AlternatingRowStyle BackColor="#E0E0E0" />
                            <Columns>

                                <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                    <ItemStyle CssClass="HidePanel" />
                                    <HeaderStyle CssClass="HidePanel" />
                                </asp:BoundField>

                                <asp:BoundField DataField="SKU_CODE" HeaderText="Item Code">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="15%"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SKU_NAME" HeaderText="Item Name">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="40%"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Purchase Price">

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPURCHASE_PRICE" runat="server" Width="75px" Text='<%# Eval("PURCHASE_PRICE")%>'></asp:TextBox>


                                    </ItemTemplate>
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cost Price">

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDISTRIBUTOR_PRICE" runat="server" Width="75px" Text='<%# Eval("DISTRIBUTOR_PRICE")%>'></asp:TextBox>


                                    </ItemTemplate>
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Sale Price">

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTRADE_PRICE" runat="server" Width="75px" Text='<%# Eval("TRADE_PRICE")%>'></asp:TextBox>


                                    </ItemTemplate>
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" />

                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Tax Price">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTax_price" runat="server" Width="75px" Text='<%# Eval("TAX_PRICE")%>'></asp:TextBox>

                                    </ItemTemplate>
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" />

                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Date Effected">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDATE_EFFECTED" runat="server" Width="70px" Enabled="false" Text='<%# Eval("Date_effected", "{0:dd-MMM-yyyy}")%>'></asp:TextBox>
                                        <asp:ImageButton ID="ImgBntFromCalc" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                            CausesValidation="False"></asp:ImageButton>
                                        <cc1:CalendarExtender ID="CalendarExtender10"
                                            runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImgBntFromCalc"
                                            TargetControlID="txtDATE_EFFECTED">
                                        </cc1:CalendarExtender>
                                    </ItemTemplate>
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" />

                                </asp:TemplateField>


                                 <asp:BoundField DataField="RETAIL_PRICE" HeaderText="RETAIL_PRICE">
                                    <ItemStyle CssClass="HidePanel" />
                                    <HeaderStyle CssClass="HidePanel" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="SED_TAX" HeaderText="SED_TAX">
                                    <ItemStyle CssClass="HidePanel" />
                                    <HeaderStyle CssClass="HidePanel" />
                                </asp:BoundField>
                                
                                 <asp:BoundField DataField="DISTRIBUTOR_DISCOUNT" HeaderText="DISTRIBUTOR_DISCOUNT">
                                    <ItemStyle CssClass="HidePanel" />
                                    <HeaderStyle CssClass="HidePanel" />
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
