import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class IngredientsService{
async getIngredients(recipeId){
  const res = await api.get(`api/recipes/${recipeId}/ingredients`)
  logger.log('ingredients =', res.data)
  AppState.ingredients = res.data
}
async addIngredient(data){
const ingredient = await api.post('api/ingredients', data)
logger.log(ingredient.data)
// the line below might not work
this.getIngredients(ingredient.data.recipeId)
}
}
export const ingredientsService = new IngredientsService