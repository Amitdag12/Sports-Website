using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace SportsWebsiteV2
{
    public class ProgramMaker
    {
        //diagram link https://app.diagrams.net/#G1g3-DBfHIAhL6WCZtoql0fXmQ1rUyPgZ_
        // constants
        private const string select = "SELECT * FROM Excrcise WHERE ";

        private const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\Excrcises.mdf';Integrated Security=True";

        //----------------------------------
        private Random Rnd = new Random();

        //properties
        private ExerciseList exerciseList;//where the excs are stored

        private string exerciseTable;
        private int time;
        private int kind;
        private MicroMuscleGeter microMuscleGenerator = new MicroMuscleGeter();
        private MuscleExcNumGenerator muscleExcNum = new MuscleExcNumGenerator();
        //------------------------

        public ProgramMaker(int time, int kind)
        {
            exerciseList = new ExerciseList();
            this.time = time;
            this.kind = kind;
        }

        public string GetProgram()
        {
            ExerciseGenerator();
            exerciseTable = CreateTable();
            return exerciseTable;
        }

        private void ExerciseGenerator()
        {//generates all exc for program
            List<string> muscleGroupNames = new List<string> { "Chest", "Shoulder", "Tricep", "Abs", "Back", "Bicep", "Leg" };
            if (kind == 0)
            {
                muscleGroupNames.Add("Tricep&Bicep");
            }
            foreach (string muscleGroup in muscleGroupNames)
            {
                if (IsMuscleBig(muscleGroup))
                {
                    SelectForBigMuscle(muscleGroup);
                }
                else
                {
                    SelectForSmallMuscle(muscleGroup);
                }
            }
        }

        private bool IsExcCompound(string exc)
        {
            string query = "SELECT count(*) FROM Excrcise WHERE name='" + exc + "' AND IsCompound=1";
            return Execute_Query_Check(query);
        }

        private Exercise PullExc(string muscleGroup, int IsCompound)
        {
            string microMuscle = microMuscleGenerator.GetMicroMuscle(muscleGroup);
            DataSet dataSet = new DataSet();
            Exercise exc;
            string commandUserString = "SELECT name FROM Excrcise WHERE " + muscleGroup + "=" + 1 + " AND IsCompound=" + IsCompound,
             excName;
            if (IsCompound == 0)
            {
                commandUserString = "(" + commandUserString + ") EXCEPT (SELECT name FROM MuscleWork WHERE [" + microMuscle + "] < 5)";
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Debug.WriteLine(commandUserString);
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(commandUserString, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                adapter.Fill(dataSet);
            }
            int rowNum = dataSet.Tables[0].Rows.Count;
            if (rowNum != 0)
            {
                do
                {
                    excName = dataSet.Tables[0].Rows[Rnd.Next(0, rowNum)]["name"].ToString();//get a random exc that meets the requirement
                    exc = new Exercise(excName, IsExcCompound(excName));
                    Debug.WriteLine(excName);
                } while (exerciseList.GetExcList(muscleGroup).Any(x => x.name == excName));
                return exc;
            }
            else
            {
                return PullExc(muscleGroup, IsCompound);
            }
            return new Exercise("null", true);
        }

        private bool IsMuscleBig(string muscle)
        {//checks if a muscle is a big muscle
            string[] majorMuscleGroup = new string[] { "Chest", "Back", "Leg" };
            return Array.IndexOf(majorMuscleGroup, muscle) != -1;
        }

        private void SelectForBigMuscle(string muscleGroup)
        {//picks exc for big muscle
            Exercise smallExercise;
            Exercise bigExercise = PullExc(muscleGroup, 1);//selects the big compound exc for the big muscle group the 1 is as true
            exerciseList.AddExercise(muscleGroup, bigExercise.name, true);
            for (int i = 0; i < muscleExcNum.GetExcNum(kind, time, muscleGroup) - 1; i++)
            {
                smallExercise = PullExc(muscleGroup, 0);
                exerciseList.AddExercise(muscleGroup, smallExercise.name, IsExcCompound(smallExercise.name));
            }
        }

        private void SelectForSmallMuscle(string givenMuscleGroup)
        {//picks exc for small muscle
            Exercise smallExercise;
            string muscleGroup = givenMuscleGroup;
            int i = 0,
                excNum = muscleExcNum.GetExcNum(kind, time, muscleGroup);

            //Special case
            bool isSpecial = givenMuscleGroup == "Tricep&Bicep";
            //Special case

            for (; i < excNum; i++)
            {
                if (isSpecial)
                {
                    muscleGroup = muscleGroup == "Bicep" ? "Tricep" : "Bicep";//checks if its first run put bicep and if second put tricep
                    smallExercise = PullExc(muscleGroup, 0);
                    muscleGroup = muscleGroup == "Bicep" ? "Tricep" : "Bicep";
                    smallExercise.name += " X " + PullExc(muscleGroup, 0).name;
                }
                else
                {
                    smallExercise = PullExc(muscleGroup, 0);
                }
                exerciseList.AddExercise(givenMuscleGroup, smallExercise.name, IsExcCompound(smallExercise.name));
            }
        }

        private bool Execute_Query_Check(string query)//executes a given check query
        {
            string commandUserString = query;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(commandUserString, connection);

                return (int)sqlCommand.ExecuteScalar() == 1;
            }
        }

        private string CreateTable()
        {
            TableConstractur table = new TableConstractur();
            Dictionary<string, string[]> days = table.GetDaysDic(this.kind);
            foreach (KeyValuePair<string, string[]> day in days)
            {
                table.AddDayHeadline(day.Key);
                foreach (string muscleGroup in day.Value)
                {
                    List<Exercise> excList = exerciseList.GetExcList(muscleGroup);
                    if (excList.Count == 0)
                        continue;
                    table.AddMuscleHeadline(muscleGroup);
                    foreach (Exercise exc in excList)
                    {
                        table.AddExcToTable(exc);
                    }
                }
            }
            return table.ToString();
        }
    }
}