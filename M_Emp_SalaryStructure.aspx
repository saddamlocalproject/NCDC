<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="M_Emp_SalaryStructure.aspx.cs" Inherits="M_Emp_SalaryStructure" Title="Employee Salary Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrols/Messagebox.ascx" TagName="Messagebox" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=grdEmp.ClientID %>').Scrollable({
                ScrollHeight: 300,
                IsInUpdatePanel: true
            });
        });
    </script>
    <script type="text/javascript" src="../../js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.collapse.js"></script>
    <script type="text/javascript">
        $.noConflict();
        jQuery(document).ready(function ($) {
            $("#custom-markup-example").collapse({
                query: "div h8"
            });

            $('#ca-container').contentcarousel();
        });
    </script>
    <script language="javascript" type="text/javascript">

        function minmax(value) {

            if (parseInt(value) < 0 || isNaN(value))
                return 0;
            else if (parseInt(value) >= 100)
                return 100;
            else return value;
        }


    </script>

    <script type="text/javascript">



        function ChangeIncrement() {
            var e = document.getElementById("<%=ddl_IncrementFlag.ClientID%>");
            var ddVal = e.options[e.selectedIndex].value;
            if (ddVal === '1')
             {
               document.getElementById('<%=txtEmpPrvIncmentDt.ClientID%>').disabled = false;
                document.getElementById('<%=txtEmpNxtIncmentDt.ClientID%>').disabled = false;
            }
            else 
            {
               
                document.getElementById('<%=txtEmpNxtIncmentDt.ClientID%>').value = "";
                document.getElementById('<%=txtEmpPrvIncmentDt.ClientID%>').disabled = true;
                document.getElementById('<%=txtEmpNxtIncmentDt.ClientID%>').disabled = true;
            }
        }



