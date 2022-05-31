<template>
  <div class="d-flex">
    <p>{{ step.position }}</p>
    <p class="ms-2">{{ step.body }}</p>
  </div>
</template>


<script>
export default {
  props: {
    step: {
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