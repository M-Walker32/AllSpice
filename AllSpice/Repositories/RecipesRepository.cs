using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using AllSpice.Models;

namespace AllSpice.Repositories
{
  public class RecipesRepository
  {
    private readonly IDbConnection _db;
    public RecipesRepository(IDbConnection db)
    {
      _db = db;
    }
    // GET
    internal List<Recipe> Get()
    {
      string sql = @"
      SELECT
        r.*,
        act.*
      FROM recipes r
      JOIN accounts act ON r.creatorId =act.id";
      return _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
      {
        recipe.Creator = account;
        return recipe;
      }).ToList();
    }
    // GET BY ID
    internal Recipe Get(int id)
    {
      string sql = @"
      SELECT
        r.*,
        act.*
      FROM recipes r
      JOIN accounts act ON r.creatorId = act.Id
      WHERE r.id = @id";
      return _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
      {
        recipe.Creator = account;
        return recipe;
      }, new { id }).FirstOrDefault();
    }
    // CREATE
    internal Recipe Create(Recipe recipeData)
    {
      string sql = @"
      INSERT INTO recipes
      (picture, title, subtitle, category, creatorId)
      VALUES
      (@Picture, @Title, @Subtitle, @Category, @CreatorId);
      SELECT LAST_INSERT_ID();";
      recipeData.Id = _db.ExecuteScalar<int>(sql, recipeData);
      return recipeData;
    }
  }
}