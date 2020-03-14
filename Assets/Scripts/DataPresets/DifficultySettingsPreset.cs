using UnityEditor;
using UnityEngine;

namespace DataPresets
{
    [CreateAssetMenu(fileName = "NewDifficultySettingsPreset",menuName = "DifficultySettingsPreset",order = 3)]
    public class DifficultySettingsPreset : ScriptableObject
    {
        [SerializeField] private int circlesCount;

        [Header("Weights")]
        [SerializeField] private int minWeightOfGame;
        [SerializeField] private int maxWeightOfGame;
        [SerializeField] private int levelWeightRange;

        [Header("Next level minimum weight function")]
        [SerializeField] private int levelCountToRiseWeight;
        [SerializeField] private int amountOfWeightToRise;
    
        [SerializeField] private int levelCountToLowerWeight;
        [SerializeField] private int amountOfWeightToLower;
    
        private AnimationCurve nextLevelMinWeightFunction;

        public int CirclesCount => circlesCount;

        private void BuildDifficultyCurve()
        {
            nextLevelMinWeightFunction = new AnimationCurve();
        
            nextLevelMinWeightFunction.AddKey(0, 0);
            nextLevelMinWeightFunction.AddKey(levelCountToRiseWeight, amountOfWeightToRise);
            nextLevelMinWeightFunction.AddKey(levelCountToRiseWeight + levelCountToLowerWeight,
                amountOfWeightToRise - amountOfWeightToLower);

            for(int i = 0;i<nextLevelMinWeightFunction.length;i++)
            {
                AnimationUtility.SetKeyLeftTangentMode(nextLevelMinWeightFunction,i,AnimationUtility.TangentMode.Linear);
                AnimationUtility.SetKeyRightTangentMode(nextLevelMinWeightFunction,i,AnimationUtility.TangentMode.Linear);
            }
        }

        public void CalculateLevelWeights(int passedLevels, out int minLevelWeight, out int maxLevelWeight)
        {
            if(nextLevelMinWeightFunction.length==0)
                BuildDifficultyCurve();
        
            int xSteps = levelCountToLowerWeight + levelCountToRiseWeight;
            int wholeDifficultyCurveCount = passedLevels / xSteps;
            int x = passedLevels - wholeDifficultyCurveCount * xSteps;

            minLevelWeight = minWeightOfGame + wholeDifficultyCurveCount * (int) nextLevelMinWeightFunction.Evaluate(xSteps) +
                             (int)nextLevelMinWeightFunction.Evaluate(x);
            maxLevelWeight = Mathf.Clamp(minLevelWeight + levelWeightRange-1,minLevelWeight,maxWeightOfGame);
        }
    }
}
