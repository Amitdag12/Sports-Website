using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsWebsiteV2
{
    public class TableConstractur
    {
        private string table;

        public TableConstractur()
        {
            table = "";
        }

        public Dictionary<string, string[]> GetDaysDic(int kind)
        {
            Dictionary<string, string[]> days = new Dictionary<string, string[]>();
            switch (kind)
            {
                case 1:
                    days["PUSH"] = new string[] { "Chest", "Shoulder", "Tricep", "Abs" };
                    days["PULL"] = new string[] { "Leg", "Back", "Bicep" };
                    break;

                case 2:
                    days["PUSH"] = new string[] { "Chest", "Shoulder", "Tricep" };
                    days["PULL"] = new string[] { "Back", "Bicep", "Abs" };
                    days["LEGS"] = new string[] { "Leg" };
                    break;

                default:
                    days["FULLBODY"] = new string[] { "Leg", "Chest", "Back", "Shoulder", "Tricep&Bicep", "Abs" };
                    break;
            }
            return days;
        }

        public void AddDayHeadline(string day)
        {
            table += "<table  border='1' style='width: 40 %; height: 70 %; text - align:center; margin: auto'class='Centerd NiceBacground'>\n";
            table += "<tr>\n" +
        "  <th colspan = '2' class='headline'>\n" +
                day +
            " </th>\n" +
            "  <th colspan = '2' class='headline'>Rest time</ th > \n" +
        " </tr>\n";
        }

        public void AddExcToTable(Exercise exc)
        {
            table +=
                       "   <tr>\n" +
                       "       <td><a href = '" + (exc.link) + "' >" + exc.name + "</a></td>\n" +
                     "     <td>" + (exc.reps + "X" + exc.sets) + "</td>\n" +
                     "     <td>" + (exc.rest) + "</td>\n" +
                     " </tr>\n";
        }

        public void AddMuscleHeadline(string muscleGroup)
        {
            string addText = "";
            if (muscleGroup == "Tricep&Bicep")
            {
                addText = " These exercises will be preformed without rest between ";
            }
            table +=
                "<tr>\n" +
                      "<th colspan = '2' class='headline'>" + muscleGroup + "<br>" + addText + "</th>\n" +
                " </tr> \n";
        }

        public override string ToString()
        {
            return table;
        }
    }
}