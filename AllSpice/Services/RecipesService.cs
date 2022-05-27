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
    internal Recipe Edit(Recipe recipeData)
    {
      Recipe original = Get(recipeData.Id);
      if (original.CreatorId != recipeData.CreatorId)
      {
        throw new Exception("This is not yours to edit");
      }
      original.Title = recipeData.Title ?? original.Title;
      original.Subtitle = recipeData.Subtitle ?? original.Subtitle;
      original.Picture = recipeData.Picture ?? original.Picture;
      original.Category = recipeData.Category ?? original.Category;
      _repo.Edit(original);
      return Get(original.Id);
    }
    // DELETE
    internal void Delete(int id, string userId)
    {
      Recipe recipe = Get(id);
      if (recipe.CreatorId != userId)
      {
        throw new Exception("You cannot delete this!");
      }
      _repo.Delete(id);
    }


  }
}