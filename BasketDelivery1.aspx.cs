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
using System.Windows.Forms;

public partial class BasketDelivery : System.Web.UI.Page
{
    onlineshopingDataContext linq_obj = new onlineshopingDataContext();
    static DataTable dt;
    static int count = 0;
    static int totalvalue;
    protected void Page_Load(object sender, EventArgs e)
    {

        //if (Session["Login"] != null)
        //{
        //    Session["Login"] = "yes";
        //}
        //else
        //{
        //    Session["Login"] = "no";
        //}
        if (IsPostBack)
            return;
        // createtable();
        gridfill();


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
                    DropDownList drp = (DropDownList)grd_shoping.Rows[i].FindControl("drpquantity");
                    total_quantity += Convert.ToInt32(drp.SelectedValue.ToString());
                    total_amt = total_amt + Convert.ToDecimal(grd_shoping.Rows[i].Cells[5].Text.ToString());
                    // lbltotal_qty.Text = total_quantity.ToString();

                    lbltotal_amt.Text = total_amt.ToString();
                    totalvalue = Convert.ToInt32(total_amt);
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

            if (DialogResult.No == MessageBox.Show("Are you sure You Want to delete this product?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
                return;
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
    protected void drpquantity_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < grd_shoping.Rows.Count; i++)
            {
                DropDownList drp = (DropDownList)grd_shoping.Rows[i].FindControl("drpquantity");
                int drp_t = Convert.ToInt32(drp.SelectedValue.ToString());
                decimal actual_price = Convert.ToDecimal(grd_shoping.Rows[i].Cells[3].Text.ToString());
                decimal total = Convert.ToDecimal(actual_price) * Convert.ToInt32(drp_t);
                total = Math.Round(total, 2);
                dt.Rows[i][5] = total.ToString();
                dt.Rows[i][4] = drp.SelectedValue.ToString();
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
                DropDownList drp = (DropDownList)e.Row.FindControl("drpquantity");
                drp.SelectedValue = dt.Rows[count][4].ToString();
                count++;

                // e.Row.Cells[6].Attributes.Add("onclientclick", "Are You Sure You Want to Delete This Record");
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
            Response.Redirect("product_catlog.aspx", false);
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
            Response.Redirect("login.aspx", false);
        }
        catch (Exception ex)
        {
        }
    }
    protected void btncheckout_Click(object sender, EventArgs e)
    {
        try
        {
            //var id = (from a in linq_obj.final_amounts
            //          where a.fk_mem_id == Convert.ToInt32(Session["userid"].ToString())
            //          select a).Single();
            //if (totalvalue < Convert.ToInt32(id.total_amt))
            //{

            //panlogin.Visible = true;
            if (Session["username"] != null)
            {

                pancontinue.Visible = true;
                Panform.Visible = true;
                panregisterform.Visible = true;

            }
            else
            {

                Panform.Visible = true;
                panregisterform.Visible = true;
                // panloginform.Visible = true;
            }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "onload", "alert('Not sufficiant balance ')", true);
            //}

        }
        catch (Exception ex)
        {
        }
    }
    protected void btnpay_Click(object sender, EventArgs e)
    {
        try
        {
            //ccjoin.ValidateCaptcha(txtcapcha.Text);
            //if (!ccjoin.UserValidated)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "onload", "alert('Please insert correct combination of words In Captcha ')", true);
            //    //Response.Write("<script>alert('Please insert correct combination of words.');</script>");
            //}
            //else
            //{
            linq_obj.insert_consumer_mst(txtname.Text, txtaddress.Text, txtcountry.Text, txtstate.Text, txtcity.Text, txtpincode.Text, txtmobile.Text, txtemail.Text, "", Convert.ToDateTime(System.DateTime.Now.ToShortDateString()));
            linq_obj.SubmitChanges();
            panregisterform.Visible = false;
            pancontinue.Visible = true;
            // panloginform.Visible = true;
            // }

            //clear();
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
        try
        {
            //ccjoin.ValidateCaptcha(txtcapcha.Text);
            //if (!ccjoin.UserValidated)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "onload", "alert('Please insert correct combination of words In Captcha ')", true);
            //    //Response.Write("<script>alert('Please insert correct combination of words.');</script>");
            //}
            //else
            //{
            int total_quantity = 0;
            decimal total_amt = 0;
            var id7 = (from a in linq_obj.consumer_msts
                       orderby a.intglcode descending
                       select a).ToList();

            if (rdo_payment.SelectedItem.Text == "Online Payment")
            {
                for (int i = 0; i < grd_shoping.Rows.Count; i++)
                {
                    DropDownList drp = (DropDownList)grd_shoping.Rows[i].FindControl("drpquantity");
                    total_quantity += Convert.ToInt32(drp.SelectedValue.ToString());
                    total_amt = total_amt + Convert.ToDecimal(grd_shoping.Rows[i].Cells[5].Text.ToString());
                    ViewState["total_amt"] = total_amt;
                }

                panregisterform.Visible = true;
                int y = linq_obj.Insert_order_detail(txtname.Text, txtaddress.Text, txtcountry.Text, txtstate.Text, txtcity.Text, txtpincode.Text, txtmobile.Text, txtemail.Text, Convert.ToDateTime(System.DateTime.Now.ToShortDateString()), total_quantity.ToString(), total_amt.ToString(), "Pending", Convert.ToInt32(Session["userid"].ToString()),Convert.ToInt32(id7[0].intglcode));
                linq_obj.SubmitChanges();

                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    linq_obj.Insert_shoping(Convert.ToInt32(dt.Rows[k][6].ToString()), dt.Rows[k][4].ToString(), dt.Rows[k][5].ToString(), Convert.ToInt32(y));
                    linq_obj.SubmitChanges();
                }

                panMain.Visible = false;
                string price = ViewState["total_amt"].ToString();

                //RemotePost myremotepost = new RemotePost();
                //myremotepost.Url = "https://www.paypal.com/cgi-bin/webscr";
                //myremotepost.Add("cmd", "_xclick");
                //myremotepost.Add("hosted_button_id", "FSA3T6C75CA8E");
                //myremotepost.Add("business", "webcodetechno@gmail.com");
                //myremotepost.Add("item_name", "Payment Details:");
                //myremotepost.Add("currency_code", "USD");
                //myremotepost.Add("amount", price);
                //myremotepost.Add("return", "http://demo.rubberstampmake.com/thankyou.aspx");
                //myremotepost.Add("cancel_return", "http://demo.rubberstampmake.com/");
                //myremotepost.Post();

                RemotePost myremotepost = new RemotePost();
                myremotepost.Url = "https://www.paypal.com/cgi-bin/webscr";
                myremotepost.Add("cmd", "_xclick");
                myremotepost.Add("hosted_button_id", "FSA3T6C75CA8E");
                myremotepost.Add("business", "webcodetechno@gmail.com");
                myremotepost.Add("item_name", "Payment Details:");
                myremotepost.Add("currency_code", "USD");
                myremotepost.Add("amount", price);
                myremotepost.Add("return", "http://demo.rubberstampmake.com/thankyou.aspx");
                //  myremotepost.Add("cancel_return", "http://llc.houseofstationery.in/");
                myremotepost.Post();


                panMain.Visible = false;

                Session["total_qty"] = 0;
                Response.Redirect("thankyou.aspx", false);
            }
            else if (rdo_payment.SelectedItem.Text == "Offline Payment")
            {




                for (int i = 0; i < grd_shoping.Rows.Count; i++)
                {
                    DropDownList drp = (DropDownList)grd_shoping.Rows[i].FindControl("drpquantity");
                    total_quantity += Convert.ToInt32(drp.SelectedValue.ToString());
                    total_amt = total_amt + Convert.ToDecimal(grd_shoping.Rows[i].Cells[5].Text.ToString());
                    ViewState["total_amt"] = total_amt;
                }

                panregisterform.Visible = true;
                int y = linq_obj.Insert_order_detail(txtname.Text, txtaddress.Text, txtcountry.Text, txtstate.Text, txtcity.Text, txtpincode.Text, txtmobile.Text, txtemail.Text, Convert.ToDateTime(System.DateTime.Now.ToShortDateString()), total_quantity.ToString(), total_amt.ToString(), "Pending", Convert.ToInt32(Session["userid"].ToString()),Convert.ToInt32(id7[0].intglcode));
                linq_obj.SubmitChanges();

                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    linq_obj.Insert_shoping(Convert.ToInt32(dt.Rows[k][6].ToString()), dt.Rows[k][4].ToString(), dt.Rows[k][5].ToString(), Convert.ToInt32(y));
                    linq_obj.SubmitChanges();
                }




                
                
                Session["total_qty"] = 0;
                Response.Redirect("thankyou.aspx", false);

            }
            //}
        }

