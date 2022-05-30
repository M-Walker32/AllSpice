import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class IngredientsService{
async getIngredients(recipeId){
  ingredients = await api.get(`recipes/${recipeId}/ingredients`)
  logger.log('ingredients =',ingredients.data)
  Appstate.ingredients = ingredients.data
}
}
export const ingredientsService = new IngredientsService