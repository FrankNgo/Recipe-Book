using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RecipeBook.Models;
using RecipeBook.Controllers;

namespace RecipeBook.Tests
{
  [TestClass]
  public class StudentsControllerTest
  {
    [TestMethod]
    public void Index_ReturnIfTrue_View()
    {
      //arrange
      RecipeController controller = new RecipeController();

      //act
      IActionResult indexView = controller.Index();
      ViewResult result = indexView as ViewResult;

      //assert
      Assert.IsInstanceOfType(result, typeof(ViewResult));
    }

    [TestMethod]
    public void Index_HasCorrectModelType_True()
    {
      //arrange
      ViewResult indexView = new RecipeController().Index() as ViewResult;

      //act
      var result = indexView.ViewData.Model;

      //assert
      Assert.IsInstanceOfType(result, typeof(List<Recipe>));
    }

    [TestMethod]
    public void CreateRecipeForm_ReturnIfTrue_View()
    {
      //arrange
      RecipeController controller = new RecipeController();

      //act
      IActionResult createRecipeFormView = controller.CreateRecipeForm();
      ViewResult result = createRecipeFormView as ViewResult;

      //assert
      Assert.IsInstanceOfType(result, typeof(ViewResult));
    }
  }
}