</script>

    <script language="javascript" type="text/javascript">

        function ValidatePAN(Obj) {
            if (Obj.value != "") {
                var ObjVal = Obj.value;
                var panPat = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;
                if (ObjVal.search(panPat) == -1) {
                    alert("Invalid Pan No");
                    Obj.focus();
                    return false;
                }
            }
        }
        </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField id="hfdSCf" runat="server" />
    <table style="width: 100%;">
        <tr class="PageHeaddingBox">
            <td colspan="4" style="text-align: center; border-bottom: solid 1px #666633">
                <b>Employee Salary Structure</b>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblMsg" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <table id="panelLinks" runat="server" visible="false" style="width: 100%; padding-left: 4px;
        padding-right: 4px;">
        <tr>
            <td>
                <div id="custom-markup-example">
                    <%-- <td align="right" class="TabImg">
                                        <asp:HyperLink ID="hyplnkHome" runat="server" NavigateUrl="~/HomePage.aspx" Font-Bold="True">Home</asp:HyperLink>
                                    </td>
                                    <td align="right" class="TabImg">
                                        <asp:LinkButton ID="hyplnkLogout" runat="server" OnClick="hyplnkLogout_Click" PostBackUrl="~/Login.aspx"
                                            Font-Bold="True">Logout</asp:LinkButton>
                                    </td>--%>
                    <h8>
          <asp:Panel ID="lblclps" runat="server" >
        
                    <asp:ImageButton ID="img_profile" runat="server" ImageUrl="../../App_Themes/BasicTheme/Images/sprite.png"
                        AlternateText="(Show Details...)" />
                   Employee Search
               
            </asp:Panel>
 </h8>
                    <%-- <td align="right" class="TabImg">
                                        <asp:HyperLink ID="hyplnkHome" runat="server" NavigateUrl="~/HomePage.aspx" Font-Bold="True">Home</asp:HyperLink>
                                    </td>
                                    <td align="right" class="TabImg">
                                        <asp:LinkButton ID="hyplnkLogout" runat="server" OnClick="hyplnkLogout_Click" PostBackUrl="~/Login.aspx"
                                            Font-Bold="True">Logout</asp:LinkButton>
                                    </td>--%>
                    <div class="content">
                        <asp:Panel ID="pnlEmp" runat="server">
                            <table width="100%">
                                <tr style="display:none">
                                    <td colspan="8" style="font-weight: 700">
                                        List of Employee
                                        <asp:LinkButton ID="lnlShwAll" runat="server" OnClick="lnlShwAll_Click" Visible="false">Show All</asp:LinkButton>
                                    </td>
                                    <%-- <td align="right" class="TabImg">
                                        <asp:HyperLink ID="hyplnkHome" runat="server" NavigateUrl="~/HomePage.aspx" Font-Bold="True">Home</asp:HyperLink>
                                    </td>
                                    <td align="right" class="TabImg">
                                        <asp:LinkButton ID="hyplnkLogout" runat="server" OnClick="hyplnkLogout_Click" PostBackUrl="~/Login.aspx"
                                            Font-Bold="True">Logout</asp:LinkButton>
                                    </td>--%>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Emp Code
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtECode" runat="server" Width="91px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Emp Name
                                    </td>
                                    <td align="right">
                                        <asp:TextBox ID="txtEname" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCenterCode" runat="server" Style="display: none;"></asp:TextBox>
                                    </td>
                                    <td style="visibility: hidden; display: none;">
                                        Select Designation
                                    </td>
                                    <td style="visibility: hidden; display: none;">
                                        <asp:TextBox ID="txtDesignation" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="cmdbutton" OnClick="btnSearch_Click"
                                            CausesValidation="False" />
                                        <%-- <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                                             />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="9">
                                        <asp:Panel ID="pnl_search" runat="server" Style="width: 100%; height: 200px; overflow: scroll;">
                                            <asp:GridView ID="grdEmp" runat="server" AutoGenerateColumns="False" Width="100%"
                                                EmptyDataText="No employees are available.">
                                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Emp Code" HeaderStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblEmployeeCodes" runat="server" Text='<%# bind("empno") %>' Visible="true"></asp:Label>
                                                            <asp:Label ID="lblECode" runat="server" Text='<%# bind("ECODE") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Emp Name" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEName" runat="server" Text='<%# bind("EMP_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Category" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDepartmentName" runat="server" Text='<%# bind("Caste") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Designation Name" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDESIGNATION" runat="server" Text='<%# bind("DESIGNATION") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkSelect" CausesValidation="false" runat="server" OnClick="lnkRelSelect_Click">Select</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                        <cc1:AutoCompleteExtender ID="txtDesignation_AutoCompleteExtender" runat="server"
                                            ServiceMethod="GetCompletionDesignationName" ServicePath="~/WebService1.asmx"
                                            CompletionSetCount="5" EnableCaching="true" MinimumPrefixLength="1" CompletionInterval="1000"
                                            UseContextKey="True" FirstRowSelected="True" BehaviorID="AutoCompleteExt3" TargetControlID="txtDesignation">
                                        </cc1:AutoCompleteExtender>
                                        <cc1:AutoCompleteExtender ID="txtCenterCode_AutoCompleteExtender" runat="server"
                                            ServiceMethod="GetCompletionCenter" ServicePath="~/WebService1.asmx" CompletionSetCount="5"
                                            EnableCaching="true" MinimumPrefixLength="1" CompletionInterval="1000" UseContextKey="True"
                                            FirstRowSelected="True" BehaviorID="AutoCompleteExt2" TargetControlID="txtCenterCode">
                                        </cc1:AutoCompleteExtender>
                                        <cc1:AutoCompleteExtender ID="txtECode_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionEmployeeCode"
                                            ServicePath="~/WebService1.asmx" CompletionSetCount="5" EnableCaching="true"
                                            MinimumPrefixLength="1" CompletionInterval="1000" UseContextKey="True" FirstRowSelected="True"
                                            BehaviorID="AutoCompleteExt" TargetControlID="txtECode">
                                        </cc1:AutoCompleteExtender>
                                        <cc1:AutoCompleteExtender ID="txtEname_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionEmpName"
                                            ServicePath="~/WebService1.asmx" CompletionSetCount="5" EnableCaching="true"
                                            MinimumPrefixLength="1" CompletionInterval="1000" UseContextKey="True" FirstRowSelected="True"
                                            BehaviorID="AutoCompleteExt1" TargetControlID="txtEname">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="9">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="pnlEmp"
                        CollapsedText="Hide Employee Detail" ExpandedText="Show Employee Detail" ExpandControlID="lblclps"
                        CollapseControlID="lblclps" Collapsed="true" ExpandDirection="Vertical" SuppressPostBack="true"
                        CollapsedImage="../../App_Themes/BasicTheme/Images/sprite1.png" ExpandedImage="../../App_Themes/BasicTheme/Images/sprite2.png"
                        ImageControlID="img_profile" />
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <fieldset>
                    <table align="center" style="width: 100%;">
                        <tr>
                            <td align="center" width="130" rowspan="2">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Payroll/images/empUser.png" Width="100px"
                                    Height="110px" />
                            </td>
                            <td>
                                <table width="100%" style="vertical-align: top">
                                    <tr>
                                        <td>
                                            <b>Employee Code</b>
                                        </td>
                                        <td>
                                             <asp:Label ID="lblEmployeeCode" runat="server" Font-Bold="True"></asp:Label>
                                            <asp:Label ID="lblEmpCode" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <b>Name</b>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblShowName" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Designation</b>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblShowDesignation" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <b>Sex</b>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblShowSex" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Father/Hus.Name</b>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblShowFHName" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <b>Category</b>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblShowCategory" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Date &amp; Place of Birth</b>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblShowDOB" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <b>Marital Status</b>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblShowMaritalStatus" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                    </table>
                  <%--  <table width="100%" cellpadding="2" cellspacing="2">
                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkEmpBasic" runat="server" Style="background-image: url('../../images/sublink.gif');
                                    background-repeat: no-repeat; background-position: left center; padding-left: 15px;"
                                    OnClick="lnkEmpBasic_Click" CausesValidation="False">Basic Details</asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkEmpAddDetial" runat="server" Style="background-image: url('../../images/sublink.gif');
                                    background-repeat: no-repeat; background-position: left center; padding-left: 15px;"
                                    OnClick="lnkEmpAddDetial_Click" CausesValidation="False">Additional Details</asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkF" runat="server" Style="background-image: url('../../images/sublink.gif');
                                    background-repeat: no-repeat; background-position: left center; padding-left: 15px;"
                                    OnClick="lnkF_Click" CausesValidation="False">Family Details</asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkND" runat="server" Style="background-image: url('../../images/sublink.gif');
                                    background-repeat: no-repeat; background-position: left center; padding-left: 15px;"
                                    OnClick="lnkND_Click" CausesValidation="False">Nomination Details</asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkLPC" runat="server" Style="background-image: url('../../images/sublink.gif');
                                    background-repeat: no-repeat; background-position: left center; padding-left: 15px;"
                                    OnClick="lnkLPC_Click" CausesValidation="False">Salary Structure</asp:LinkButton>
                            </td>
                        </tr>
                    </table>--%>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <div>
        <fieldset>
            <table width="100%" style="margin-bottom: 0px;">
                <tr>
                    <td colspan="4" style="text-align: left; border-bottom: solid 1px #666633" class="PageHeaddingBox">
                        <b>Basic Detail:</b>
                    </td>
                </tr>
                <tr>
                    <td>
                        Current Pay Scale
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlPayGrade" Width="90%" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlPayGrade_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        Pay Band
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlPayBand" Width="90%" SkinID="ddl">
                           <%-- <asp:ListItem>I</asp:ListItem>
                            <asp:ListItem>II</asp:ListItem>
                            <asp:ListItem>III</asp:ListItem>
                            <asp:ListItem>IV</asp:ListItem>--%>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr><td> Basic pay(7th pay)</td> <td>
                    <asp:TextBox runat="server" Width="100px" ID="txt7thPaybasic">0</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                            Enabled="True" TargetControlID="txt7thPaybasic" FilterMode="ValidChars" ValidChars="0123456789.">
                        </cc1:FilteredTextBoxExtender>


                                                 </td>

                    <td>  7th pay Level(1 to 18) </td><td> <asp:TextBox runat="server" Width="100px" ID="txt7thPayLeval" Enabled="false">0</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                            Enabled="True" TargetControlID="txt7thPayLeval" FilterMode="ValidChars" ValidChars="0123456789A">
                        </cc1:FilteredTextBoxExtender></td>



                </tr>
                <tr>
                    <td>
                        Basic pay(6th pay)
                    </td>
                    <td>
                        <asp:TextBox runat="server" Width="100px" ID="txtBasicSalary">0</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="txtBasicSalary_FilteredTextBoxExtender" runat="server"
                            Enabled="True" TargetControlID="txtBasicSalary" FilterMode="ValidChars" ValidChars="0123456789.">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                    <td>
                        Grade Pay(6th pay)
                    </td>
                    <td>
                        <asp:TextBox runat="server" Width="100px" ID="txtPayGrade" Enabled="False">0</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="txtPayGrade_FilteredTextBoxExtender" runat="server"
                            Enabled="True" TargetControlID="txtPayGrade" FilterMode="ValidChars" ValidChars="0123456789.">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr id="trcc" runat="server" visible="false">
                    <td>
                        NPA(%)
                    </td>
                    <td>
                        <asp:TextBox runat="server" Width="100px" onkeyup="this.value = minmax(this.value)"
                            ID="txtIncrementRs">0</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="txtIncrementRs_FilteredTextBoxExtender" runat="server"
                            Enabled="True" TargetControlID="txtIncrementRs" FilterMode="ValidChars" ValidChars="0123456789.">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                    <td style="visibility: hidden; display: none;">
                        CCC Passing Date
                    </td>
                    <td style="visibility: hidden; display: none;">
                        <asp:TextBox ID="txt_cccdate" runat="server" Width="100px"></asp:TextBox>
                        <cc1:CalendarExtender ID="clndr" runat="server" TargetControlID="txt_cccdate" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
              
                <tr runat="server" visible="false">
                    <td colspan="4" class="PageHeaddingBox">
                        <b>Allowances:</b>
                    </td>
                </tr>
                <tr>
                    <td>
                        Personal Pay
                    </td>
                    <td style="width: 131px">
                        <asp:TextBox ID="txt_cta" runat="server" Width="100px" Text="0"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="txt_cta_FilteredTextBoxExtender" runat="server"
                            Enabled="True" TargetControlID="txt_cta" FilterMode="ValidChars" ValidChars="0123456789.">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                    <td>
                        Special Pay
                    </td>
                    <td>
                        <asp:TextBox runat="server" Width="100px" ID="txtSpecialPay">0</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="txtSpecialPay_FilteredTextBoxExtender" runat="server"
                            Enabled="True" TargetControlID="txtSpecialPay" FilterMode="ValidChars" ValidChars="0123456789.">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td >
                        Increment Flag
                    </td>
                    <td >
                        <asp:DropDownList ID="ddl_IncrementFlag" runat="server" SkinID="ddl" onchange="ChangeIncrement();">
                             <asp:ListItem Value="1">Give Increment</asp:ListItem>
                      <asp:ListItem Value="0">No Increment</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>

                        ODDA Flag(Transport Allowance)
                    </td>
                    <td>

