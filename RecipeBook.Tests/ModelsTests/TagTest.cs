using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using RecipeBook.Models;
using System;

namespace RecipeBook.Models.Tests
{
  [TestClass]
  public class TagTest : IDisposable
  {
    public void Dispose()
    {
      Recipe.DeleteAll();
      Tag.DeleteAll();
    }

    public TagTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=recipe_book_tests;";
    }

    [TestMethod]
    public void GetTagInfo_GetAllTagInfo_String()
    {
      //arrange
      Tag newTag = new Tag("Pasta");
      string testTagName = "Pasta";
      int testId = 0;

      //act
      string TagNameResult = newTag.GetTagName();
      int idResult = newTag.GetTagId();

      //assert
      Assert.AreEqual(TagNameResult,testTagName);
      Assert.AreEqual(idResult,testId);
    }
    [TestMethod]
    public void GetAll_DatebaseEmptyAtFirst_0()
    {
      //arrange, act
      int result = Tag.GetAll().Count;

      //assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_SavesToDatabase_TagList()
    {
      //arrange
      Tag testTag = new Tag("Pasta");

      //act
      testTag.Save();
      List<Tag> result = Tag.GetAll();
      List<Tag> testList = new List<Tag>{testTag};

      //assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Save_AssignedIdToObject_Id()
    {
      //arrange
      Tag testTag = new Tag("Pasta");

      //act
      testTag.Save();
      Tag savedTag = Tag.GetAll()[0];
      int result = savedTag.GetTagId();
      int testId = testTag.GetTagId();

      //assert
      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void Equals_ReturnsTrueIfAllSame_Tag()
    {
      //arrange, act
      Tag firstTag = new Tag("Pasta");
      Tag secondTag = new Tag("Pasta");

      //assert
      Assert.AreEqual(firstTag, secondTag);
    }

    [TestMethod]
    public void Find_FinfTagInDatabase_Course()
    {
      //arrange
      Tag testTag = new Tag("Pasta");
      testTag.Save();

      //act
      Tag foundTag = Tag.Find(testTag.GetTagId());

      //assert
      Assert.AreEqual(foundTag, testTag);
    }
  }
}
