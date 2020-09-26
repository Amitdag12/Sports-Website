using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsWebsiteV2
{
    public class ExerciseList
    {
        public Dictionary<string, List<Exercise>> exerciseDictionary;

        public ExerciseList()
        {
            if (exerciseDictionary == null)
            {
                string[] muscleGroupNames = new string[] { "Chest", "Shoulder", "Tricep", "Abs", "Back", "Bicep", "Leg", "Tricep&Bicep" };
                exerciseDictionary = new Dictionary<string, List<Exercise>>();
                foreach (string muscleGroup in muscleGroupNames)
                {
                    exerciseDictionary[muscleGroup] = new List<Exercise>();
                }
            }
        }

        public void AddExercise(string MuscleGroup, string name, bool IsCompund)
        {
            Exercise exercise = new Exercise(name, IsCompund);
            List<Exercise> ExcList = exerciseDictionary[MuscleGroup];
            ExcList.Add(exercise);
            exerciseDictionary[MuscleGroup] = ExcList;
        }

        public List<Exercise> GetExcList(string muscleGroup)
        {
            return exerciseDictionary[muscleGroup];
        }
    }
}