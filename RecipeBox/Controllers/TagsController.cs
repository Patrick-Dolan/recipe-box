using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeBox.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace RecipeBox.Controllers
{
  [Authorize]
  public class TagsController : Controller
  {
    private readonly RecipeBoxContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TagsController(UserManager<ApplicationUser> userManager, RecipeBoxContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userTags = _db.Tags.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userTags);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Tag tag, int RecipeId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      tag.User = currentUser;
      _db.Tags.Add(tag);
      if (RecipeId != 0)
      {
        _db.RecipeTags.Add(new RecipeTag{ TagId = tag.TagId, RecipeId = RecipeId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Tag foundTag = _db.Tags
        .Include(tag => tag.JoinEntities)
        .ThenInclude(join => join.Recipe)
        .FirstOrDefault(model => model.TagId == id);
      return View(foundTag);
    }

    public ActionResult Edit(int id)
    {
      Tag foundTag = _db.Tags.FirstOrDefault(model => model.TagId == id);
      return View(foundTag);
    }

    [HttpPost]
    public ActionResult Edit(Tag tag)
    {
      if (tag.Name == null)
      {
        return RedirectToAction("Edit", new { id = tag.TagId});
      }
      _db.Entry(tag).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = tag.TagId});
    }

    public ActionResult Delete(int id)
    {
      Tag foundTag = _db.Tags.FirstOrDefault(model => model.TagId == id);
      return View(foundTag);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Tag foundTag = _db.Tags.FirstOrDefault(model => model.TagId == id);
      _db.Tags.Remove(foundTag);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public async Task<ActionResult> AddRecipe(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      ViewBag.RecipeId = new SelectList(_db.Recipes.Where(entry => entry.User.Id == currentUser.Id), "RecipeId", "Name");
      Tag foundTag = _db.Tags.FirstOrDefault(model => model.TagId == id);
      return View(foundTag);
    }

    [HttpPost]
    public ActionResult AddRecipe(Tag tag, int RecipeId)
    {
      if (RecipeId != 0)
      {
        _db.RecipeTags.Add(new RecipeTag{ TagId = tag.TagId, RecipeId = RecipeId});
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteRecipe(int id)
    {
      var joinEntry = _db.RecipeTags.FirstOrDefault(entry => entry.RecipeTagId == id);
      _db.RecipeTags.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}