using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RecipeBook.Models;
using System;

namespace RecipeBook.Controllers
{
  public class TagController : Controller
  {
    [HttpGet("/tag")]
    public ActionResult Index()
    {
      List<Tag> allTags = Tag.GetAll();
      return View("Index", allTags);
    }

    [HttpGet("/tag/new")]
    public ActionResult CreateTagForm()
    {
      return View("CreateTagForm");
    }

    [HttpPost("/tag")]
    public ActionResult CreateTag()
    {
      Tag newTag = new Tag(Request.Form["tag-name"]);
      newTag.Save();
      return RedirectToAction("Index", "tag");
    }

    [HttpGet("/tag/{id}")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Tag foundTag = Tag.Find(id);
      List<Recipe> recipes = foundTag.GetRecipes();
      List<Recipe> allRecipes = Recipe.GetAll();
      model.Add("tag", foundTag);
      model.Add("recipes", recipes);
      model.Add("allrecipes", allRecipes);
      return View("Details", model);
    }


    [HttpPost("/tag/{tagId}/recipe")]
    public ActionResult AddRecipe(int tagId)
    {
      Tag tag = Tag.Find(tagId);
      Recipe foundRecipe = Recipe.Find(Int32.Parse(Request.Form["recipe-id"]));
      tag.AddRecipe(foundRecipe);
      return RedirectToAction("Details", new {Id = tagId});
    }


    [HttpGet("/tag/{tagId}/recipe/{recipeId}")]
    public ActionResult DropRecipe(int tagId, int recipeId)
    {
      Recipe foundRecipe = Recipe.Find(recipeId);
      Tag foundTag = Tag.Find(tagId);

      foundTag.DropRecipe(foundRecipe);
      return RedirectToAction("Details",new {Id = tagId});
    }
  }
}
