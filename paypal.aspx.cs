using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class paypal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["total"] != null)
        {
            old();
        }
    }
    private void old()
    {
        RemotePost myremotepost = new RemotePost();
        myremotepost.Url = "https://www.paypal.com/cgi-bin/webscr";
        myremotepost.Add("cmd", "_xclick");
        myremotepost.Add("hosted_button_id", "FSA3T6C75CA8E");
        myremotepost.Add("business", "lbhowmick@bosdistribution.com");
        myremotepost.Add("item_name", "Payment Details:");
        myremotepost.Add("currency_code", "USD");
        myremotepost.Add("amount", Request.QueryString["total"].ToString());
       // myremotepost.Add("return", "http://ecommerce.demo4client.in/thankyou.aspx");
       // myremotepost.Add("cancel_return", "http://ecommerce.demo4client.in/cancel.aspx");
        myremotepost.Post();

    }
    public class RemotePost
    {
        private System.Collections.Specialized.NameValueCollection Inputs = new System.Collections.Specialized.NameValueCollection();


        public string Url = "https://www.paypal.com/cgi-bin/webscr";
        public string Method = "post";
        public string FormName = "form";

        public void Add(string name, string value)
        {
            Inputs.Add(name, value);
        }

        public void Post()
        {
            System.Web.HttpContext.Current.Response.Clear();

            System.Web.HttpContext.Current.Response.Write("<html><head>");

            System.Web.HttpContext.Current.Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
            System.Web.HttpContext.Current.Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, Url));
            for (int i = 0; i < Inputs.Keys.Count; i++)
            {
                System.Web.HttpContext.Current.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", Inputs.Keys[i], Inputs[Inputs.Keys[i]]));
            }
            System.Web.HttpContext.Current.Response.Write("</form>");
            System.Web.HttpContext.Current.Response.Write("</body></html>");
            System.Web.HttpContext.Current.Response.End();
        }
    }
}