<template>
  <div class="col-4 mt-4">
    <div class="mt-2 h-100 bg-light align-items-center shadow">
      <p class="on-hover position-absolute bg-danger p-2" @click="deleteRecipe">
        X
      </p>
      <img class="img-fluid pb-2" :src="recipe.picture" :alt="recipe.title" />
      <div class="p-2 d-flex justify-content-between">
        <h5 class="text-secondary m-0 p-0">{{ recipe.title }}</h5>
        <p class="text-primary m-0 p-0">
          <!-- change heart based on favorite -->
          <i
            @click="favorite(recipe.id)"
            class="selectable mdi mdi-heart-outline ms-2"
            title="favorite recipe"
          />
          <i
            @click="unfavorite(recipe.id)"
            class="mdi mdi-heart ms-2"
            title="unfavorite recipe"
          />
        </p>
      </div>
      <h5 class="ms-2 text-secondary m-0 p-0">{{ recipe.subtitle }}</h5>
      <button
        class="btn btn-warning w-100 mt-4"
        data-bs-toggle="modal"
        :data-bs-target="'#recipe-modal'"
        @click="setActive"
      >
        Details
      </button>
    </div>
  </div>
  <!-- MODAL -->
  <Modal id="recipe-modal">
    <template #modal-title-slot>
      <h4 v-if="activeRecipe">{{ activeRecipe.title }}</h4>
    </template>
    <template #modal-body-slot>
      <!-- RECIPE DETAILS  -->
      <div class="container-fluid w-100">
        <div v-if="activeRecipe" class="row-fluid d-flex">
          <div class="col-md-4 h-100 col-12">
            <img
              class="img-fluid pb-2"
              :src="activeRecipe.picture"
              :alt="activeRecipe.title"
            />
          </div>
          <div class="col-md-4 m-2 col-12">
            <div class="card h-100">
              <h5 class="card-header text-light bg-primary">Ingredients</h5>
              <div class="card-body">
                <p class="card-text text-wrap">
                  <!-- Ingredients Here -->
                  <Ingredient
                    v-for="i in ingredient"
                    :key="i.id"
                    :ingredient="i"
                  />
                </p>
              </div>
              <div class="m-1 d-flex">
                <form @submit.prevent="addIngredient(activeRecipe.id)">
                  <input
                    type="text"
                    class="form-control text-wrap m-1"
                    minlength="3"
                    required
                    maxlength="50"
                    placeholder="ingredient"
                    v-model="ingredientData.name"
                  />
                  <input
                    type="text"
                    class="form-control text-wrap m-1"
                    minlength="1"
                    required
                    maxlength="25"
                    placeholder="2 cups"
                    v-model="ingredientData.quantity"
                  />
                  <button class="btn btn-outline-secondary m-1" type="submit">
                    +
                  </button>
                </form>
              </div>
            </div>
          </div>
          <!-- STEPS COL -->
          <div class="col-md-4 m-2 col-12">
            <div class="card h-100">
              <h5 class="card-header text-light bg-primary">Steps</h5>
              <div class="card-body">
                <p class="card-text text-wrap" id="task">
                  <!-- Steps Here -->
                  <!-- <Step v-for="s in steps" :key="s.id" :step="s"/> -->
                </p>
              </div>
              <form @submit.prevent="addStep">
                <div class="m-1 d-flex">
                  <input
                    type="number"
                    class="form-control text-wrap m-1"
                    minlength="1"
                    required
                    maxlength="10"
                    placeholder="1"
                    v-model="stepData.position"
                  />
                  <input
                    type="text"
                    class="form-control text-wrap m-1"
                    minlength="1"
                    required
                    maxlength="150"
                    placeholder="description"
                    v-model="stepData.body"
                  />
                  <button class="btn btn-outline-secondary m-1" type="submit">
                    +
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
        <div v-else>
          <div class="spinner-border text-primary" role="status">
            <span class="visually-hidden">Loading...</span>
          </div>
        </div>
      </div>
      <!-- END DETAILS -->
    </template>
  </Modal>
</template>


<script>
import { computed, ref } from "@vue/reactivity"
import { useRoute } from "vue-router"
import { AppState } from "../AppState.js"
import { recipesService } from "../services/RecipesService.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { onMounted, watchEffect } from "@vue/runtime-core"
import { ingredientsService } from "../services/IngredientsService.js"
export default {
  props: {
    recipe: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const route = useRoute()
    const ingredientData = ref({})
    const stepData = ref({})
    const activeRecipe = AppState.activeRecipe
    return {
      activeRecipe,
      ingredientData,
      stepData,
      async addIngredient(id) {
        try {
          ingredientData.value.recipeId = id.toString()
          logger.log('ingredient obj', ingredientData)
          const newIngredient = await ingredientsService.addIngredient(ingredientData.value, id)
          ingredientData.value = ''
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
          ingredientData.value = ''
        }
      },
      async deleteRecipe() {
        try {
          if (await Pop.confirm()) {
            await recipesService.deleteRecipe(props.recipe.id)
          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      async setActive() {
        try {
          await recipesService.getById(props.recipe.id)
          await ingredientsService.getIngredients(props.recipe.id)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      async favorite(recipeId) {
        try {
          // TODO logic on data passed to create a favorite, make the favorite service
          await favoritesService.createFavorite(favorite)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      activeRecipe: computed(() => AppState.activeRecipe),
      ingredient: computed(() => AppState.ingredients),
      step: computed(() => AppState.steps)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>