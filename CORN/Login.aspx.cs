using System;
using System.Data;
using System.Web.UI;
using CORNBusinessLayer.Classes;


public partial class Login : Page
{
    readonly UserController _mController = new UserController();
    readonly DistributorController _mDist = new DistributorController();   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                if (Convert.ToInt32(Session["UserID"]) > 0)
                {
                    Response.Redirect("Forms/Home.aspx");
                }
            }
            catch (Exception ex)
            {
                txtLogin.Focus();
            }
            txtLogin.Focus();
        }
    }    
    public void ValidateUser()
    {

        if (txtLogin.Text == "" && txtPassword.Text == "")
        {
            return;
        }
        else
        {
            DataTable dt = _mController.SelectSlashUser(txtLogin.Text, txtPassword.Text);
            Session.Clear();
            if (dt.Rows.Count > 0)
            {
                Session.Add("UserID", Convert.ToInt32(dt.Rows[0]["USER_ID"].ToString()));
                Session.Add("UserName", dt.Rows[0]["USER_DETAIL"].ToString());
                Session.Add("DISTRIBUTOR_ID", Convert.ToInt32(dt.Rows[0]["DISTRIBUTOR_ID"]));
                Session.Add("CompanyId", Convert.ToInt32(dt.Rows[0]["COMPANY_ID"].ToString()));
                Session.Add("RoleID", Convert.ToInt32(dt.Rows[0]["ROLE_ID"]));
                Session.Add("MANUAL_DISCOUNT", dt.Rows[0]["MANUAL_DISCOUNT"].ToString());
                LastClosedDay(Convert.ToInt32(dt.Rows[0]["USER_ID"]), Convert.ToInt32(dt.Rows[0]["DISTRIBUTOR_ID"]));
                Session.Add("UserName2", dt.Rows[0]["USER_NAME"].ToString());                
                long userLogId = _mController.InsertUserLoginTime(Convert.ToInt32(dt.Rows[0]["USER_ID"]));
                Session.Add("User_Log_ID", userLogId);
                Response.Redirect(dt.Rows[0]["ROLE_ID"].ToString() == "49"
                    ? "Forms/frmOrderPOS.aspx"
                    : "Forms/Home.aspx");
            }
            else
            {
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = "Wrong User Id/Password";
                return;
            }
        }
    }
    private void LastClosedDay(int userId, int pDistributor)
    {
        DataTable dt = _mDist.SelectMaxDayClose(userId, pDistributor);
        Session.Add("CurrentWorkDate", dt.Rows.Count > 0 ? DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString()) : DateTime.Parse(DateTime.Now.ToShortDateString()));
    }
    protected void btnSignIn_Click(object sender, EventArgs e)
    {
        ValidateUser();
    }
}