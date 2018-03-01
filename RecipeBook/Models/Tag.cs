using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace RecipeBook.Models
{
  public class Tag
  {
    private string _tagName;
    private int _tagId;

    public Tag(string tagName, int tagId = 0)
    {
      _tagName = tagName;
      _tagId = tagId;
    }

    public string GetTagName(){return _tagName;}
    public int GetTagId(){return _tagId;}

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM tag;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public void DropRecipe(Recipe droppedRecipe)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM recipe_tag WHERE recipe_id = @RecipeId AND tag_id = @TagId;";

      MySqlParameter recipeIdParameter = new MySqlParameter();
      recipeIdParameter.ParameterName = "@RecipeId";
      recipeIdParameter.Value = droppedRecipe.GetId();

      MySqlParameter tagIdParameter = new MySqlParameter();
      tagIdParameter.ParameterName = "@TagId";
      tagIdParameter.Value = this._tagId;

      cmd.Parameters.Add(recipeIdParameter);
      cmd.Parameters.Add(tagIdParameter);
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
      cmd.CommandText = @"INSERT INTO `tag` (`tag_name`) VALUES (@TagName);";

      MySqlParameter tagName = new MySqlParameter();
      tagName.ParameterName = "@TagName";
      tagName.Value = this._tagName;

      cmd.Parameters.Add(tagName);

      cmd.ExecuteNonQuery();
      _tagId = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
     }
     public static List<Tag> GetAll()
     {
      List<Tag> allTags = new List<Tag>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM tag;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string tagName = rdr.GetString(0);
        int tagId = rdr.GetInt32(1);
        Tag newTag = new Tag(tagName,tagId);
        allTags.Add(newTag);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allTags;
    }

    public override bool Equals(System.Object otherTag)
    {
      if(!(otherTag is Tag))
      {
        return false;
      }
      else
      {
        Tag newTag = (Tag) otherTag;
        bool tagNameEquality = (this.GetTagName() == newTag.GetTagName());
        bool tagIdEquality = (this.GetTagId() == newTag.GetTagId());
        return (tagNameEquality && tagIdEquality);
      }
    }
    public static Tag Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * from `tag` WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int tagId = 0;
      string tagName = "";

      while (rdr.Read())
      {
        tagId = rdr.GetInt32(1);
        tagName = rdr.GetString(0);
      }

      Tag foundTag = new Tag(tagName, tagId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return foundTag;
    }


    public List<Recipe> GetRecipes()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT recipe.* FROM tag
      JOIN recipe_tag ON (tag.id = recipe_tag.tag_id)
      JOIN recipe ON (recipe_tag.recipe_id = recipe.id)
      WHERE tag.id = @TagId;";

      MySqlParameter tagIdParameter = new MySqlParameter();
      tagIdParameter.ParameterName = "@TagId";
      tagIdParameter.Value = _tagId;
      cmd.Parameters.Add(tagIdParameter);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Recipe> recipes = new List<Recipe>{};

      while(rdr.Read())
      {
        int recipeId = rdr.GetInt32(3);
        string name = rdr.GetString(0);
        string ingredient = rdr.GetString(1);
        string instruction = rdr.GetString(2);
        Recipe newRecipe = new Recipe(name, ingredient, instruction, recipeId);
        recipes.Add(newRecipe);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return recipes;
    }

    public void AddRecipe(Recipe newRecipe)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO recipe_tag (tag_id, recipe_id) VALUES (@TagId, @RecipeId);";

      MySqlParameter tag_id = new MySqlParameter();
      tag_id.ParameterName = "@TagId";
      tag_id.Value = this._tagId;
      cmd.Parameters.Add(tag_id);

      MySqlParameter recipe_id = new MySqlParameter();
      recipe_id.ParameterName = "@RecipeId";
      recipe_id.Value = newRecipe.GetId();
      cmd.Parameters.Add(recipe_id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
