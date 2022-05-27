using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using AllSpice.Models;

namespace AllSpice.Repositories
{
  public class StepsRepository
  {
    private readonly IDbConnection _db;
    public StepsRepository(IDbConnection db)
    {
      _db = db;
    }
    // Get Steps by recipe
    internal List<Step> GetSteps(int id)
    {
      string sql = @"
        SELECT
          a.*,
          s.*
        FROM steps s
        JOIN accounts a ON s.creatorId = a.id
        WHERE s.recipeId = @id;
      ";
      return _db.Query<Account, Step, Step>(sql, (a, s) =>
      {
        s.Creator = a;
        return s;
      }, new { id }).ToList();
    }
    // Get by Id
    internal Step Get(int id)
    {
      string sql = @"
      SELECT
      s.*,
      act.*
      FROM steps s
      JOIN accounts act ON s.creatorId = act.Id
      WHERE s.id = @id";
      return _db.Query<Step, Account, Step>(sql, (step, account) =>
      {
        step.Creator = account;
        return step;
      }, new { id }).FirstOrDefault();
    }
    // Create
    internal Step Create(Step stepData)
    {
      string sql = @"
      INSERT INTO steps
      (position, body, creatorId, recipeId)
      VALUES
      (@Position, @Body, @CreatorId, @RecipeId);
      SELECT LAST_INSERT_ID();";
      stepData.Id = _db.ExecuteScalar<int>(sql, stepData);
      return stepData;
    }

    // Delete
    internal void Delete(int id)
    {
      string sql = "DELETE FROM steps WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }
  }
}