<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmImageViewer.aspx.cs" Inherits="frmImageViewer" Title="CORN :: Image Viewer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        function pageLoad() {

            $("#<%=drpDistributor.ClientID %>").searchable();
        }


        function CheckBoxListSelectGrid() {
           
                var chkBoxList = document.getElementById('<%= GrdImage.ClientID %>');
                var chkBox = document.getElementById('<%= chbSelectAllDoc.ClientID %>');
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




        function CheckBoxListSelectPrincipal() {
                var chkBoxList = document.getElementById('<%= chbPrincipal.ClientID %>');
                var chkBox = document.getElementById('<%= ChbSelectAllPrincipal.ClientID %>');
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


            function CheckBoxListSelectSalePerson() {
                var chkBoxList = document.getElementById('<%= chbGender.ClientID %>');
                var chkBox = document.getElementById('<%= ChbSelectAllSalePerson.ClientID %>');
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

            function CheckBoxListSelectSeason() {
                var chkBoxList = document.getElementById('<%= ChbSeason.ClientID %>');
                var chkBox = document.getElementById('<%= ChbSelectAllSeason.ClientID %>');
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

            function CheckBoxListSelectRegion() {
                var chkBoxList = document.getElementById('<%= chbcategory.ClientID %>');
                var chkBox = document.getElementById('<%= ChbSelectAllRegion.ClientID %>');
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
        <table width="100%">
            <tr>
                <td style="padding-left: 5px;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnl_rpt" runat="server">
                                <table>
                                    <tr>
                                        <td align="left" colspan="4">
                                            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="lbltoLocation" runat="server" CssClass="lblbox" Text="Location" Width="73px"></asp:Label></strong>
                                        </td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList">
                                            </asp:DropDownList>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:CheckBox ID="ChbSelectAllPrincipal" Checked="true" onclick="CheckBoxListSelectPrincipal()" runat="server" Width="75px"
                                                Font-Size="8pt" Text="Select All"></asp:CheckBox>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:CheckBox ID="ChbSelectAllSalePerson" Checked="true" onclick="CheckBoxListSelectSalePerson()" runat="server" Width="75px"
                                                Font-Size="8pt" Text="Select All"></asp:CheckBox>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td align="left" style="height: 25px"></td>
                                        <td align="left" style="height: 25px">
                                            <strong>

                                                <asp:Label ID="Label2" runat="server" CssClass="lblbox" Text="Supplier" Width="78px"></asp:Label></strong>
                                        </td>
                                        <td align="left" style="height: 25px"></td>
                                        <td align="left" style="height: 25px">
                                            <asp:Panel ID="Panel3" runat="server" BorderWidth="1px" Width="100%" ScrollBars="Auto" Height="150px">
                                                <asp:CheckBoxList ID="chbPrincipal" runat="server"></asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                        <td align="left" style="height: 25px">
                                            <strong>

                                                <asp:Label ID="Label6" runat="server" CssClass="lblbox" Text="Gender" Width="78px"></asp:Label></strong>
                                        </td>

                                        <td align="left" style="height: 25px; width: 200px">
                                            <asp:Panel ID="Panel1" runat="server" BorderWidth="1px" Width="100%" ScrollBars="Auto" Height="150px">
                                                <asp:CheckBoxList ID="chbGender" runat="server"></asp:CheckBoxList>
                                            </asp:Panel>

                                        </td>

                                    </tr>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:CheckBox ID="ChbSelectAllSeason" Checked="true" onclick="CheckBoxListSelectSeason()" runat="server" Width="75px"
                                                Font-Size="8pt" Text="Select All"></asp:CheckBox>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:CheckBox ID="ChbSelectAllRegion" Checked="true" onclick="CheckBoxListSelectRegion()" runat="server" Width="75px"
                                                Font-Size="8pt" Text="Select All"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    </tr>
                                   
                                        <td align="left"></td>
                                    <td align="left">
                                        <strong>

                                            <asp:Label ID="Label5" runat="server" CssClass="lblbox" Text="Season" Width="78px"></asp:Label></strong>
                                    </td>
                                    <td align="left"></td>
                                    <td align="left" style="height: 25px">
                                        <asp:Panel ID="Panel2" runat="server" BorderWidth="1px" Width="100%" ScrollBars="Auto" Height="150px">
                                            <asp:CheckBoxList ID="ChbSeason" runat="server"></asp:CheckBoxList>
                                        </asp:Panel>
                                    </td>
                                    <td align="left">
                                        <strong>

                                            <asp:Label ID="Label1" runat="server" CssClass="lblbox" Text="Category" Width="78px"></asp:Label></strong>
                                    </td>

                                    <td align="left" style="height: 25px">
                                        <asp:Panel ID="abc" runat="server" BorderWidth="1px" Width="100%" ScrollBars="Auto" Height="150px">
                                            <asp:CheckBoxList ID="chbcategory" runat="server"></asp:CheckBoxList>
                                        </asp:Panel>

                                    </td>

                                    <tr style="display:none;">
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Height="13px" Text="Till Date" Width="70px"></asp:Label></strong>
                                        </td>
                                        <td align="left"></td>
                                        <td align="left" style="height: 25px">&nbsp;<asp:TextBox ID="txtStartDate" runat="server" CssClass="txtBox" MaxLength="10"
                                            onkeyup="BlockStartDateKeyPress()" Width="150px"></asp:TextBox>
                                            <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                Width="16px" />
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Height="13px" Text="Min. Stock" Width="70px"></asp:Label></strong>
                                        </td>
                                        <td align="left"></td>
                                        <td align="left" style="height: 25px">
                                            <asp:TextBox ID="txtMiminumStock" runat="server" CssClass="txtBox" MaxLength="10"
                                             Width="150px"></asp:TextBox>
                                        
                                        </td>
                                    </tr>
                                   
                                    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
                             
                                    <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate"
                                        TargetControlID="txtStartDate">
                                    </cc1:CalendarExtender>
                                  
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <asp:Button ID="btnSearch" runat="server" CssClass="Button" Width="90" Text="Get Data"
                        OnClick="btnSearch_Click" />
                    <asp:Button ID="btnDownLoadall" runat="server" Width="120" Text="Download All" OnClick="btnDownLoadall_Click"     CssClass="Button" />
                </td>
            </tr>

        </table>
        <table>
            <tr>
                <asp:UpdatePanel ID="upnl" runat="server">
                    <ContentTemplate>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                       
                                           <asp:CheckBox ID="chbSelectAllDoc" Checked="false" onclick="CheckBoxListSelectGrid()"  runat="server" Width="75px"
                                                Font-Size="8pt" Text="Select All"></asp:CheckBox>
                                      
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlGrid" runat="server" Width="900px" Height="300px" ScrollBars="Horizontal" BorderStyle="Solid" BorderWidth ="1px">
                                             <asp:GridView ID="GrdImage" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                BorderColor="White" ForeColor="SteelBlue" HorizontalAlign="Center"  DataKeyNames="PicUrl" OnRowEditing="GrdImage_RowEditing" OnRowDeleting="GrdImage_RowDeleting" Width="100%">
                                                <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                    PreviousPageText="Previous" />
                                                 <AlternatingRowStyle BackColor="#E0E0E0" />
                                                <Columns>
                                                    <asp:BoundField ReadOnly="true" DataField="Sku_id" HeaderText="Sku_id">
                                                         <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                     <asp:BoundField ReadOnly="true" DataField="PicUrl" HeaderText="PicUrl">
                                                         <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                       <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ChbSku" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" Width="20px" BorderStyle="Solid" BorderWidth="1px" />
                                                            </asp:TemplateField>
                                                    <asp:BoundField ReadOnly="true" DataField="SKU_CODE" HeaderText="Style  Code">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField ReadOnly="true" DataField="SKU_NAME" HeaderText="Item Name">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="250px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField ReadOnly="true" DataField="Bar_code" HeaderText="Bar Code">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="100px" />
                                                    </asp:BoundField>
                                                     <asp:BoundField DataField="DISTRIBUTOR_PRICE" ReadOnly="true" HeaderText="Cost Price" DataFormatString="{0:F2}">
                                                <ItemStyle CssClass="grdDetail" Width="10%" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                                     <asp:BoundField DataField="TRADE_PRICE" ReadOnly="true" HeaderText="Sale Price" DataFormatString="{0:F2}">
                                                <ItemStyle CssClass="grdDetail" Width="10%" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                                    <asp:BoundField DataField="CLOSING_STOCK" ReadOnly="true" HeaderText="Closing Stock">
                                                <ItemStyle CssClass="grdDetail" Width="10%" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                                <asp:BoundField DataField="STOCK_POSITION" ReadOnly="true" HeaderText="Stock Position">
                                                <ItemStyle CssClass="grdDetail" Width="10%" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            
                                                    <asp:TemplateField HeaderText="View">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnview" runat="server" CommandName="edit" Text="View" ToolTip="View">
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid" Width="45px"></ItemStyle>
                                                    </asp:TemplateField>

                                                      <asp:TemplateField HeaderText="Download">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btndownload" runat="server" CommandName="delete" Text="Download" ToolTip="Download">
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid" Width="45px"></ItemStyle>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <HeaderStyle CssClass="tblhead" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </ContentTemplate>
                   
                </asp:UpdatePanel>
            </tr>
        </table>

    </div>
</asp:Content>
