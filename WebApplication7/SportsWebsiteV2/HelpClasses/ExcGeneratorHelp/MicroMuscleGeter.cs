using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsWebsiteV2
{
    public class MicroMuscleGeter
    {
        private Random Rnd = new Random();
        private string[] muscleGroupNames = new string[] { "Chest", "Shoulder", "Tricep", "Abs", "Back", "Bicep", "Leg" };

        private string[] names = new string[20]{"Shoulder  front deltoid","Shoulder  rear deltoid","Shoulder lateral deltoid","Bicep short",
                "Bicep long","Bicep brachialis","Tricep long","Tricep medial","Back traps","Back lats","Back teres major","Chest upper","Chest lower",
                "Chest middle","Abs oblique","Abs abdominal","Leg glutes","Leg quads","Leg Hamstring","Leg Calves"};

        private Dictionary<string, List<string>> microMuscleAssigment = new Dictionary<string, List<string>>();

        public MicroMuscleGeter()
        {
            if (Rnd.Next(0, 2) != 1)
            {
                names.Reverse();
            }
            if (Rnd.Next(0, 2) != 0)
            {
                Array.Sort(names);
            }
            foreach (string muscleGroup in muscleGroupNames)
            {
                microMuscleAssigment[muscleGroup] = new List<string>();
                microMuscleAssigment[muscleGroup].Add("1");
            }
            foreach (string name in names)
            {
                microMuscleAssigment[FirstWord(name)].Add(name);
            }
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

        public string GetMicroMuscle(string muscleGroup)
        {
            List<string> microList = microMuscleAssigment[muscleGroup];
            int index = int.Parse(microList[0]);
            if (index > microList.Count - 1)
            {
                index = 1;
            }
            microList[0] = index + 1 + "";
            return microMuscleAssigment[muscleGroup][index];
        }
    }
}