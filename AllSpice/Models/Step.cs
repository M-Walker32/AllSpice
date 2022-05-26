namespace AllSpice.Models
{
  public class Step
  {
    public int Id { get; set; }
    public string Position { get; set; }
    public string Body { get; set; }
    public string RecipeId { get; set; }
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }
  }
}