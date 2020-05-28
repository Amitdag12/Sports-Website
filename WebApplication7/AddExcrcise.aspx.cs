using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication7
{
    public partial class AddExcrcise : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void AddExcrciseToTable(object sender, EventArgs e)
        {

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\Excrcises.mdf';Integrated Security=True";
            string name = TxtExcrciseName.Text;
            string strCheckExcrciseName = "SELECT count(*) FROM Excrcise WHERE name = '" + name + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(strCheckExcrciseName, connection);

                if ((int)sqlCommand.ExecuteScalar() > 0) // בודק אם התרגיל  קיים כבר קיים
                {
                    Response.Write("<script>alert('already exist')</script>");
                    return;
                }

            }
            int compound = IsCompundOrIsolate.SelectedValue == "IsCompound" ? 1 : 0,
                isolate = (compound + 1) % 2;
            
            string cmdInsertString1 = "INSERT INTO Excrcise VALUES ('"+name+"', " + compound + "," + isolate + "," + 
                int.Parse(Shoulder.SelectedValue) + "," + 
                int.Parse(Triceps.SelectedValue) + ","
                + int.Parse(Lats.SelectedValue) + "," 
                + int.Parse(Gluts.SelectedValue) + "," 
                + int.Parse(Hamstring.SelectedValue) + "," 
                + int.Parse(Chest.SelectedValue) + "," 
                + int.Parse(Biceps.SelectedValue) + "," 
                + int.Parse(Abdominal.SelectedValue) + ","
                + int.Parse(Obliques.SelectedValue) + ","
                + int.Parse(Quads.SelectedValue) + ","
                + int.Parse(Calves.SelectedValue) + ","
                + int.Parse(Difficult.Text) + ","
                + int.Parse(PowerLifting.SelectedValue) + ","
                + int.Parse(Street.SelectedValue) + ")";
            //LblExcrciseName.Text = cmdInsertString1;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(cmdInsertString1, connection);
                    sqlCommand.ExecuteNonQuery();
                }
                Response.Redirect("AddExcrcise.aspx");
            }catch(Exception ex) { }
             
        }
    }
}