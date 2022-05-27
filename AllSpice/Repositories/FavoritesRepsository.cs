using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{
  public class FavoritesRepository
  {
    private readonly IDbConnection _db;
    public FavoritesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<FavoriteModel> GetFavoritedRecipes(string id)
    {
      string sql = @"
      SELECT 
      act.*
      r.*,
      f.id AS favoriteID
      FROM favorites f
      JOIN recipes r ON f.recipeId = r.id
      JOIN accounts act ON a.creatorId = act.id
      WHERE f.accountId = @id";
      return _db.Query<Account, FavoriteModel, FavoriteModel>(sql, (act, fave) =>
      {
        fave.Creator = act;
        return fave;
      }, new { id }).ToList();
    }

    internal Recipe Get(int id)
    {
      throw new NotImplementedException();
    }

    // CREATE

    internal Favorite Create(Favorite favoriteData)
    {
      string sql = @"
      INSERT INTO favorites
      (recipeId, accountId)
      VALUES
      (@RecipeId, @AccountId);
      SELECT LAST_INSERT_ID();";
      favoriteData.Id = _db.ExecuteScalar<int>(sql, favoriteData);
      return favoriteData;
    }
    // DELETE
    internal void Delete(int id)
    {
      string sql = "DELETE FROM favorites WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }
  }
}