<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmOpeningStock.aspx.cs" Inherits="Forms_frmOpeningStock" Title="CORN :: Stock Adjustment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    
   
    <script language="JavaScript" type="text/javascript">

        function ConfirmDelete() {
            if (confirm("Do you want to Cancel this record?") == true)
                return true;

            else {
                return false;
            }
        }

        function ValidateForm() {
            var str;

           
            str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Quantity');
                return false;
            }

            return true;
        }
        function SearchList() {
            var l = document.getElementById('<%= lstCode.ClientID %>');
            var tb = document.getElementById('<%= txtskuCode.ClientID %>');

            if (tb.value == "") {
                ClearSelection(l);
            }
            else {
                for (var i = 0; i < l.options.length; i++) {
                    if (l.options[i].value.toLowerCase().match(tb.value.toLowerCase())) {
                        l.options[i].selected = true;
                        return false;
                    }
                    else {
                        ClearSelection(l);
                    }
                }
            }
        }
        function SetFocusTocode(e) {

            if (e.keyCode == 13) {
                document.getElementById("<%= txtskuCode.ClientID %>").focus();
            }
        }
        function SearchedCode() {
            var l = document.getElementById('<%= lstCode.ClientID %>');
            var str;
            for (var i = 0; i < l.options.length; i++) {
                if (l.options[i].selected) {
                    str = l.options[i].value;
                    ClearSelection(l);
                    break;
                }
                else {
                    str = "";
                }
            }
            var stroption = document.getElementById("<%= txtskuCode.ClientID %>").value;
            if (str.length > 0) {
                document.getElementById("<%= txtskuCode.ClientID %>").value = str.substring(0, str.indexOf('-'));
                document.getElementById("<%= txtskuName.ClientID %>").value = str.substring(str.indexOf('-') + 1, str.indexOf('('));
             
                document.getElementById("<%= txtsize.ClientID %>").value = str.substring(str.indexOf(')') + 1);
                document.getElementById("<%= Panel1.ClientID %>").className = "HidePanel";
            }
            else if (stroption.length == 0) {
                document.getElementById("<%= Panel1.ClientID %>").className = "ShowPanel";
                document.getElementById("<%= lstCode.ClientID %>").focus();

            }

        }
        function SelectCode(e) {
            if (e.keyCode == 13) {
                var str = document.getElementById("<%= lstCode.ClientID %>").value;
                document.getElementById("<%= txtskuCode.ClientID %>").value = str.substring(0, str.indexOf('-'));
                document.getElementById("<%= txtskuName.ClientID %>").value = str.substring(str.indexOf('-') + 1);
                document.getElementById("<%= txtsize.ClientID %>").value = str.substring(str.indexOf(')') + 1);
                document.getElementById("<%= Panel1.ClientID %>").className = "HidePanel";
                document.getElementById("<%= txtQuantity.ClientID %>").focus();

            }
        }
        function ClearSelection(lb) {
            lb.selectedIndex = -1;
        }

    </script>
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
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="2">
                                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                            </td>
                                            <td align="left" colspan="1">
                                                &nbsp; &nbsp; &nbsp; &nbsp;
                                            </td>
                                            <td style="width: 316px" valign="middle" align="center" colspan="1" rowspan="7">
                                                <strong>
                                                    <asp:Label ID="Label4" runat="server" Width="239px" Height="8px" CssClass="lblbox"></asp:Label></strong>
                                                &nbsp; &nbsp;&nbsp;
                                                <asp:Panel ID="Panel1" runat="server" Width="250px" Height="170px" CssClass="HidePanel"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver" BackColor="Silver">
                                                    <strong>
                                                        <asp:Label ID="Label3" runat="server" CssClass="lblbox" Width="170px">Select from Item List</asp:Label></strong><br />
                                                    <asp:ListBox ID="lstCode" runat="server" Height="154px" onkeyup="SelectCode(event)"
                                                        SelectionMode="Multiple" Width="245px"></asp:ListBox>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label2" runat="server" Width="102px" Height="14px" Text="Transaction Type"
                                                        CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="DrpDocumentType" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="DrpDocumentType_SelectedIndexChanged">
                                                    <%--<asp:ListItem Value="7">Opening Stock</asp:ListItem>--%>
                                                    <asp:ListItem Value="8">Short</asp:ListItem>
                                                    <asp:ListItem Value="9">Excess</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px">
                                                <strong>
                                                    <asp:Label ID="Label5" runat="server" Width="50px" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblDocumentNo" runat="server" Width="94px" Text="Document No" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpDocumentNo" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px">
                                            </td>
                                        </tr>
                                        <tr>
                                            
                                                    <asp:Label ID="lbltoLocation" Visible="false" runat="server" Width="94px" Text="Supplier" CssClass="lblbox"></asp:Label>
                                                <asp:DropDownList ID="drpPrincipal" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" Visible="false" OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged">
                                                </asp:DropDownList>
                                          
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblfromLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="94px" Text="Remarks" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:TextBox ID="txtDocumentNo" runat="server" Width="300px" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                            <td style="height: 25px">
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
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblskuCode" runat="server" Width="360px" 
                                                CssClass="label"  Text="Item Description" ></asp:Label>
                                            </td>
                                            
                                             
                                            <td >
                                                <asp:Label ID="lblSize" runat="server" Width="96%" 
                                                     Text="Size" CssClass="label" ></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblquantity" runat="server" Width="69px"  
                                                    Text="Quantity" CssClass="label" ></asp:Label>
                                            </td>
                                         
                                            
                                            <td style="width: 100px">
                                               
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                              <asp:DropDownList ID="ddlSKuCde" runat="server" Width="364px"  onfocus="ddlFocus(this);" 
                                                    onblur="ddlBlur(this);" >
                                                </asp:DropDownList>
                                          
                                                <asp:TextBox ID="txtskuCode" onkeyup="SearchList()" runat="server" Width="145px" Visible="False" CssClass="txtBox"></asp:TextBox>
                                            
                                                <asp:TextBox ID="txtskuName" runat="server" Width="200px" Font-Bold="True" CssClass="txtBox"
                                                    Enabled="False" Visible="False"></asp:TextBox>
                                            </td>
                                            
                                            <td>
                                                <asp:TextBox ID="txtsize" runat="server" Width="76px" CssClass="txtBox" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQuantity" runat="server" Width="70px" Text="1"
                                                    CssClass="txtBox "></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                    FilterType="Custom" ValidChars="0123456789" TargetControlID="txtQuantity">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </td>
                                        
                                            
                                            <td style="width: 100px">
                                                <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="95px"
                                                    Font-Size="8pt" Text="Add Item" ValidationGroup="vg" CssClass="Button" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="4">
                                                <asp:Panel ID="Panel2" runat="server" Width="625px" Height="220px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="GrdPurchase" runat="server" Width="608px" ForeColor="SteelBlue"
                                                        CssClass="gridRow2" BorderColor="White" BackColor="White" ShowHeader="False"
                                                        OnRowDeleting="GrdPurchase_RowDeleting"  OnRowEditing="GrdPurchase_RowEditing"
                                                         HorizontalAlign="Center" AutoGenerateColumns="False">
                                                        
                                                        <RowStyle ForeColor="Black" />
                                                         <AlternatingRowStyle BackColor="#E0E0E0" />
                                                        <Columns>
                                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                                                                <ItemStyle  CssClass="grdDetail" HorizontalAlign="Left"
                                                                    Width="85px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                                <ItemStyle  CssClass="grdDetail" HorizontalAlign="Left"
                                                                    Width="264px" />
                                                            </asp:BoundField>
                                                           
                                                            <asp:BoundField DataField="FREE_SKU" HeaderText="Free SKU">
                                                                 <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                  
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="RACK_NO" HeaderText="BatchNo">
                                                              <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            </asp:BoundField>
                                                              
                                                             <asp:BoundField DataField="PACKSIZE" HeaderText="PACKSIZE">
                                                               <ItemStyle  CssClass="grdDetail" HorizontalAlign="Right"
                                                                    Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="COLOR" HeaderText="COLOR">
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            </asp:BoundField>
                                                             <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                                <ItemStyle  CssClass="grdDetail" HorizontalAlign="Right"
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
                                <asp:Button AccessKey="S" ID="btnSaveDocument" OnClick="btnSaveDocument_Click" runat="server"
                                    Width="119px" Font-Size="8pt" Text="Save Document" UseSubmitBehavior="False"
                                    CssClass="Button" />
                                <asp:Button AccessKey="C" ID="btnCancel" OnClick="btnCancel_Click" runat="server"
                                    Width="120px" Font-Size="8pt" Text="Cancel" UseSubmitBehavior="False" CssClass="Button" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            &nbsp;
        </div>
    </div>
</asp:Content>
