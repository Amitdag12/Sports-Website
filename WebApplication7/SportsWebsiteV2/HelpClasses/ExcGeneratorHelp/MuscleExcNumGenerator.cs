using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace SportsWebsiteV2
{
    public class MuscleExcNumGenerator
    {
        private Dictionary<string, int[,]> excNumMatrix = new Dictionary<string, int[,]>();

        public MuscleExcNumGenerator()
        {
            excNumMatrix["Chest"] = new int[,] { { 1, 2, 3 }, { 2, 3, 4 }, { 2, 4, 4 } };
            excNumMatrix["Back"] = new int[,] { { 1, 2, 3 }, { 2, 3, 4 }, { 3, 4, 4 } };
            excNumMatrix["Abs"] = new int[,] { { 0, 0, 1 }, { 1, 1, 2 }, { 1, 2, 3 } };
            excNumMatrix["Leg"] = new int[,] { { 1, 2, 2 }, { 2, 3, 4 }, { 4, 5, 6 } };
            excNumMatrix["Bicep"] = new int[,] { { 0, 0, 0 }, { 1, 2, 2 }, { 1, 2, 2 } };
            excNumMatrix["Tricep"] = new int[,] { { 0, 0, 0 }, { 1, 2, 2 }, { 1, 2, 2 } };
            excNumMatrix["Shoulder"] = new int[,] { { 0, 1, 1 }, { 1, 2, 2 }, { 1, 2, 3 } };
            excNumMatrix["Tricep&Bicep"] = new int[,] { { 1, 1, 1 }, { 0, 0, 0 }, { 0, 0, 0 } };
        }

        public int GetExcNum(int kind, int time, string muscleGroup)
        {//https://docs.google.com/spreadsheets/d/1J3hhQ73broaRTmyNAJNujNapndACeHoV4aphUL2UDaE/edit#gid=0
            Debug.WriteLine("DEBUG:" + muscleGroup + " " + excNumMatrix[muscleGroup][kind, time]);
            return excNumMatrix[muscleGroup][kind, time];
        }
    }
}