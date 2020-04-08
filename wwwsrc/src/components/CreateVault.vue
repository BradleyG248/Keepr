<template>
  <div>
    <button
      type="button"
      class="btn btn-primary mb-3"
      data-toggle="modal"
      data-target="#createVault"
    >Create Vault</button>

    <div
      class="modal fade"
      id="createVault"
      tabindex="-1"
      role="dialog"
      aria-labelledby="exampleModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Create Vault</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <form>
              <input
                class="form-control"
                v-model="newVault.name"
                placeholder="Title"
                type="text"
              />
              <input
                class="form-control"
                v-model="newVault.description"
                placeholder="Description"
                type="text"
              />
              <button
                type="submit"
                class="btn btn-warning"
                data-dismiss="modal"
                @click.prevent="createVault()"
              >Create</button>
            </form>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      newVault: {}
    };
  },
  methods: {
    async createVault() {
      await this.$store.dispatch("createVault", this.newVault);
      this.newVault = {};
    }
  }
};
</script>
<style>
</style>