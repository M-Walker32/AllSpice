using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
  public class StepsService
  {
    private readonly StepsRepository _repo;
    public StepsService(StepsRepository repo)
    {
      _repo = repo;
    }
    // GET by Id
    internal Step Get(int id)
    {
      Step step = _repo.Get(id);
      if (step == null)
      {
        throw new Exception("Invalid Id");
      }
      return step;
    }
    // Get Steps by Recipe
    internal List<Step> GetSteps(int id)
    {
      return _repo.GetSteps(id);
    }
    // Create
    internal Step Create(Step stepData)
    {
      return _repo.Create(stepData);
    }
    // Delete
    internal void Delete(int id, string userId)
    {
      Step step = Get(id);
      if (step.CreatorId != userId)
      {
        throw new Exception("You cannot delete this!");
      }
      _repo.Delete(id);
    }
  }
}