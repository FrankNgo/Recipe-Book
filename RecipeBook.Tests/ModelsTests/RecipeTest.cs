using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using RecipeBook.Models;
using System;

namespace RecipeBook.Models.Tests
{
  [TestClass]
  public class RecipeTest : IDisposable
 {
    public void Dispose()
    {
      Recipe.DeleteAll();
    }
    public RecipeTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=recipe_book_tests;";
    }

    [TestMethod]
    public void GetRecipe_TestingUserInputForAll_String()
    {
      //Arrange
      Recipe newRecipe = new Recipe("pasta","red sauce","pasta in pot");
      string testName = "pasta";
      string testIngredient = "red sauce";
      string testInstruction = "pasta in pot";
      int testId = 0;

      //Act
      string name = newRecipe.GetName();
      string ingredient = newRecipe.GetIngredient();
      string instruction = newRecipe.GetInstruction();
      int id = newRecipe.GetId();

      //Assert
      Assert.AreEqual(name, testName);
      Assert.AreEqual(ingredient, testIngredient);
      Assert.AreEqual(instruction, testInstruction);
      Assert.AreEqual(id, testId);
    }
    [TestMethod]
    public void Save_SavesToTheDatebase_Recipe()
    {
      //Arrange
      Recipe testRecipe = new Recipe("pasta","red sauce","pasta in pot");

      //Act
      testRecipe.Save();
      List<Recipe> result = Recipe.GetAll();
      List<Recipe> testList = new List<Recipe>{testRecipe};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetAll_DatabaseEmpty_0()
    {
      //Arrange, Act
      int result = Recipe.GetAll().Count;

      //Assert
      Assert.AreEqual(0,result);
    }

    [TestMethod]
    public void Save_AssignIdToRecipe_Id()
    {
      //Arrange
      Recipe testRecipe = new Recipe("pasta","red sauce","pasta in pot");

      //Act
      testRecipe.Save();
      Recipe savedRecipe = Recipe.GetAll()[0];
      int result = savedRecipe.GetId();
      int testId = testRecipe.GetId();

      //Assert
      Assert.AreEqual(testId,result);
    }


    [TestMethod]
    public void Equals_ReturnsTrueIfAllTheSame_Recipe()
    {
      //Arrange
      Recipe firstRecipe = new Recipe("pasta","red sauce","pasta in pot");
      Recipe secondRecipe = new Recipe("pasta","red sauce","pasta in pot");

      //Assert
      Assert.AreEqual(firstRecipe, secondRecipe);
    }
  }
}
