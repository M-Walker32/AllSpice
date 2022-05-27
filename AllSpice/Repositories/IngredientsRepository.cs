using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using AllSpice.Models;

namespace AllSpice.Repositories
{
  public class IngredientsRepository
  {
    private readonly IDbConnection _db;
    public IngredientsRepository(IDbConnection db)
    {
      _db = db;
    }
    // Get Ingredients by Recipe
    internal List<Ingredient> GetIngredients(int id)
    {
      string sql = @"
        SELECT
          a.*,
          i.*
        FROM ingredients i
        JOIN accounts a ON i.creatorId = a.id
        WHERE i.recipeId = @id;
      ";
      return _db.Query<Account, Ingredient, Ingredient>(sql, (a, i) =>
      {
        i.Creator = a;
        return i;
      }, new { id }).ToList();
    }

    // Get by Id
    internal Ingredient Get(int id)
    {
      string sql = @"
      SELECT
      i.*,
      act.*
      FROM ingredients i
      JOIN accounts act ON i.creatorId = act.Id
      WHERE i.id = @id";
      return _db.Query<Ingredient, Account, Ingredient>(sql, (ingredient, account) =>
      {
        ingredient.Creator = account;
        return ingredient;
      }, new { id }).FirstOrDefault();
    }
    // CREATE
    internal Ingredient Create(Ingredient ingredientData)
    {
      string sql = @"
      INSERT INTO ingredients
      (quantity, name, creatorId, recipeId)
      VALUES
      (@Quantity, @Name, @CreatorId, @RecipeId);
      SELECT LAST_INSERT_ID();";
      ingredientData.Id = _db.ExecuteScalar<int>(sql, ingredientData);
      return ingredientData;
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM ingredients WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }
  }
}