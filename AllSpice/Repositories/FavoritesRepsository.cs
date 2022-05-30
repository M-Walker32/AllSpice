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
    // GET users favorites
    internal List<FavoriteModel> GetFavoritedRecipes(string id)
    {
      string sql = @"
            SELECT 
            act.*,
            f.*,
            r.*
            FROM favorites f
            JOIN recipes r ON f.recipeId = r.id
            JOIN accounts act ON r.creatorId = act.id
            WHERE f.accountId = @id;
            ";
      List<FavoriteModel> recipes = _db.Query<Account, Favorite, FavoriteModel, FavoriteModel>(sql, (act, f, r) =>
      {
        r.Creator = act;
        r.FavoriteId = f.Id;
        return r;
      }, new { id }).ToList<FavoriteModel>();
      return recipes;
    }

    internal Favorite Get(int id)
    {
      string sql = @"
      SELECT
      i.*,
      act.*
      FROM ingredients i
      JOIN accounts act ON i.creatorId = act.Id
      WHERE i.id = @id";
      return _db.Query<Favorite, Account, Favorite>(sql, (favorite, account) =>
      {
        favorite.AccountId = account.Id;
        return favorite;
      }, new { id }).FirstOrDefault();
    }

    // CREATE

    internal Favorite Create(Favorite favoriteData)
    {
      string sql = @"
      INSERT INTO favorites
      (recipeId, creatorId, accountId)
      VALUES
      (@RecipeId, @CreatorId, @AccountId);
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