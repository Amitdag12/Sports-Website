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
        private int restTimeIsolate;
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\Excrcises.mdf';Integrated Security=True";
        private Random Rnd = new Random();
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
            string[] muscleGroupNames = new string[7] { "Chest", "Shoulder", "Tricep", "Abs", "Back", "Bicep", "Leg" };
            string[] majorMuscles = new string[] { "Chest", "Back", "Leg" };//used to make bigger muscles more important
            foreach (string muscleGroupName in muscleGroupNames)
            {
                List<string> muscles = GetMusclesGroupMuscles(muscleGroupName);
                muscleGroup_muscle_points[muscleGroupName] = new Dictionary<string, int>();
                foreach (string muscle in muscles)
                {
                    muscleGroup_muscle_points[muscleGroupName][muscle] = Array.IndexOf(majorMuscles, muscleGroupName) == -1 ? (generalPoints >20? 20 : generalPoints): generalPoints + 5;//bigger nuscles get 5 more points than smaller muscles
                    if (WorkoutKindRBL.SelectedItem.Text == "abc"&& muscleGroupName=="Leg")
                    {
                        muscleGroup_muscle_points[muscleGroupName][muscle] *= 2;
                    }
                }

            }
            muscleGroup_muscle_points["Leg"]["Leg Calves"] = 5;

        }
        protected List<string> GetMusclesGroupMuscles(string muscleGroup)
        {//puts individual muscle inside muscle groups
            string[] names = new string[20]{"Shoulder  front deltoid","Shoulder  rear deltoid","Shoulder lateral deltoid","Bicep short",
                "Bicep long","Bicep brachialis","Tricep long","Tricep medial","Back traps","Back lats","Back teres major","Chest upper","Chest lower",
                "Chest middle","Abs oblique","Abs abdominal","Leg glutes","Leg quads","Leg Hamstring","Leg Calves"};
            List<string> muscleGroupMuscles = new List<string>();
            foreach (string x in names)
            {
                if (FirstWord(x) == muscleGroup)
                {
                    muscleGroupMuscles.Add(x);
                }
            }
            return muscleGroupMuscles;
        }
        protected string FirstWord(string x)
        {
            string word = "";
            foreach (char c in x)
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
        protected string BuildTable(List<List<string>> excLists, string workoutKind,Dictionary<string,string> repAndSetCount, Dictionary<string, string> restTime)
        {
            // PrintLists(excLists);
            int index = 0;
            string[] muscleGruopNames = new string[7] { "Leg", "Back", "Bicep", "Chest", "Shoulder", "Tricep", "Abs" };
            Dictionary<string, string[]> days = new Dictionary<string, string[]>();
            switch (workoutKind)
            {

                case "AB":
                    days["PUSH"] = new string[] { "Chest", "Shoulder", "Tricep", "Abs" };
                    days["PULL"] = new string[] { "Leg", "Back", "Bicep" };
                    break;
                case "ABC":
                    days["PUSH"] = new string[] { "Chest", "Shoulder", "Tricep" };
                    days["PULL"] = new string[] { "Back", "Bicep", "Abs" };
                    days["LEGS"] = new string[] { "Leg" };
                    break;
                default:
                    days["FULLBODY"] = new string[] { "Chest", "Shoulder", "Tricep", "Abs", "Back", "Bicep", "Leg" };
                    break;
            }
            string table= "";
            foreach (KeyValuePair<string, string[]> day in days)
            {
                table += "<table  border='1' style='width: 40 %; height: 70 %; text - align:center; margin: auto'class='Centerd NiceBacground'>";               
                table += "<tr>" +
            "  <th colspan = '2' class='headline'>" +
                    day.Key +
                " </th>" +
                "  <th colspan = '2' class='headline'>Rest time</ th > "+
            " </tr>";
                while (excLists.Count > 0)
                {

                    List<string> list = excLists[0];
                    string[] muscleGroups = day.Value;
                    if (Array.IndexOf(muscleGroups, list[0]) == -1)
                    {//checks if ,uscle is inside currnet day if not contibues  to next day
                        break;
                    }
                    table +=
                  "<tr>" +
                        "<th colspan = '2' class='headline'>" + list[0] + "</th>" +
                  " </tr> ";
                    list.Remove(list[0]);
                    foreach (string exc in list)
                    {
                        table +=
                        "   <tr>" +
                        "       <td><a href = '" + (GenerateYT_Link(exc)) + "' >" + exc + "</a></td>" +
                      "     <td>" + (repAndSetCount[exc]) + "</td>" +
                      "     <td>" + (restTime[exc]) + "</td>" +
                      " </tr>";
                    }
                    excLists.Remove(list);
                    index++;
                }
                table += "</table>";
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
        protected bool IsNotWorkingAll(string muscleGroup, Dictionary<string, Dictionary<string, int>> muscleGroup_muscle_points)
        {
            Dictionary<string, int> theMuscleGroup = muscleGroup_muscle_points[muscleGroup];
            foreach (KeyValuePair<string, int> muscle in theMuscleGroup)
            {
                if (muscle.Value > 0)
                {
                    return true;
                }
            }

            return false;
        }
        protected void CreateWotkout(object sender, EventArgs e)
        {
            WotkoutCreater();
        }
        private int GenerteExcNum(string muscleGroup)
        {
            string[] mainMuscleGruopNames = new string[] { "Chest", "Back", "Leg" },
                secondaryMuscleGruopNames = new string[] { "Shoulder", "Tricep", "Abs", "Bicep" };
            int workoutKind = int.Parse(WorkoutKindRBL.SelectedValue),
                workoutLength = int.Parse(WorkoutsLengthRBL.SelectedValue),
                numberOfExc = workoutLength / workoutKind;//the calculation is thw workout kind which is 1-3 divided by how much time the workout is supposed to be
            if(muscleGroup=="Leg"&& workoutKind == 1)
            {
                workoutLength += 3;//change is mae because plus 3 exc to the workou kin is prettey aproprite for leg day
                return workoutLength > 6 ? 6 : workoutLength ;
            }
            if (workoutLength == 5&&workoutKind==1)
            {
                return Array.IndexOf(mainMuscleGruopNames, muscleGroup) != -1 ? 3 : 2;
            }
            return numberOfExc;
        }
        protected void WotkoutCreater()
        {
            DateTime startTime = DateTime.Now,
                    runinugTime;
            int startSecond = startTime.Second;
            string[] muscleGruopNames = new string[7] { "Chest", "Shoulder", "Tricep", "Abs", "Back", "Bicep", "Leg" };
            List<string> usedExc = new List<string>();
            string lowestMuscleUsed = "";
            int max = 1;
            int numberOfExc = int.Parse(Math.Round(double.Parse(WorkoutsLengthRBL.SelectedValue) / double.Parse(WorkoutKindRBL.SelectedValue)).ToString());//the calculation is thw workout kind which is 1-3 divided by how much time the workout is supposed to be
            IntalizeMuscleDictionary(numberOfExc);
            string exc;
            Debug.WriteLine("---------------------" + numberOfExc);
            numberOfExc = numberOfExc > 5 ? 5 : numberOfExc;
            List<List<string>> excLists = new List<List<string>>();

            foreach (string muscleGroup in muscleGruopNames)
            {
                List<string> muscleGroupList = new List<string>();
                muscleGroupList.Add(muscleGroup);
                exc = PullMainExc(muscleGroup, true, usedExc.ToArray(), int.Parse(TypeOfWorkoutRBL.SelectedValue));
                muscleGroup_muscle_points = SubtractAndAddExc(exc, muscleGroup_muscle_points, "-");
                muscleGroupList.Add(exc);
                usedExc.Add(exc);
                // for (; i < numberOfExc; i++)
                //while (IsNotWorkingAll(muscleGroup, muscleGroup_muscle_points))
                for (int i = 0; i < GenerteExcNum(muscleGroup); i++)
                {


                    foreach (KeyValuePair<string, int> muscle in muscleGroup_muscle_points[muscleGroup])
                    {
                        if (max < muscle.Value)
                        {
                            lowestMuscleUsed = muscle.Key;
                            max = muscle.Value;
                        }
                        // Debug.WriteLine(lowestMuscleUsed);
                    }
                    if (lowestMuscleUsed != "")
                    {
                        exc = PullSecondaryExcs(lowestMuscleUsed, usedExc.ToArray(), muscleGroup);
                        if (exc == "")
                        {
                            Debug.WriteLine("EXC: " + exc);
                            break;
                        }
                        muscleGroupList.Add(exc);
                        usedExc.Add(exc);
                        muscleGroup_muscle_points = SubtractAndAddExc(exc, muscleGroup_muscle_points,"-");
                        //Debug.WriteLine("EXC: " + exc);
                    }
                    else
                    {
                        break;
                    }
                    
                }
                excLists.Add(muscleGroupList);
                max = 0;
                lowestMuscleUsed = "";
                runinugTime = DateTime.Now;
                if (startTime.AddSeconds(3) < runinugTime)
                {
                    Debug.WriteLine("time:" + startTime);
                    WotkoutCreater();
                }
            }
            string workoutKind = "ABC";
            switch (WorkoutKindRBL.SelectedValue)
            {
                case "3":
                    workoutKind = "FB";
                    break;
                case "2":
                    workoutKind = "AB";
                    break;
            }
            excLists =CheckIfAnyExcsRedundant(excLists, new Dictionary<string, Dictionary<string, int>>(muscleGroup_muscle_points));
            Debug.WriteLine("---------EXC LIST------------------");
            PrintPoints(muscleGroup_muscle_points);
            Dictionary<string, string> restTime = new Dictionary<string, string>(),
                repAndSetCount = new Dictionary<string, string>();
            GenerateRepsCountAndRest(repAndSetCount,restTime,excLists);
            Debug.WriteLine("here3");
            LblTable.Text = BuildTable(excLists, workoutKind,repAndSetCount,restTime);

        }
        protected List<List<string>> CheckIfAnyExcsRedundant(List<List<string>> excLists, Dictionary<string, Dictionary<string, int>> Local_muscleGroup_muscle_points)
        {
            foreach (List<string> excList in excLists)
            {
                for (int i = 1; i < excList.Count; i++)
                {
                    
                    string exc = excList[i];
                    if (!IsExcNecessary(exc))//check if after removing the exc still working all muscls
                    {
                        SubtractAndAddExc(exc, muscleGroup_muscle_points, "+");
                        excList.Remove(exc);
                        Debug.WriteLine("REMOVED: "+exc);
                    }
                }
            }
            return excLists;
        }
        protected bool IsExcNecessary(string exc)
        {
            bool isNecessary = false;
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
                    if (column.ToString() == "name")
                    {
                        continue;
                    }
                    getMusclePoint = "SELECT [" + column.ToString() + "] FROM MuscleWork WHERE name='" + exc + "'";
                    sqlCommand = new SqlCommand(getMusclePoint, connection);
                    int musclePoint = (int)sqlCommand.ExecuteScalar();
                    if (muscleGroup_muscle_points[FirstWord(column.ToString())][column.ToString()]+ musclePoint >0)//gets each colmun muslce group because first word and then by colmun and either adds or subtracts
                    {
                        isNecessary = true;
                    }
                }
            }
            return isNecessary;
        }
    
        protected string PullSecondaryExcs(string muscle, string[] usedExc, string muscleGroup)
        {
            DataSet dataset = new DataSet();
            List<string> potentialExcList = new List<string>();

            string pullExc = "SELECT * FROM MuscleWork WHERE [" + muscle + "] >0";
            
            string exc;
            string finalExc = "";
            int max = 0;
            string Difficulty = " AND (Difficulty =" + difficultyDDL.SelectedItem.Text + " OR Difficulty =" + (int.Parse(difficultyDDL.SelectedItem.Text) - 1) + ")",
            street = int.Parse(TypeOfWorkoutRBL.SelectedValue) == 1 ? " AND Street= 1" : "",
            muslceG = " AND [" + muscleGroup + "] =1";//muscle group check
            string isCompound = TypeOfWorkoutRBL.SelectedValue == "MG" ? "" : " AND IsCompound=1 ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(pullExc, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                adapter.Fill(dataset);

                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                   
                    exc = row["name"].ToString();
                    string checkIfExcGood = "SELECT COUNT(*) FROM Excrcise WHERE name='" + exc + "'" + Difficulty + street + muslceG;// +isCompound;//+ isIsolate
                    sqlCommand = new SqlCommand(checkIfExcGood, connection);
                    if (int.Parse(row[muscle].ToString()) > max && Array.IndexOf(usedExc, exc) == -1 && (int)sqlCommand.ExecuteScalar() == 1)
                    {
                        finalExc = exc;
                        max = int.Parse(row[muscle].ToString());
                    }

                }
            }
            if (finalExc == "")
            {
                Debug.WriteLine("EXC: " + pullExc);
                //  Debug.WriteLine("SELECT COUNT(*) FROM Excrcise WHERE name='" + finalExc + "'" + Difficulty + street + muslceG + isCompound);
            }
            return finalExc;
        }
        protected void PrintPoints(Dictionary<string, Dictionary<string, int>> muscleGroup_muscle_points)
        {
            foreach (KeyValuePair<string, Dictionary<string, int>> muscleGroup in muscleGroup_muscle_points)
            {
                foreach (KeyValuePair<string, int> muscle in muscleGroup.Value)
                {
                    Debug.WriteLine(muscle.Key + ": " + muscle.Value);
                }
            }
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
        protected Dictionary<string, Dictionary<string, int>> SubtractAndAddExc(string exc, Dictionary<string, Dictionary<string, int>> Local_muscleGroup_muscle_points,string mathSymbol)//mathSymbol can either be "+" or "-"
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
                    if (column.ToString() == "name")
                    {
                        continue;
                    }
                    getMusclePoint = "SELECT [" + column.ToString() + "] FROM MuscleWork WHERE name='" + exc + "'";
                    sqlCommand = new SqlCommand(getMusclePoint, connection);
                    // Debug.WriteLine("-------------------------------");
                    // Debug.WriteLine("A: "+ column.ToString());
                    //   Debug.WriteLine("B: " + FirstWord(column.ToString()));
                    // Debug.WriteLine(getMusclePoint);
                    int musclePoint = (int)sqlCommand.ExecuteScalar();
                    if (mathSymbol.Equals("-"))//gets each colmun muslce group because first word and then by colmun and either adds or subtracts
                    {
                        Local_muscleGroup_muscle_points[FirstWord(column.ToString())][column.ToString()] -= musclePoint;
                    }
                    else
                    {
                        Local_muscleGroup_muscle_points[FirstWord(column.ToString())][column.ToString()] += musclePoint;
                    }
                }
            }
            return Local_muscleGroup_muscle_points;
        }
        
        protected void GenerateRepsCountAndRest(Dictionary<string, string> repAndSetCount, Dictionary<string, string> restTime, List<List<string>> excLists)
        {
            string repAndSetCountText,
                rest;
            int time = 0;
            switch (WorkoutsFoucusRBL.SelectedValue)
            {
                case "MG":
                    {
                        foreach (List<string> excList in excLists)
                        {
                            for (int i = 1; i < excList.Count; i++)
                            {
                                string exc = excList[i];
                                if (IsExcCompound(exc))
                                {
                                    int a = Rnd.Next(1, 3);
                                    repAndSetCountText = a == 2 ? "8" : "10";
                                    repAndSetCountText += " Reps for ";
                                    a = Rnd.Next(1, 3);
                                    repAndSetCountText += a == 2 ? "3" : "4";
                                    repAndSetCountText += " Sets";
                                    time += a * 20;//actual exc time
                                    time += a * 150;//rest time
                                    rest = "2 - 3";
                                }
                                else
                                {
                                    int a = Rnd.Next(1, 3);
                                    repAndSetCountText = a == 2 ? "12" : "14";
                                    repAndSetCountText += " Reps for ";
                                    a = Rnd.Next(1, 3);
                                    repAndSetCountText += a == 2 ? "3" : "4";
                                    repAndSetCountText += " Sets";
                                    time += a * 25;//actual exc time
                                    time += a * 90;//rest time
                                    rest = "1 - 2";
                                }
                                repAndSetCount[exc] = repAndSetCountText;
                                restTime[exc] = rest + " min";
                            }
                        }
                        break;
                    }
                    default:
                    {
                        foreach (List<string> excList in excLists)
                        {
                            for (int i = 1; i < excList.Count; i++)
                            {
                                string exc = excList[i];
                                if (IsExcCompound(exc))
                                {
                                    int a = Rnd.Next(1, 3);
                                    repAndSetCountText = a == 2 ? "3" : "5";
                                    repAndSetCountText += " Reps for ";
                                    a = Rnd.Next(1, 3);
                                    repAndSetCountText += a == 2 ? "3" : "4";
                                    repAndSetCountText += " Sets";
                                    time += a * 35;//actual exc time
                                    time += a * 240;//rest time
                                    rest = "3 - 5";
                                }
                                else
                                {
                                    int a = Rnd.Next(1, 3);
                                    repAndSetCountText = a == 2 ? "8" : "10";
                                    repAndSetCountText += " Reps for ";
                                    a = Rnd.Next(1, 3);
                                    repAndSetCountText += a == 2 ? "3" : "4";
                                    repAndSetCountText += " Sets";
                                    time += a * 25;//actual exc time
                                    time += a * 90;//rest time
                                    rest = "1 - 2";
                                }
                                repAndSetCount[exc] = repAndSetCountText;
                                restTime[exc] = rest + " min";
                            }
                        }
                        break;
                    }
            }
            int maxTime;
            switch(WorkoutsLengthRBL.SelectedValue)
            {
                case "1":
                    maxTime = 45 * 60;
                    break;
                case "2":
                    maxTime = 90 * 60;
                    break;
                default:
                    maxTime = 120 * 60;
                    break;
            }
                foreach (List<string> excList in excLists)
                {
                    for (int i = 1; i < excList.Count; i++)
                    {
                        string exc = excList[i];
                        if (!IsExcCompound(exc)&& repAndSetCount[exc][11]=='4')
                        {
                            repAndSetCount[exc] = repAndSetCount[exc].Substring(0,11)+ 3 ;//removes 1 set from the excrcise
                            time -= 90;
                            break;
                        }
                    }
                    if (time > maxTime)
                        break;
                }
            Debug.WriteLine("max:" + maxTime + " myTime:" + time);
        }
        protected void PrintLists(List<List<dynamic>> excLists)
        {
            foreach (List<dynamic> list in excLists)
            {
                foreach (object str in list)
                {
                    Debug.WriteLine(str.ToString());
                }
            }
        }
    }
}