        catch (Exception ex)
        {
        }
        finally
        {
        }
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
    //private void fillmember()
    //{
    //    try
    //    {
    //        Panform.Visible = true;
    //        var id_member = (from a in linq_obj.registration_finals
    //                         join b in linq_obj.state_msts on (a.fk_state) equals (b.int_glcode)
    //                         join c in linq_obj.country_msts on (a.fk_country) equals (c.int_glcode)
    //                         join d in linq_obj.city_msts on (a.fk_city) equals (d.int_glcode)
    //                         where a.id_user == Session["username"].ToString()
    //                         select new
    //                         {
    //                             Code = a.intglcode,
    //                             name = a.f_name,
    //                             address = a.address,
    //                             country = c.country_name,
    //                             state = b.state_name,
    //                             city = d.city_name,
    //                             pincode = a.pincode,
    //                             email = a.email,
    //                             mobile = a.mobile
    //                         }).Single();
    //        txtaddress.Text = id_member.address;
    //        txtcity.Text = id_member.city;
    //        txtcountry.Text = id_member.country;
    //        txtemail.Text = id_member.email;
    //        txtmobile.Text = id_member.mobile;
    //        txtpincode.Text = id_member.pincode;
    //        txtstate.Text = id_member.state;
    //        txtname.Text = id_member.name;
    //        Label3.Visible = true;
    //        drptype.Visible = true;
    //        txtpassword1.Visible = false;
    //        Label1.Visible = false;
    //        btnpay.Visible = false;
    //        pancontinue.Visible = true;
    //        Session["Login"] = "yes";
    //        btncontinue.Visible = true;
    //        if (Session["Login"] != null)
    //        {
    //            Session["Login"] = "yes";
    //        }
    //        else
    //        {
    //            Session["Login"] = "no";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex.Message);

