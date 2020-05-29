using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace WebApplication7
{

    public partial class TraningRoutineGenerator : System.Web.UI.Page
    {   
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\Excrcises.mdf';Integrated Security=True";
        private Random Rnd=new Random();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                for (int i = 1; i < 7; i++)
                {
                    difficultyDDL.Items.Add(i.ToString());
                }
            }
        }
        protected void PrintArray(string[] arr)
        {
            for (int i = 0; i < 5; i++)
            {
                Debug.WriteLine("");

            }
            for (int i = 0; i < arr.Length; i++)
            {
                Debug.WriteLine(arr[i]);
            }
            
        }
        /*
         * Exchange Butoon?
        //to finish
        protected void ExchangeExcrciseButton(object sender, EventArgs e)
        {
            Button btn =sender as Button;
            int[]orders=(int[])Session["orders"];
            string[]excList=(string[])Session["excList"];
            BuildProgramTable(orders, ExchangeExcrcise(excList, btn.ID));
        }
        protected string[] ExchangeExcrcise(string[] excList, string exc)
        {
             excList[Array.IndexOf(excList,exc)]=PullMainExc(FindMuscleGroup(exc), IsExcCompound(exc), excList, IsExcStreet(exc));//puts in the list a diffrent exc in the place of the last one
            return excList;
        }
        protected string FindMuscleGroup(string exc)
        {
            string[] names = new string[7] { "Leg", "Back", "Bicep", "Chest", "Shoulder", "Tricep", "Abs" };
            DataSet dataset = new DataSet();
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\Excrcises.mdf';Integrated Security=True";
            string commandUserString = "SELECT * FROM Excrcise WHERE name='" + exc + "'";
            Debug.WriteLine(commandUserString);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(commandUserString, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                adapter.Fill(dataset);
            }
            foreach (DataRow row in dataset.Tables[0].Rows)
            {
                foreach(string name in names)
                {
                    if(row[name].ToString()=="1")
                    {
                        return name;
                    }
                }
            }
            return "";
        }
        */
        protected bool IsExcCompound(string exc)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\Excrcises.mdf';Integrated Security=True";
            string commandUserString = "SELECT count(*) FROM Excrcise WHERE name='" + exc + "' AND IsCompound=1";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(commandUserString, connection);

                return (int)sqlCommand.ExecuteScalar() == 1;
            }
        }
        protected int IsExcStreet(string exc)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\Excrcises.mdf';Integrated Security=True";
            string commandUserString = "SELECT count(*) FROM Excrcise WHERE name='" + exc + "' AND Street=1";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(commandUserString, connection);

                return (int)sqlCommand.ExecuteScalar();
            }
        }
        //-------------------------------------------------------------------NEW ALOGRITHM---------------------------------------------------------------------/
        private Dictionary<string, Dictionary<string, int>> muscleGroup_muscle_points = new Dictionary<string, Dictionary<string, int>>();//a dictinary contaning muscle groups muscle inside that muscle group and number of points each muscle gets
        protected void IntalizeMuscleDictionary(int generalPoints)//generalPoints is defind by like workout length and kind of workput
        {
            generalPoints *= 5;
            string[] names = new string[7] { "Chest", "Shoulder", "Tricep", "Abs", "Back", "Bicep", "Leg" };
            int i = 0;//used to make bugger muscles more important
            foreach(string name in names)
            {
                i++;
                List<string> muscles = GetMusclesGroupMuscles(name);
                muscleGroup_muscle_points[name] = new Dictionary<string, int>();
                foreach (string muscle in muscles)
                {
                    muscleGroup_muscle_points[name][muscle]= i > 3 ? generalPoints : generalPoints + 5;
                }
                
            }

        }
        protected List<string> GetMusclesGroupMuscles(string muscleGroup)
        {//puts individual muscle inside muscle groups
            string[] names = new string[20]{"Shoulder  front deltoid","Shoulder  rear deltoid","Shoulder lateral deltoid","Bicep short",
                "Bicep long","Bicep brachialis","Tricep long","Tricep medial","Back traps","Back lats","Back teres major","Chest upper","Chest lower",
                "Chest middle","Abs oblique","Abs abdominal","Leg glutes","Leg quads","Leg Hamstring","Leg Calves"};
            List<string> muscleGroupMuscles = new List<string>();
            foreach (string x in names)
            {
                if(FirstWord(x)==muscleGroup)
                {
                    muscleGroupMuscles.Add(x);
                }
            }
            return muscleGroupMuscles;
        }
        protected string FirstWord(string x)
        {
            string word="";
            foreach(char c in x)
            {
                if (c == ' ')
                    return word;
                word += c;
            }
            return word;
        }
        /*
         * excLists shoulg go in the following order:
         * "Chest", "Shoulder", "Tricep", "Abs" , "Back", "Bicep","Leg"
         * 
         * 
         */
        protected string BuildTable(List<List<string>> excLists,string workoutKind)
        {
           // PrintLists(excLists);
            int index=0;
            string[] muscleGruopNames = new string[7] { "Leg", "Back", "Bicep", "Chest", "Shoulder", "Tricep", "Abs" };
            Dictionary<string,string[]> days=new Dictionary<string, string[]>();
            switch(workoutKind)
            {
                
                case "AB":
                    days["PUSH"] = new string[] { "Chest", "Shoulder", "Tricep", "Abs" };
                    days["PULL"] = new string[] { "Leg", "Back", "Bicep" };
                    break;
                case "ABC":
                    days["PUSH"] = new string[] { "Chest", "Shoulder", "Tricep" };
                    days["PULL"] = new string[] {  "Back", "Bicep" , "Abs" };
                    days["LEGS"] = new string[] { "Leg" };
                    break;
                default:
                    days["FULLBODY"] = new string[] { "Chest", "Shoulder", "Tricep", "Abs", "Back", "Bicep", "Leg" };
                    break;
            }
            string table = "<table  border='1' style='width: 40 %; height: 70 %; text - align:center; margin: auto'>";
            foreach(KeyValuePair<string, string[]> day in days)
            {
                table += "<tr>" +
            "  <th colspan = '2' class='headline'>" +
                    day.Key+
                " </th>" +
            " </tr>";
                while(excLists.Count>0)
                {

                    List<string> list = excLists[0];
                    string[] muscleGroups = day.Value;
                    if (Array.IndexOf(muscleGroups,list[0])==-1)
                    {//checks if ,uscle is inside currnet day if not contibues  to next day
                        break;
                    }
                    table +=
                  "<tr>" +
                        "<th colspan = '2' class='headline'>" + list[0] + "</th>" +
                  " </tr> ";
                    list.Remove(list[0]);
                    foreach(string exc in list)
                    {
                        table +=
                        "   <tr>" +
                        "       <td><a href = '" + (GenerateYT_Link(exc)) + "' >" + exc + "</a></td>" +
                      "     <td>" + (GenerateRepsCount()) + "</td>" +
                      " </tr>";
                    }
                    excLists.Remove(list);
                    index++;
                }
            }
            return table;
        }
        protected string PullMainExc(string muscleGroup, bool iscompound, string[] usedExc, int IsStreet)
        {
            DataSet dataset = new DataSet();

            List<string> excList = new List<string>();
            int isIsolate = iscompound ? 0 : 1;
            string exc,
                Difficulty = " AND (Difficulty =" + difficultyDDL.SelectedItem.Text + " OR Difficulty =" + (int.Parse(difficultyDDL.SelectedItem.Text) - 1) + ")",
             street = IsStreet == 1 ? " AND Street= 1" : "",
             connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\Excrcises.mdf';Integrated Security=True",
             commandUserString = "SELECT * FROM Excrcise WHERE " + muscleGroup + "=" + 1 + Difficulty + " AND IsIsolate=" + isIsolate + street;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(commandUserString, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                adapter.Fill(dataset);
            }            
            foreach (DataRow row in dataset.Tables[0].Rows)
            {
                excList.Add(row["name"].ToString());
            }
            do
            {
                exc = excList[Rnd.Next(0, excList.Count)];
            } while (Array.IndexOf(usedExc, exc) != -1);
            return exc;
        }
        /*
         * the idea behind this is to create a workout which centers around one big exc for each muscle group and then filling 
         * the rest of the excrcises for that muscle group with smaller excrcises which work on the muscles that are most not used
         */ 
        protected void CreateWotkout(object sender, EventArgs e)
        {

            string[] muscleGruopNames = new string[7] { "Chest", "Shoulder", "Tricep", "Abs", "Back", "Bicep", "Leg" };
            List<string> usedExc = new List<string>();
            string lowestMuscleUsed = "";
            int max = 1;
            int numberOfExc=int.Parse(WorkoutsLengthRBL.SelectedValue)*int.Parse(WorkoutKindRBL.SelectedValue);//the calculation is thw workout kind which is 1-3 multiplied by how much time the workout is supposed to be
            IntalizeMuscleDictionary(numberOfExc);
            string exc;
            numberOfExc = numberOfExc > 5 ? 5 : numberOfExc;
            List<List<string>> excLists = new List<List<string>>();
            
            foreach (string muscleGroup in muscleGruopNames)
            {
                List<string> muscleGroupList = new List<string>();
                muscleGroupList.Add(muscleGroup);
                exc = PullMainExc(muscleGroup, true, usedExc.ToArray(), int.Parse(TypeOfWorkoutRBL.SelectedValue));
                SubtractExc(exc);
                muscleGroupList.Add(exc);
                usedExc.Add(exc);
                for (int i = 0; i < numberOfExc; i++)
                {


                    foreach (KeyValuePair<string, int> muscle in muscleGroup_muscle_points[muscleGroup])
                    {
                        if (max < muscle.Value)
                        {
                            lowestMuscleUsed = muscle.Key;
                            max = muscle.Value;
                        }
                        Debug.WriteLine(lowestMuscleUsed);
                    }
                    if (lowestMuscleUsed != "")
                    {
                        exc = PullSecondaryExcs(lowestMuscleUsed, usedExc.ToArray(), muscleGroup);
                        if(exc=="")
                        {
                            break;
                        }
                        muscleGroupList.Add(exc);
                        usedExc.Add(exc);
                        SubtractExc(exc);
                        Debug.WriteLine("EXC: " + exc);
                    }
                    else
                    {
                        break;
                    }
                }
                excLists.Add(muscleGroupList);
                max = 0;
                lowestMuscleUsed = "";
            }
            string workoutKind= "ABC";
            switch(WorkoutKindRBL.SelectedValue)
            {
                case "1":
                    workoutKind = "FB";
                    break;
                case "2":
                    workoutKind = "AB";
                    break;
            }

        
            LblTable.Text=BuildTable(excLists, workoutKind);

        }
        protected string PullSecondaryExcs(string muscle,string[] usedExc,string muscleGroup)
        {
            Debug.WriteLine("------------------------------------");
            DataSet dataset = new DataSet();
            PrintArray(usedExc);
            Debug.WriteLine("qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq");
            List<string> potentialExcList = new List<string>();
            
            string pullExc = "SELECT * FROM MuscleWork WHERE [" + muscle + "] >0";
            string exc;
            string finalExc="";
            int max=0;
            string Difficulty = " AND (Difficulty =" + difficultyDDL.SelectedItem.Text + " OR Difficulty =" + (int.Parse(difficultyDDL.SelectedItem.Text) - 1) + ")",
            street = int.Parse(TypeOfWorkoutRBL.SelectedValue) == 1 ? " AND Street= 1" : "",
            muslceG = " AND [" + muscleGroup + "] =1";//muscle group check

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(pullExc, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                adapter.Fill(dataset);
                           
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    exc = row["name"].ToString();
                    string checkIfExcGood = "SELECT COUNT(*) FROM Excrcise WHERE name='" + exc+"'"+ Difficulty + street+muslceG;//+ isIsolate
                    sqlCommand = new SqlCommand(checkIfExcGood, connection);
                    if (int.Parse(row[muscle].ToString()) > max && Array.IndexOf(usedExc, exc) == -1&&(int)sqlCommand.ExecuteScalar()==1)
                    {
                        Debug.WriteLine(exc);
                        finalExc=exc;
                        max = int.Parse(row[muscle].ToString());
                    }

                }
            }
            Debug.WriteLine("------------------------------------");
            return finalExc;
        }
        protected bool IsFinished(Dictionary<string, Dictionary<string, int>> muscleGroup_muscle_points)
        {
            foreach (KeyValuePair<string, Dictionary<string, int>> muscleGroup in muscleGroup_muscle_points)
            {
                foreach (KeyValuePair<string, int> muscle in muscleGroup.Value)
                {
                    if (muscle.Value > 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        protected string GenerateYT_Link(string excName)
        {
            string link = " https://www.youtube.com/results?search_query=",
                word = "";
            bool first = true;
            for (int i = 0; i < excName.Length; i++)
            {
                if (excName[i] == ' ')
                {
                    if (!first)
                    {
                        link += "+" + word;
                    }
                    else
                    {
                        link += word;
                        first = false;
                    }
                    word = "";

                }
                else
                {
                    word += excName[i];
                }

            }
            link += word;
            return link;
        }
        //hi
        protected void SubtractExc(string exc)
        {
            string[] names = new string[7] { "Leg", "Back", "Chest", "Bicep", "Shoulder", "Tricep", "Abs" };
            DataSet dataSet = new DataSet();
            string getExc = "SELECT * FROM MuscleWork WHERE name='" + exc + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(getExc, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                adapter.Fill(dataSet);
                string getMusclePoint;
                foreach (DataColumn column in dataSet.Tables[0].Columns)
                {
                    if(column.ToString()=="name")
                    {
                        continue;
                    }
                    getMusclePoint="SELECT ["+column.ToString()+"] FROM MuscleWork WHERE name='" + exc + "'";
                    sqlCommand = new SqlCommand(getMusclePoint, connection);
                    // Debug.WriteLine("-------------------------------");
                    // Debug.WriteLine("A: "+ column.ToString());
                    //   Debug.WriteLine("B: " + FirstWord(column.ToString()));
                    Debug.WriteLine(getMusclePoint);
                    muscleGroup_muscle_points[FirstWord(column.ToString())][column.ToString()] -= (int)sqlCommand.ExecuteScalar();//gets each colmun muslce group because first word and then by colmun
                }
            }
        }
            protected string GenerateRepsCount()
        {
            switch (WorkoutsFoucusRBL.SelectedValue)
            {
                case "MG":
                    return Rnd.Next(8, 16).ToString();
                default:
                    return Rnd.Next(4, 9).ToString();

            }
        }
        protected void PrintLists(List<List<string>> excLists)
        {
            foreach(List<string>list in excLists)
            {
                foreach(string str in list)
                {
                    Debug.WriteLine(str);
                }
            }
        }
    }
}