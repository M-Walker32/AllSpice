<template>
  <div class="modal" tabindex="-1" id="create-recipe-modal">
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