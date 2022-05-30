<template>
  <div class="container-fluid w-100">
    <div class="row-fluid d-flex">
      <div class="col-md-4 h-100 col-12">
        <img class="img-fluid pb-2" :src="recipe.picture" :alt="recipe.title" />
      </div>
      <div class="col-md-4 m-2 col-12">
        <div class="card h-100">
          <h5 class="card-header text-light bg-primary">Ingredients</h5>
          <div class="card-body">
            <p class="card-text text-wrap" id="task">
              <!-- <Ingredient v-for="i in ingredient" :key="i.id" :ingredient="i" /> -->
              <!-- Ingredients Here -->
            </p>
          </div>
          <form @onsubmit.prevent="addIngredient">
            <div class="m-3 d-flex">
              <input
                type="text"
                class="form-control text-wrap m-1"
                minlength="3"
                required
                maxlength="50"
                placeholder="Pet the dog."
                id="newtask"
              />
              <button class="btn btn-outline-secondary m-1" type="submit">
                +
              </button>
            </div>
          </form>
        </div>
      </div>
      <div class="col-md-4 m-2 col-12">
        <div class="card h-100">
          <h5 class="card-header text-light bg-primary">Steps</h5>
          <div class="card-body">
            <p class="card-text text-wrap" id="task">
              <!-- Steps Here -->
            </p>
          </div>
          <form @onsubmit.prevent="addStep">
            <div class="m-3 d-flex">
              <input
                type="text"
                class="form-control text-wrap m-1"
                minlength="3"
                required
                maxlength="50"
                placeholder="Pet the dog."
                id="newtask"
              />
              <button class="btn btn-outline-secondary m-1" type="submit">
                +
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState.js"
import { onMounted, watchEffect } from "@vue/runtime-core"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { ingredientsService } from "../services/IngredientsService.js"
export default {
  setup() {
    let recipe = AppState.activeRecipe
    onMounted(async () => {
      try {
        logger.log('WHAT AM I< DO TELL', recipe)
        // await ingredientsService.getIngredients(recipe)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      recipe: computed(() => AppState.activeRecipe),
      ingredient: computed(() => AppState.ingredient)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>