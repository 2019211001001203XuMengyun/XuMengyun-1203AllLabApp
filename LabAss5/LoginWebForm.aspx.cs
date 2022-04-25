using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LabAss5
{
    public partial class LoginWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SampleDatabaseConnectionString"].ConnectionString);
            conn.Open();
            string queryString = "Select UserName,Password from Logon where UserName= '" + txtUsername.Text + " '";

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(queryString, conn);

            DataSet dsl = new DataSet();
            adapter.Fill(dsl, "Logon");
            if (dsl.Tables["Logon"].Rows.Count == 0)
            {
                lblCaption.Text = "Invalid Username";
            }
            else
            {
                if (dsl.Tables["Logon"].Rows[0][1].ToString().Trim() == txtPassword.Text.Trim())
                {
                    lblCaption.Text = "welcome," + txtUsername.Text;

                    //session - Lab Assignment 10
                    Session["Uname"] = txtUsername.Text;
                    Server.Transfer("Welcome.aspx");

                }
                else
                {
                    lblCaption.Text = "Invalid Password";
                }
            }
            conn.Close();
        }

    }
}
