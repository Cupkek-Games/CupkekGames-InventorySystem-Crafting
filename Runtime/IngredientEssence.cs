using System;
using System.Collections.Generic;
using UnityEngine;

namespace CupkekGames.InventorySystem.Crafting
{
    /// <summary>
    /// Per-essence value pair. <see cref="EssenceKey"/> is the <see cref="IngredientEssenceTypeSO"/>
    /// asset name (catalog key) — stable across reorderings of the essence-type registry.
    /// </summary>
    [Serializable]
    public class IngredientEssenceValue
    {
        public string EssenceKey;
        public int Value;
    }

    /// <summary>
    /// Essence values keyed by stable <see cref="IngredientEssenceTypeSO"/> identifier (catalog key).
    /// Replaces the old positional <c>int[5]</c> shape so reordering essence types doesn't corrupt data.
    /// </summary>
    [Serializable]
    public class IngredientEssence
    {
        [SerializeField] private List<IngredientEssenceValue> _values = new List<IngredientEssenceValue>();
        public IReadOnlyList<IngredientEssenceValue> Values => _values;

        public int BonusValue;

        public IngredientEssence() { }

        public int GetValue(string essenceKey)
        {
            if (string.IsNullOrEmpty(essenceKey)) return 0;
            for (int i = 0; i < _values.Count; i++)
            {
                if (_values[i].EssenceKey == essenceKey) return _values[i].Value;
            }
            return 0;
        }

        public int GetValue(IngredientEssenceTypeSO essenceType) =>
            essenceType != null ? GetValue(essenceType.name) : 0;

        public void SetValue(string essenceKey, int value)
        {
            if (string.IsNullOrEmpty(essenceKey)) return;
            for (int i = 0; i < _values.Count; i++)
            {
                if (_values[i].EssenceKey == essenceKey)
                {
                    _values[i].Value = value;
                    return;
                }
            }
            _values.Add(new IngredientEssenceValue { EssenceKey = essenceKey, Value = value });
        }

        public void Add(IngredientEssence other)
        {
            if (other == null) return;
            foreach (IngredientEssenceValue entry in other._values)
            {
                SetValue(entry.EssenceKey, GetValue(entry.EssenceKey) + entry.Value);
            }
        }

        public void Remove(IngredientEssence other)
        {
            if (other == null) return;
            foreach (IngredientEssenceValue entry in other._values)
            {
                SetValue(entry.EssenceKey, GetValue(entry.EssenceKey) - entry.Value);
            }
        }

        public int GetTotalValue()
        {
            int total = BonusValue;
            for (int i = 0; i < _values.Count; i++)
            {
                total += _values[i].Value;
            }
            return total;
        }

        public int GetCookingTime(RecipeSettingsSO settings) => settings.GetCookingTime(GetTotalValue());
    }
}
