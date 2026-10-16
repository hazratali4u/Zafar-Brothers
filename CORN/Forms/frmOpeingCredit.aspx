<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmOpeingCredit.aspx.cs" Inherits="Forms_frmOpeingCredit" Title="CORN :: Opening Credit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <style>
        .ajax__calendar .ajax__calendar_container {
    border: 1px solid #646464;
    background-color: #ffffff;
    color: #000000;
    z-index: 999;
}
    </style>
   
    <script language="JavaScript" type="text/javascript">

     
        function ValidateForm() {
            var str;
           
            str = document.getElementById("<%= txtAmount.ClientID %>").value;
            if (str == null || str.length == 0) {
                alert('Must Enter Amount');
                return false;
            }
            str = document.getElementById("<%= txtFromdate.ClientID %>").value;
            if (str == null || str.length <= 1) {
                alert('Must Enter Invoice Date');
                return false;
            }
            str = document.getElementById("<%= txtChequeNo.ClientID %>").value;
            if (str == null || str.length <= 1) {
                alert('Must Enter Invoice No');
                return false;
            }
        }
      
        function SelectCode(e) {
            if (e.keyCode == 13) {
                

                var str = document.getElementById("<%= ListCustomer.ClientID %>").value;
             
                document.getElementById("<%= txtChequeNo.ClientID %>").focus();
            }
        }
        function ClearSelection(lb) {
            lb.selectedIndex = -1;
        }
 
    </script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="2">
                                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                            </td>
                                            <td style="width: 50px" align="left" colspan="1"></td>
                                            <td align="left" colspan="1"></td>
                                        </tr>
                                        <tr>
                                            <td align="left"></td>
                                            <td align="left"></td>
                                            <td style="width: 50px" align="left"></td>
                                            <td align="left"></td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label7" runat="server" Width="94px" Text="Credit Type" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DrpCreditType" runat="server" Width="200px" AutoPostBack="true"
                                                    OnSelectedIndexChanged="DrpCreditType_SelectedIndexChanged">
                                                    <asp:ListItem Value="24">New Credit</asp:ListItem>
                                                    <asp:ListItem Value="25" Selected="True">Opening Credit</asp:ListItem>
                                                    <asp:ListItem Value="26">Credit Transfer In</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 50px" align="left"></td>
                                            <td>
                                               
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lblfromLocation" runat="server" Width="94px" Text="Distributor" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                            <td align="left" rowspan="11">


                                           <%--    
                                                <asp:ListBox ID="ListCustomer" onkeyup="SelectCode(event)" runat="server" Width="250px"
                                                    Height="210px" CssClass="DropList"></asp:ListBox>--%>
                                               
                                                <strong>
                                                    <asp:Label ID="lblDocumentNo" runat="server" Width="32px" Text="" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="89px" Text="Bill Date" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:TextBox Style="text-align: justify" ID="txtFromdate" onkeyup="BlockFromDateKeyPress()"
                                                    runat="server" Width="192px" ></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:ImageButton ID="ImgBntFromCalc" runat="server" CausesValidation="False" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton>
                                            </td>
                                        </tr>
                                       
                                        <tr>
                                            <td  align="left">
                                           <strong>
                                                    <asp:Label ID="Label2" runat="server" Width="94px" Text="Territory" CssClass="lblbox">

                                                    </asp:Label></strong>
                                                
                                            </td>
                                            <td >
                                                <asp:DropDownList ID="drpTerritory"
                                                    runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="drpTerritory_SelectedIndexChanged">
                                                </asp:DropDownList></td>
                                            </td>
                                            <td valign="top">
                                                <asp:HiddenField ID="hfPrincipalID" runat="server"  Value="-1"></asp:HiddenField>
                                                <asp:HiddenField ID="hfLegendID" runat="server" Visible="False" 
                                                    Value="-1"></asp:HiddenField>
                                                <asp:HiddenField ID="hfCustomerID" runat="server"  Value="-1"></asp:HiddenField>
                                                <asp:HiddenField ID="hfSaleInvoiceID" runat="server"  Value="-1"></asp:HiddenField>
                                            </td>
                                        </tr>
                                        <tr>

                                            <td>
                                                 <strong>
                                                    <asp:Label ID="Label8" runat="server" Width="94px" Text="Customer" CssClass="lblbox">

                                                    </asp:Label></strong>
                                          
                                            </td>
                                            <td>

                                                  <asp:DropDownList ID="ListCustomer"  runat="server" Width="200px" OnSelectedIndexChanged="ListCustomer_SelectedIndexChanged" 
                                                     CssClass="DropList"></asp:DropDownList>
                                            </td>



                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label5" runat="server" Width="94px" Text="Invoice No" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                                <strong>
                                                    <asp:Label ID="Label6" runat="server" Width="74px" Text="Amount" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td valign="top" align="left"></td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <asp:TextBox ID="txtChequeNo" onfocus="SearchedCode()" runat="server" Width="112px"
                                                    CssClass="uppercase" MaxLength="10"></asp:TextBox>
                                            </td>
                                            <td valign="top" align="left">
                                                <asp:TextBox ID="txtAmount" runat="server" Width="118px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td valign="top" align="left">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <strong>
                                                    <asp:Label ID="Label9" runat="server" Width="73px" Text="Remarks" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td valign="top" align="left"></td>
                                            <td valign="top" align="left"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 22px" valign="top" align="left" colspan="2">
                                                <asp:TextBox ID="txtRemarks" runat="server" Width="239px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="height: 22px" valign="top" align="left"></td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2"></td>
                                            <td align="left" colspan="1"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 36px" align="left">
                                                <asp:Button AccessKey="S" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="119px"
                                                    Font-Size="8pt" Text="Save" CssClass="Button" />
                                                <div style="z-index: 101; left: 503px; width: 100px; position: absolute; top: 251px; height: 100px">
                                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                                        <ProgressTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" Height="23px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                                                Width="22px" />
                                                            Record Update
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </div>
                                            </td>
                                            <td style="width: 201px; height: 36px" valign="middle" align="left">
                                                <asp:Button AccessKey="C" ID="btnCancel" OnClick="btnCancel_Click" runat="server"
                                                    Width="120px" Font-Size="8pt" Text="Cancel" CssClass="Button" />
                                            </td>
                                            <td style="width: 201px; height: 36px" valign="middle" align="left"></td>
                                            <td style="width: 201px; height: 36px" valign="middle" align="left"></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                    FilterType="Custom" ValidChars="0123456789." TargetControlID="txtAmount">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromdate" 
                                    Format="dd-MMM-yyyy" PopupButtonID="ImgBntFromCalc">
                                </ajaxToolkit:CalendarExtender>
                                &nbsp;
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="drpTerritory" EventName="SelectedIndexChanged">
                                </asp:AsyncPostBackTrigger>
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="border-right: silver thin inset; border-top: silver thin inset; border-left: silver thin inset;
                        width: 650px; border-bottom: silver thin inset">
                        <tbody>
                            <tr>
                                <td style="height: 20px" align="left" colspan="5">
                                    <asp:Panel ID="Panel12" runat="server" Width="750px" Height="200px" ScrollBars="Vertical">
                                        <asp:GridView ID="GrdOrder" runat="server" Width="728px" ForeColor="SteelBlue" CssClass="gridRow2"
                                            AutoGenerateColumns="False" BackColor="White" BorderColor="White" HorizontalAlign="Center"
                                            OnRowEditing="GrdOrder_RowEditing" OnRowDeleting="GrdOrder_RowDeleting">
                                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                PreviousPageText="Previous"></PagerSettings>
                                            <AlternatingRowStyle BackColor="#E0E0E0" />
                                            <Columns>
                                                <asp:BoundField DataField="CUSTOMER_ID" HeaderText="CUSTOMER_ID">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PRINCIPAL_ID" HeaderText="PRINCIPAL_ID">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Principal">
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Customer">
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Invoice Date">
                                                    <EditItemTemplate>
                                                        <asp:TextBox runat="server" Text='<%# Bind("DOCUMENT_DATE") %>' ID="TextBox1"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Bind("DOCUMENT_DATE", "{0:dd-MM-yyyy}") %>' ID="Label1"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                    </ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="SALE_INVOICE_ID" HeaderText="Invoice No">
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CURRENT_CREDIT_AMOUNT" HeaderText="Credit Amount">
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LEGEND_ID" HeaderText="LEGEND_ID">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="CUSTOMER_CODE">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="DOCUMENT_DATE">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SALE_INVOICE_MASTER_ID" HeaderText="SALE_INVOICE_MASTER_ID">
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
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="tblhead">
                                            </HeaderStyle>
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
