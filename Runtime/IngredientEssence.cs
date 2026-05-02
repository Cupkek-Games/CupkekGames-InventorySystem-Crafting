using System;
using UnityEngine;

namespace CupkekGames.InventorySystem.Crafting
{
  [Serializable]
  public class IngredientEssence
  {
    [SerializeField] private int[] _values = new int[5];
    public int[] Values => _values;
    public int Count => _values.Length;
    public int BonusValue = 0;

    public IngredientEssence() { }

    public IngredientEssence(int size)
    {
      _values = new int[size];
    }

    public void SetValue(int index, int value)
    {
      if (index >= 0 && index < _values.Length)
        _values[index] = value;
    }

    public int GetValue(int index)
    {
      return (index >= 0 && index < _values.Length) ? _values[index] : 0;
    }

    public void Add(IngredientEssence other)
    {
      int count = Math.Min(_values.Length, other._values.Length);
      for (int i = 0; i < count; i++)
        _values[i] += other._values[i];
    }

    public void Remove(IngredientEssence other)
    {
      int count = Math.Min(_values.Length, other._values.Length);
      for (int i = 0; i < count; i++)
        _values[i] -= other._values[i];
    }

    public int GetTotalValue()
    {
      int total = BonusValue;
      for (int i = 0; i < _values.Length; i++)
        total += _values[i];
      return total;
    }

    public int GetCookingTime(RecipeSettingsSO settings)
    {
      return settings.GetCookingTime(GetTotalValue());
    }
  }
}
