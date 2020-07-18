using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication7
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnRegister_Click(object sender, EventArgs e)
        {
            string username = TxtUsername.Text;
            string password = TxtPassword.Text;
            string email = TxtEmail.Text;
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\Users_DB.mdf';Integrated Security=True";
            string strCheckUsername = "SELECT count(*) FROM Users WHERE Username = '" + username + "'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(strCheckUsername, connection);

                if ((int)sqlCommand.ExecuteScalar() > 0) // בודק אם יש משתמש קיים
                {
                    LblNotification.Text = "username already taken! try another one";
                    return;
                }

            }

            string cmdInsertString1= "select count(*) FROM Users";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(cmdInsertString1, connection);
                int id = (int)sqlCommand.ExecuteScalar();                
                cmdInsertString1 = "INSERT INTO Users VALUES (' " +Anigma.Crypt(username) + "','" + Anigma.Crypt(password) + "','" + Convert.ToString(id, 16)+ "','" + Anigma.Crypt(email) + "')";//converts id to base 16 and encrypts data
                sqlCommand = new SqlCommand(cmdInsertString1, connection);
                sqlCommand.ExecuteNonQuery();
                Response.Redirect("Login.aspx");
            }
        }
    }
}