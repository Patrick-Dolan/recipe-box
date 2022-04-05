using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeBox.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecipeBox.Controllers
{
  [Authorize]
  public class RecipesController : Controller
  {
    private readonly RecipeBoxContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public RecipesController(UserManager<ApplicationUser> userManager, RecipeBoxContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userRecipes = _db.Recipes.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userRecipes);
    }

    public async Task<ActionResult> Create()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      ViewBag.TagId = new SelectList(_db.Tags.Where(entry => entry.User.Id == currentUser.Id), "TagId", "Name");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Recipe recipe, int TagId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      recipe.User = currentUser;
      _db.Recipes.Add(recipe);
      _db.SaveChanges();
      if (TagId != 0)
      {
        _db.RecipeTags.Add(new RecipeTag() {TagId = TagId, RecipeId = recipe.RecipeId});
      }
      _db.SaveChanges();
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

    public ActionResult Edit(int id)
    {
      Recipe foundRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      ViewBag.TagId = new SelectList(_db.Tags, "TagId", "Name");
      return View(foundRecipe);
    }

    [HttpPost]
    public ActionResult Edit(Recipe recipe, int TagId)
    {
      if (recipe.Name == null || recipe.Ingredients == null || recipe.Instructions == null)
      {
        return RedirectToAction("Edit", new { id = recipe.RecipeId});
      }
      if (TagId != 0)
      {
        _db.RecipeTags.Add(new RecipeTag() {TagId = TagId, RecipeId = recipe.RecipeId});
      }
      _db.Entry(recipe).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Recipe foundRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      return View(foundRecipe);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Recipe foundRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      _db.Recipes.Remove(foundRecipe);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public async Task<ActionResult> AddTag(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      ViewBag.TagId = new SelectList(_db.Tags.Where(entry => entry.User.Id == currentUser.Id), "TagId", "Name");
      Recipe foundRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      return View(foundRecipe);
    }

    [HttpPost]
    public ActionResult AddTag(Recipe recipe, int TagId)
    {
      if (TagId != 0)
      {
        _db.RecipeTags.Add(new RecipeTag() {TagId = TagId, RecipeId = recipe.RecipeId});
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteTag(int joinId)
    {
        var joinEntry = _db.RecipeTags.FirstOrDefault(entry => entry.RecipeTagId == joinId);
        _db.RecipeTags.Remove(joinEntry);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
  }
}