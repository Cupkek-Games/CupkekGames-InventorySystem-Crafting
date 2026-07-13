using System;
using System.Collections.Generic;
using UnityEngine;

namespace CupkekGames.InventorySystem.Crafting
{
    [CreateAssetMenu(fileName = "RecipeSettings", menuName = "CupkekGames/Crafting/Recipe Settings")]
    public class RecipeSettingsSO : ScriptableObject
    {
        [Serializable]
        public class DistanceGradeThreshold
        {
            public float MaxDistance;
            public RecipeDistanceGrade Grade;
        }

        [SerializeField] private List<int> _tierThresholds = new() { 10, 30, 60, 100 };
        public IReadOnlyList<int> TierThresholds => _tierThresholds;
        [SerializeField] private List<DistanceGradeThreshold> _distanceGradeThresholds = new();
        [SerializeField] private RecipeDistanceGrade _defaultGrade = RecipeDistanceGrade.CHAOTIC;

        [Header("Essence Types")]
        [SerializeField] private List<IngredientEssenceTypeSO> _essenceTypes = new();
        public IReadOnlyList<IngredientEssenceTypeSO> EssenceTypes => _essenceTypes;

        [Header("Cooking")]
        [SerializeField] private int _cookingTimeDivisor = 5;
        public int CookingTimeDivisor => _cookingTimeDivisor;

        public int GetMaxTier(int totalEssence)
        {
            for (int i = _tierThresholds.Count - 1; i >= 0; i--)
            {
                if (totalEssence > _tierThresholds[i])
                    return i + 1;
            }
            return 0;
        }

        public int GetMaxTier(IngredientEssence totalEssence)
        {
            return GetMaxTier(totalEssence.GetTotalValue());
        }

        public RecipeDistanceGrade GetDistanceGrade(float distance)
        {
            foreach (var threshold in _distanceGradeThresholds)
            {
                if (distance < threshold.MaxDistance)
                    return threshold.Grade;
            }
            return _defaultGrade;
        }

        public int CalculateFinalTier(int maxTier, RecipeDistanceGrade distanceGrade)
        {
            return maxTier - (int)distanceGrade;
        }

        public int GetCookingTime(int totalEssenceValue)
        {
            return _cookingTimeDivisor > 0 ? totalEssenceValue / _cookingTimeDivisor : totalEssenceValue;
        }
    }
}
