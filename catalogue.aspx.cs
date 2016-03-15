using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    DataClassesDataContext linq_obj = new DataClassesDataContext();
    static DataTable dt;
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
      
        fill_repeater();
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
    private void fill_repeater()
    {
        var id = (from a in linq_obj.catalogs
                  select new
                  {
                      code = a.intglcode,
                      image = "./admin/Images/" + a.image,
                      discription = a.description,
                      price = a.prics 
                  }).ToList();
        DataList1.DataSource = id;
        DataList1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Button lnk = (Button)sender;
        int code = Convert.ToInt32(lnk.CommandArgument.ToString());

        var id = (from a in linq_obj.catalogs
                  where a.intglcode == code
                  select a).Single();

        string path = "~/admin/Images/";
        decimal total = Convert.ToDecimal(id.prics) * 1;
        int total_qty = 1;
        int flag = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (id.intglcode == Convert.ToInt32(dt.Rows[i][5].ToString()))
            {
                total_qty =     1 + Convert.ToInt32(dt.Rows[i][3].ToString());
                total = Convert.ToDecimal(id.prics) * Convert.ToDecimal(total_qty);
                dt.Rows[i][3] = total_qty.ToString();
                dt.Rows[i][4] = total.ToString();
                flag = 1;
                break;
            }
        }
        if (flag == 0)
        {
            dt.Rows.Add(path + id.image, id.description, id.prics, total_qty, total, id.intglcode);
        }
        Session["addcart"] = dt;

        Response.Redirect("BasketDelivery.aspx");
    }
}