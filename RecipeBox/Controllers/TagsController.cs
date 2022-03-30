using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeBox.Models;
using System.Linq;

namespace RecipeBox.Controllers
{
  public class TagsController : Controller
  {
    private readonly RecipeBoxContext _db;
    public TagsController(RecipeBoxContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Tags.ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Tag tag, int RecipeId)
    {
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
      _db.Entry(tag).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = tag.TagId});
    }
  }
}