namespace AllSpice.Models
{
  public class Favorite
  {
    public int Id { get; set; }
    public string RecipeId { get; set; }
    public string AccountId { get; set; }
    public string CreatorId { get; set; }
  }
}