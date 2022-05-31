<template>
  <div class="d-flex">
    <p>{{ ingredient.name }}</p>
    <p class="ms-1">{{ ingredient.quantity }}</p>
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