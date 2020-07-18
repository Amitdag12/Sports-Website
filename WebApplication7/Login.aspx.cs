using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication7
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BtnRegister_Click(object sender, EventArgs e)
        {
            string username = TxtUsername.Text;
            string password = TxtPassword.Text;
            if (username == "amit" && password == "654321dag")
            {
                Session["category"] = "admin";
                //Response.Redirect("2-AdminPage.aspx");
            }
            else
            {
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\Users_DB.mdf';Integrated Security=True";
                string strCheckUsernamePassword = "SELECT count(*) FROM Users WHERE Username = '" + Anigma.Crypt(username) + "'" + "and password ='" + Anigma.Crypt(password) + "'";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(strCheckUsernamePassword, connection);
                    sqlCommand.ExecuteNonQuery();
                    if ((int)sqlCommand.ExecuteScalar() == 0)
                    {
                        LblNotification.Text = "username or password does not exist";
                        Label1.Text = "maybe you would like to registre:";
                    }
                    else
                    {
                        Session["username"] = username;
                        Session["category"] = "user";
                        Response.Redirect("FrontPage.aspx");
                        
                    }
                }
            }
        }
    }
}