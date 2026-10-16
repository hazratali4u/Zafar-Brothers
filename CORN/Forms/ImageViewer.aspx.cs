using System;
using CORNCommon.Classes;

public partial class Forms_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            var ImgPath = this.Session["ImgPath"].ToString();
            imgProduct.ImageUrl = ImgPath;
        }
        catch (Exception exp)
        {
            ExceptionPublisher.PublishException(exp);

        }
        finally
        {

        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
           
    }

}

