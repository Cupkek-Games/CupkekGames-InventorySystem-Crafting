using System;
using UnityEngine;

namespace CupkekGames.InventorySystem.Crafting
{
  [Serializable]
  public class IngredientEssenceRatio
  {
    [SerializeField] private float[] _values = new float[5];
    public float[] Values => _values;
    public int Count => _values.Length;

    public float GetValue(int index)
    {
      return (index >= 0 && index < _values.Length) ? _values[index] : 0f;
    }

    public void SetValue(int index, float value)
    {
      if (index >= 0 && index < _values.Length)
        _values[index] = Mathf.Clamp01(value);
    }

    public IngredientEssenceRatio() { }

    public IngredientEssenceRatio(IngredientEssence essence)
    {
      int count = essence.Count;
      _values = new float[count];
      float total = 0f;
      for (int i = 0; i < count; i++)
        total += essence.GetValue(i);

      if (total > 0f)
      {
        for (int i = 0; i < count; i++)
          _values[i] = essence.GetValue(i) / total;
      }
    }

    public override bool Equals(object obj)
    {
      if (obj == null || GetType() != obj.GetType())
        return false;

      IngredientEssenceRatio other = (IngredientEssenceRatio)obj;
      if (_values.Length != other._values.Length)
        return false;

      for (int i = 0; i < _values.Length; i++)
      {
        if (!Mathf.Approximately(_values[i], other._values[i]))
          return false;
      }
      return true;
    }

    public override int GetHashCode()
    {
      HashCode hash = new();
      for (int i = 0; i < _values.Length; i++)
        hash.Add(_values[i]);
      return hash.ToHashCode();
    }

    public float Distance(IngredientEssenceRatio other)
    {
      float sum = 0f;
      int count = Math.Min(_values.Length, other._values.Length);
      for (int i = 0; i < count; i++)
        sum += (float)Math.Pow(_values[i] - other._values[i], 2);
      return (float)Math.Sqrt(sum);
    }

    private bool ValidateValues()
    {
      float total = 0f;
      for (int i = 0; i < _values.Length; i++)
        total += _values[i];
      return Mathf.Approximately(total, 1f);
    }

    public IngredientEssence GetMin()
    {
      float minValue = 1f;
      for (int i = 0; i < _values.Length; i++)
      {
        if (_values[i] > 0f && _values[i] < minValue)
          minValue = _values[i];
      }

      float scaleFactor = 1f / minValue;
      IngredientEssence result = new IngredientEssence(_values.Length);
      for (int i = 0; i < _values.Length; i++)
        result.SetValue(i, Mathf.RoundToInt(_values[i] * scaleFactor));
      return result;
    }

    public void Debug()
    {
      string debugMessage = "IngredientEssenceRatio Debug:";
      for (int i = 0; i < _values.Length; i++)
        debugMessage += $" [{i}]={_values[i]}";
      if (!ValidateValues())
        debugMessage += " (Warning: Values do not sum to 1!)";
      UnityEngine.Debug.Log(debugMessage);
    }
  }
}
