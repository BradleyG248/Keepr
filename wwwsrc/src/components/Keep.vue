<template>
  <div class="rounded mb-1 p-1 bg-dark text-light d-flex flex-column align-items-center">
    <img class="img-fluid rounded" :src="keepData.img" alt />
    <div v-if="!form" class="align-self-start">
      <h5 class="keep-title">{{keepData.name}}</h5>
      <p>{{keepData.description}}</p>
    </div>
    <div v-else class>
      <form class="d-flex justify-content-center flex-column align-items-center">
        <input v-model="editedKeep.name" class="form-control mt-1" placeholder="Title" type="text" />
        <input
          v-model="editedKeep.description"
          class="form-control mt-1"
          placeholder="Description"
          type="text"
        />
      </form>
      <button @click="editKeep()" type="submit" class="mt-1 btn btn-primary">Submit</button>
    </div>
    <div class="mt-1" v-if="keepData.userId == profile.userId">
      <button class="btn btn-danger mx-1">Delete</button>
      <button class="btn btn-warning mx-1" @click="form=!form">Edit</button>
    </div>
  </div>
</template>
<script>
export default {
  props: ["keepData"],
  computed: {
    profile() {
      return this.$store.state.profile;
    }
  },
  data() {
    return {
      form: false,
      editedKeep: {
        name: this.keepData.name,
        description: this.keepData.description,
        id: this.keepData.id
      }
    };
  },
  methods: {
    async editKeep() {
      debugger;
      if (this.$route.name == "dashboard") {
        await this.$store.dispatch("editUserKeep", this.editedKeep);
      } else {
        await this.$store.dispatch("editPublicKeep", this.editedKeep);
      }
      this.editedKeep = {
        title: this.keepData.name,
        description: this.keepData.description,
        id: this.keepData.id
      };
      this.form = false;
    }
  }
};
</script>
<style scoped>
h5.keep-title {
  font-size: 1rem;
}
p {
  font-size: 0.75rem;
}
</style>