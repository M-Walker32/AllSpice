import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class StepsService{
async getSteps(recipeId){
  const res = await api.get(`api/recipes/${recipeId}/steps`)
  logger.log('steps =', res.data)
  AppState.steps = res.data
}
async addStep(data){
const step = await api.post('api/steps', data)
logger.log(step.data)
// the line below might not work
this.getSteps(step.data.recipeId)
}
}
export const stepsService = new StepsService