using Microsoft.AspNetCore.Mvc;
using Recipe.Models;

namespace Recipe.Controllers
{
  public class HomeController : Controller
  {

    [HttpGet("/")]
    public ActionResult Index()
    {
      return View("Index", RecipeModel.GetString());
    }
  }
}
