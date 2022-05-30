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
          <i class="mdi mdi-heart-outline ms-2" />
          <i class="mdi mdi-heart ms-2" />
        </p>
      </div>
      <h5 class="ms-2 text-secondary m-0 p-0">{{ recipe.subtitle }}</h5>
      <button
        class="btn btn-warning w-100 mt-4"
        data-bs-toggle="modal"
        :data-bs-target="'#create-recipe-modal'"
        @click="setActive"
      >
        Details
      </button>
    </div>
  </div>
  <!-- MODAL -->
  <Modal id="create-recipe-modal">
    <template #modal-title-slot>
      <h4>{{ recipe.title }}</h4>
    </template>
    <template #modal-body-slot>
      <RecipeDetails />
    </template>
  </Modal>
</template>


<script>
import { useRoute } from "vue-router"
import { AppState } from "../AppState.js"
import { recipesService } from "../services/RecipesService.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
export default {
  props: {
    recipe: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const route = useRoute()
    return {
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
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>