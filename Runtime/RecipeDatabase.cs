using System.Collections.Generic;
using UnityEngine;
using CupkekGames.InventorySystem;
using CupkekGames.KeyValueDatabases;
using System;

namespace CupkekGames.InventorySystem.Crafting
{
    public class RecipeDatabase : KeyValueDatabaseMonoSO<string, RecipeSO>
    {
        [SerializeField] private RecipeSettingsSO _settings;
        public RecipeSettingsSO Settings => _settings;

        public RecipeSearchResult Search(
            IngredientEssenceRatio ingredientEssenceRatio,
            IInventoryItemDatabase itemDatabase,
            Func<InventoryItemDefinition, bool> resultFilter = null)
        {
            float minDistance = float.MaxValue;
            string result = null;

            foreach (string key in Keys)
            {
                RecipeSO recipeSO = GetValue(key);

                InventoryItemDefinition resultDefinition = recipeSO.Recipe.GetResultSO(itemDatabase);
                if (resultDefinition == null)
                    continue;

                if (resultFilter != null && !resultFilter(resultDefinition))
                {
                  continue;
                }

                float distance = recipeSO.Recipe.Distance(ingredientEssenceRatio);

                if (distance < minDistance)
                {
                  minDistance = distance;
                  result = key;
                }
            }

            return new RecipeSearchResult(result, minDistance);
        }

        public List<string> Filter(
            IInventoryItemDatabase itemDatabase,
            Func<InventoryItemDefinition, bool> resultFilter,
            ICollection<string> unlocked,
            bool showLocked)
        {
            List<string> filtered = new List<string>();

            foreach (string key in Keys)
            {
                RecipeSO recipeSO = GetValue(key);

                if (!showLocked && !unlocked.Contains(key))
                {
                    // Dont add locked
                    continue;
                }

                InventoryItemDefinition resultDefinition = recipeSO.Recipe.GetResultSO(itemDatabase);
                if (resultDefinition == null)
                    continue;

                if (resultFilter != null && !resultFilter(resultDefinition))
                {
                    continue;
                }

                filtered.Add(key);
            }

            return filtered;
        }
    }
}
