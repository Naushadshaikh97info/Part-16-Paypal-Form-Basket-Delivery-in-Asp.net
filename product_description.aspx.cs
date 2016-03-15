using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class product_description : System.Web.UI.Page
{
    cosmicDataContext linq_obj = new cosmicDataContext();
    static int procode;
    static DataTable dt;
    static int code = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        if (Session["addcart"] == null)
        {
            createtable();
        }
        else
        {
            dt = (DataTable)Session["addcart"];
        }
        if (Request.QueryString["id"] != null && Request.QueryString["id2"] != null)
        {
            procode = Convert.ToInt32(Request.QueryString[1].ToString());
            descriptino(Convert.ToInt32(Request.QueryString[1].ToString()));

        }
        else
        {
            procode = Convert.ToInt32(Request.QueryString[0].ToString());
            descriptino(Convert.ToInt32(Request.QueryString[0].ToString()));
        }


       fill_product();

       int total;
       total = Convert.ToInt32(drpquantity.SelectedValue) * Convert.ToInt32(Label2.Text);
       total_price.Text = total.ToString();
        
    }
    private void createtable()
    {
        try
        {
            dt = new DataTable();
            dt.Columns.Add("productimg");
            dt.Columns.Add("productname");
            dt.Columns.Add("actualprice");

            dt.Columns.Add("drpquantity");
            dt.Columns.Add("totalprice");
            dt.Columns.Add("productcode");


        }
        catch (Exception ex)
        {
        }
    }
    protected void onclick_image1(object sender, EventArgs e)
    {
       
        if (Session["userid"] == null)
        {
            LinkButton lnk = (LinkButton)sender;
            int code = Convert.ToInt32(lnk.CommandArgument.ToString());
            var id = (from a in linq_obj.product_master_ts
                      where a.intglcode == code
                      select a).ToList();
            Response.Redirect("user_login.aspx?id=" + id[0].fk_category_id + "&" + "id2=" + code);
        }
        else
        {
            LinkButton lnk = (LinkButton)sender;
            int code = Convert.ToInt32(lnk.CommandArgument.ToString());
            var id = (from a in linq_obj.product_master_ts
                      where a.intglcode == code
                      select a).ToList();
            Response.Redirect("product_description.aspx?id=" + id[0].fk_category_id + "&" + "id2=" + code);
        }
    }
    private void fill_product()
    {

        if (Request.QueryString["id"] != null)
        {

            var id = (from a in linq_obj.product_master_ts
                      where a.fk_category_id == Convert.ToInt32(Request.QueryString[0].ToString())
                      select new
                      {
                          code = a.intglcode,
                          Imagename = "uploads/" + a.image,

                          var_name = a.title,
                          product_mrp = a.price

                      }).Take(5).ToList();
            dlCountry.DataSource = id;
            dlCountry.DataBind();
        }
        else if (Request.QueryString["id2"] != null)
        {
            var id = (from a in linq_obj.product_master_ts
                      where a.fk_product_id == Convert.ToInt32(Request.QueryString[1].ToString())
                      select new
                      {
                          code = a.intglcode,
                          Imagename = "uploads/" + a.image,

                          var_name = a.title,
                          product_mrp = a.price

                      }).Take(5).ToList();
            dlCountry.DataSource = id;
            dlCountry.DataBind();
        }
    }
    private void descriptino( int procode)
    {
        var id = (from a in linq_obj.product_master_ts
                  where a.intglcode == procode
                  select new
                  {
                      code = a.intglcode,
                      img = "./uploads/" + a.image,
                      name = a.title,
                      pcode = a.intglcode,
                      price = a.price

                  }).Single();
        Main_Img.Src = id.img;
        Main_Imga.HRef = id.img;
        Main_Imga.Title = id.name;
        p_name.Text = id.name;
        Label1.Text = id.pcode.ToString();
        Label2.Text = id.price;
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        var id = (from a in linq_obj.product_master_ts
                  where a.intglcode == Convert.ToInt32(Request.QueryString[1])
                  select a).Single();
        string path = "~/uploads/";
        decimal total = Convert.ToDecimal(id.price) * Convert.ToDecimal(drpquantity.SelectedItem.Text);
        int total_qty = Convert.ToInt32(drpquantity.SelectedItem.Text);
        int flag = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (id.intglcode == Convert.ToInt32(dt.Rows[i][5].ToString()))
            {
                total_qty = Convert.ToInt32(drpquantity.SelectedItem.Text) + Convert.ToInt32(dt.Rows[i][4].ToString());
                total = Convert.ToDecimal(id.price) * Convert.ToDecimal(total_qty);
                dt.Rows[i][4] = total_qty.ToString();
                dt.Rows[i][5] = total.ToString();
                flag = 1;
                break;
            }
        }
        if (flag == 0)
        {
            dt.Rows.Add(path + id.image, id.title, id.price, total_qty, total, id.intglcode);
        }
        Session["addcart"] = dt;

        int c_code = Convert.ToInt32(Request.QueryString[0].ToString());
        Response.Redirect("BasketDelivery.aspx?id=" + c_code);
    }

    protected void drpquantity_SelectedIndexChanged(object sender, EventArgs e)
    {
        int total;
        total = Convert.ToInt32(drpquantity.SelectedValue) * Convert.ToInt32(Label2.Text);
        total_price.Text = total.ToString();
    }
}