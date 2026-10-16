<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmCustomerRelization.aspx.cs" Inherits="Forms_frmCustomerRelization"
    Title="CORN :: Bank Transaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
       
        function ValidateForm() {
            var str;
            str = document.getElementById("<%= txtAmount.ClientID %>").value;
            if (str == null || str.length == 0) {
                alert('Must enter Amount');
                return false;
            }

        }
        function pageLoad() {
           $("select").searchable();
            $('#<%=GrdOrder.ClientID %>').tablesorter(
	     {
	         headers: {
	             3: {
	                 sorter: false
	             },
	             6: {
	                 sorter: false
	             },
	             10: {
	                 sorter: false
	             },
	             11: {
	                 sorter: false
	             },
	             12: {
	                 sorter: false
	             },
	             13: {
	                 sorter: false
	             }
	         }
	     }
	     );
            $('#<%=gvSaleForceCash.ClientID %>').tablesorter(
	     {
	         headers: {
	             0: {
	                 sorter: false
	             },
	             1: {
	                 sorter: false
	             },
	             2: {
	                 sorter: false
	             },
	             3: {
	                 sorter: false
	             },
	             4: {
	                 sorter: false
	             },
	             5: {
	                 sorter: false
	             },
	             7: {
	                 sorter: false
	             },
	             8: {
	                 sorter: false
	             }
	         }
	     }
	     );
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
                                            <td style="width: 100px" align="left" colspan="1">
                                            </td>
                                            <td style="width: 100px" align="left" colspan="1">
                                            </td>
                                            <td align="left" colspan="1">
                                            </td>
                                            <td align="left" colspan="1">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lblfromLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px" align="left">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList"
                                                    OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 100px" align="left">
                                            </td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label4" runat="server" Width="55px" Text="Customer" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DrpCustomer" runat="server" Width="280px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpCustomer_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left" rowspan="8">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label8" runat="server" Width="89px" Text="Supplier" CssClass="lblbox"
                                                        Visible="false"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px" align="left">
                                                <asp:DropDownList ID="DrpPrincipal" runat="server" Width="200px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged" AutoPostBack="True"
                                                    Visible="false">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 100px" align="left">
                                            </td>
                                            <td valign="middle" align="left" colspan="2" rowspan="8">
                                                <asp:Panel ID="Panel1" runat="server" Height="160px" ScrollBars="Vertical" BorderColor="Silver"
                                                    BorderStyle="Groove" BorderWidth="1px" Width="380px">
                                                    <asp:GridView ID="GrdCredit" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="SteelBlue" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                        Width="100%" OnRowDeleting="GrdOrder_RowDeleting" DataKeyNames="SALE_INVOICE_ID">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ChbIsAssigned" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Center" Width="4%" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="MANUAL_INVOICE_ID" HeaderText="Invoice No">
                                                                <ItemStyle CssClass="grdDetail" Width="13%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="Invoice Date">
                                                                <ItemStyle CssClass="grdDetail" Width="13%" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CURRENT_CREDIT_AMOUNT" HeaderText="Credit Amount" DataFormatString="{0:F2}">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="20%" />
                                                            </asp:BoundField>
                                                              <asp:BoundField DataField="InvoiceType" >
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                        <HeaderStyle CssClass="grdHead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="99px" Text="Account Type" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px" align="left">
                                                <asp:DropDownList ID="DrpAccountType" runat="server" Width="200px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpAccountType_SelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem Value="19">Cash Realization</asp:ListItem>
                                                    <asp:ListItem Value="28">Credit Transfer Out</asp:ListItem>
                                                    <asp:ListItem Value="22">Bank Deposit</asp:ListItem>
                                                    <asp:ListItem Value="29">Online Transfer</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label7" runat="server" Width="97px" Text="Account" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px">
                                                <asp:DropDownList ID="DrpAccountDetail" runat="server" Width="200px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpAccountDetail_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 100px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label5" runat="server" Width="94px" Text="Cheque No" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                                &nbsp; <strong>
                                                    <asp:Label ID="Label3" runat="server" Width="94px" Text="Slip No" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 100px" valign="top" align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <asp:TextBox ID="txtChequeNo" runat="server" Width="94px" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                                <asp:TextBox ID="txtSlipNo" runat="server" Width="94px" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                            <td style="width: 100px" valign="top" align="left">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <strong>
                                                    <asp:Label ID="Label6" runat="server" Width="74px" Text="Amount" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                                <strong>
                                                    <asp:Label ID="Label9" runat="server" Width="73px" Text="Remarks" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 100px" valign="top" align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <asp:TextBox ID="txtAmount" runat="server" Width="94px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                                <asp:TextBox ID="txtRemarks" runat="server" Width="194px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="width: 100px" valign="top" align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 36px" align="left">
                                                <asp:Button AccessKey="S" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="100px"
                                                    Font-Size="8pt" Text="Save" CssClass="Button" />
                                            </td>
                                            <td style="width: 201px; height: 36px" valign="middle" align="left">
                                                <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="100px" Font-Size="8pt"
                                                    Text="Cancel" CssClass="Button" />
                                                <div style="z-index: 101; left: 320px; width: 100px; position: absolute; top: 150px;
                                                    height: 90px">
                                                    <asp:Panel ID="Panel21" runat="server">
                                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                                            <ProgressTemplate>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                                                    Width="23px" />
                                                                Wait Update
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </asp:Panel>
                                                </div>
                                            </td>
                                            <td style="width: 100px; height: 36px" valign="middle" align="left">
                                            </td>
                                            <td style="width: 100px; height: 36px" valign="middle" align="left">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                &nbsp;
                                <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                    FilterType="Custom" ValidChars="0123456789." TargetControlID="txtAmount">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </ContentTemplate>
                              <Triggers>
                               <asp:PostBackTrigger ControlID="btnSave" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel12" runat="server" Width="800px" Height="200px" ScrollBars="Vertical"
                        BorderColor="Silver" BorderStyle="Inset" BorderWidth="1px">
                        <asp:GridView ID="GrdOrder" runat="server" Width="100%" ForeColor="SteelBlue" BorderColor="SteelBlue"
                            OnRowDeleting="GrdOrder_RowDeleting" BackColor="White" AutoGenerateColumns="False">
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
                                <asp:BoundField DataField="voucher_type_id" HeaderText="voucher_type_id">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Supplier">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Customer">
                                    <ItemStyle CssClass="grdDetail" Width="12%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Manual_Document_no" HeaderText="Document No">
                                    <ItemStyle CssClass="grdDetail" Width="6%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Ledger_date" HeaderText="Ledger Date">
                                    <ItemStyle HorizontalAlign="Center" CssClass="grdDetail" Width="6%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Voucher_no" HeaderText="Voucher No">
                                    <ItemStyle CssClass="grdDetail" Width="6%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Balance" DataFormatString="{0:F2}" HeaderText="Amount">
                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="8%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="15%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Document_no" HeaderText="Document_no">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField >
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                            ToolTip="Remove"><img alt="" runat="server" src="~/images/delete.gif"/></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="grdDetail" Width="5%" HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" HeaderText="Edit" Visible="false" >
                                    <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" Width="5%" BorderStyle="Solid">
                                    </ItemStyle>
                                </asp:CommandField>
                            </Columns>
                            <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="grdHead" />
                        </asp:GridView>
                        <asp:GridView ID="gvSaleForceCash" runat="server" Visible="False" Width="728px" ForeColor="SteelBlue"
                            CssClass="tablesorter" BorderColor="White" OnRowDeleting="gvSaleForceCash_RowDeleting"
                            HorizontalAlign="Center" BackColor="White" AutoGenerateColumns="False" OnRowEditing="gvSaleForceCash_RowEditing">
                            <AlternatingRowStyle BackColor="#E0E0E0" />
                             <Columns>
                                <asp:BoundField DataField="SALE_FORCE_CASH_ID" HeaderText="SALE_FORCE_CASH_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PRINCIPAL_ID" HeaderText="PRINCIPAL_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PRINCIPAL" HeaderText="PRINCIPAL">
                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                    </ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DELIVERYMAN_ID" HeaderText="DELIVERYMAN_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DELIVERYMAN" HeaderText="DELIVERY MAN">
                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                    </ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="DOCUMENT DATE">
                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                    </ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT">
                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                    </ItemStyle>
                                </asp:BoundField>
                                <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                    <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" Width="40px">
                                    </ItemStyle>
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDeleteSaleForceCash" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                            Text="Delete"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                    </ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="tabs_css">
                            </HeaderStyle>
                        </asp:GridView>
                        <asp:HiddenField ID="hfSALE_FORCE_CASH_ID" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hfPRINCIPAL_ID" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hfDELIVERYMAN_ID" runat="server"></asp:HiddenField>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