<%--                        AutoPostBack="true" OnSelectedIndexChanged="ddlTransportAllowance_SelectedIndexChanged"--%>
 <asp:DropDownList ID="ddlTransportAllowance" runat="server" SkinID="ddl"  >

      
                            <asp:ListItem Value="1">Give ODDA</asp:ListItem>
                      <asp:ListItem Value="0">No ODDA</asp:ListItem>

                        </asp:DropDownList>

                        <asp:TextBox runat="server" Width="100px" ID="txtCompulsary" Style="display: none;"
                            Text="0"></asp:TextBox>

                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                            Enabled="True" TargetControlID="txtCompulsary" FilterMode="ValidChars" ValidChars="0123456789.">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                </tr>

                  <tr >
                    <td>
                        Previous Increment date
                    </td>
                    <td>
                        <asp:TextBox runat="server" Width="100px" ID="txtEmpPrvIncmentDt"></asp:TextBox>
                        <cc1:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtEmpPrvIncmentDt"
                            ID="CalendarExtender1">
                        </cc1:CalendarExtender>
                    </td>
                    <td>
                        Next Increment date
                    </td>
                    <td>
                        <asp:TextBox runat="server" Width="100px" ID="txtEmpNxtIncmentDt"></asp:TextBox>
                        <cc1:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtEmpNxtIncmentDt"
                            ID="CalendarExtender2">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
               
              
                <tr>
                    <td style="display:none">
                        <asp:Label ID="lblpfno" runat="server" Text="GPF No."></asp:Label>
                    </td>
                    <td style="display:none">
                        <asp:Label runat="server" Width="100px" ID="txtPFAcNo"></asp:Label>
                    </td>
                    <td style="display:none">
                        
                        <asp:Label ID="lblStaffCarDeduction" runat="server" Text="Staff Car Deduction" Visible="false"></asp:Label>
                           
                    </td>
                    <td style="display:none">
                       
                        <asp:TextBox runat="server" Width="100px" ID="txtStaffCarDeductionAmount" Visible="false">0</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="txtStaffCarDeductionAmount_FilteredTextBoxExtender" runat="server"
                            Enabled="True" TargetControlID="txtStaffCarDeductionAmount" FilterMode="ValidChars" ValidChars="0123456789.">
                        </cc1:FilteredTextBoxExtender>
                           
                    </td>
                </tr>
               <%-- <tr>
                    <td>
                        HRA
                    </td>
                    <td>
                        <asp:TextBox runat="server" Width="100px" ID="txtHouseRent">0</asp:TextBox>
                    </td>
                    <td>
                        CCA
                    </td>
                    <td>
                        <asp:TextBox runat="server" Width="100px" ID="txtcca">0</asp:TextBox>
                    </td>
                </tr>--%>
              
               
                <tr><td style="display:none">Deputation Pay</td><td style="display:none"><asp:DropDownList runat="server" ID="ddlDeputationPay" SkinID="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlDeputationPay_SelectedIndexChanged">
                          
                      <asp:ListItem Value="N">NO</asp:ListItem>
                      <asp:ListItem Value="Y">Yes</asp:ListItem>
                        </asp:DropDownList> </td> <td>
                            
                            <asp:Label ID="lblDeputationCity" runat="server" Text="Deputation City"></asp:Label>  </td><td>
                            <asp:DropDownList runat="server" ID="ddlDeputationCity" SkinID="ddl">
                          
                            <asp:ListItem Value="S">Same City</asp:ListItem>
                      <asp:ListItem Value="D">Different City</asp:ListItem>
                        </asp:DropDownList></td> </tr>
                <tr><td style="display:none">Deputation Allowance</td><td style="display:none"><asp:TextBox runat="server" Width="100px" ID="txtDeputationAllowance">0</asp:TextBox>
                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                            Enabled="True" TargetControlID="txtDeputationAllowance" FilterMode="ValidChars" ValidChars="0123456789.">
                        </cc1:FilteredTextBoxExtender>


                                                 </td><td style="display:none">Parent Office Earning(CA)</td><td style="display:none"><asp:TextBox runat="server" Width="100px" ID="txtParentOfficeEaring">0</asp:TextBox>

                                                      <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                            Enabled="True" TargetControlID="txtParentOfficeEaring" FilterMode="ValidChars" ValidChars="0123456789.">
                        </cc1:FilteredTextBoxExtender>


                                                                                    </td></tr>


                <tr><td>HRA Flag</td><td>   
                    <asp:DropDownList runat="server" ID="ddlAccomodation" SkinID="ddl">
                          
                       <asp:ListItem Value="0">NO HRA</asp:ListItem>
                      <asp:ListItem Value="1">Give HRA</asp:ListItem>
                      <asp:ListItem Value="2">Two HRA</asp:ListItem>
                      <asp:ListItem Value="3">Fixed HRA & Leased Accomodation</asp:ListItem>
                         <asp:ListItem Value="4">with in 1 km Radius</asp:ListItem>
                        </asp:DropDownList> </td>
                    
                    
                    <td >CGIS Flag</td><td ><asp:DropDownList runat="server" ID="ddlCGISFlage" SkinID="ddl">
                          
                            <asp:ListItem Value="0">Not a CGIS Member </asp:ListItem>
                      <asp:ListItem Value="1">CGIS Member</asp:ListItem>
                        <asp:ListItem Value="2">Member of parent office</asp:ListItem>
                        </asp:DropDownList></td></tr>


                <tr ><td>Staff Club Member</td><td>
                    <asp:DropDownList runat="server" ID="ddlStaffClubMember" SkinID="ddl" >
                          
                            <asp:ListItem Value="0">NOT Member</asp:ListItem>
                      <asp:ListItem Value="1">Member</asp:ListItem>
                        </asp:DropDownList></td><td> 
                            
                             <asp:Label ID="lblKitMaintenanceAllowance" runat="server" Text="HRA"></asp:Label></td><td> 
                            <asp:TextBox runat="server" Width="100px" ID="txtHRA">0</asp:TextBox>
                              <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                            Enabled="True" TargetControlID="txtHRA" FilterMode="ValidChars" ValidChars="0123456789.">
                        </cc1:FilteredTextBoxExtender>

                                                                                                                                                                   </td></tr>

                 <tr>
                    <td > Non Taxable TA
                    </td>
                    <td >

                       <asp:TextBox runat="server" Width="100px" ID="txtNonTaxableTA">0</asp:TextBox>

                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                            Enabled="True" TargetControlID="txtNonTaxableTA" FilterMode="ValidChars" ValidChars="0123456789.">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                    <td >  Taxable TA
                    </td>
                    <td > <asp:TextBox runat="server" Width="100px" ID="txtTaxableTA">0</asp:TextBox>

                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                            Enabled="True" TargetControlID="txtTaxableTA" FilterMode="ValidChars" ValidChars="0123456789.">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                </tr>

                  <tr><td> 
                  
                  
                  <asp:Label ID="lblgpf_amt" runat="server" Text="GPF Subscription" Visible="false"></asp:Label></td>
                  
                  
                  <td> <asp:TextBox ID="txt_gpf" runat="server" Width="100px" Text="0" Visible="false"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="txt_gpf_FilteredTextBoxExtender" runat="server"
                            Enabled="True" TargetControlID="txt_gpf" FilterMode="ValidChars" ValidChars="0123456789.">
                        </cc1:FilteredTextBoxExtender></td><td><asp:Label ID="lblGPFCPFNPSTYPE" runat="server" Visible="false"> </asp:Label></td>
                        
                        
                        
                        </tr>



              <%--  <tr style="visibility: hidden; display: none;">
                    <td colspan="4" class="PageHeaddingBox">
                        <b>Balance as on 31-Oct-2012</b>
                    </td>
                </tr>
                <tr style="visibility: hidden; display: none;">
                    <td>
                       
                    </td>
                    <td>
                        GSS(Rs.)
                    </td>
                    <td>
                        Earned leaved accured<br />
                        (in days)
                    </td>
                </tr>
                <tr style="visibility: hidden; display: none;">
                    <td>
                       
                    </td>
                    <td>
                        <asp:TextBox runat="server" Width="100px" ID="txtGSSAmt">0</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="txtGSSAmt_FilteredTextBoxExtender" runat="server"
                            Enabled="True" TargetControlID="txtGSSAmt" FilterMode="ValidChars" ValidChars="0123456789.">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <asp:TextBox runat="server" Width="100px" ID="txtEarnedLeave">0</asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="txtEarnedLeave_FilteredTextBoxExtender" runat="server"
                            Enabled="True" TargetControlID="txtEarnedLeave" FilterMode="ValidChars" ValidChars="0123456789.">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                </tr>--%>


                 <tr>
                                              <%--  <asp:Panel ID="Panel1" runat="server" Width="100%">--%>
                                                    <td> GPF /CPF/NPS Type</td>
                                                    <td id="tdCPFGPFValue" runat="server" >
                                                     
                                                             <asp:RadioButtonList ID="rbgpftype" runat="server" RepeatDirection="Horizontal" Width="70%">
                                                            <asp:ListItem Text="GPF" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="CPF" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="NPS" Value="3" Selected="True"></asp:ListItem>
                                                        </asp:RadioButtonList></td>
