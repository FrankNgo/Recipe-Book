using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RecipeBook.Models;

namespace RecipeBook.Controllers
{
  public class RecipeController : Controller
  {
    [HttpGet("/recipe")]
    public ActionResult Index()
    {
      List<Recipe> allRecipe = Recipe.GetAll();
      return View("Index", allRecipe);
    }
    [HttpGet("/recipe/new")]
    public ActionResult CreateRecipeForm()
    {
      return View("CreateRecipeForm");
    }
    [HttpPost("/recipe")]
    public ActionResult CreateRecipe()
    {
      Recipe newRecipe = new Recipe(Request.Form["recipe-name"],Request.Form["recipe-ingridient"],Request.Form["recipe-instruction"]);
      newRecipe.Save();

      return RedirectToAction("Index", "recipe");
    }
  }
}
