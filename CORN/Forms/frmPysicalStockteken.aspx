<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmPysicalStockteken.aspx.cs" Inherits="Forms_frmPysicalStockteken"
    Title="CORN :: Physical Stock Taking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">


        function pageLoad() {
            $("select").searchable();
            ScanCheck();
        }
        function ProductSelected(source, eventArgs) {
            document.getElementById("<%=hfSKUID.ClientID %>").value = eventArgs.get_value();
            document.getElementById("<%=txtCtn.ClientID %>").focus();
        }
        function SetContextKey() {
            $find("<%=aceProduct.ClientID%>").set_contextKey(document.getElementById("<%=drpDistributor.ClientID %>").value);

        }

        function AddQuantity(e) {
            var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
            if (key == 13) {
                e.preventDefault();
                if (document.getElementById('<%=txtSKUScan.ClientID%>').value != "") {
                    document.getElementById("<%=btnSave.ClientID %>").click();
                }
            }
        }
        function ScanCheck() {
            document.getElementById('<%=txtSKU.ClientID%>').value = "";
            document.getElementById('<%=txtSKUScan.ClientID%>').value = "";

            if (document.getElementById('<%=cbScan.ClientID%>').checked) {
                document.getElementById("<%=btnSave.ClientID %>").style.visibility = "hidden";
                document.getElementById('<%=txtSKU.ClientID%>').style.display = "none";
                document.getElementById('<%=txtSKUScan.ClientID%>').style.display = "block";
            }
            else {
                document.getElementById("<%=btnSave.ClientID %>").style.visibility = "visible";
                document.getElementById('<%=txtSKU.ClientID%>').style.display = "block";
                document.getElementById('<%=txtSKUScan.ClientID%>').style.display = "none";
            }
        }

    </script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="height: 25px; width: 95px;" align="left">
                                                <strong>
                                                    <asp:Label Width="74px" CssClass="lblbox" ID="lblDocumentNo" runat="server" Text="Location"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px; width: 203px;">
                                                <asp:DropDownList CssClass="DropList" ID="drpDistributor" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 95px"></td>
                                            <td style="width: 15%;" colspan="2" >
                                               
                                            </td>
                                            </tr>
                                        <tr>
                                            <td style="width: 95px">
                                                <strong><asp:Label ID="lbl" Text="Qty Type" runat="server"></asp:Label></strong>
                                            </td>
                                            <td style="width: 203px" colspan="2">
                                                <strong>
                                                    
                                                    <asp:RadioButtonList ID="rbUnitType" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Selected="True" Text="Unit" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="CTN" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList></strong>
                                            </td>
                                            </tr>
                                        <tr>
                                            <td style="width: 95px">
                                                <strong> <asp:Label ID="Label1" Text="Document Type" runat="server"></asp:Label></strong></td>
                                            <td colspan="2">
                                                <strong>
                                                   
                                                    <asp:RadioButtonList ID="rbDocTpe" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Selected="True" Text="Saleable" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Unsaleable" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList></strong>
                                            </td>
                                            </tr>
                                       
<tr>
    <td style="width: 95px"> <strong>
                                                    
                                                    <asp:CheckBox ID="cbScan" runat="server" Text="Scan" onchange="ScanCheck();" />
                                                </strong></td>
    <td colspan="2">
           <asp:Button ID="btnGetData" runat="server" AccessKey="A" Font-Size="8pt" OnClick="btnGetData_Click"
                                                Text="Get Data" ValidationGroup="vg" Width="87px" CssClass="Button" />
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
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <table style="width: 630px; margin-right: 26">
                                    <tr>
                                        <td>
                                            <strong>
                                                <asp:Label ID="lblskuCode" runat="server" Width="100%"
                                                    CssClass="label" Text="Item Description"></asp:Label></strong>
                                        </td>

                                        <td>
                                            <strong>
                                                <asp:Label ID="lblquantity" runat="server" CssClass="label"
                                                    Text="Qty" Width="100%"></asp:Label></strong>
                                        </td>
                                        <td>
                                            <%--  <strong>
                                                <asp:Label ID="Label41" runat="server" BackColor="#006699" CssClass="lblbox" Font-Bold="True"
                                                    ForeColor="White" Height="16px" Text="" Width="96%"></asp:Label></strong>--%>
                                        </td>
                                         <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtSKU" runat="server" Width="357px" onkeyup="SetContextKey()"></asp:TextBox>
                                            <ajaxToolkit:AutoCompleteExtender ID="aceProduct" runat="server" TargetControlID="txtSKU"
                                                ServicePath="wsProductList.asmx" MinimumPrefixLength="1" CompletionInterval="500"
                                                CompletionSetCount="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" EnableCaching="true" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                OnClientItemSelected="ProductSelected" FirstRowSelected="true" ServiceMethod="GetProducts3">
                                            </ajaxToolkit:AutoCompleteExtender>
                                            <asp:TextBox ID="txtSKUScan" runat="server" Width="357px" onkeypress="AddQuantity(event);" Style="display: none"></asp:TextBox>
                                            <asp:HiddenField ID="hfSKUID" runat="server" />


                                        </td>

                                        <td>
                                            <asp:TextBox ID="txtCtn" runat="server" Width="88px" TabIndex="6" Text="1"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                FilterType="Custom" ValidChars="0123456789" TargetControlID="txtCtn">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </td>
                                        <td style="height: 9px">
                                            <asp:Button ID="btnSave" runat="server" AccessKey="A" Font-Size="8pt" OnClick="btnSave_Click"
                                                Text="Save" ValidationGroup="vg" Width="87px" CssClass="Button" />
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="4">
                                            <asp:Panel ID="Panel2" runat="server" BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px"
                                                Height="150px" ScrollBars="Vertical" Width="100%">
                                                <asp:GridView ID="GrdPurchase" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                    BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                   OnRowDeleting="GrdPurchase_RowDeleting" ShowHeader="true" Width="100%">
                                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                        PreviousPageText="Previous" />
                                                    <RowStyle ForeColor="Black" />
                                                     <AlternatingRowStyle BackColor="#E0E0E0" />
                                                    <Columns>
                                                        <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                                Width="150px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                                Width="205px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SALEABLE_QUANTITY" HeaderText="Saleable" DataFormatString="{0:F0}">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                Width="82px" />
                                                        </asp:BoundField>
                                                         <asp:BoundField DataField="UNSALEABLE_QUANTITY" HeaderText="UnSaleable" DataFormatString="{0:F0}">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                Width="82px" />
                                                        </asp:BoundField>

                                                         <asp:BoundField DataField="VALUE_TYPE" HeaderText="VALUE_TYPE">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                     <%--   <asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" Width="40px" />
                                                        </asp:CommandField>--%>
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
                                </table>
                                &nbsp; &nbsp;
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        &nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
