using System.Collections.Generic;

namespace TagBox.Models
{
  public class Tag
  {
    private readonly RecipeBoxContext _db;
    public Tag()
    {
      this.JoinEntities = new HashSet<RecipeTag>();
    }
    public int TagId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<RecipeTag> JoinEntities { get; set; }
  }
}