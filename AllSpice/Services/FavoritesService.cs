using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
  public class FavoritesService
  {

    private readonly FavoritesRepository _repo;

    public FavoritesService(FavoritesRepository repo)
    {
      _repo = repo;
    }

    // GET by Account
    internal List<FavoriteModel> GetFavoritedRecipes(string id)
    {
      return _repo.GetFavoritedRecipes(id);
    }
    // GET BY ID
    internal Favorite Get(int id)
    {
      Favorite favorite = _repo.Get(id);
      if (favorite == null)
      {
        throw new Exception("Invalid Id");
      }
      return favorite;
    }
    // CREATE
    internal Favorite Create(Favorite favoriteData)
    {
      return _repo.Create(favoriteData);
    }
    // DELETE
    internal void Delete(int id, string userId)
    {
      Favorite favorite = Get(id);
      if (favorite.AccountId != userId)
      {
        throw new Exception("You cannot delete this!");
      }
      _repo.Delete(id);
    }
  }

}