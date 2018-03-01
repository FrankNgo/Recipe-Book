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
  }
}
