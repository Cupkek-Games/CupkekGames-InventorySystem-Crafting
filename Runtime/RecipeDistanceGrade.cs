using System;
using CupkekGames.Luna;

namespace CupkekGames.InventorySystem.Crafting
{
  public enum RecipeDistanceGrade
  {
    PERFECT = 0,
    BALANCED = 1,
    OFF = 2,
    CHAOTIC = 3
  }

  public static class RecipeDistanceGradeExtensions
  {
    public static string GetDescription(this RecipeDistanceGrade type)
    {
      return type switch
      {
        RecipeDistanceGrade.PERFECT => "The essences are perfectly balanced!",
        RecipeDistanceGrade.BALANCED => "The mix is balanced, but there's room for improvement.",
        RecipeDistanceGrade.OFF => "The balance is slightly off, adjustments needed.",
        RecipeDistanceGrade.CHAOTIC => "The mix is chaotic and needs significant correction.",
        _ => throw new Exception("RecipeDistanceGrade not found"),
      };
    }
    public static string GetDisplayName(this RecipeDistanceGrade type)
    {
      return char.ToUpper(type.ToString()[0]) + type.ToString()[1..].ToLower();
    }
    public static string GetTextColor(this RecipeDistanceGrade type)
    {
      return type switch
      {
        RecipeDistanceGrade.PERFECT => RichTextColor.AQUA,
        RecipeDistanceGrade.BALANCED => RichTextColor.YELLOW,
        RecipeDistanceGrade.OFF => RichTextColor.ORANGE,
        RecipeDistanceGrade.CHAOTIC => RichTextColor.RED,
        _ => throw new Exception("RecipeDistanceGrade not found"),
      };
    }
  }
}
