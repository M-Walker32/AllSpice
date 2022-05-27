using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
  public class IngredientsService
  {
    private readonly IngredientsRepository _repo;
    public IngredientsService(IngredientsRepository repo)
    {
      _repo = repo;
    }
    // Get by id
    internal Ingredient Get(int id)
    {
      Ingredient ingredient = _repo.Get(id);
      if (ingredient == null)
      {
        throw new Exception("Invalid Id");
      }
      return ingredient;
    }
    // Get ingredients by Recipe
    internal List<Ingredient> GetIngredients(int id)
    {
      return _repo.GetIngredients(id);
    }
    // Create
    internal Ingredient Create(Ingredient ingredientData)
    {
      return _repo.Create(ingredientData);
    }
    // Delete
    internal void Delete(int id, string userId)
    {
      Ingredient ingredient = Get(id);
      if (ingredient.CreatorId != userId)
      {
        throw new Exception("You cannot delete this!");
      }
      _repo.Delete(id);
    }
  }
}