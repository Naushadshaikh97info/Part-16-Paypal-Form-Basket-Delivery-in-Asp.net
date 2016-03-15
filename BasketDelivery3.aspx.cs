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
using System.Net.Mail;
using System.Text;
using System.IO;


public partial class BasketDelivery : System.Web.UI.Page
{
    cosmicDataContext linq_obj = new cosmicDataContext();
    static DataTable dt;
    static int count = 0;
    static int totalvalue;
    decimal totalPrice = 0M;
    decimal totalStock = 0M;
    int totalItems = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["Login"] != null)
        {
            gridfill();
         
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>alert('Please continue shopping before login  !');window.location='user_login.aspx';</script>'");
        }
    }
    private void gridfill()
    {
        try
        {
            createtable();
            count = 0;
            if (dt.Rows.Count != 0)
            {
                grd_shoping.Visible = true;
                grd_shoping.DataSource = dt;
                grd_shoping.DataBind();
                int total_quantity = 0;
                decimal total_amt = 0;
                for (int i = 0; i < grd_shoping.Rows.Count; i++)
                {
                    TextBox drp = (TextBox)grd_shoping.Rows[i].FindControl("drpquantity");
                    drp.Text = dt.Rows[i]["drpquantity"].ToString();
                    //total_quantity += Convert.ToInt32(drp.Text.ToString());
                  //  total_amt = total_amt + Convert.ToDecimal(grd_shoping.Rows[i].Cells[3].Text.ToString());
                    // lbltotal_qty.Text = total_quantity.ToString();

                    lbltotal_amt.Text = totalPrice.ToString();
                    totalvalue = Convert.ToInt32(totalPrice);
                }
                Session["total_qty"] = total_quantity;
            }
            else
            {
                grd_shoping.Visible = false;
            }

        }
        catch (Exception ex)
        {
        }
    }
    private void createtable()
    {
        try
        {
            if (dt == null)
            {
                dt = new DataTable();
                dt.Columns.Add("productimg");
                dt.Columns.Add("productname");
                dt.Columns.Add("actualprice");
                dt.Columns.Add("discountprice");
                dt.Columns.Add("drpquantity");
                dt.Columns.Add("totalprice");
                dt.Columns.Add("productcode");

                dt = (DataTable)Session["addcart"];
            }
            else
            {
                dt = (DataTable)Session["addcart"];
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void grd_shoping_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            //if (DialogResult.No == MessageBox.Show("Are you sure You Want to delete this product?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
            //    return;
            if (dt.Rows.Count != 0)
            {
                dt.Rows.RemoveAt(e.RowIndex);
                Session["addcart"] = dt;
                if (dt.Rows.Count == 0)
                {
                    panMain.Visible = false;
                    Session["total_qty"] = 0;
                    Response.Redirect("Default.aspx", false);
                }
                else
                {
                    gridfill();
                }
            }
            if (dt.Rows.Count == 0)
            {
                createtable();
                grd_shoping.Visible = false;
            }
            else
            {
                gridfill();
                grd_shoping.Visible = true;
            }


            linq_obj.delete_shoping(Convert.ToInt32(grd_shoping.DataKeys[e.RowIndex].Value.ToString()));
            linq_obj.SubmitChanges();

        }
        catch (Exception ex)
        {

        }

    }
    protected void drpquantity_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            createtable();
            for (int i = 0; i < grd_shoping.Rows.Count; i++)
            {
                TextBox drp = (TextBox)grd_shoping.Rows[i].FindControl("drpquantity");
                int drp_t = Convert.ToInt32(drp.Text.ToString());
                decimal actual_price = Convert.ToDecimal(grd_shoping.Rows[i].Cells[2].Text.ToString());
                decimal total = Convert.ToDecimal(actual_price) * Convert.ToInt32(drp_t);
                total = Math.Round(total, 2);
                dt.Rows[i][5] = total.ToString();
                dt.Rows[i][4] = drp.Text.ToString();
            }
            count = 0;
            gridfill();

        }
        catch (Exception ex)
        {
        }
    }
    protected void grd_shoping_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                TextBox drp = (TextBox)e.Row.FindControl("drpquantity");
                drp.Text = dt.Rows[count][4].ToString();
                count++;

                Label lblPrice = (Label)e.Row.FindControl("lbl_total_price");
                

                decimal price = Decimal.Parse(lblPrice.Text);
                

                totalPrice += price;
                

                totalItems += 1; 

                // e.Row.Cells[6].Attributes.Add("onclientclick", "Are You Sure You Want to Delete This Record");
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPrice = (Label)e.Row.FindControl("lblTotalPrice");
               

                lblTotalPrice.Text = totalPrice.ToString();
               

               
            } 

        }
        catch (Exception ex)
        {
        }
    }
    protected void btncontinue_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("default.aspx", false);
        }
        catch (Exception ex)
        {
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            grd_shoping.Visible = false;
            Session["addcart"] = null;
            Session["total_qty"] = null;
            Session["Login"] = "no";
            Response.Redirect("user_login.aspx", false);
        }
        catch (Exception ex)
        {
        }
    }
    protected void btncheckout_Click(object sender, EventArgs e)
    
   {
       //try
       //{
       //    #region namipaypal link for demo


       //    string amount = "5";


       //    RemotePost myremotepost = new RemotePost();
       //    myremotepost.Url = "https://www.paypal.com/cgi-bin/webscr";
       //    myremotepost.Add("cmd", "_xclick");
       //    myremotepost.Add("hosted_button_id", "FSA3T6C75CA8E");
       //    myremotepost.Add("business", "shiv.pty.ltd@gmail.com");
       //    myremotepost.Add("item_name", "Payment Details:");
       //    myremotepost.Add("currency_code", "USD");
       //    myremotepost.Add("amount", amount);
       //    myremotepost.Add("return", "http://reliefsalon.com.au/thanku.html");
       //    myremotepost.Add("cancel_return", "http://reliefsalon.com.au/new_book.aspx");
       //    myremotepost.Post();


       //    #endregion
       //}
       //catch (Exception ex)
       //{

       //    Response.Write(ex.Message);

       //}
       //finally
       //{
       //}



       try
       {


           panregisterform.Visible = true;
        
           

              
               //btncheckout.Visible = false;
               //btncontinue.Visible = false;
               //btncancel.Visible = false;
               //btncontinue_pay.Visible = true;
              

       }
       catch (Exception ex)
       {
       }
    }
   
    protected void btnpay_Click(object sender, EventArgs e)
    {

        try
        {
            ccjoin.ValidateCaptcha(TextBox10.Text);
            if (!ccjoin.UserValidated)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "onload", "alert('Please insert correct combination of words In Captcha ')", true);
                TextBox10.Text = "";
            }
            else
            {

                linq_obj.insert_billing_info(TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, TextBox6.Text, TextBox7.Text, TextBox9.Text, Convert.ToDateTime(System.DateTime.Now), Convert.ToInt32(Session["userid"].ToString()));
                linq_obj.SubmitChanges();

                int total_quantity = 0;
                decimal total_amt = 0;
                for (int i = 0; i < grd_shoping.Rows.Count; i++)
                {
                    try
                    {
                        TextBox drp = (TextBox)grd_shoping.Rows[i].FindControl("drpquantity");
                      //  total_quantity += Convert.ToInt32(drp.Text.ToString());
                      //  total_amt = total_amt + Convert.ToDecimal(grd_shoping.Rows[i].Cells[3].Text.ToString());
                        total_quantity = totalItems;
                        total_amt = totalPrice;
                        ViewState["total_amt"] = totalPrice;
                    }
                    catch (Exception ex)
                    {
                    }
                }
                Random rnd = new Random();
                int startNumber = rnd.Next(1, 9000);

                string rndno= "jwc" + startNumber.ToString();

                panregisterform.Visible = false;

                var id = (from a in linq_obj.registration_msts
                          where a.intglcode==Convert.ToInt32( Session["userid"].ToString())
                          select a).ToList();
                int y = linq_obj.Insert_order_detail(System.DateTime.Now.ToShortDateString(), System.DateTime.Now.Month.ToString(), System.DateTime.Now.Year.ToString(), Session["user_type"].ToString(), id[0].email, "NOTPAY", rndno, System.DateTime.Now.ToShortDateString(), Convert.ToDateTime(System.DateTime.Now.ToShortDateString()), total_quantity.ToString(), total_amt.ToString(), "Pending", Convert.ToInt32(Session["userid"].ToString()));
                linq_obj.SubmitChanges();

                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    linq_obj.Insert_shoping(Convert.ToInt32(dt.Rows[k][5].ToString()), dt.Rows[k][3].ToString(), dt.Rows[k][4].ToString(), Convert.ToInt32(y));
                    linq_obj.SubmitChanges();
                }
                gridfill();
                send_grid();
                string price = ViewState["total_amt"].ToString();

                RemotePost myremotepost = new RemotePost();
                myremotepost.Url = "https://www.paypal.com/cgi-bin/webscr";
                myremotepost.Add("cmd", "_xclick");
                myremotepost.Add("hosted_button_id", "FSA3T6C75CA8E");
                myremotepost.Add("business", "sarkisaga@aol.com");
                myremotepost.Add("item_name", "Payment Details:");
                myremotepost.Add("currency_code", "USD");
                myremotepost.Add("amount", price);
                myremotepost.Add("return", "http://llc.houseofstationery.in");
                //  myremotepost.Add("cancel_return", "http://llc.houseofstationery.in/");
                myremotepost.Post();

              
                txtname.Text = "";
                txtaddress.Text = "";
                txtcountry.Text = "";
                txtstate.Text = "";
                txtcity.Text = "";
                txtpincode.Text = "";
                txtemail.Text = "";
                txtmobile.Text = "";
                TextBox1.Text = "";
                TextBox2.Text = "";
                TextBox3.Text = "";
                TextBox4.Text = "";
                TextBox5.Text = "";
                TextBox6.Text = "";
                TextBox7.Text = "";
                TextBox9.Text = "";
                TextBox10.Text = "";
                panloginform.Visible = false;
                pancontinue.Visible = true;
               // ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>alert('Your Shpping Sucessfully Your Product will be delivered 6 to 8 day  !');window.location='user_login.aspx';</script>'");
            }


        }
        catch (Exception ex)
        {
        }
      
        
    }
   
    protected void btn_from_1_next_click(object sender, EventArgs e)
    {
        try
        {
            linq_obj.insert_shipping_info(txtname.Text, txtaddress.Text, txtcountry.Text, txtstate.Text, txtcity.Text, txtpincode.Text, txtemail.Text, txtmobile.Text, Convert.ToDateTime(System.DateTime.Now), Convert.ToInt32(Session["userid"].ToString()));
            linq_obj.SubmitChanges();

            panregisterform.Visible = false;
            panloginform.Visible = true;
        }
        catch (Exception ex)
        {
        }
    }
    private void clear()
    {
        txtaddress.Text = "";
        txtcity.Text = "";
        txtcountry.Text = "";
        txtemail.Text = "";
        txtmobile.Text = "";
        txtname.Text = "";
        txtpincode.Text = "";
        txtstate.Text = "";
       // txtcapcha.Text = "";
    }

    public string base64Decode(string sData)
    {

        System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();

        System.Text.Decoder utf8Decode = encoder.GetDecoder();

        byte[] todecode_byte = Convert.FromBase64String(sData);

        int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);

        char[] decoded_char = new char[charCount];

        utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);

        string result = new String(decoded_char);

        return result;

    }
    private string base64Encode(string sData)
    {
        try
        {
            byte[] encData_byte = new byte[sData.Length];

            encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);

            string encodedData = Convert.ToBase64String(encData_byte);

            return encodedData;

        }
        catch (Exception ex)
        {
            throw new Exception("Error in base64Encode" + ex.Message);
        }
    }

    protected void btncontinue_pay_Click(object sender, EventArgs e)
    {

       
          
            string price = ViewState["total_amt"].ToString();

           
            RemotePost myremotepost = new RemotePost();
            myremotepost.Url = "https://www.paypal.com/cgi-bin/webscr";
            myremotepost.Add("cmd", "_xclick");
            myremotepost.Add("hosted_button_id", "FSA3T6C75CA8E");
            myremotepost.Add("business", "sarkisaga@aol.com");
            myremotepost.Add("item_name", "Payment Details:");
            myremotepost.Add("currency_code", "USD");
            myremotepost.Add("amount", price);
            myremotepost.Add("return", "http://llc.houseofstationery.in");
            myremotepost.Add("cancel_return", "http://llc.houseofstationery.in/");
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
    private void send_grid()
    {

        SmtpClient smtpclient;
        MailMessage message;

        smtpclient = new SmtpClient();
        message = new MailMessage();
        try
        {

            var id = (from a in linq_obj.registration_msts
                      where a.intglcode == Convert.ToInt32(Session["userid"].ToString())
                      select a).ToList();
            message.From = new MailAddress("jewelrywholesaleclub@gmail.com");
            message.To.Add(id[0].email);  //send email to yahoo
            message.Subject = "Order Invoice";
            message.IsBodyHtml = true;
            message.Body += "Your Order Traking No :  <br/><br/>";
            message.Body += GetGridviewData(grd_shoping);
            smtpclient.Host = "smtp.gmail.com";
            smtpclient.EnableSsl = true;
            smtpclient.UseDefaultCredentials = true;
            System.Net.NetworkCredential network = new System.Net.NetworkCredential();
            network.UserName = "jewelrywholesaleclub@gmail.com"; // moksha mail
            network.Password = "jewelry123"; //password
            smtpclient.UseDefaultCredentials = true;
            smtpclient.Credentials = network;
            smtpclient.Port = 25;
            smtpclient.Send(message);
            //MailMessage Msg = new MailMessage();
            //MailAddress fromMail = new MailAddress("jewelrywholesaleclub@gmail.com");
            //// Sender e-mail address.
            //Msg.From = fromMail;
            //// Recipient e-mail address.
            //Msg.To.Add(new MailAddress("motisariya12@gmail.com"));
            //// Subject of e-mail
            //Msg.Subject = "Send Gridivew in EMail";
            //Msg.Body += "Please check below data <br/><br/>";
            //Msg.Body += GetGridviewData(grd_shoping);
            //Msg.IsBodyHtml = true;
            //string sSmtpServer = "";
            //sSmtpServer = "10.2.160.101";
            //SmtpClient a = new SmtpClient();
            //a.Host = sSmtpServer;
            //a.EnableSsl = true;
            //a.Send(Msg);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    // This Method is used to render gridview control
    public string GetGridviewData(GridView gv)
    {
        StringBuilder strBuilder = new StringBuilder();
        StringWriter strWriter = new StringWriter(strBuilder);
        HtmlTextWriter htw = new HtmlTextWriter(strWriter);
        gv.RenderControl(htw);
        return strBuilder.ToString();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}

  


