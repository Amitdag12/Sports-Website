using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication7
{
    public partial class AddMuscleWorkToExcrcise : System.Web.UI.Page
    {
        //i realy dont want to give the DDL names so they in the order of the table
        static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\Excrcises.mdf';Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DataSet dataSet = GetAllExc();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    foreach (DataRow Row in dataSet.Tables[0].Rows)
                    {
                        string CheckIfExist = "SELECT COUNT(*) FROM MuscleWork WHERE name='" + Row["name"].ToString() + "'";
                        SqlCommand sqlCommand = new SqlCommand(CheckIfExist, connection);
                        if ((int)sqlCommand.ExecuteScalar() == 0)
                        {
                            DDL_ExcNames.Items.Add(Row["name"].ToString());
                        }

                    }
                }
                for (int i = 0; i < 6; i++)
                {
                    DropDownList1.Items.Add(i.ToString());
                    DropDownList2.Items.Add(i.ToString());
                    DropDownList3.Items.Add(i.ToString());
                    DropDownList4.Items.Add(i.ToString());
                    DropDownList5.Items.Add(i.ToString());
                    DropDownList6.Items.Add(i.ToString());
                    DropDownList7.Items.Add(i.ToString());
                    DropDownList8.Items.Add(i.ToString());
                    DropDownList9.Items.Add(i.ToString());
                    DropDownList10.Items.Add(i.ToString());
                    DropDownList11.Items.Add(i.ToString());
                    DropDownList12.Items.Add(i.ToString());
                    DropDownList13.Items.Add(i.ToString());
                    DropDownList14.Items.Add(i.ToString());
                    DropDownList15.Items.Add(i.ToString());
                    DropDownList16.Items.Add(i.ToString());
                    DropDownList17.Items.Add(i.ToString());
                    DropDownList18.Items.Add(i.ToString());
                    DropDownList19.Items.Add(i.ToString());
                    DropDownList20.Items.Add(i.ToString());
                }
            }
        }
        protected DataSet GetAllExc()
        {
            string commandUserString = "SELECT * FROM Excrcise";
            DataSet dataset = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(commandUserString, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                adapter.Fill(dataset);
            }
            return dataset;
        }
        protected void AddExcrciseToTable(object sender, EventArgs e)
        {

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\Excrcises.mdf';Integrated Security=True";
            string name = DDL_ExcNames.SelectedItem.Text;
            string strCheckExcrciseName = "SELECT count(*) FROM MuscleWork WHERE name = '" + name + "'";
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


            string cmdInsertString1 = "INSERT INTO MuscleWork VALUES ('" + name + "', "+
                int.Parse(DropDownList1.SelectedItem.Text) + "," +
                int.Parse(DropDownList2.SelectedItem.Text) + ","
                + int.Parse(DropDownList3.SelectedItem.Text) + ","
                + int.Parse(DropDownList4.SelectedItem.Text) + ","
                + int.Parse(DropDownList5.SelectedItem.Text) + ","
                + int.Parse(DropDownList6.SelectedItem.Text) + ","
                + int.Parse(DropDownList7.SelectedItem.Text) + ","
                + int.Parse(DropDownList8.SelectedItem.Text) + ","
                + int.Parse(DropDownList9.SelectedItem.Text) + ","
                + int.Parse(DropDownList10.SelectedItem.Text) + ","
                + int.Parse(DropDownList11.SelectedItem.Text) + ","
                + int.Parse(DropDownList12.SelectedItem.Text) + ","
                + int.Parse(DropDownList13.SelectedItem.Text) + ","
                + int.Parse(DropDownList14.SelectedItem.Text) + ","
                + int.Parse(DropDownList15.SelectedItem.Text) + ","
                + int.Parse(DropDownList16.SelectedItem.Text) + ","
                + int.Parse(DropDownList17.SelectedItem.Text) + ","
                + int.Parse(DropDownList18.SelectedItem.Text) + ","
                + int.Parse(DropDownList19.SelectedItem.Text) + ","
                + int.Parse(DropDownList20.SelectedItem.Text) + ")";
            //LblExcrciseName.Text = cmdInsertString1;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(cmdInsertString1, connection);
                    sqlCommand.ExecuteNonQuery();
                }
                Response.Redirect("AddMuscleWorkToExcrcise.aspx");
            }
            catch (Exception ex) { }

        }
    }
}