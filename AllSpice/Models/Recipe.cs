using System;

namespace AllSpice.Models
{
  public class Recipe
  {
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Picture { get; set; }
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Category { get; set; }
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }
  }
  // TODO ViewModel will go here for favorites
  public class FavoriteModel : Recipe
  {
    public int FavoriteId { get; set; }
    public string AccountId { get; set; }

  }
}