<td>CPF/PRAN No.:</td>
                                                   
                                                    <td id="tdCPFGPFText" runat="server" ><a style="color: Red;">*</a>  <asp:TextBox ID="txtCPFGPF" runat="server" Width="50%"></asp:TextBox></td>
                                                    
                                              <%--  </asp:Panel>--%>
                                            </tr>

                
                <tr><td>Pan No.:</td><td><asp:TextBox ID="txtPan" OnChange="return ValidatePAN(this);" runat="server" MaxLength="10"></asp:TextBox> </td> <td> Pan Card Scan Copy</td><td> 

                    
                          <asp:UpdatePanel ID="pnlphoto" runat="server">
        <ContentTemplate>
                    
                     <asp:FileUpload ID="upPAN" Width="265px" runat="server"  />
                                                                        &nbsp;<asp:HyperLink ID="hlPAN" Target="_blank" runat="server" Visible="False" ForeColor="Green" Width="40%">
                                                                            [View]
                                                                         <asp:Image ID="ImgPan" runat="server" Height="25px" ImageUrl="~/App_Themes/BasicTheme/Images/docu.png" Width="25px" />

                                                                        </asp:HyperLink>
            
            

              </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
            
            </td> </tr>
              

                <tr>
                    <td>Double TA Applicable </td>
                    <td>
                        <asp:DropDownList ID="ddlDTA" runat="server" >
                            <asp:ListItem Text="NO" Selected="True" Value="N"></asp:ListItem>
                            <asp:ListItem Text="YES" Value="Y"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td> Tax Regime Type</td>
                     <td>
                        <asp:DropDownList ID="ddlTaxType" runat="server">
                            <asp:ListItem Text="NEW" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="OLD" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
              
                <tr>
                    <td colspan="4" class="PageHeaddingBox">
                        <b>Bank Detail:</b>
                    </td>
                </tr>
                <tr>
                    <td>
                        Salary pay Mode
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlsalaryMode" Width="100px" SkinID="ddl">
                            <asp:ListItem>Cheque</asp:ListItem>
                            <asp:ListItem>Bank</asp:ListItem>
                            <asp:ListItem>Transfer</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Bank Name
                    </td>
                    <td>
                        
                        <asp:DropDownList ID="ddlbank" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbank_SelectedIndexChanged">
                        </asp:DropDownList>
                       
                        <asp:Label runat="server" Visible="false" Width="100px" ID="lblBankName"></asp:Label>
                    </td>

                    <td>
                        Branch Name
                    </td>
                    <td>
                       <%-- <asp:TextBox runat="server" Width="100px" ID="txtBranchCode"></asp:TextBox>--%>

                           <asp:DropDownList ID="txtBranch9" runat="server"></asp:DropDownList>
                    </td>
                </tr>
               
                <tr>
                    <td>
                        IFSC Code
                    </td>
                    <td>
                        <asp:TextBox runat="server" Width="100px" ID="txtIFSCCode"></asp:TextBox>
                    </td>

                    <td>
                        Account number
                    </td>
                    <td>
                        <asp:TextBox runat="server" Width="100px" ID="txtAccountNumber"></asp:TextBox>
                       
                    </td>
                </tr>

                <tr>  <td>MICR Code</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtmicr" runat="server" Width="90px"></asp:TextBox>
                                                                                </td>
                                                                                <td>BSR</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtbsr" runat="server" Width="90px"></asp:TextBox>
                                                                                </td></tr>

            </table>
            <%--<table style="display: none;">
                <tr>
                    <td>
                        <asp:CheckBox ID="chkCity" runat="server" Text="City" AutoPostBack="True" OnCheckedChanged="chkCity_CheckedChanged" />
                    </td>
                    <td>
                        Rs.<asp:TextBox runat="server" Width="100px" ID="txtCity">0</asp:TextBox>
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkWashing" runat="server" Text="Washing" AutoPostBack="True" OnCheckedChanged="chkWashing_CheckedChanged" />
                    </td>
                    <td>
                        Rs.
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkEmergency" runat="server" Text="Emergency" AutoPostBack="True"
                            OnCheckedChanged="chkEmergency_CheckedChanged" />
                    </td>
                    <td>
                        Rs.<asp:TextBox runat="server" Width="100px" ID="txtEmergency">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkNonPractice" runat="server" Text="Non-Practice" AutoPostBack="True"
                            OnCheckedChanged="chkNonPractice_CheckedChanged" />
                    </td>
                    <td>
                        Rs.<asp:TextBox runat="server" Width="100px" ID="txtNonPractice">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkUniform" runat="server" Text="Uniform" AutoPostBack="True" OnCheckedChanged="chkUniform_CheckedChanged" />
                    </td>
                    <td>
                        Rs.<asp:TextBox runat="server" Width="100px" ID="txtUniform">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkSpecial" runat="server" Text="Special" AutoPostBack="True" OnCheckedChanged="chkSpecial_CheckedChanged" />
                    </td>
                    <td>
                        Rs.<asp:TextBox runat="server" Width="100px" ID="txtSpecial">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="display: none;">
                        Employee Contribution (%)<br />
                        (Basic + Grade Pay + DA) for a new employee only
                    </td>
                    <td style="display: none;">
                        <asp:TextBox runat="server" Width="100px" ID="txtEmpContribution">0</asp:TextBox>
                    </td>
                </tr>
            </table>--%>
        </fieldset>
    </div>
    <div align="center">
        <asp:Button ID="btnSave" runat="server" CssClass="cmdbutton" OnClick="btnSave_Click" Text="Save" />
        &nbsp;&nbsp;
        <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="cmdbutton" OnClick="btnReset_Click1" />
    </div>
    <uc1:Messagebox ID="Messagebox" runat="server" />
</asp:Content>
