<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmChequeEntry.aspx.cs" Inherits="Forms_frmChequeEntry" Title="CORN :: Cheque Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    
    
    <script language="JavaScript" type="text/javascript">


        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

        function startRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            document.getElementById('<%=btnCancel.ClientID%>').disabled = true;

        }

        function endRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
            document.getElementById('<%=btnCancel.ClientID%>').disabled = false;

        }
      

       


        function ValidateForm() {
            var str;
            str = document.getElementById("<%= txtAmount.ClientID %>").value;
            if (str == null || str.length == 0) {
                alert('Must enter Amount');
                return false;
            }

        }
 
    </script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                <div style="z-index: 101; left: 356px; width: 100px; position: absolute; top: 100px;
                                    height: 100px">
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
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="2">
                                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                            </td>
                                            <td style="width: 104px" align="left" colspan="1">
                                            </td>
                                            <td align="left" colspan="1">
                                            </td>
                                            <td style="width: 55px" align="left" colspan="1">
                                            </td>
                                            <td align="left" colspan="1">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 17px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label12" runat="server" Width="84px" Text="Cheque Type" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px; height: 17px" align="left">
                                                <asp:DropDownList ID="DrpChequeType" runat="server" Width="226px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpChequeType_SelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem>Cheque Realized</asp:ListItem>
                                                    <asp:ListItem>Cheque Advance</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                           <%-- <td style="width: 201px; height: 17px" align="left">
                                            </td>
                                            <td style="height: 17px" align="left">
                                               <strong>
                                                    <asp:Label ID="Label7" runat="server" Width="66px" Text="Sale Force" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                             <td style="width: 201px; height: 17px" align="left">
                                              <asp:DropDownList ID="DrpDeliveryMan" runat="server" Width="240px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpCustomer_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 17px" align="left">
                                            </td>--%>
                                        </tr>
                                        <tr>
                                            <td style="height: 17px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblfromLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px; height: 17px" align="left">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="226px" CssClass="DropList"
                                                    OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 55px; height: 17px" align="left">
                                            </td>
                                            <td style="height: 17px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label11" runat="server" Width="66px" Text="Customer" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px; height: 17px" align="left">
                                                <asp:DropDownList ID="DrpRoute" runat="server" Width="240px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged" AutoPostBack="True" Visible="false">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="DrpCustomer" runat="server" Width="240px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpCustomer_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 17px" align="left">
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td style="height: 17px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label8" runat="server" Width="89px" Text="Supplier" CssClass="lblbox" Visible="false"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px; height: 17px" align="left">--%>
                                                <asp:DropDownList ID="DrpPrincipal" runat="server" Width="226px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged" AutoPostBack="True" Visible="false">
                                                </asp:DropDownList>
                                            <%--</td>
                                            <td style="width: 201px; height: 17px" align="left">
                                            </td>
                                            <td style="height: 17px" align="left">
                                                
                                            </td>
                                            <td style="width: 201px; height: 17px" align="left">
                                                
                                            </td>
                                            <td style="height: 17px" align="left">
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label10" runat="server" Width="86px" Text="Status" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px" align="left">
                                                <asp:DropDownList ID="DrpStatus" runat="server" Width="226px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpStatus_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 55px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblDocumentNo" runat="server" Width="25px" Text="            " CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left" colspan="2" rowspan="10">
                                                <asp:Panel ID="Panel1" runat="server" Height="200px" ScrollBars="Vertical" BorderColor="Silver"
                                                    BorderStyle="Groove" BorderWidth="1px">
                                                    <asp:GridView ID="GrdCredit" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="SteelBlue" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                        Width="304px" DataKeyNames="SALE_INVOICE_ID">
                                                          <AlternatingRowStyle BackColor="#E0E0E0" />
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ChbIsAssigned" runat="server"  />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grdDetail" Width="4%" HorizontalAlign="Center"/>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="MANUAL_INVOICE_ID" HeaderText="Invoice No">
                                                                <ItemStyle CssClass="grdDetail" Width="13%"/>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="Invoice Date">
                                                                <ItemStyle CssClass="grdDetail" Width="13%" HorizontalAlign="Center"/>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CURRENT_CREDIT_AMOUNT" HeaderText="Credit Amount" DataFormatString="{0:F2}">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="20%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="InvoiceType" >
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                         <HeaderStyle CssClass="grdHead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                            <td align="left" rowspan="11">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label14" runat="server" Width="98px" Text="Deposit Account" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DrpBankAccount" runat="server" Width="226px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpStatus_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 55px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label5" runat="server" Width="94px" Text="Chque No" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" Width="94px" Text="Bank Name" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 55px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:TextBox ID="txtChequeNo" runat="server" Width="94px" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                            <td valign="top" align="left">
                                                <asp:TextBox ID="txtBankName" runat="server" Width="192px" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                            <td style="width: 55px" valign="top" align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <strong>
                                                    <asp:Label ID="Label13" runat="server" Width="94px" Text="Remarks" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 55px" valign="top" align="left">
                                            </td>
                                            <td style="width: 55px" valign="top" align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left" colspan="2">
                                                <asp:TextBox ID="txtRemarks" runat="server" Width="298px" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                            <td style="width: 55px" valign="top" align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <strong>
                                                    <asp:Label ID="Label6" runat="server" Width="74px" Text="Chq Amount" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                                <strong>
                                                    <asp:Label ID="Label9" runat="server" Width="76px" Text="Cheque Date" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 55px" valign="top" align="left">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <asp:TextBox ID="txtAmount" runat="server" Width="94px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                                <asp:TextBox ID="txtStartDate" runat="server" Width="132px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="width: 55px" valign="top" align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <strong>
                                                    <asp:Label ID="Label2" runat="server" Width="96px" Text="Slip No" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="100px" Text="Received Date" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 55px" valign="top" align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <asp:TextBox ID="txtSlipNo" runat="server" Width="94px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                                <asp:TextBox ID="txtReceivedDate"  runat="server" Width="132px" CssClass="txtBox"></asp:TextBox>
                                           <asp:ImageButton ID="ImgBntToDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                __designer:wfdid="w46"></asp:ImageButton> </td>
                                            <td style="width: 55px" valign="top" align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <asp:Button AccessKey="S" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="100px"
                                                    Font-Size="8pt" Text="Save" CssClass="Button" />
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                                <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="100px" Font-Size="8pt"
                                                    Text="Cancel" OnClick="btnCancel_Click" CssClass="Button" />
                                            </td>
                                            <td style="width: 55px" valign="top" align="left">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom"
                                    ValidChars="0123456789." TargetControlID="txtAmount">
                                </cc1:FilteredTextBoxExtender>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtStartDate"
                                    Mask="99/99/9999" MaskType="Date">
                                </cc1:MaskedEditExtender>
                                <asp:HiddenField ID="HFChqueProcessId" runat="server"></asp:HiddenField>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReceivedDate"
                                PopupButtonID="ImgBntToDate" EnableViewState="False" Format="dd-MMM-yyyy" __designer:wfdid="w53">
                            </cc1:CalendarExtender>
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
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                               
                                                <asp:Panel ID="Panel12" runat="server" Width="950px" Height="200px" ScrollBars="Vertical"
                                                BorderColor="Silver" BorderStyle="Inset" BorderWidth="1px">
                                                    <table style="background-color:Silver; width:100%">
                                                        <tbody>
                                                            <tr>
                                                                <td style="height: 21px" align="left">
                                                                    <strong>
                                                                        <asp:Label ID="Label110" runat="server" Width="154px" Text="Select Searching Type"></asp:Label></strong>
                                                                </td>
                                                                <td style="width: 170px; height: 21px" align="left">
                                                                    <asp:DropDownList ID="ddSearchType" runat="server" Width="200px" CssClass="DropList">
                                                                        <asp:ListItem Value="CHEQUE_NO">All Records</asp:ListItem>
                                                                        <asp:ListItem Value="CUSTOMER_NAME">Customer</asp:ListItem>
                                                                        <asp:ListItem Value="BANK_NAME">Bank Name</asp:ListItem>
                                                                        <asp:ListItem Value="SlipNo">Slip No</asp:ListItem>
                                                                        <asp:ListItem Value="account_name">Deposit Account </asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 224px; height: 21px" align="left">
                                                                    <asp:TextBox ID="txtSeach" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                                                </td>
                                                                <td style="width: 250px; height: 21px" align="left">
                                                                    <asp:Button ID="btnFilter" runat="server" Width="85px" Font-Size="8pt" Text="Filter"
                                                                        OnClick="btnFilter_Click"></asp:Button>
                                                                </td>
                                                                <td style="width:20%;">
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <asp:GridView ID="GrdOrder" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="gridRow2"
                                                        BorderColor="SteelBlue" HorizontalAlign="Center" BackColor="White" AutoGenerateColumns="False"
                                                        OnRowEditing="GrdOrder_RowEditing" OnRowDeleting="GrdOrder_RowDeleting">
                                                           <AlternatingRowStyle BackColor="#E0E0E0" />
                                                        <Columns>
                                                            <asp:BoundField DataField="CHEQUE_PROCESS_ID" ReadOnly="true" HeaderText="CHEQUE_PROCESS_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CUSTOMER_ID" ReadOnly="true" HeaderText="CUSTOMER_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CUSTOMER_NAME" ReadOnly="true" HeaderText="Customer">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CHEQUE_NO" ReadOnly="true" HeaderText="Chq. No">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="BANK_NAME" ReadOnly="true" HeaderText="Bank Name">
                                                                <ItemStyle CssClass="grdDetail" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CHEQUE_DATE" ReadOnly="true" HeaderText="Chq.Date">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="RECEIVED_DATE" ReadOnly="true" HeaderText="Received Date">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DEPOSIT_DATE" ReadOnly="true" HeaderText="Deposit Date">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CHEQUE_AMOUNT" ReadOnly="true" DataFormatString="{0:F2}" HeaderText="Chq.Amount">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SlipNo" ReadOnly="true" HeaderText="Slip No">
                                                                <ItemStyle CssClass="grdDetail" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Remarks" ReadOnly="true" HeaderText="Remarks">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="area_id" ReadOnly="true" HeaderText="area_id">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="account_name" ReadOnly="true" HeaderText="Deposit Account">
                                                                
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="account_head_id" ReadOnly="true" HeaderText="account_head_id">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DeliveryManID" ReadOnly="true" HeaderText="DeliveryManID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                             <asp:TemplateField >
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnPrint" runat="server" CommandName="Edit" ToolTip="Edit"
                                                                ><img alt="" id="Img2" runat="server" src="~/images/edit.gif" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="grdDetail" HorizontalAlign="Center"/>
                                                    </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="grdHead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                         <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                   
                                                    </asp:GridView>
                                                </asp:Panel>
                                       
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
