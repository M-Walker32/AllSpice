<template>
  <div class="d-flex">
    <h3>{{ step.position }}</h3>
    <h3 class="ms-2">{{ step.body }}</h3>
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