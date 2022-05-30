<template>
  <form @submit.prevent="createRecipe">
    <div class="mb-3">
      <label for="recipe-title" class="form-label">Title:</label>
      <input
        type="text"
        class="form-control"
        v-model="editable.title"
        required
      />
    </div>
    <div class="mb-3">
      <label for="recipe-subtitle" class="form-label">Subtitle:</label>
      <input
        type="text"
        class="form-control"
        v-model="editable.subtitle"
        required
      />
    </div>
    <div class="mb-3">
      <label for="recipe-image" class="form-label">Recipe Image URL:</label>
      <input
        type="text"
        class="form-control"
        max="200"
        v-model="editable.picture"
        required
      />
    </div>
    <div class="mb-3">
      <label for="recipe-category" class="form-label">Category:</label>
      <select
        class="form-select my-button"
        aria-label="select event type"
        v-model="editable.category"
        required
      >
        <option selected disabled>Type</option>
        <option value="appetizer">appetizers</option>
        <option value="soup">soups</option>
        <option value="bread">breads</option>
        <option value="main course">main dishes</option>
        <option value="dessert">desserts</option>
      </select>
    </div>
    <button type="submit" class="btn btn-primary">Create</button>
  </form>
</template>


<script>
import { ref } from "@vue/reactivity"
import Pop from "../utils/Pop.js"
import { logger } from "../utils/Logger.js"
import { router } from "../router.js"
import { useRouter } from "vue-router"
import { Modal } from "bootstrap"
import { recipesService } from "../services/RecipesService.js"
export default {
  setup() {
    const editable = ref({})
    const router = useRouter()
    return {
      editable,
      async createRecipe() {
        try {
          const newRecipe = await recipesService.createRecipe(editable.value)
          editable.value = ''
          Modal.getOrCreateInstance(document.getElementById('create-recipe-modal')).toggle()
        } catch (error) {
          editable.value = ''
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