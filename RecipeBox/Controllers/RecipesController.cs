using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeBox.Models;
using System.Linq;

namespace RecipeBox.Controllers
{
  public class RecipesController : Controller
  {
    private readonly RecipeBoxContext _db;
    public RecipesController(RecipeBoxContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Recipes.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.TagId = new SelectList(_db.Tags, "TagId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Recipe recipe, int TagId)
    {
      _db.Recipes.Add(recipe);
      _db.SaveChanges();
      if (TagId != 0)
      {
        _db.RecipeTags.Add(new RecipeTag() {TagId = TagId, RecipeId = recipe.RecipeId});
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Recipe foundRecipe = _db.Recipes
        .Include(recipe => recipe.JoinEntities)
        .ThenInclude(join => join.Tag)
        .FirstOrDefault(recipe => recipe.RecipeId == id);
      return View(foundRecipe);
    }
  }
}