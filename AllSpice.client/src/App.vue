<template>
  <main>
    <router-view />
    <button
      class="btn btn-success float"
      title="create a new recipe"
      data-bs-toggle="modal"
      :data-bs-target="'#create-recipe-modal'"
    >
      +
    </button>
  </main>
  <Modal id="create-recipe-modal">
    <template #modal-title-slot>
      <h4>Create a New Recipe</h4>
    </template>
    <template #modal-body-slot>
      <RecipeForm />
    </template>
  </Modal>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import { recipesService } from "./services/RecipesService.js"
export default {
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState)
    }
  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";
.float {
  position: absolute;
  bottom: 2em;
  right: 2em;
}
</style>
