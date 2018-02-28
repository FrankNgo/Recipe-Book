using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recipe.Models;
using System;

namespace Recipe.Models.Tests
{
  [TestClass]
  public class RecipeModelTest : IDisposable
 {
    public RecipeModelTest()
    {
      Console.WriteLine("The port number and database name probably need to be changed");
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=my_database_name_test;";
    }
    
    public void Dispose()
    {
      //Delete everything from the database
    }
    
    [TestMethod]
    public void Test_JustATest_String()
    {
      Assert.AreEqual("this is a string from the model", RecipeModel.GetString());
    }
  }
}
