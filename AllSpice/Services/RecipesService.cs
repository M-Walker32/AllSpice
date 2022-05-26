using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
  public class RecipesService
  {
    private readonly RecipesRepository _repo;
    public RecipesService(RecipesRepository repo)
    {
      _repo = repo;
    }
    // GET
    internal List<Recipe> Get()
    {
      return _repo.Get();
    }
    // GET BY ID
    internal Recipe Get(int id)
    {
      Recipe recipe = _repo.Get(id);
      if (recipe == null)
      {
        throw new Exception("Invalid Id");
      }
      return recipe;
    }
    // CREATE
    internal Recipe Create(Recipe recipeData)
    {
      return _repo.Create(recipeData);
    }
    // EDIT
    // DELETE
  }
}