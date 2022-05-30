<template>
  <div class="modal" tabindex="-1" id="recipe-modal">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <slot class="modal-title" name="modal-title-slot"></slot>
        </div>
        <div class="modal-body">
          <slot name="modal-body-slot"></slot>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>
.modal {
  max-width: 90vw;
}
.modal-content {
  position: relative;
  display: flex;
  flex-direction: column;
  width: 60vw;
  pointer-events: auto;
  background-color: #fff;
  background-clip: padding-box;
  border: 1px solid rgba(0, 0, 0, 0.2);
  border-radius: 0.3rem;
  outline: 0;
}
</style>