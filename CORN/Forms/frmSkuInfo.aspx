<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSkuInfo.aspx.cs" Inherits="Forms_SkuInfo" Title="CORN :: Item Information" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
  
    <script language="JavaScript" type="text/javascript">
     
        function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtstyleCode.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Style Name');
                return false;
            }
            str = document.getElementById('<%=txtBrandName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Brand Name');
                return false;
            }
            str = document.getElementById('<%=txtUnitInCase.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Units in Pack');
                return false;
            }

            return true;
        }

        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#mainCopy_cphPage_imgProduct').attr('src', e.target.result).width(250).height(250);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }

        $(document).ready(function () {
            $('#<%=txtstyleCode.ClientID%>').keyup(function (e) {

                $("#<%=grdSKUData.ClientID%> tr:has(td)").hide(); // Hide all the rows.

                var iCounter = 0;
                var sSearchTerm = $('#<%=txtstyleCode.ClientID%>').val(); //Get the search box value

                if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                {
                    $("#<%=grdSKUData.ClientID%> tr:has(td)").show();
                    return false;
                }
                //Iterate through all the td.
                $("#<%=grdSKUData.ClientID%> tr:has(td)").children().each(function () {
                    var cellText = $(this).text().toLowerCase();
                    if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                    {
                        $(this).parent().show();
                        iCounter++;
                        return true;
                    }
                    return true;
                });
                return false;
            });
        });
    </script>
    <div id="right_data">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div>
                    <table width="100%">
                        <tr>
                            <td style="width: 50%">
                                <table width="100%">
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:HiddenField ID="HdnSku_ID" runat="server" />
                                        </td>
                                    </tr>


                                    <tr>
                                        <td style="width: 25%">
                                            <strong>
                                                <asp:Label ID="Label5" runat="server" Text="Style Code"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 75%">
                                            <asp:TextBox ID="txtstyleCode" runat="server" onkeyup="Search_Gridview(this, 'grdSKUData')" Width="215px" CssClass="txtBox "></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            <strong>
                                                <asp:Label ID="Label6" runat="server" Text="Bar Code"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 75%">
                                            <asp:TextBox ID="txtbarcode" runat="server" Width="215px" CssClass="txtBox" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 25%">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Width="100px" Text="Supplier"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 75%">
                                            <asp:DropDownList ID="drpPrincipal" runat="server" Width="220px" AutoPostBack="true" CssClass="DropList" OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Width="100px" Text="Gender"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 75%">
                                            <asp:DropDownList ID="drpGender"
                                                runat="server" Width="220px" CssClass="DropList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Width="100px" Text="Category"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 75%">
                                            <asp:DropDownList ID="drpcategory"
                                                runat="server" Width="220px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            <strong>
                                                <asp:Label ID="Label17" runat="server" Width="100px" Text="Type"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 75%">
                                            <asp:DropDownList ID="drptype" runat="server" Width="220px" CssClass="DropList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            <strong>
                                                <asp:Label ID="Label15" runat="server" Text="Brand"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 75%">

                                            <asp:TextBox ID="txtBrandName" runat="server" Width="215px"></asp:TextBox>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td style="width: 25%">
                                            <strong>
                                                <asp:Label ID="Label18" runat="server" Text="Item Name"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 75%">
                                            <asp:TextBox ID="txtskuname" runat="server" Width="215px" CssClass="txtBox" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 25%">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Text="Season"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 75%">
                                            <asp:DropDownList ID="DrpSeason" runat="server" Width="220px" CssClass="DropList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            <strong>
                                                <asp:Label ID="Label16" runat="server" Text="Color"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 75%">
                                            <asp:Panel ID="pnlColor" runat="server" Height="150px" Width="220px" ScrollBars="Auto" BorderWidth="1px">

                                                <asp:CheckBoxList ID="chkColor" runat="server" Width="100%" Height="100%">
                                                </asp:CheckBoxList>

                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            <strong>
                                                <asp:Label ID="Label8" runat="server" Text="Size"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 75%">
                                            <asp:Panel ID="pnlSize" runat="server" Height="150px"
                                                Width="220px" ScrollBars="Auto" BorderWidth="1px">
                                                <asp:CheckBoxList ID="chbSize" runat="server" Width="100%" Height="100%">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 25%">
                                            <strong>
                                                <asp:Label ID="Label13" runat="server" Text="Country"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 75%">
                                            <asp:DropDownList ID="drpCountry" runat="server" Width="220px" CssClass="DropList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>





                                    <tr>
                                        <td style="width: 25%">
                                            <strong>
                                                <asp:Label ID="Label7" runat="server" Text="Units in Pack"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 75%">
                                            <asp:TextBox ID="txtUnitInCase" Width="215px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 25%">
                                            <strong>
                                                <asp:Label ID="Label9" runat="server" Text="Remarks"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 75%">
                                            <asp:TextBox ID="txtRemarks" runat="server" Width="212px" TextMode="MultiLine" CssClass="txtBox"></asp:TextBox>

                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 30%;" valign="top" align="center">

                                <label for="mainCopy_cphPage_fuImage" id="lbl" runat="server" style="cursor: pointer;">
                                    <asp:Image ID="imgProduct" runat="server" ImageUrl="~/images/noimage.jpg" Width="250px" Height="250px" />
                                </label>
                                <asp:FileUpload ID="fuImage" onchange="readURL(this);" Style="display: none;" runat="server"></asp:FileUpload>
                                <br />
                                <asp:CheckBox ID="cbDisplay" runat="server" Text="Display on e-Store" Visible="false" />
                            </td>
                            <td style="width: 20%"></td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td>
                                <table>
                                    <tbody>

                                        <tr>
                                            <td style="width: 100px; height: 12px">
                                                <asp:Button ID="BtnGenerateCode" OnClick="BtnGenerateCode_Click" runat="server" Width="120px" Font-Size="8pt"
                                                    Text="Generate Name" CssClass="Button" />
                                            </td>
                                            <td style="height: 12px" align="left" colspan="2">
                                                <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="86px" Font-Size="8pt"
                                                    Text="Save" CssClass="Button" />
                                            </td>
                                            <td style="width: 37px; height: 12px" align="left"></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <div style="z-index: 101; position: absolute; left: 50%; top: 50%;">
                                    <asp:UpdateProgress ID="UpdateProgress5" runat="server">
                                        <ProgressTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" Width="26px" Height="23px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"></asp:ImageButton>&nbsp; Loading....
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <table class="tblhead">
                    <tbody>
                        <tr>
                            <td style="color: White; font-weight: bold;">
                                <asp:Label ID="Label10" runat="server" Width="153px" Text="Select Searching Type"></asp:Label>
                            </td>
                            <td style="width: 170px; height: 22px" align="left">
                                <asp:DropDownList ID="ddSearchType" runat="server" Width="200px" CssClass="DropList">

                                    <asp:ListItem Value="BAR_CODE">Bar Code</asp:ListItem>
                                    <asp:ListItem Value="SKU_CODE">Style Code</asp:ListItem>
                                    <asp:ListItem Value="Category">Category</asp:ListItem>
                                    <asp:ListItem Value="Country">Country</asp:ListItem>
                                    <asp:ListItem Value="Brand_Name">Brand</asp:ListItem>
                                    <asp:ListItem Value="Color">Color</asp:ListItem>
                                    <asp:ListItem Value="SKU_NAME">Item Name</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 224px; height: 22px" align="left">
                                <asp:TextBox ID="txtSeach" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                            </td>
                            <td style="width: 250px; height: 22px" align="left">
                                <asp:Button ID="btnFilter" runat="server" Width="85px" Font-Size="8pt" Text="Filter"
                                    OnClick="btnFilter_Click"></asp:Button>
                            </td>
                            <td style="width: 95px; height: 22px"></td>
                        </tr>
                    </tbody>
                </table>

                <asp:Panel ID="Panel1" runat="server" Width="100%" Height="200px" ScrollBars="Vertical"
                    BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                    <asp:GridView ID="grdSKUData" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="gridRow2"
                        HorizontalAlign="Center" BorderColor="White" BackColor="White" AutoGenerateColumns="False"
                        OnRowEditing="grdSKUData_RowEditing" OnRowDeleting="grdSKUData_RowDeleting">
                           <AlternatingRowStyle BackColor="#E0E0E0" />
                        <Columns>
                            <asp:BoundField DataField="SKU_ID" ReadOnly="true" HeaderText="SKU_ID">
                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="Principal_Id" ReadOnly="true">
                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Gender_Id" ReadOnly="true">
                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Category_Id" ReadOnly="true" HeaderText="Category_Id">
                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Type_ID" ReadOnly="true" HeaderText="Type_ID">
                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SKU_season" ReadOnly="true" HeaderText="SKU_season">
                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="Size_ID" ReadOnly="true" HeaderText="Size_ID">
                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="COLOR_ID" ReadOnly="true" HeaderText="COLOR_ID">
                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                            </asp:BoundField>


                            <asp:BoundField DataField="BAR_CODE" ReadOnly="true" HeaderText="Bar Code">
                                <HeaderStyle CssClass="grdHead" HorizontalAlign="Left" />
                                <ItemStyle CssClass="grdDetail"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SKU_CODE" ReadOnly="true" HeaderText="Style Code">
                                <HeaderStyle CssClass="grdHead" HorizontalAlign="Left" />
                                <ItemStyle CssClass="grdDetail"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SKU_NAME" ReadOnly="true" HeaderText="Item Name">
                                <HeaderStyle CssClass="grdHead" HorizontalAlign="Left" />
                                <ItemStyle CssClass="grdDetail"></ItemStyle>
                            </asp:BoundField>


                            <asp:BoundField DataField="Category" ReadOnly="true" HeaderText="Category">
                                <HeaderStyle CssClass="grdHead" HorizontalAlign="Left" />
                                <ItemStyle CssClass="grdDetail"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Country" ReadOnly="true" HeaderText="Country">
                                <HeaderStyle CssClass="grdHead" HorizontalAlign="Left" />
                                <ItemStyle CssClass="grdDetail"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="COLOR" ReadOnly="true" HeaderText="Color">
                                <HeaderStyle CssClass="grdHead" HorizontalAlign="Left" />
                                <ItemStyle CssClass="grdDetail"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="Size" ReadOnly="true" HeaderText="Size">
                                <HeaderStyle CssClass="grdHead" HorizontalAlign="Left" />
                                <ItemStyle CssClass="grdDetail"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Brand_Name" ReadOnly="true" HeaderText="Brand">
                                <HeaderStyle CssClass="grdHead" HorizontalAlign="Left" />
                                <ItemStyle CssClass="grdDetail"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="Units_in_case" ReadOnly="true" HeaderText="Units">
                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="REMARKS" ReadOnly="true">
                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PicUrl" ReadOnly="true">
                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                            </asp:BoundField>


                            <asp:BoundField DataField="Country_id" ReadOnly="true">
                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                            </asp:BoundField>
                             <asp:TemplateField HeaderText="Edit">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Delete">
                                                                    <img src="../images/edit.gif" />
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                   <HeaderStyle CssClass="grdHead" HorizontalAlign="Left" />          
                                 <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="45px" HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                           
                                                                 <asp:TemplateField HeaderText="Delete" >
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnDelete" ToolTip="Delete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                                    CommandName="Delete">
                                                                                    <img src="../images/delete.gif" width="16" height="16">
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                      <HeaderStyle CssClass="grdHead" HorizontalAlign="Left" />
                                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="45px" HorizontalAlign="Center">
                                                                            </ItemStyle>
                                                                        </asp:TemplateField>
                           
                        </Columns>
                      
                    </asp:GridView>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
