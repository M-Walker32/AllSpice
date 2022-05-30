<template>
  <div class="d-flex">
    <h3>{{ ingredient.name }}</h3>
    <h3>{{ ingredient.quantity }}</h3>
  </div>
</template>


<script>
export default {
  props: {
    ingredient: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
</style>