using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;
using System.Collections.Generic;
using System.Web.Services;
using System.IO;

public partial class M_Emp_SalaryStructure : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    CommonCode cc = new CommonCode();
    MyConnection Mycon = new MyConnection();
    PLSalaryStructure pd = new PLSalaryStructure();
    BLSalaryStructure bs = new BLSalaryStructure();
    public string empid;
    public string EmpCode
    {
        get
        {
            string text = (string)ViewState["Eid"];
            if (text != null)
                return text;
            else
                return string.Empty;
        }
        set
        {
            ViewState["Eid"] = value;
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Eid"])))
        {
            ViewState["Eid"] = Request.QueryString["Eid"].ToString();
            EmpCode = Request.QueryString["Eid"].ToString();
            ViewState["Eid"] = EmpCode;
        }

        panelLinks.Visible = true;
        // PanelNoRecordShow.Visible = true;

        if (!string.IsNullOrEmpty(Convert.ToString(EmpCode)))
        {

            Display1(EmpCode);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            hfdSCf.Value = (Session["sess"] == null ? null : Session["sess"].ToString().Trim());
            reset();
            txtStaffCarDeductionAmount.Visible = true;
            lblStaffCarDeduction.Visible = true;
            //bindHRA();
       
            cc.FillDDL(ref ddlbank, "--Select--", "Name", "TableId", " M_Bank");
            if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Eid"])))
            {
                EmpCode = Request.QueryString["Eid"].ToString();
            }
            cc.FillPayScale(ref ddlPayGrade);
            FillPayBand();
            if (EmpCode != "")
            {
                Display(EmpCode);
                Display1(EmpCode);
            }


            checkAllowence();
            //if (Session["VolumeName"].ToString().ToLower().Contains("officer") || Session["VolumeName"].ToString().ToLower().Contains("officers"))
            //{
            //    //txtCompulsary.Text = "45";
            //    //txtCompulsary.Enabled = true;
            //    //txtMediacalAllow.Text = "200";
            //    //txtMediacalAllow.Enabled = true;
            //}
            //else
            //{
            //    //txtCompulsary.Text = "0";
            //    //txtCompulsary.Enabled = true;
            //    //txtMediacalAllow.Text = "0";
            //    //txtMediacalAllow.Enabled = true;
            //}
            //if (Session["VolumeName"].ToString().ToLower().Contains("gpf"))
            //{
            //    ddlGpfType.SelectedValue = "GPF";
            //    ddlGpfType_SelectedIndexChanged(null, null);
            //}
            //if (Session["VolumeName"].ToString().ToLower().Contains("cps"))
            //{
            //    ddlGpfType.SelectedValue = "CPS";
            //    ddlGpfType_SelectedIndexChanged(null, null);
            //}
            //if (Session["VolumeName"].ToString().ToLower().Contains("nps"))
            //{
            //    ddlGpfType.SelectedValue = "NPS";
            //    ddlGpfType_SelectedIndexChanged(null, null);
            //}
            //if (Session["VolumeName"].ToString().ToLower().Contains("cps"))
            //{
            //    trcc.Visible = false;
            //}

            BindFundGrid();
        }

        //if (ddlGpfType.SelectedValue == "GPF")
        //{
        //    lblpfno.Text = "GPF No.";
        //    txtGPFAmt.Visible = true;
        //    lbl_gpf.Visible = true;
        //}
    }
    //public void bindHRA()
    //{

    //    Mycon.adp.SelectCommand.CommandText = "USP_BIND_HRA";
    //    Mycon.adp.SelectCommand.CommandType = CommandType.StoredProcedure;
    //    Mycon.adp.SelectCommand.Parameters.Clear();
    //    DataTable dt = new DataTable();
    //    Mycon.adp.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        ddl_IncrementFlag.DataSource = dt;
    //        ddl_IncrementFlag.DataTextField = "HRAType";
    //        ddl_IncrementFlag.DataValueField = "HRACode";
    //        ddl_IncrementFlag.DataBind();
    //    }
    //    else
    //    {
    //        ddl_IncrementFlag.DataSource = null;
    //        ddl_IncrementFlag.DataBind();
    //    }
    //}
    public void Display1(string ID)
    {
        DataTable dt = new DataTable();
        dt = cc.EQ("select *,Convert(varchar, Date_OF_BIRTH,103) as dob, (select Name from M_Designation Where TableID=M_EMP_MAST.DESIG_CODE) as Designation,GPFType from M_Emp_mast where ECODE='" + ID + "'");

        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["Photo"].ToString() != "")
            {
                Image1.ImageUrl = "~/EmployeeDocoments/EmpPhoto" + dt.Rows[0]["Photo"].ToString();
            }
            else
            {
                Image1.ImageUrl = "~/Payroll/images/empUser.png";
            }
            empid = dt.Rows[0]["ECODE"].ToString();
            lblEmpCode.Text = dt.Rows[0]["ECODE"].ToString();
            lblShowName.Text = dt.Rows[0]["ECODE"].ToString();
            lblShowCategory.Text = dt.Rows[0]["Caste"].ToString();
            if (dt.Rows[0]["DOB"].ToString() != "")
                lblShowDOB.Text = Convert.ToDateTime(dt.Rows[0]["DOB"]).ToString("dd/MM/yyyy");
            lblShowDesignation.Text = dt.Rows[0]["Designation"].ToString();
            lblShowFHName.Text = dt.Rows[0]["Fname"].ToString();
            if (dt.Rows[0]["MaritalStatus"].ToString() == "S".ToString())
                lblShowMaritalStatus.Text = "Unmarried";
            else if (dt.Rows[0]["MaritalStatus"].ToString() == "M".ToString())
                lblShowMaritalStatus.Text = "Married";
            else if (dt.Rows[0]["MaritalStatus"].ToString() == "D".ToString())
                lblShowMaritalStatus.Text = "Divorcee";
            lblShowName.Text = dt.Rows[0]["Emp_Name"].ToString();
            if (dt.Rows[0]["Sex"].ToString() == "M")
                lblShowSex.Text = "Male";
            else
                lblShowSex.Text = "Female";

            lblGPFCPFNPSTYPE.Text = dt.Rows[0]["GPFType"].ToString();

       

            if (lblGPFCPFNPSTYPE.Text == "2")
            {
                lblgpf_amt.Visible = true;
                txt_gpf.Visible = true;
            }
            else
            {
                lblgpf_amt.Visible = false;
                txt_gpf.Visible = false;
            }

         

            panelLinks.Visible = true;

        }
    }
    private void checkAllowence()
    {
        //chkCity.Checked = false;
        //chkEmergency.Checked = false;
        //chkNonPractice.Checked = false;
        //chkSpecial.Checked = false;
        //chkWashing.Checked = false;
        //chkUniform.Checked = false;
       // txtCity.Text = "0.00";
       // txtEmergency.Text = "0.00";
       // txtNonPractice.Text = "0.00";
        txt7thPayLeval.Text = "";
        txt7thPaybasic.Text = "";
        txtTaxableTA.Text = "";
        txtNonTaxableTA.Text = "0";
      //  txtUniform.Text = "0.00";
      //  txtSpecial.Text = "0.00";
     //   txtCity.Enabled = false;
     //   txtEmergency.Enabled = false;
       // txtNonPractice.Enabled = false;
        //txtWashing.Enabled = false;
      //  txtUniform.Enabled = false;
        //txtSpecial.Enabled = false;
       // ddlDeputationPay.SelectedValue = "N";
       
        ddlAccomodation.SelectedValue = "0";
        lblDeputationCity.Visible = false;
        ddlDeputationCity.Visible = false;
    }

    public void FillPayBand()
    {
        DataTable dtPayBand = new DataTable();
        dtPayBand = cc.EQ("select PBCode,Convert(varchar,Begins)+'-'+Convert(varchar,EndS) as PayBand from [dbo].[M_PAYBAND]");

        if (dtPayBand.Rows.Count > 0)
        {

            ddlPayBand.DataSource = dtPayBand;
            ddlPayBand.DataTextField = "PayBand";
            ddlPayBand.DataValueField = "PBCode";
            ddlPayBand.DataBind();
            ddlPayBand.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--" });
        }
        else
        {
            ddlPayBand.DataSource = null;
            ddlPayBand.DataBind();
        }
    }
    public void Display(string ID)
    {
        DataTable dt = new DataTable();

        dt = cc.EQ("select convert(varchar(15),PREV_INCRE_DATE,103)PREV_INCRE_DATE1,convert(varchar(15),NEXT_INCRE_DATE,103)NEXT_INCRE_DATE1,bank_name,* from EMP_SALARY_STRUCTURE where ECODE='" + ID + "'");
        if (dt.Rows.Count > 0)
        {
            try
            {
                ddlPayBand.Text = dt.Rows[0]["PAY_BAND"].ToString();
                ddlPayGrade.SelectedValue = dt.Rows[0]["CUR_PAY_GRADE"].ToString();
            }
            catch
            {
            }

           if (dt.Rows[0]["BASIC"].ToString() == "")
            {
                txtBasicSalary.Text = "0";
            }
            else
            {
                txtBasicSalary.Text = dt.Rows[0]["BASIC"].ToString();
            }

           if (dt.Rows[0]["BASIC_7PAY"].ToString() == "")
           {

               txt7thPaybasic.Text = "0";
           }
           else
           {
               txt7thPaybasic.Text = dt.Rows[0]["BASIC_7PAY"].ToString();
           }

           if (dt.Rows[0]["Level_code"].ToString() == "")
           {

               
               txt7thPayLeval.Text = "";
           }
           else
           {
               txt7thPayLeval.Text = dt.Rows[0]["Level_code"].ToString();
           }


            if (dt.Rows[0]["DP"].ToString() == "")
            {
                txtPayGrade.Text = "0";
            }
            else
            {
                txtPayGrade.Text = dt.Rows[0]["DP"].ToString();
            }

            if (dt.Rows[0]["PERSONAL_PAy"].ToString() == "")
            {
                txt_cta.Text = "0";
            }
            else
            {
                txt_cta.Text = dt.Rows[0]["PERSONAL_PAy"].ToString();
            }
            if (dt.Rows[0]["SPECIAL_PAY"].ToString() == "")
            {
                txtSpecialPay.Text = "0";
            }
            else
            {
                txtSpecialPay.Text = dt.Rows[0]["SPECIAL_PAY"].ToString();
            }
            if (dt.Rows[0]["IncrFlg"].ToString() != "")
            {
                ddl_IncrementFlag.SelectedValue = dt.Rows[0]["IncrFlg"].ToString();
            }

            if (!string.IsNullOrEmpty(dt.Rows[0]["TA"].ToString()))
            {
                ddlTransportAllowance.SelectedValue = dt.Rows[0]["TA"].ToString();
            }
            ddlTransportAllowance_SelectedIndexChanged(null,null);
            //if (!string.IsNullOrEmpty(dt.Rows[0]["StaffCarDeduction"].ToString()))
            //{
            //    txtStaffCarDeductionAmount.Text = dt.Rows[0]["StaffCarDeduction"].ToString();
            //}
            //else
            //{
            //    txtStaffCarDeductionAmount.Text = "0";
            //}
            if (!string.IsNullOrEmpty(dt.Rows[0]["E_ODDA"].ToString()))
            {
                txtNonTaxableTA.Text = dt.Rows[0]["E_ODDA"].ToString();
            }
            else
            {
                txtNonTaxableTA.Text = "0";
            }
            if (!string.IsNullOrEmpty(dt.Rows[0]["E_CCA"].ToString()))
            {
                txtTaxableTA.Text = dt.Rows[0]["E_CCA"].ToString();
            }
            else
            {
                txtTaxableTA.Text = "0";
            
            }



            if (!string.IsNullOrEmpty(dt.Rows[0]["GPFAmount"].ToString()))
            {

                txt_gpf.Text = dt.Rows[0]["GPFAmount"].ToString();
            }
            else
            {
                txt_gpf.Text = "0";

            }



            //if (!string.IsNullOrEmpty(dt.Rows[0]["DeputationPay"].ToString()))
            //{
            //ddlDeputationPay.SelectedValue= dt.Rows[0]["DeputationPay"].ToString();
            //}
            // ddlDeputationPay_SelectedIndexChanged(null,null);
            // if (ddlDeputationPay.SelectedValue == "Y")
            // {



            //     if (!string.IsNullOrEmpty(dt.Rows[0]["DeputationCity"].ToString()))
            //     {
            //         ddlDeputationCity.SelectedValue = dt.Rows[0]["DeputationCity"].ToString();
            //     }

            //     if (!string.IsNullOrEmpty(dt.Rows[0]["DeputationAllowance"].ToString()))
            //     {
            //         txtDeputationAllowance.Text = dt.Rows[0]["DeputationAllowance"].ToString();
            //     }



            // }
            // else
            // {

            //     txtDeputationAllowance.Text = "0";
            // }

            // if (!string.IsNullOrEmpty(dt.Rows[0]["ParentOfficeEarring"].ToString()))
            // {
            //     txtParentOfficeEaring.Text = dt.Rows[0]["ParentOfficeEarring"].ToString();
            // }
            // else
            // {
            //     txtParentOfficeEaring.Text = "0";
            // }

             if (!string.IsNullOrEmpty(dt.Rows[0]["Accomodation"].ToString()))
             {
                 ddlAccomodation.SelectedValue = dt.Rows[0]["Accomodation"].ToString();
             }


             if (!string.IsNullOrEmpty(dt.Rows[0]["CGISFlg"].ToString()))
             {

                 ddlCGISFlage.SelectedValue = dt.Rows[0]["CGISFlg"].ToString();
             }
             //if (!string.IsNullOrEmpty(dt.Rows[0]["KitMaintenFlag"].ToString()))
             //{
             if (!string.IsNullOrEmpty(dt.Rows[0]["StaffClub"].ToString()))
             ddlStaffClubMember.SelectedValue = dt.Rows[0]["StaffClub"].ToString();

                 if (!string.IsNullOrEmpty(dt.Rows[0]["E_HRA"].ToString()))
                 {

                     txtHRA.Text = dt.Rows[0]["E_HRA"].ToString();
                 }
                 else
                 {
                     txtHRA.Text = "0";
                 }

            // }
        
            //if (dt.Rows[0]["HRA"].ToString() == "")
            //{
            //    txtHouseRent.Text = "0";
            //}
            //else
            //{
          //  txtHouseRent.Text = dt.Rows[0]["HRA"].ToString();
            //}
          
          
            //chkGPF.Checked = (double.Parse(txtIncrementRs.Text) == 0 ? false : true);

           
          
           // chkWashing.Checked = (txtWashing.Text == "0" || txtWashing.Text == "" ? false : true);

            //txtEmergency.Text = dt.Rows[0]["EMERGENCY_ALLOW"].ToString();
            //chkEmergency.Checked = (txtEmergency.Text == "0" || txtEmergency.Text == "" ? false : true);

            //txtNonPractice.Text = dt.Rows[0]["NON_PRAC_ALLOW"].ToString();
            //chkNonPractice.Checked = (txtNonPractice.Text == "0" || txtNonPractice.Text == "" ? false : true);

            //txtUniform.Text = dt.Rows[0]["UNIFORM_ALLOW"].ToString();
            //chkUniform.Checked = (txtUniform.Text == "0" || txtUniform.Text == "" ? false : true);

            //txtSpecial.Text = dt.Rows[0]["SPECIAL_ALLOW"].ToString();
            //chkSpecial.Checked = (txtSpecial.Text == "0.00" || txtSpecial.Text == "" ? false : true);
            //chkGPF.Checked = (dt.Rows[0]["gpf_flag"].ToString() == "0" || dt.Rows[0]["gpf_flag"].ToString() == "" ? false : true);
          //  ddlGPFStop.SelectedIndex = ddlGPFStop.Items.IndexOf(ddlGPFStop.Items.FindByValue(dt.Rows[0]["gpf_flag"].ToString()));
            if (dt.Rows[0]["PREV_INCRE_DATE1"].ToString() == "01/01/1900")
            {
                txtEmpPrvIncmentDt.Text = "";
            }
            else
            {
                txtEmpPrvIncmentDt.Text = dt.Rows[0]["PREV_INCRE_DATE1"].ToString();
            }
            if (dt.Rows[0]["NEXT_INCRE_DATE1"].ToString() == "01/01/1900")
            {
                txtEmpNxtIncmentDt.Text = "";
            }
            else
            {
                txtEmpNxtIncmentDt.Text = dt.Rows[0]["NEXT_INCRE_DATE1"].ToString();
            }
            ddlsalaryMode.SelectedValue = dt.Rows[0]["PAY_MODE"].ToString();
            
            //if (dt.Rows[0]["BANK_NAME"].ToString() == "0")
            //{ ddlbank.SelectedIndex = 0; }
            //else
            //{ ddlbank.SelectedValue = dt.Rows[0]["BANK_NAME"].ToString(); }
            DataTable dtBankDetail = new DataTable();

            dtBankDetail = cc.EQ("select *,(SELECT TOP(1) ISNULL(IPS_Type,'N')  FROM [dbo].[M_Designation] WHERE [TableID]=M_EMP_MAST.DESIG_CODE)  as IPS_Type,(select (CASE WHEN [panCertificate] IS NULL OR [panCertificate]=' ' OR [panCertificate]='' THEN '~/App_Themes/BasicTheme/Images/NotExixts.jpg' ELSE [panCertificate] END) From  [dbo].[M_Employee] where EmpCode=M_EMP_MAST.ECODE) as PanCertificate ,(select ISNULL(IsDoubleTA,'N') from EMP_SALARY_STRUCTURE where ECode=M_EMP_MAST.ECODE) as IsDTA,(select micr From  [dbo].[M_Employee] where EmpCode=M_EMP_MAST.ECODE) as micr ,(select bsr From  [dbo].[M_Employee] where EmpCode=M_EMP_MAST.ECODE) as bsr from M_EMP_MAST where ECODE='" + ID + "'");
        if (dtBankDetail.Rows.Count > 0)
        {

            if (!string.IsNullOrEmpty(dtBankDetail.Rows[0]["GPFTYpe"].ToString()))
            {
                rbgpftype.SelectedValue = Convert.ToString(dtBankDetail.Rows[0]["GPFTYpe"].ToString());
            }
            txtCPFGPF.Text = dtBankDetail.Rows[0]["PRANNO"].ToString();
            txtPan.Text = dtBankDetail.Rows[0]["PAN"].ToString();
                ddlDTA.SelectedValue= dtBankDetail.Rows[0]["IsDTA"].ToString();

                try
            {
                ddlbank.SelectedValue = Convert.ToString(dtBankDetail.Rows[0]["BankName"] == "" ? "" : dtBankDetail.Rows[0]["BankName"]);
                ddlbank_SelectedIndexChanged(null, null);
                txtBranch9.SelectedValue = Convert.ToString(dtBankDetail.Rows[0]["BranchName"] == "" ? "" : dtBankDetail.Rows[0]["BranchName"]);

            }
            catch (Exception ex)
            {
            }
               
                txtIFSCCode.Text = Convert.ToString(dtBankDetail.Rows[0]["IFSCCode"] == "" ? "" : dtBankDetail.Rows[0]["IFSCCode"]);
                txtAccountNumber.Text = Convert.ToString(dtBankDetail.Rows[0]["ACNO"] == "" ? "" : dtBankDetail.Rows[0]["ACNO"]);
                txtPFAcNo.Text = Convert.ToString(dtBankDetail.Rows[0]["GpfCpfNo"] == "" ? "" : dtBankDetail.Rows[0]["GpfCpfNo"]);
                txtmicr.Text = Convert.ToString(dtBankDetail.Rows[0]["micr"] == "" ? "" : dtBankDetail.Rows[0]["micr"]);
                txtbsr.Text = Convert.ToString(dtBankDetail.Rows[0]["bsr"] == "" ? "" : dtBankDetail.Rows[0]["bsr"]);
                string PANCer = Convert.ToString(dtBankDetail.Rows[0]["PanCertificate"]);
                ViewState["PANCert"] = Convert.ToString(dtBankDetail.Rows[0]["PanCertificate"]);

            if (PANCer == "~/App_Themes/BasicTheme/Images/NotExixts.gif")
            {
                ImgPan.ImageUrl = "~/App_Themes/BasicTheme/Images/docu.png";
            }
            else
            {
                //---
                FileInfo finfo = new FileInfo(PANCer);
                string fileExtension = finfo.Extension.ToLower();
                if (fileExtension == ".pdf")
                {
                    ImgPan.Visible = false;
                    hlPAN.Visible = true;
                    hlPAN.Text = "View PAN Certificate (pdf)";
                    hlPAN.NavigateUrl = "~/EmployeeDocoments/PANCertificate/" + PANCer;

                }

                if (fileExtension != ".pdf")
                {

                    ImgPan.Visible = true;
                    hlPAN.Visible = true;

                    ImgPan.ImageUrl = "~/EmployeeDocoments/PANCertificate/" + PANCer;
                    hlPAN.NavigateUrl = "~/EmployeeDocoments/PANCertificate/" + PANCer;
                }
                //---

            }


            


        }
            //if (dt.Rows[0]["GPS_AMT"].ToString() == "")
            //{
            //    txtGPFAmt.Text = "0";
            //}
            //else
            //{
            //    txtGPFAmt.Text = dt.Rows[0]["GPS_AMT"].ToString();
            //}
            //if (dt.Rows[0]["BAL_GSS"].ToString() == "")
            //{
            //    txtGSSAmt.Text = "0";
            //}
            //else
            //{
            //    txtGSSAmt.Text = dt.Rows[0]["BAL_GSS"].ToString();
            //}
            //if (dt.Rows[0]["BAL_EARN"].ToString() == "")
            //{
            //    txtEarnedLeave.Text = "0";
            //}
            //else
            //{
            //    txtEarnedLeave.Text = dt.Rows[0]["BAL_EARN"].ToString();
            //}
            //if (dt.Rows[0]["BAL_GPF"].ToString() == "")
            //{
            //    txt_gpf.Text = "0";
            //}
            //else
            //{
            //    txt_gpf.Text = dt.Rows[0]["BAL_GPF"].ToString();
            //}           

       
            //if (dt.Rows[0]["CCC_DATE"].ToString() == "01/01/1900")
            //{
            //    txt_cccdate.Text = "";
            //}
            //else
            //{
            //    txt_cccdate.Text = dt.Rows[0]["CCC_DATE"].ToString();
            //}
            //if (dt.Rows[0]["GPF_TYPE"].ToString() != "")
            //{
            //    //string type = dt.Rows[0]["GPF_TYPE"].ToString();
            //    ddlGpfType.SelectedValue = dt.Rows[0]["GPF_TYPE"].ToString().Trim();
            //}
            // txtOWFAcNo.Text = dt.Rows[0]["Owf_AcctNo"].ToString().Trim();
            btnSave.Text = "Update";
        }
        else
        {
            btnSave.Text = "Save";
        }
    }
    private bool Valid()
    {
        if (ddlPayGrade.SelectedIndex == 0)
        {
            Messagebox.Show("Please Select Current Pay Scale");
            return false;
        }
        if (ViewState["Eid"] == null || ViewState["Eid"].ToString() == "")
        {
            Messagebox.Show("Please select employee code.");
            return false;
        }
        //if (txtEmpNxtIncmentDt.Text != "" && txtEmpPrvIncmentDt.Text != "")
        //{
        //    DateTime nextDate = DateTime.ParseExact(txtEmpNxtIncmentDt.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

        //    DateTime previousDate = DateTime.ParseExact(txtEmpPrvIncmentDt.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //    if (previousDate > nextDate)
        //    {
        //        Messagebox.Show("Please Next Increment Date Should be more than Previous Increment Date");
        //        return false;
        //    }
        //}
        if (ddlPayGrade.SelectedIndex == 0)
        {
            Messagebox.Show("Please Select Current Pay Scale");
            return false;
        }

        if (string.IsNullOrEmpty(txt7thPaybasic.Text) || Convert.ToDecimal(txt7thPaybasic.Text) == 0)
        {
            Messagebox.Show("Please Enter Basic Pay");
            return false;
        }

        //try
        //{
        //    if (double.Parse(txtBasicSalary.Text.Trim()) < double.Parse(txtGPFAmt.Text.Trim()))
        //    {
        //        Messagebox.Show("GPF amount should be less than basic salary..");
        //        txtGPFAmt.Text = "0.00";
        //        txtGPFAmt.Focus();
        //        return false;
        //    }
        //}
        //catch
        //{
        //    Messagebox.Show("GPF amount should be less than basic salary..");
        //    txtGPFAmt.Text = "0.00";
        //    return false;
        //}
        //try
        //{
        //    if (string.IsNullOrEmpty(txtBasicSalary.Text) || double.Parse(txtBasicSalary.Text) == 0)
        //    {
        //        throw new Exception("");
        //    }
        //}
        //catch
        //{
        //     Messagebox.Show("Please Select Current Pay Scale");
        //    return false;
        //}

        //try
        //{
        //    if (string.IsNullOrEmpty(txtPayGrade.Text) || double.Parse(txtPayGrade.Text) == 0)
        //    {
        //        throw new Exception("");
        //    }
        //}
        //catch
        //{
        //    Messagebox.Show("Please Enter Grade Pay");
        //    return false;
        //}
        //if (txtIncrementRs.Text.Trim()!="0" && txtIncrementRs.Text.Trim()!="")
        //{
        //    if (txt_cccdate.Text.Trim() == "")
        //    {
        //        Messagebox.Show("Please Enter CCC Passing Date");
        //        return false;
        //    }

        //}

        return true;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        if (Valid() == true)
        {
            if (btnSave.Text == "Save")
            {
                pd.SpType = "1";
            }
            else
            {
                pd.SpType = "2";
            }

            pd.ECODE = lblEmpCode.Text;
            if (txtSpecialPay.Text.Trim() == "")
            {
                pd.SPECIAL_PAY = 0;
            }
            else
            {
                pd.SPECIAL_PAY = double.Parse(txtSpecialPay.Text);
            }
            if (txtBasicSalary.Text.Trim() == "")
            {
                pd.BASIC = 0;
            }
            else
            {
                pd.BASIC = double.Parse(txtBasicSalary.Text);
            }

            if (string.IsNullOrEmpty(txt_cta.Text))
            {
                pd.PERSONAL_PAY = 0;
            }
            else
            {
                pd.PERSONAL_PAY = double.Parse(txt_cta.Text);
            }
            pd.PAY_BAND = ddlPayBand.Text;
            pd.CUR_PAY_GRADE = ddlPayGrade.SelectedValue;
            if (txtPayGrade.Text.Trim() == "")
            {
                pd.GRADE = 0;
            }
            else
            {
                pd.GRADE = double.Parse(txtPayGrade.Text);
            }


            pd.PREV_INCRE_DATE = txtEmpPrvIncmentDt.Text;
            pd.NEXT_INCRE_DATE = txtEmpNxtIncmentDt.Text;
            pd.PAY_MODE = ddlsalaryMode.SelectedValue;
            pd.TransportAllowance = ddlTransportAllowance.SelectedValue;
            pd.HRAFlag = Convert.ToInt32(ddlAccomodation.SelectedValue);
            pd.CGISFlag = Convert.ToInt32(ddlCGISFlage.SelectedValue);
            pd.StaffClupFlag = Convert.ToInt32(ddlStaffClubMember.SelectedValue);
            if (!string.IsNullOrEmpty(txtHRA.Text))
            {
                pd.HRAAmount = double.Parse(txtHRA.Text);
            }
            else
            {
                pd.HRAAmount = 0;
            }

            if (string.IsNullOrEmpty(txtTaxableTA.Text.Trim()))
            {
                pd.TaxableTA = 0;
            }
            else
            {
                pd.TaxableTA = double.Parse(txtTaxableTA.Text.Trim());
            }
            if (string.IsNullOrEmpty(txtNonTaxableTA.Text.Trim()))
            {
                pd.NONTaxableTA = 0;
            }
            else
            {
                //pd.WASHING_ALLOW = ((chkWashing.Checked) ? double.Parse(txtWashing.Text) : 0);
                pd.NONTaxableTA = double.Parse(txtNonTaxableTA.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txt7thPaybasic.Text.Trim()))
            {
                pd.SeventhPaybasic = double.Parse(txt7thPaybasic.Text);
            }
            else
            {
                pd.SeventhPaybasic = 0;
            }
            pd.SeventhPayLevevcode = txt7thPayLeval.Text;

            if (!string.IsNullOrEmpty(txt_gpf.Text))
            {
                pd.GPFAmount = double.Parse(txt_gpf.Text);
            }
            else
            {
                pd.GPFAmount = 0;
            }

            //if (txtRateOfDA.Text.Trim() == "")
            //{
            //    pd.RATE_DA = 0;
            //}
            //else
            //{
            //    pd.RATE_DA = double.Parse(txtRateOfDA.Text);
            //}
            //if (txtDA.Text.Trim() == "")
            //{
            //    pd.DA = 0;
            //}
            //else
            //{
            //    pd.DA = double.Parse(txtDA.Text);
            //}
           

           
           


           
           
          
            
            //if (txtEmpContribution.Text.Trim() == "")
            //{
            //    pd.EMP_CONTRIBUTION = 0;
            //}
            //else
            //{
            //    pd.EMP_CONTRIBUTION = double.Parse(txtEmpContribution.Text);
            //}
            //if (txtHouseRent.Text.Trim() == "")
            //{
            //    pd.HRA = null;
            //}
            //else
            //{
            //    pd.HRA = double.Parse(txtHouseRent.Text);
            //}
            //if (txtVechileAllowance.Text.Trim() == "")
            //{
            //    pd.VechileAllowance = 0;
            //}
            //else
            //{
            //    pd.VechileAllowance = double.Parse(txtVechileAllowance.Text);
            //}
            //if (txtIncrementRs.Text.Trim() == "")
            //{
            //    pd.CCC_RATE = 0;
            //}
            //else
            //{
            //    pd.CCC_RATE = double.Parse(txtIncrementRs.Text);
            //}
            //if (txtCity.Text.Trim() == "")
            //{
            //    pd.CITY_ALLOW = 0;
            //}
            //else
            //{
            //    pd.CITY_ALLOW = ((chkCity.Checked) ? double.Parse(txtCity.Text) : 0);
            //}
            
           
           //if (txtEmergency.Text.Trim() == "")
           // {
           //     pd.EMERGENCY_ALLOW = 0;
           // }
           // else
           // {
           //     pd.EMERGENCY_ALLOW = ((chkEmergency.Checked) ? double.Parse(txtEmergency.Text) : 0);
           // }
           // if (txtNonPractice.Text.Trim() == "")
           // {
           //     pd.NON_PRAC_ALLOW = 0;
           // }
           // else
           // {
           //     pd.NON_PRAC_ALLOW = ((chkNonPractice.Checked) ? double.Parse(txtNonPractice.Text) : 0);
           // }
           // if (txtUniform.Text.Trim() == "")
           // {
           //     pd.UNIFORM_ALLOW = 0;
           // }
           // else
           // {
           //     pd.UNIFORM_ALLOW = ((chkUniform.Checked) ? double.Parse(txtUniform.Text) : 0);
           // }
           // if (txtSpecial.Text.Trim() == "")
           // {
           //     pd.SPECIAL_ALLOW = 0;
           // }
           // else
           // {
           //     pd.SPECIAL_ALLOW = ((chkSpecial.Checked) ? double.Parse(txtSpecial.Text) : 0);
           // }
           // if (txtcca.Text.Trim() == "")
           // {
           //     pd.CCA = 0;
           // }
           // else
           // {
           //     pd.CCA = double.Parse(txtcca.Text);
           // }


            if (ddlbank.SelectedIndex == 0)
            {
                pd.BANK_NAME = "0";
            }
            else
            {
                pd.BANK_NAME = ddlbank.SelectedValue.Trim();
            }
            if (txtBranch9.SelectedIndex == 0)
            {
                pd.BRANCH = "0";
            }
            else
            {
                pd.BRANCH = txtBranch9.SelectedValue.ToString();
            }
            pd.GPF_TYPE = rbgpftype.SelectedValue;
            pd.PFNumber = txtCPFGPF.Text;
            pd.IFSC = txtIFSCCode.Text;
            pd.ACCOUNT_NO = txtAccountNumber.Text;
            pd.MICR = txtmicr.Text;
            pd.BSR = txtbsr.Text;
            pd.PAN = txtPan.Text;
            pd.IsDTA = ddlDTA.SelectedValue;
            UPLOADIMAGES();
            if (Convert.ToString(ViewState["PANCert"]) == "0")
                pd.panCertificate = null;
            else
                pd.panCertificate = Convert.ToString(ViewState["PANCert"]);
            //if (txtGPFAmt.Text.Trim() == "")
            //{
            //    pd.GPS_AMT = 0;
            //}
            //else
            //{
            //    pd.GPS_AMT = double.Parse(txtGPFAmt.Text);
            //}
            //if (txtGSSAmt.Text.Trim() == "")
            //{
            //    pd.GSS_AMT = 0;
            //}
            //else
            //{
            //    pd.GSS_AMT = double.Parse(txtGSSAmt.Text);
            //}
            //if (txtEarnedLeave.Text.Trim() == "")
            //{
            //    pd.EARN_LEAVE = 0;
            //}
            //else
            //{

            //    pd.EARN_LEAVE = double.Parse(txtEarnedLeave.Text);
            //}

            pd.IncrementFlag =Convert.ToInt32(ddl_IncrementFlag.SelectedValue);
            //pd.cccdate = txt_cccdate.Text.Trim();
            //if (txt_cta.Text.Trim() == "")
            //{
            //    pd.CTA = 0;
            //}
            //else
            //{
            //    pd.CTA = double.Parse(txt_cta.Text.Trim());
            //}
            //pd.OWF_ACCT_NO = txtOWFAcNo.Text.Trim();
            if (btnSave.Text == "Save")
                pd.CREATEDBY = Session["USERID"].ToString();
            else if (btnSave.Text == "Update")
                pd.UPDATEDBY = Session["USERID"].ToString();
            //if (txt_gpf.Text.Trim() != "")
            //{
            //    pd.BAL_GPF = double.Parse(txt_gpf.Text);
            //}
            //else
            //{
            //    pd.BAL_GPF = 0;
            //}
            //if (txtGSSAmt.Text.Trim() != "")
            //{
            //    pd.BAL_GSS = double.Parse(txtGSSAmt.Text);
            //}
            //else
            //{
            //    pd.BAL_GSS = 0;
            //}
            //if (txtEarnedLeave.Text.Trim() != "")
            //{
            //    pd.BAL_EARN = double.Parse(txtEarnedLeave.Text);
            //}
            //else
            //{
            //    pd.BAL_EARN = 0;
            //}




            //if (string.IsNullOrEmpty(txtParentOfficeEaring.Text.Trim()))
            //{
            //    pd.ParentOfficeEarning = 0;
            //}
            //else
            //{
            //    pd.ParentOfficeEarning = double.Parse(txtParentOfficeEaring.Text.Trim());
            //}





            //if (ddlTransportAllowance.SelectedValue == "CC")
            //{
            //    pd.StaffCarDeduction = double.Parse(string.IsNullOrEmpty(txtStaffCarDeductionAmount.Text.Trim()) ? "0" : txtStaffCarDeductionAmount.Text.Trim());
            //}

            //if (ddlDeputationPay.SelectedValue == "Y")
            //{
            //    pd.DeputationPay = 'Y';
            //    if (ddlDeputationCity.SelectedValue == "S")
            //    {
            //        pd.DeputationCity = 'S';
            //    }
            //    else
            //    {
            //        pd.DeputationCity = 'D';
            //    }

            //    if (string.IsNullOrEmpty(txtDeputationAllowance.Text))
            //    {
            //        pd.DeputationAllowance = 0;
            //    }
            //    else
            //    {
            //        pd.DeputationAllowance = double.Parse(txtDeputationAllowance.Text.Trim());
            //    }
            //}
            //else
            //{
            //    pd.DeputationPay = 'N';
            //}

            //if (ddlAccomodation.SelectedValue == "N")
            //{


            //}
            //else
            //{
            //    pd.Accomodation = 'Y';
            //}
            //if (ddlCGISFlage.SelectedValue == "Y")
            //{

            //}
            //else
            //{
            //    pd.SmallFamilyAllowance = 'N';
            //}


            //if (ddlStaffClubMember.SelectedValue == "Y")
            //{
            //    pd.KitMaintenanceFlage = 'Y';
            //    pd.KitMaintenanceAllowance = double.Parse(string.IsNullOrEmpty(txtHRA.Text.Trim()) ? "0" : txtHRA.Text.Trim());
            //}
            //else
            //{
            //    pd.KitMaintenanceFlage = 'N';
            //    pd.KitMaintenanceAllowance = 0;
            //}
            pd.TRType = ddlTaxType.SelectedValue;
            bs.Insert(pd);
            Messagebox.Show(pd.msg);
            Display(lblEmpCode.Text);
            BindFundGrid();
            btnSave.Text = "Update";
            reset();
        }
        //Messagebox.Show(lblMsg.Text);
    }
    protected void txtRateOfDA_TextChanged(object sender, EventArgs e)
    {
        //if (txtBasicSalary.Text.Trim() == "" )
        //{
        //    lblMsg.Text = "Please enter basic salary.";
        //    return;
        //}
        //if (txtPayGrade.Text.Trim() == "")
        //{
        //    lblMsg.Text = "Please enter grade pay.";
        //    return;
        //}
        //if (txtRateOfDA.Text.Trim() == "")
        //{
        //    lblMsg.Text = "Please enter Rate of DA.";
        //    return;
        //}
        //double bsal, gpay, daRate,DA;
        //bsal = Convert.ToDouble(txtBasicSalary.Text);
        //gpay = Convert.ToDouble(txtPayGrade.Text);
        //daRate = Convert.ToDouble(txtRateOfDA.Text);
        //DA = ((bsal + gpay) * daRate) / 100;
        //txtDA.Text = DA.ToString();      

    }
    //protected void chkCity_CheckedChanged(object sender, EventArgs e)
    //{

    //    if (chkCity.Checked == true)
    //    {
    //        txtCity.Enabled = true;
    //    }
    //    else
    //    {
    //        txtCity.Enabled = false;
    //        txtCity.Text = "0.00";
    //    }
    //}
    protected void chkWashing_CheckedChanged(object sender, EventArgs e)
    {
        //if (chkWashing.Checked == true)
        //{
        //    txtWashing.Enabled = true;
        //}
        //else
        //{
        //    txtWashing.Enabled = false;
        //    txtWashing.Text = "0.00";
        //}
    }
    //protected void chkEmergency_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkEmergency.Checked == true)
    //    {
    //        txtEmergency.Enabled = true;
    //    }
    //    else
    //    {
    //        txtEmergency.Enabled = false;
    //        txtEmergency.Text = "0.00";

    //    }
    //}
    //protected void chkNonPractice_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkNonPractice.Checked == true)
    //    {
    //        txtNonPractice.Enabled = true;
    //    }
    //    else
    //    {
    //        txtNonPractice.Enabled = false;
    //        txtNonPractice.Text = "0.00";

    //    }
    //}
    //protected void chkUniform_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkUniform.Checked == true)
    //    {
    //        txtUniform.Enabled = true;
    //    }
    //    else
    //    {
    //        txtUniform.Enabled = false;
    //        txtUniform.Text = "0.00";

    //    }
    //}
    //protected void chkSpecial_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkSpecial.Checked == true)
    //    {
    //        txtSpecial.Enabled = true;
    //    }
    //    else
    //    {
    //        txtSpecial.Enabled = false;
    //        txtSpecial.Text = "0.00";

    //    }
    //}
    protected void ddlbank_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string query = "select BRANCH_NAME,Table_id from M_Branch where BankNAME='" + ddlbank.SelectedValue + "'";

            DataTable dt = SQL_DBCS.ExecuteDataTable(query);
            if (dt.Rows.Count > 0)
            {
                txtBranch9.DataSource = dt;
                txtBranch9.DataTextField = "BRANCH_NAME";
                txtBranch9.DataValueField = "Table_id";
                txtBranch9.DataBind();
                txtBranch9.Items.Insert(0, "Select");
            }
            else 
            {
                txtBranch9.Items.Clear();
                //txtBranch9.DataSource = null;
                //txtBranch9.DataBind();
                txtBranch9.Items.Insert(0, "Select");
               
            }
        }
        catch (Exception ex)
        {
        }
    }

  
    //protected void lnkEmpBasic_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/Payroll/Employee Master/M_Employee.aspx?Eid=" + lblEmpCode.Text);
    //}
    //protected void lnkEmpAddDetial_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/Payroll/Employee Master/MEmpAdditionalDetail.aspx?Eid=" + lblEmpCode.Text);
    //}
    //protected void lnlEmp_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/Payroll/Employee Master/M_Employee.aspx?Eid=" + lblEmpCode.Text);
    //}
    //protected void lnkF_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/Payroll/Employee Master/M_Employee_Family.aspx?Eid=" + lblEmpCode.Text);
    //}
    //protected void lnkND_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/Payroll/Employee Master/M_Employee_Nominee.aspx?Eid=" + lblEmpCode.Text);
    //}
    //protected void lnkLPC_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/Payroll/Employee Master/M_Emp_SalaryStructure.aspx?Eid=" + lblEmpCode.Text);
    //}
    //protected void lnlPayYear_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/Payroll/Monthly Salary Preparation/M_Last_Payment_Certificate.aspx?Eid=" + lblEmpCode.Text);
    //}
    protected void ddlGpfType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlGpfType.SelectedValue=="GPF")
        //{
        //    lblpfno.Text = "GPF No.";
        //    txtGPFAmt.Visible = true;
        //    lbl_gpf.Visible = true;
        //}
        //if (ddlGpfType.SelectedValue=="CPS")
        //{
        //    lblpfno.Text = "PRAN(CPS) No.";
        //    txtGPFAmt.Visible = false;
        //    lbl_gpf.Visible = false;
        //}
        //if (ddlGpfType.SelectedValue=="NPS")
        //{
        //    lblpfno.Text = "PRAN(NPS) No.";
        //    txtGPFAmt.Visible = false;
        //    lbl_gpf.Visible = false;
        //}
        //lblgpf_amt.Text = ddlGpfType.SelectedValue.ToString() + "(Rs.)";
    }
    protected void lnlShwAll_Click(object sender, EventArgs e)
    {
       
        if (Session["USERID"].ToString().ToLower() == "admin")
        {
            grdEmp.DataSource = cc.FillEmpDetailsSearchNew("USP_DisplayEmp_SearchNew", null, null, null, null, null, null);

        }
        else
        {
            grdEmp.DataSource = cc.FillEmpDetailsSearchNew("USP_DisplayEmp_SearchNew", null, null, Session["CENTERCODE"].ToString(), null, null, null);
        }
        grdEmp.DataBind();

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindFundGrid();
        txtECode.Text = "";
        txtEname.Text = "";
    }
    protected void lnkRelSelect_Click(object sender, EventArgs e)
    {
        reset();
        LinkButton lnk = (LinkButton)sender;
        GridViewRow grdIndex = (GridViewRow)lnk.Parent.Parent;
        foreach (GridViewRow row in grdEmp.Rows)
        {
            row.BackColor = System.Drawing.Color.Transparent;
        }
        grdIndex.BackColor = System.Drawing.Color.Yellow;
        int index = grdIndex.RowIndex;

        Label lblECode = (Label)grdEmp.Rows[index].FindControl("lblECode");
        Label lblEmployeeCodes = (Label)grdEmp.Rows[index].FindControl("lblEmployeeCodes");
        ViewState["Eid"] = lblECode.Text;

        lblEmployeeCode.Text = lblEmployeeCodes.Text;
        lblEmpCode.Text = lblECode.Text;
        Display1(lblECode.Text);
        Display(lblECode.Text);

        // Response.Redirect("~/Payroll/Employee Master/EmpDash.aspx?Eid=" + lblECode.Text);
    }
    public void BindFundGrid()
    {
        if (Session["USERID"].ToString().ToLower() == "admin")
        {
            //grdEmp.DataSource = cc.FillEmpDetailsSearchNew("USP_DisplayEmp_SearchNew", txtECode.Text, txtEname.Text, Session["CENTERCODE"].ToString(), txtDesignation.Text, null, null);
            grdEmp.DataSource = cc.FillEmpDetailsSearchNew("USP_DisplayEmp_SearchNew", txtECode.Text, txtEname.Text,null, txtDesignation.Text, null, null);

        }
        else
        {
            //grdEmp.DataSource = cc.FillEmpDetailsSearchNew("USP_DisplayEmp_SearchNew", txtECode.Text, txtEname.Text, Session["CENTERCODE"].ToString(), txtDesignation.Text, null, Session["CompanyCode"].ToString());
            //grdEmp.DataSource = cc.FillEmpDetailsSearchNew("USP_DisplayEmp_SearchNew", txtECode.Text, txtEname.Text, Session["CENTERCODE"].ToString(), txtDesignation.Text, null, null);
            grdEmp.DataSource = cc.FillEmpDetailsSearchNew("USP_DisplayEmp_SearchNew", txtECode.Text, txtEname.Text, null, txtDesignation.Text, null, null);

        }

        // Session["DATA"] = (DataTable)cc.FillEmpDetailsSearch("USP_DisplayEmp_Search", txtECode.Text, txtEname.Text, txtCenterCode.Text, txtDesignation.Text, null);
        grdEmp.DataBind();
        change_Color();

    }
    public void change_Color()
    {
        if (Session["RI"] != null)
        {
            grdEmp.DataSource = (DataTable)Session["DATA"];
            grdEmp.DataBind();
            GridViewRow grdIndex = (GridViewRow)Session["RI"];
            int index = grdIndex.RowIndex;
            foreach (GridViewRow row in grdEmp.Rows)
            {
                if (row.RowIndex == grdIndex.RowIndex)
                    row.BackColor = System.Drawing.Color.Yellow;
                else
                    row.BackColor = System.Drawing.Color.Transparent;
            }

            //grdIndex.BackColor = System.Drawing.Color.Yellow;
        }


    }
    protected void btnReset_Click1(object sender, EventArgs e)
    {

        reset();
        Image1.ImageUrl = "~/Payroll/images/empUser.png";
        // Response.Redirect("M_Employee.aspx");
    }
    private void reset()
    {
        //lblEmpCode.Text = "";
        //lblShowCategory.Text = "";
        //lblShowDesignation.Text = "";
        //lblShowDOB.Text = "";
        //lblShowFHName.Text = "";
        //lblShowMaritalStatus.Text = "";
        //lblShowName.Text = "";
        //lblShowSex.Text = "";
        ddlPayGrade.SelectedIndex = 0;
        ddlPayBand.SelectedIndex = 0;
        txtBasicSalary.Text = "0";
        txtPayGrade.Text = "0";
        txtIncrementRs.Text = "0";
        //txtDA.Text = "0";
        txtEmpPrvIncmentDt.Text = "";
        txtEmpNxtIncmentDt.Text = "";
        txt_cta.Text = "0";
        txtSpecialPay.Text = "0";
        ddl_IncrementFlag.SelectedIndex = 0;
        txtStaffCarDeductionAmount.Text = "0";
        txtPFAcNo.Text = "";
        //txtOWFAcNo.Text = "";
       // txtHouseRent.Text = "0";
       // txtcca.Text = "0";
        ddlsalaryMode.SelectedIndex = 0;
       // ddlbank.SelectedIndex = 0;
       // txtBankName.Text = "";
       // txtBankName.Visible = false;
      //  txtBranchCode.Text = "";
        txtIFSCCode.Text = "";
        txtAccountNumber.Text = "";
        //chkGPF.Checked = false;
        //ddlGPFStop.SelectedIndex = 0;
        txt7thPaybasic.Text = "";
        txt7thPayLeval.Text="";
        txtHRA.Text = "";
        txtTaxableTA.Text = "";
        txtNonTaxableTA.Text = "";
        ddlStaffClubMember.SelectedIndex = 0;
        ddlTransportAllowance.SelectedIndex = 0;
        ddlCGISFlage.SelectedIndex = 0;
        ddlAccomodation.SelectedIndex = 0;
        txt_gpf.Text = "0";
        lblgpf_amt.Visible = false;
        txt_gpf.Visible = false;
        ddlPayBand.SelectedIndex = 0;
        txtCPFGPF.Text = "";
        txtPan.Text = "";
        ddlbank.SelectedIndex = 0;
        txtBranch9.Items.Clear();
        txtIFSCCode.Text = "";
        txtAccountNumber.Text = "";
        txtPFAcNo.Text = "";
        txtmicr.Text = "";
        txtbsr.Text ="";
        ImgPan.ImageUrl = "~/App_Themes/BasicTheme/Images/docu.png";
        ddlDTA.SelectedIndex = 0;




        btnSave.Text = "Save";
    }
    protected void ddlPayGrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        //txtPayGrade.Text = ddlPayGrade.SelectedItem.Text.Split('-')[3];
        //txtPayGrade.Enabled = false;

        DataTable dt7PAYLEVEL = new DataTable();
        dt7PAYLEVEL = cc.EQ("select PAY_LEVEL FRom M_SCALE_MAST where Tableid="+ddlPayGrade.SelectedValue+"");

        if (dt7PAYLEVEL.Rows.Count > 0)
        {

            txt7thPayLeval.Text = dt7PAYLEVEL.Rows[0]["PAY_LEVEL"].ToString();
        }


    }
    protected void ddlDeputationPay_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDeputationPay.SelectedValue == "Y")
        {

            lblDeputationCity.Visible = true;
            ddlDeputationCity.Visible = true;
            txtDeputationAllowance.Enabled = true;
           
        }
        else
        {
            lblDeputationCity.Visible = false;
            ddlDeputationCity.Visible = false;
            txtDeputationAllowance.Text = "";
            txtDeputationAllowance.Enabled = false;
        }
    }
    protected void ddlTransportAllowance_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlTransportAllowance.SelectedValue == "CC")
        //{
        //    txtStaffCarDeductionAmount.Visible = true;
        //    lblStaffCarDeduction.Visible = true;

        //}
        //else
        //{

        //    txtStaffCarDeductionAmount.Visible = false;
        //    txtStaffCarDeductionAmount.Text = "";
        //    lblStaffCarDeduction.Visible = false;
        //}

    }
    string PanCerti = "";
    public void UPLOADIMAGES()
    {
        #region PanCard
        string PanCertificate = "";
        ViewState["Path"] = 14;
        if (btnSave.Text == "Save")
        {
            PanCertificate = UploadFile("Insert");
            ViewState["PANCert"] = PanCertificate;
        }
        else
        {
            if (upPAN.HasFile)
            {
                ViewState["PANCert"] = "0";
                PanCertificate = UploadFile("Update");
                ViewState["PANCert"] = PanCertificate;
            }
        }
        if (ViewState["PANCert"] == "" || ViewState["PANCert"] == null)
        {
            ViewState["PANCert"] = "0";
        }
        else
        {
            PanCertificate = ViewState["PANCert"].ToString();
        }

        ImgPan.ImageUrl = "~/EmployeeDocoments/PANCertificate/" + PanCertificate;
        #endregion
    }

 private string UploadFile(string type)
    {
        string file = "";
       
        ///////////////PAN Certificate
        if (Convert.ToInt32(ViewState["Path"]) == 14)
        {
            if (upPAN.HasFile)
            {
                if (upPAN.PostedFile.ContentLength != 0)
                {
                    FileInfo finfo = new FileInfo(upPAN.FileName);
                    string fileExtension = finfo.Extension.ToLower();
                    if (fileExtension == ".gif" || fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".pdf")
                    {
                        if (type == "Update")
                        {
                            if (File.Exists(HttpContext.Current.Server.MapPath("~") + "/EmployeeDocoments/PANCertificate/" + ViewState["PANCert"].ToString()))
                                File.Delete(HttpContext.Current.Server.MapPath("~") + "/EmployeeDocoments/PANCertificate/" + ViewState["PANCert"].ToString());
                        }
                        PanCerti = GetUniqueFileName(upPAN.FileName);
                        upPAN.PostedFile.SaveAs(Server.MapPath("~/EmployeeDocoments/PANCertificate/") + PanCerti);
                    }
                    else
                    {

                        Messagebox.Show("Please Select .jpg,.jpeg,.png,.gif,.pdf type of Pictures");
                    }

                }
                else
                    PanCerti = Convert.ToString(ViewState["PANCert"]);
                file = PanCerti;
            }
        }
      


        return file;

    }
 public string GetUniqueFileName(string filename)
 {
     try
     {
         string[] ext = new string[5];
         ext = filename.Split('.');
         return ext[0] + Guid.NewGuid().ToString() + "." + ext[ext.Length - 1];
     }
     catch (Exception ex)
     {
         return ex.ToString();
     }
 }
    
       
}