    //    }
    //    finally
    //    {
    //    }
    //}

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            //var id = (from a in linq_obj.registration_finals
            //          select new
            //          {
            //              user_name = a.id_user,
            //              password = a.login_password,
            //              int_glcode = a.intglcode,
            //              epin = a.epin
            //          }).ToList();
            //int flag = 0;

            //for (int i = 0; i < id.Count(); i++)
            //{
            //    if (id[i].user_name == txtadmin.Text && txtpassword.Text == base64Decode(id[i].password))
            //    {
            //        flag = 1;
            //        var package = (from a in linq_obj.pin_allocations
            //                       join b in linq_obj.Package_Masters on a.fk_package_id equals b.intGlcode
            //                       where a.pin_generated == id[i].epin
            //                       select b).Single();
            //        Session["Packagename"] = package.package_category;
            //        Session["username"] = txtadmin.Text;
            //        Session["MemberID"] = txtadmin.Text;
            //        Session["referense"] = id[i].int_glcode;
            //        Session["id"] = id[i].int_glcode;
            //        Session["account"] = "Member Account";
            //        Session["Login"] = "yes";
            //        break;
            //    }
            //}
            //if (flag == 1)
            //{
            //    Panform.Visible = true;
            //    panloginform.Visible = false;
            //    panregisterform.Visible = true;
            //  //  fillmember();
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "onload", "alert('**Incorrect UserName or Password**')", true);
            //}
            //txtadmin.Text = "";
            //txtpassword.Text = "";
        }
        catch (Exception ex)
        {

            Response.Write("<script laguage='javascript'>alert('** Some Error is occured During Login**')</Script>");

        }
        finally
        {
        }
    }



}
