using System;
using System.Collections.Generic;
using UnityEngine;

namespace CupkekGames.InventorySystem.Crafting
{
    /// <summary>Per-essence ratio (0..1) keyed by stable essence-type id.</summary>
    [Serializable]
    public class IngredientEssenceRatioValue
    {
        public string EssenceKey;
        public float Value;
    }

    /// <summary>
    /// Recipe ratio profile keyed by <see cref="IngredientEssenceTypeSO"/> name.
    /// Designer authors per-essence target proportions; <see cref="Distance"/> compares against
    /// a sampled <see cref="IngredientEssence"/> for recipe matching.
    /// </summary>
    [Serializable]
    public class IngredientEssenceRatio
    {
        [SerializeField] private List<IngredientEssenceRatioValue> _values = new List<IngredientEssenceRatioValue>();
        public IReadOnlyList<IngredientEssenceRatioValue> Values => _values;

        public IngredientEssenceRatio() { }

        public IngredientEssenceRatio(IngredientEssence essence)
        {
            if (essence == null) return;

            int total = 0;
            foreach (IngredientEssenceValue v in essence.Values) total += v.Value;
            if (total <= 0) return;

            foreach (IngredientEssenceValue v in essence.Values)
            {
                _values.Add(new IngredientEssenceRatioValue
                {
                    EssenceKey = v.EssenceKey,
                    Value = (float)v.Value / total,
                });
            }
        }

        public float GetValue(string essenceKey)
        {
            if (string.IsNullOrEmpty(essenceKey)) return 0f;
            for (int i = 0; i < _values.Count; i++)
            {
                if (_values[i].EssenceKey == essenceKey) return _values[i].Value;
            }
            return 0f;
        }

        public void SetValue(string essenceKey, float value)
        {
            if (string.IsNullOrEmpty(essenceKey)) return;
            value = Mathf.Clamp01(value);
            for (int i = 0; i < _values.Count; i++)
            {
                if (_values[i].EssenceKey == essenceKey)
                {
                    _values[i].Value = value;
                    return;
                }
            }
            _values.Add(new IngredientEssenceRatioValue { EssenceKey = essenceKey, Value = value });
        }

        public override bool Equals(object obj)
        {
            if (obj is not IngredientEssenceRatio other) return false;
            if (_values.Count != other._values.Count) return false;
            // Compare key-aligned (order-independent) by checking each entry against the other side.
            foreach (IngredientEssenceRatioValue v in _values)
            {
                if (!Mathf.Approximately(v.Value, other.GetValue(v.EssenceKey))) return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            foreach (IngredientEssenceRatioValue v in _values) hash.Add(v.EssenceKey);
            return hash.ToHashCode();
        }

        /// <summary>Euclidean distance between two ratio profiles, summed over the union of essence keys.</summary>
        public float Distance(IngredientEssenceRatio other)
        {
            if (other == null) return float.MaxValue;
            HashSet<string> keys = new HashSet<string>();
            foreach (IngredientEssenceRatioValue v in _values) keys.Add(v.EssenceKey);
            foreach (IngredientEssenceRatioValue v in other._values) keys.Add(v.EssenceKey);

            float sum = 0f;
            foreach (string key in keys)
            {
                float diff = GetValue(key) - other.GetValue(key);
                sum += diff * diff;
            }
            return Mathf.Sqrt(sum);
        }

        /// <summary>
        /// Returns an <see cref="IngredientEssence"/> with the smallest integer values matching the ratio
        /// (used to display "minimal recipe" hints in UI).
        /// </summary>
        public IngredientEssence GetMin()
        {
            float minValue = 1f;
            foreach (IngredientEssenceRatioValue v in _values)
            {
                if (v.Value > 0f && v.Value < minValue) minValue = v.Value;
            }

            float scaleFactor = minValue > 0f ? 1f / minValue : 1f;
            IngredientEssence result = new IngredientEssence();
            foreach (IngredientEssenceRatioValue v in _values)
            {
                result.SetValue(v.EssenceKey, Mathf.RoundToInt(v.Value * scaleFactor));
            }
            return result;
        }
    }
}
