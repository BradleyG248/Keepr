<template>
  <div>
    <button
      type="button"
      class="btn btn-primary"
      data-toggle="modal"
      data-target="#editProfile"
    >Edit Profile</button>

    <div
      class="modal fade"
      id="editProfile"
      tabindex="-1"
      role="dialog"
      aria-labelledby="exampleModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <form>
              <input
                class="form-control"
                v-model="editedProfile.name"
                placeholder="Username"
                type="text"
              />
              <input
                class="form-control"
                v-model="editedProfile.email"
                placeholder="Email"
                type="text"
              />
              <input
                class="form-control"
                v-model="editedProfile.img"
                placeholder="Image link"
                type="text"
              />
              <button
                type="submit"
                class="btn btn-warning"
                data-dismiss="modal"
                @click.prevent="editProfile()"
              >Submit Changes</button>
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
      editedProfile: {}
    };
  },
  methods: {
    async editProfile() {
      await this.$store.dispatch("editProfile", this.editedProfile);
      this.editedProfile = {};
    }
  }
};
</script>
<style>
</style>