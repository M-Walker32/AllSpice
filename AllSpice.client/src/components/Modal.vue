<template>
  <div class="modal mt-5" id="create-recipe-modal" tabindex="-1">
    <div class="bg-light m-5">
      <div class="modal-header">
        <slot class="modal-title" name="modal-title-slot"></slot>
        <button
          type="button"
          class="btn-close"
          data-bs-dismiss="modal"
          aria-label="Close"
        ></button>
      </div>
      <div>
        <slot name="modal-body-slot"></slot>
      </div>
    </div>
  </div>
</template>