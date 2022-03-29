namespace RecipeBox.Models
{
  public class Recipe
  {
    private readonly RecipeBoxContext _db;

    public Recipe()
    {
      this.JoinEntities = new HashSet<RecipeTag>();
    }

    public int RecipeId { get; set; }
    public string Name { get; set; }
    public string Ingredients { get; set; }
    public string Instructions { get; set; }
    public virtual ICollection<RecipeTag> JoinEntities { get; set; }
  }
}