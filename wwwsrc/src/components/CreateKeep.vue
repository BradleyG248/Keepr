<template>
  <div>
    <button
      type="button"
      class="btn btn-primary mb-3"
      data-toggle="modal"
      data-target="#createKeep"
    >Create Keep</button>

    <div
      class="modal fade"
      id="createKeep"
      tabindex="-1"
      role="dialog"
      aria-labelledby="exampleModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Create Keep</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <form class="d-flex flex-column align-items-center">
              <h2>Title</h2>
              <input
                class="form-control mb-2"
                v-model="newKeep.name"
                placeholder="Title"
                type="text"
              />
              <h2>Description</h2>
              <input
                class="form-control mb-2"
                v-model="newKeep.description"
                placeholder="Description"
                type="text"
              />
              <h2>Image</h2>
              <input
                class="form-control mb-2"
                v-model="newKeep.img"
                placeholder="Image URL"
                type="text"
              />
              <div class="d-flex justify-content-start w-100">
                <p class="check">Private:</p>
                <input type="checkbox" v-model="newKeep.isPrivate" class="checkbox mt-2" />
              </div>
              <button
                type="submit"
                class="btn btn-warning"
                data-dismiss="modal"
                @click.prevent="createKeep()"
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
      newKeep: {}
    };
  },
  methods: {
    async createKeep() {
      await this.$store.dispatch("createKeep", this.newKeep);
      this.newKeep = {};
    }
  }
};
</script>