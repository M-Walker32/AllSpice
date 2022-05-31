import { applyStyles } from "@popperjs/core"
import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { api } from "./AxiosService.js"

class FavoritesService{
async createFavorite(favorite){
  let found = AppState.favorites.find(f => f.recipeId == favorite.recipeId)
  if (!found){
  const newfavorite = await api.post('api/favorites', favorite)
  logger.log(newfavorite.data)
  AppState.favorites.push(newfavorite.data)}
  if (found){
    Pop.toast('Already favorited')
  }
}
async deleteFavorite(favorite){
  let found = AppState.favorites.find(f => f.recipeId == favorite.recipeId)
  if (found){
  const unfavorite = await api.delete('api/favorites/'+found.id)
}
}
}
export const favoritesService = new FavoritesService