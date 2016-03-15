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
using System.Windows.Forms;

public partial class BasketDelivery : System.Web.UI.Page
{
    MLMDataContext linq_obj = new MLMDataContext();
    static DataTable dt;
    static int count = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
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
                    total_amt = total_amt + Convert.ToDecimal(grd_shoping.Rows[i].Cells[4].Text.ToString());

                    lbltotal_amt.Text = total_amt.ToString();
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
            Response.Write(ex.Message);
            ErrHandler.WriteError(ex.Message);
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
            Response.Write(ex.Message);
            ErrHandler.WriteError(ex.Message);
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
            Response.Write(ex.Message);
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void drpquantity_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < grd_shoping.Rows.Count; i++)
            {
                DropDownList drp = (DropDownList)grd_shoping.Rows[i].FindControl("drpquantity");
                int drp_t = Convert.ToInt32(drp.Text.ToString());
                decimal actual_price = Convert.ToDecimal(grd_shoping.Rows[i].Cells[2].Text.ToString());
                decimal total = Convert.ToDecimal(actual_price) * Convert.ToInt32(drp_t);
                total = Math.Round(total, 2);
                dt.Rows[i][4] = total.ToString();
                dt.Rows[i][3] = drp.SelectedValue.ToString();
            }
            count = 0;
            gridfill();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void grd_shoping_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList drp = (DropDownList)e.Row.FindControl("drpquantity");
                drp.SelectedValue = dt.Rows[count][3].ToString();
                count++;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
        }
    }

    protected void btncontinue_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Default.aspx", false);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
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
            Response.Redirect("Default.aspx", false);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            ErrHandler.WriteError(ex.Message);

        }
    }

    protected void btncheckout_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["username"] != null)
            {
                Session["account"] = "MLM Account";
                if (Session["account"].ToString() == "MLM Account")
                {
                    pancontinue.Visible = true;
                    Panform.Visible = true;
                    panregisterform.Visible = true;
                    panloginform.Visible = false;
                    fillmember();
                }
                else
                {

                    pancontinue.Visible = true;
                    Panform.Visible = true;
                    panregisterform.Visible = true;
                    panloginform.Visible = false;
                    fillmember();
                }
            }
            else
            {
                Panform.Visible = true;
                panregisterform.Visible = true;
                panloginform.Visible = true;
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btnpay_Click(object sender, EventArgs e)
    {
        try
        {
            //ccjoin.ValidateCaptcha(txtcapcha.Text);
            //if (!ccjoin.UserValidated)
                if (txtcapcha.Text != "14")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "onload", "alert('Please insert correct combination of words In Captcha ')", true);
            }
            else
            {
                linq_obj.insert_consumer_mst(txtname.Text, txtaddress.Text, txtcountry.Text, txtstate.Text, txtcity.Text, txtpincode.Text, txtmobile.Text, txtemail.Text, base64Encode(txtpassword1.Text), Convert.ToDateTime(System.DateTime.Now.ToShortDateString()));
                linq_obj.SubmitChanges();
                panregisterform.Visible = false;
                panloginform.Visible = true;
            }

            clear();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            ErrHandler.WriteError(ex.Message);
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
        txtcapcha.Text = "";
    }

    private void fillmember()
    {
        try
        {
            Panform.Visible = true;
            var id_member = (from a in linq_obj.registration_finals
                             join b in linq_obj.state_msts on (a.fk_state) equals (b.int_glcode)
                             join c in linq_obj.country_msts on (a.fk_country) equals (c.int_glcode)
                             join d in linq_obj.city_msts on (a.fk_city) equals (d.int_glcode)
                             where a.id_user == Session["username"].ToString()
                             select new
                             {
                                 Code = a.intglcode,
                                 name = a.f_name,
                                 address = a.address,
                                 country = c.country_name,
                                 state = b.state_name,
                                 city = d.city_name,
                                 pincode = a.pincode,
                                 email = a.email,
                                 mobile = a.mobile
                             }).Single();
            txtaddress.Text = id_member.address;
            txtcity.Text = id_member.city;
            txtcountry.Text = id_member.country;
            txtemail.Text = id_member.email;
            txtmobile.Text = id_member.mobile;
            txtpincode.Text = id_member.pincode;
            txtstate.Text = id_member.state;
            txtname.Text = id_member.name;
            Label3.Visible = true;
            drptype.Visible = true;
            txtpassword1.Visible = false;
            Label1.Visible = false;
            btnpay.Visible = false;
            pancontinue.Visible = true;
            Session["Login"] = "yes";
            btncontinue.Visible = true;
            if (Session["Login"] != null)
            {
                Session["Login"] = "yes";
            }
            else
            {
                Session["Login"] = "no";
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            var id = (from a in linq_obj.registration_finals
                      select new
                      {
                          user_name = a.id_user,
                          password = a.login_password,
                          int_glcode = a.intglcode,
                          epin = a.epin
                      }).ToList();
            int flag = 0;

            for (int i = 0; i < id.Count(); i++)
            {
                if (id[i].user_name == txtadmin.Text && txtpassword.Text == base64Decode(id[i].password))
                {
                    flag = 1;
                    var package = (from a in linq_obj.pin_allocations
                                   join b in linq_obj.Package_Masters on a.fk_package_id equals b.intGlcode
                                   where a.pin_generated == id[i].epin
                                   select b).Single();
                    Session["Packagename"] = package.package_category;
                    Session["username"] = txtadmin.Text;
                    Session["MemberID"] = txtadmin.Text;
                    Session["referense"] = id[i].int_glcode;
                    Session["id"] = id[i].int_glcode;
                    Session["account"] = "Member Account";
                    Session["Login"] = "yes";
                    break;
                }
            }
            if (flag == 1)
            {
                Panform.Visible = true;
                panloginform.Visible = false;
                panregisterform.Visible = true;
                fillmember();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "onload", "alert('**Incorrect UserName or Password**')", true);
            }
            txtadmin.Text = "";
            txtpassword.Text = "";
        }
        catch (Exception ex)
        {

            Response.Write("<script laguage='javascript'>alert('** Some Error is occured During Login**')</Script>");
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
        }
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
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btncontinue_pay_Click(object sender, EventArgs e)
    {
        try
        {
            //ccjoin.ValidateCaptcha(txtcapcha.Text);
            //if (!ccjoin.UserValidated)
            if (txtcapcha.Text != "14")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Onload", "alert('Please insert correct combination of words In Captcha ')", true);
            }
            else
            {
                int total_quantity = 0;
                decimal total_amt = 0;
                for (int i = 0; i < grd_shoping.Rows.Count; i++)
                {
                    DropDownList drp = (DropDownList)grd_shoping.Rows[i].FindControl("drpquantity");
                    total_quantity += Convert.ToInt32(drp.SelectedValue.ToString());
                    total_amt = total_amt + Convert.ToDecimal(grd_shoping.Rows[i].Cells[5].Text.ToString());
                    ViewState["total_amt"] = total_amt;
                }
                if (drptype.SelectedItem.Text == "--Select--")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "onload", "alert('**Choose Payment Type**')", true);
                }
                else if (drptype.SelectedItem.Text == "Normal")
                {
                    int y = linq_obj.Insert_order_detail(Session["username"] != null ? Session["username"].ToString() : txtname.Text, txtaddress.Text, txtcountry.Text, txtstate.Text, txtcity.Text, txtpincode.Text, txtmobile.Text, txtemail.Text, Convert.ToDateTime(System.DateTime.Now.ToShortDateString()), total_quantity.ToString(), total_amt.ToString(), "Pending");
                    linq_obj.SubmitChanges();

                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        linq_obj.Insert_shoping(Convert.ToInt32(dt.Rows[k][6].ToString()), dt.Rows[k][4].ToString(), dt.Rows[k][5].ToString(), Convert.ToInt32(y));
                        linq_obj.SubmitChanges();
                    }
                    panMain.Visible = false;
                    Session["total_qty"] = "";
                    if (Session["username"] != null)
                    {
                        self_repurchase1();
                        group_repurchase1();
                    }
                    Response.Redirect("Payment_Detail.aspx", false);
                }
                else
                {
                    if (Convert.ToDecimal(lblewallbal.Text) < Convert.ToDecimal(total_amt))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "onload", "alert('**You Have No Sufficient Balance**')", true);
                    }
                    else
                    {
                        int y = linq_obj.Insert_order_detail(Session["username"] != null ? Session["username"].ToString() : txtname.Text, txtaddress.Text, txtcountry.Text, txtstate.Text, txtcity.Text, txtpincode.Text, txtmobile.Text, txtemail.Text, Convert.ToDateTime(System.DateTime.Now.ToShortDateString()), total_quantity.ToString(), total_amt.ToString(), "Pending");
                        linq_obj.SubmitChanges();

                        for (int k = 0; k < dt.Rows.Count; k++)
                        {
                            linq_obj.Insert_shoping(Convert.ToInt32(dt.Rows[k][6].ToString()), dt.Rows[k][4].ToString(), dt.Rows[k][5].ToString(), Convert.ToInt32(y));
                            linq_obj.SubmitChanges();
                        }

                        if (drptype.SelectedItem.Text == "Ewallet")
                        {
                            var id_bal = (from a in linq_obj.Temp_CashEwallets where a.memberid == Session["username"].ToString() select a).Single();

                            lblewallbal.Text = (id_bal.ewall_bal - Convert.ToDecimal(ViewState["total_amt"])).ToString();
                            id_bal.ewall_bal = Convert.ToDecimal(lblewallbal.Text);
                            linq_obj.SubmitChanges();
                            linq_obj.I_cashewallet(Session["username"].ToString(), Convert.ToDecimal(ViewState["total_amt"]), "Shopping", "Used Ewallet balance for shopping", "Out", Convert.ToDateTime(System.DateTime.Now.ToShortDateString()), null);
                            linq_obj.SubmitChanges();
                        }

                        if (drptype.SelectedItem.Text == "Self Repurchase")
                        {
                            //var id = (from a in linq_obj.Account_Details
                            //          where a.MemberID == Session["username"].ToString()
                            //          select a).Single();

                            var id = (from a in linq_obj.Member_Bonus
                                      where a.member_id == Session["username"].ToString()
                                      select a).Single();
                            lblewallbal.Text = (id.repurchase_bonus - Convert.ToDecimal(ViewState["total_amt"])).ToString();
                            id.repurchase_bonus = Convert.ToDecimal(lblewallbal.Text);
                            linq_obj.SubmitChanges();
                            linq_obj.I_cashewallet(Session["username"].ToString(), Convert.ToDecimal(ViewState["total_amt"]), "Self Repurchase Income", "Shopping Used With Self Repurchase Balance", "Out", Convert.ToDateTime(System.DateTime.Now.ToShortDateString()), null);
                            linq_obj.SubmitChanges();
                        }

                        if (drptype.SelectedItem.Text == "Group Repurchase")
                        {
                            //var id = (from a in linq_obj.Account_Details
                            //          where a.MemberID == Session["username"].ToString()
                            //          select a).Single();

                            var id = (from a in linq_obj.Member_Bonus
                                      where a.member_id == Session["username"].ToString()
                                      select a).Single();
                            lblewallbal.Text = (id.group_bonus - Convert.ToDecimal(ViewState["total_amt"])).ToString();
                            id.group_bonus = Convert.ToDecimal(lblewallbal.Text);
                            linq_obj.SubmitChanges();
                            linq_obj.I_cashewallet(Session["username"].ToString(), Convert.ToDecimal(ViewState["total_amt"]), "Group Repurchase Income", "Shopping Used With Group Repurchase Balance", "Out", Convert.ToDateTime(System.DateTime.Now.ToShortDateString()), null);
                            linq_obj.SubmitChanges();
                        }
                        self_repurchase1();
                        group_repurchase1();
                        panMain.Visible = false;
                        Session["total_qty"] = "";
                        Response.Redirect("Payment_Detail.aspx", false);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
        }
    }

    private void ewallbal()
    {
        try
        {
            var id_ebalshow = (from a in linq_obj.Temp_CashEwallets
                               where a.memberid == Session["username"].ToString()
                               select a).Single();
            lblewallbal.Text = id_ebalshow.ewall_bal.ToString();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void self_repurchase_balance()
    {
        try
        {
            var id = (from a in linq_obj.Account_Details
                      where a.MemberID == Session["username"].ToString()
                      select a).Single();

            //var id1 = (from a in linq_obj.repurchase_incomes
            //           where a.membre_id == Session["username"].ToString()
            //           group a by a.membre_id into b
            //           select new
            //           {
            //               memberid = b.Key,
            //               repurchase = b.Sum(p => p.amount)
            //           }).Single();

            var id1 = (from a in linq_obj.Member_Bonus
                       where a.member_id == Session["username"].ToString()
                       select a).Single();

            lblewallbal.Text = id1.repurchase_bonus.ToString();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void group_repurchase_balance()
    {
        try
        {
            var id = (from a in linq_obj.Account_Details
                      where a.MemberID == Session["username"].ToString()
                      select a).Single();

            //var id1 = (from a in linq_obj.repurchase_incomes
            //           where a.membre_id == Session["username"].ToString()
            //           group a by a.membre_id into b
            //           select new
            //           {
            //               memberid = b.Key,
            //               repurchase = b.Sum(p => p.amount)
            //           }).Single();

            var id1 = (from a in linq_obj.Member_Bonus
                       where a.member_id == Session["username"].ToString()
                       select a).Single();

            lblewallbal.Text = id1.group_bonus.ToString();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void drptype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (drptype.SelectedItem.Text == "Ewallet")
            {
                ewallbal();
                Label2.Visible = true;
                lblewallbal.Visible = true;
            }
            if (drptype.SelectedItem.Text == "Self Repurchase")
            {
                self_repurchase_balance();
                Label2.Visible = true;
                lblewallbal.Visible = true;
            }
            if (drptype.SelectedItem.Text == "Group Repurchase")
            {
                group_repurchase_balance();
                Label2.Visible = true;
                lblewallbal.Visible = true;
            }
            if (drptype.SelectedItem.Text == "Normal")
            {
                Label2.Visible = false;
                lblewallbal.Visible = false;
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void self_repurchase()
    {
        try
        {
            var id_repurchase = (from a in linq_obj.Member_Bonus
                                 where a.member_id == Session["username"].ToString()
                                 select a).Single();

            decimal self_repurchase = (decimal)(Convert.ToDecimal(ViewState["total_amt"].ToString()) * (id_repurchase.repurchase_percent / 100));

            linq_obj.insert_repurchase_income(Session["username"].ToString(), "Self", "Self Repurchase", self_repurchase, System.DateTime.Now);
            id_repurchase.repurchase_bonus += self_repurchase;
            linq_obj.SubmitChanges();
        }
        catch (Exception ex)
        {

            Response.Write(ex.Message);
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
        }
    }

    private void self_repurchase1()
    {
        try
        {
            decimal self_repurchase = 0.0M;

            var id_repurchase = (from a in linq_obj.Member_Bonus
                                 where a.member_id == Session["username"].ToString()
                                 select a).Single();

            for (int i = 0; i < grd_shoping.Rows.Count; i++)
            {
                int product_code = Convert.ToInt32(grd_shoping.DataKeys[i].Value);

                //var id = (from a in linq_obj.product_masters
                //          where a.intGlCode == product_code
                //          select a.repurchase_percent).Single();

                var id = (from a in linq_obj.product_masters
                          join b in linq_obj.Repurchase_Categories on a.fk_repurchase_category equals b.intglcode
                          where a.intGlCode == product_code
                          select b.self_repurchase_percent).Single();

                self_repurchase += (decimal)((Convert.ToDecimal(grd_shoping.Rows[i].Cells[5].Text)) * (Convert.ToDecimal(id) / 100));
            }

            linq_obj.insert_repurchase_income(Session["username"].ToString(), "Self", "Self Repurchase", self_repurchase, System.DateTime.Now);
            id_repurchase.repurchase_bonus += self_repurchase;
            linq_obj.SubmitChanges();
        }
        catch (Exception ex)
        {
            throw new Exception("Error in self repurchase calculation" + ex.Message, ex);
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void group_repurchase()
    {
        try
        {
            var id_grp_member = (from a in linq_obj.RegDetails
                                 where a.MemberID == Session["username"].ToString() && a.EarnedID != "1" && a.EarnedID != Session["username"].ToString()
                                 orderby a.intGlcode descending
                                 select a).ToList();

            for (int i = 0; i < id_grp_member.Count; i++)
            {
                var bonus_member = (from a in linq_obj.Member_Bonus
                                    where a.member_id == id_grp_member[i].EarnedID
                                    select a).Single();

                var mst_bonus = (from a in linq_obj.Bonus_msts
                                 select a).Single();

                var id_repurchase = (from a in linq_obj.Member_Bonus
                                     where a.member_id == Session["username"].ToString()
                                     select a).Single();

                decimal grp_percent = (decimal)(bonus_member.repurchase_percent - mst_bonus.repurchase_percent);
                decimal grp = (decimal)(Convert.ToDecimal(ViewState["total_amt"].ToString()) * (grp_percent / 100));

                bonus_member.group_bonus += grp;
                linq_obj.insert_repurchase_income(bonus_member.member_id, Session["username"].ToString(), "Group Repurchase", grp, System.DateTime.Now);
                linq_obj.SubmitChanges();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
        }
    }

    private void group_repurchase1()
    {
        try
        {
            var id_grp_member = (from a in linq_obj.RegDetails
                                 where a.MemberID == Session["username"].ToString() && a.EarnedID != "1" && a.EarnedID != Session["username"].ToString() && a.LevelNo <= 15
                                 select a).Take(15).ToList();
            decimal grp = 0.0M;

        for (int j = 0; j < grd_shoping.Rows.Count; j++)
            {
                var grp_percent = (from a in linq_obj.product_masters
                                   join b in linq_obj.Repurchase_Categories on a.fk_repurchase_category equals b.intglcode
                                   where a.intGlCode == Convert.ToInt32(grd_shoping.DataKeys[j].Value)
                                   select b.group_repurchase_percent).Single();

                grp += (decimal)(Convert.ToDecimal(grd_shoping.Rows[j].Cells[5].Text) * (Convert.ToDecimal(grp_percent) / 100));
            }

            for (int i = 0; i < id_grp_member.Count; i++)
            {
                var bonus_member = (from a in linq_obj.Member_Bonus
                                    where a.member_id == id_grp_member[i].EarnedID
                                    select a).Single();

                bonus_member.group_bonus += grp;
                linq_obj.insert_repurchase_income(bonus_member.member_id, Session["username"].ToString(), "Group Repurchase", grp, System.DateTime.Now);
                linq_obj.SubmitChanges();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void lnkdelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;
            int code = Convert.ToInt32(lnk.CommandArgument);
            if (dt.Rows.Count != 0)
            {
                dt.Rows.RemoveAt(row.RowIndex);
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
            linq_obj.delete_shoping(code);
            linq_obj.SubmitChanges();
        }
        catch (Exception ex)
        {
            Response.Write("Error in removing product from cart " + ex.Message);
            ErrHandler.WriteError(ex.Message);
        }
    }
}
