using System;
using UnityEngine;
using CupkekGames.InventorySystem;

namespace CupkekGames.InventorySystem.Crafting
{
    [Serializable]
    public class Recipe
    {
        [SerializeField] private InventoryItemReference _result;
        public InventoryItemReference Result => _result;
        [SerializeField] private IngredientEssenceRatio _essenceRatio;
        public IngredientEssenceRatio EssenceRatio => _essenceRatio;

        public Recipe(InventoryItemReference result)
        {
            _result = result;
        }

        public InventoryItem GetResultClone(IInventoryItemDatabase itemDatabase)
        {
            InventoryItem result = itemDatabase.CreateItem(_result);

            return result;
        }
        public InventoryItemDefinition GetResultSO(IInventoryItemDatabase itemDatabase)
        {
            return itemDatabase.GetItemDefinition(_result);
        }

        public float Distance(IngredientEssence essence)
        {
            IngredientEssenceRatio other = new IngredientEssenceRatio(essence);

            return Distance(other);
        }
        public float Distance(IngredientEssenceRatio other)
        {
            return _essenceRatio.Distance(other);
        }
    }
}
