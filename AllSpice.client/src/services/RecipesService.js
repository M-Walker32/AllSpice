import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { api } from "./AxiosService.js"


class RecipesService{
  async getAllRecipes(){
    const res = await api.get('api/recipes')
    AppState.recipes = res.data
    // logger.log(res.data)
  }
  async getById(recipeId){
    const recipe = await api.get('api/recipes/'+recipeId)
    logger.log('activerecipe =',recipe.data)
    AppState.activeRecipe = recipe.data
  }
  async createRecipe(formData){
    const recipe  = await api.post('api/recipes', formData)
    AppState.recipes.push(recipe.data)
    // logger.log(recipe.data)
  }
  async deleteRecipe(id){
    const recipe = await api.delete('api/recipes/'+ id)
   this.getAllRecipes
    Pop.success()
  }
}
export const recipesService = new RecipesService