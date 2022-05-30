<template>
  <div class="container">
    <div class="row">
      <div class="col-12 d-flex justify-content-center" id="header">
        <img
          class="header-img shadow"
          src="https://images.unsplash.com/flagged/photo-1587302164675-820fe61bbd55?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1180&q=80"
        />
        <div class="position-absolute text-center m-4">
          <h1>AllSpice</h1>
          <h5>quirky cookign slogan goes here</h5>
        </div>
      </div>
      <div class="nav-bar col-12 d-flex justify-content-center">
        <button class="btn btn-warning mt-2 mx-3">Favorites</button>
        <button class="btn btn-warning mt-2 mx-3">My Recipes</button>
        <button class="btn btn-warning mt-2 mx-3">Sort</button>
      </div>
      <!-- Insert Recipes here -->
      <Recipe v-for="r in recipe" :key="r.id" :recipe="r" />
      <!-- end -->
    </div>
  </div>
</template>

<script>
import { computed, onMounted, onUnmounted, watchEffect } from "@vue/runtime-core"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { recipesService } from "../services/RecipesService.js"
import { AppState } from "../AppState.js"
import { useRoute } from "vue-router"
export default {
  name: 'Home',
  setup() {
    const route = useRoute()
    watchEffect(async () => {
      try {
        await recipesService.getAllRecipes()
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      recipe: computed(() => AppState.recipes)
    }
  }
}
</script>

<style scoped lang="scss">
.header-img {
  width: 100vw;
  height: 25vh;
  object-fit: cover;
  align-content: center;
}
</style>
