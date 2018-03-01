using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace RecipeBook.Models
{
  public class Recipe
  {
    private int _id;
    private string _name;
    private string _ingredient;
    private string _instruction;

    public Recipe (string name, string ingredient, string instruction, int Id = 0)
    {
      _name = name;
      _ingredient = ingredient;
      _instruction = instruction;
      _id = Id;
    }

    public string GetName() { return _name; }

    public string GetIngredient() { return _ingredient; }

    public string GetInstruction() { return _instruction; }

    public int GetId() { return _id; }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM recipe;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM recipe WHERE id = @RecipeId;";
      //DELETE FROM roster WHERE student_id = @StudentId;";
      MySqlParameter recipeIdParameter = new MySqlParameter();
      recipeIdParameter.ParameterName = "@RecipeId";
      recipeIdParameter.Value = this._id;

      cmd.Parameters.Add(recipeIdParameter);
      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `recipe` (`name`, `ingredient`, `instruction`) VALUES (@Name, @Ingredient, @Instruction);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@Name";
      name.Value = this._name;

      MySqlParameter ingredient = new MySqlParameter();
      ingredient.ParameterName = "@Ingredient";
      ingredient.Value = this._ingredient;

      MySqlParameter instruction = new MySqlParameter();
      instruction.ParameterName = "@Instruction";
      instruction.Value = this._instruction;

      cmd.Parameters.Add(name);
      cmd.Parameters.Add(ingredient);
      cmd.Parameters.Add(instruction);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Recipe> GetAll()
    {
      List<Recipe> allRecipes = new List<Recipe>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM recipe;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string name = rdr.GetString(0);
        string ingredient = rdr.GetString(1);
        string instruction = rdr.GetString(2);
        int id = rdr.GetInt32(3);
        Recipe newRecipe = new Recipe(name, ingredient, instruction, id);
        allRecipes.Add(newRecipe);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
        return allRecipes;
    }

    public override bool Equals(System.Object otherRecipe)
    {
      if(!(otherRecipe is Recipe))
      {
        return false;
      }
      else
      {
        Recipe newRecipe = (Recipe) otherRecipe;
        bool NameEquality = (this.GetName() == newRecipe.GetName());
        bool ingredientEquality = (this.GetIngredient() == newRecipe.GetIngredient());
        bool instructionEquality = (this.GetInstruction() == newRecipe.GetInstruction());
        bool idEquality = (this.GetId() == newRecipe.GetId());
        return (NameEquality && ingredientEquality && instructionEquality && idEquality);
      }
    }
    public static Recipe Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * from `recipe` WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int recipeId = 0;
      string recipeName = "";
      string recipeIngridient = "";
      string recipeInstruction = "";

      while (rdr.Read())
     {
       recipeId = rdr.GetInt32(3);
       recipeName = rdr.GetString(0);
       recipeIngridient = rdr.GetString(1);
       recipeInstruction = rdr.GetString(2);
     }
       Recipe foundRecipe = new Recipe(recipeName, recipeIngridient, recipeInstruction, recipeId);
       conn.Close();

    if (conn != null)
    {
      conn.Dispose();
    }
    return foundRecipe;
    }
  }
}
