using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RecipeBook.Models;

namespace RecipeBook.Controllers
{
  public class HomeController : Controller
  {
    [Route("/")]
    public ActionResult Index()
    {
      return View("Index");
    }
  }
}
