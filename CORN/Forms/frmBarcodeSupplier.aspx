<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmBarcodeSupplier.aspx.cs" Inherits="Forms_frmBarcodeSupplier" Title="CORN :: Supplier Barcode" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <script language="javascript" type="text/javascript">
      
        function checkTextbox(){
            if ($('#<%=txtcode.ClientID %>').val() == "") {
                alert('Please Enter 10 digit code...');
                return false;
            }
            if ($('#<%=txtSize.ClientID %>').val() == "") {
                alert('Please enter size...');
                return false;
            }
             if ($('#<%=txtquantity.ClientID %>').val() == "") {
                alert('Please enter number of rows...');
                return false;
            }
            return true;
        }
    </script>
    <div id="right_data">
        <asp:UpdatePanel ID="up_pnl1" runat="server">
            <ContentTemplate>
                <div style="display: none;">
                    <table>
                        <tr>
                            <td>
                                <strong>
                                    <asp:Label ID="lbl_code" runat="server" Width="90px" Text="Item Code"></asp:Label></strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_code" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
            <table width="100%">
                <tr>
                    <td><asp:Label runat="server" ID="lblSelectnew" Text="Select Document"></asp:Label>
                        <asp:DropDownList runat="server" Width="350px" AutoPostBack="true" ID="drpSelectDocument" CssClass="ddlist" OnSelectedIndexChanged="drpSelectDocument_SelectedIndexChanged" ></asp:DropDownList>
                    </td></tr><tr>
                    <td>
                        
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblskuCode" runat="server" Width="300px" CssClass="label" Text="Supplier"> </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Width="100px" CssClass="label" Text="Code"> </asp:Label>
                                            </td>
                                           <td>
                                                <asp:Label ID="Label1" runat="server" Width="100px" CssClass="label" Text="Size"> </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Width="100px" CssClass="label" Text="No of Rows"> </asp:Label>
                                            </td>
                                            <td >
                                                <asp:Label ID="Label41" runat="server" Width="95px" Text="Add Item" CssClass="label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="drpSupplier" runat="server" Width="300px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtcode" MaxLength="10"  runat="server" Width="100px" CssClass="txtBox "></asp:TextBox>
                                               <%-- <cc1:FilteredTextBoxExtender ID="ftbe_txtrow" runat="server" ValidChars="123456789"
                                                    TargetControlID="txtQuantity" />--%>
                                            </td>
                                             <td>
                                                <asp:TextBox ID="txtSize" runat="server" Width="100px"  CssClass="txtBox "></asp:TextBox>
                                             
                                            </td>
                                             <td>
                                                <asp:TextBox ID="txtquantity" Text="1" runat="server" Width="100px"  CssClass="txtBox "></asp:TextBox>
                                             
                                            </td>
                                             
                                            <td >
                                                <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" OnClientClick="return checkTextbox()" runat="server" Width="100px"
                                                    Font-Size="8pt" Text="Add Item" CssClass="Button" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="5">
                                                <asp:Panel ID="Panel2" runat="server" Width="730px" Height="200px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="GrdPurchase" runat="server" Width="710px" ForeColor="SteelBlue"
                                                        CssClass="gridRow2" BorderColor="White" BackColor="White" ShowHeader="False"
                                                        OnRowDeleting="GrdPurchase_RowDeleting" OnRowEditing="GrdPurchase_RowEditing"
                                                        HorizontalAlign="Center" AutoGenerateColumns="False">
                                                        <RowStyle ForeColor="Black" />
                                                        <AlternatingRowStyle BackColor="#E0E0E0" />
                                                        <Columns>
                                                            <asp:BoundField DataField="Company_Id" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                         
                                                            <asp:BoundField DataField="Company_Name" HeaderText="Supplier">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                                    Width="300px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Code" HeaderText="Code" >
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                                    Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SIZE" HeaderText="Size">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                    Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Quantity" HeaderText="No of Rows">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                    Width="100px" />
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
                                                      </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                           
                    </td>
                </tr>
            </table>
                                 </ContentTemplate>
                        </asp:UpdatePanel>
            &nbsp;
        </div>
        <div>
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btn_generate" runat="server" Text="Generate" OnClick="btn_generate_Click"
                            Width="80" CssClass="Button" CausesValidation="true" />
                        <asp:Button ID="btn_generateSingle" runat="server" Text="Generate Single" 
                            Width="120" CssClass="Button" CausesValidation="true" OnClick="btn_generateSingle_Click" />
                         <asp:Button ID="btn_void" runat="server" Text="Void" 
                            Width="120" CssClass="Button"  OnClick="btn_void_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
