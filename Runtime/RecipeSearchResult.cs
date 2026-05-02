namespace CupkekGames.InventorySystem.Crafting
{
  public class RecipeSearchResult
  {
    public string Key;
    public float Distance;
    public RecipeSearchResult(string key, float distance)
    {
      Key = key;
      Distance = distance;
    }
    public RecipeDistanceGrade GetRecipeDistanceGrade(RecipeSettingsSO settings)
    {
      return settings.GetDistanceGrade(Distance);
    }
  }
}
