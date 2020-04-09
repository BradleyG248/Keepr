<template>
  <div
    class="rounded mb-1 p-1 bg-light text-dark d-flex flex-column justify-content-around align-items-center"
  >
    <div v-if="!form" class="align-self-start">
      <h1 class="keep-title">{{vaultData.name}}</h1>
      <h3>{{vaultData.description}}</h3>
    </div>
    <div v-else class>
      <form class="d-flex justify-content-center flex-column align-items-center">
        <input v-model="editedVault.name" class="form-control mt-1" placeholder="Title" type="text" />
        <input
          v-model="editedVault.description"
          class="form-control mt-1"
          placeholder="Description"
          type="text"
        />
      </form>
      <button @click="editVault()" type="submit" class="mt-1 btn btn-primary">Submit</button>
    </div>
    <div class="mt-1 justify-content-around d-flex w-100" v-if="vaultData.userId == profile.userId">
      <button @click="deleteVault()" class="btn btn-danger mx-1">Delete</button>
      <button class="btn btn-warning mx-1" @click="form=!form">Edit</button>
      <router-link :to="{name:'vault', params:{vaultId: vaultData.id}}">
        <button class="btn btn-primary">View</button>
      </router-link>
    </div>
  </div>
</template>
<script>
export default {
  props: ["vaultData"],
  computed: {
    profile() {
      return this.$store.state.profile;
    }
  },
  data() {
    return {
      form: false,
      editedVault: {
        name: this.vaultData.name,
        description: this.vaultData.description,
        id: this.vaultData.id
      }
    };
  },
  methods: {
    async editVault() {
      await this.$store.dispatch("editVault", this.editedVault);
      this.editedVault = {
        title: this.vaultData.name,
        description: this.vaultData.description,
        id: this.vaultData.id
      };
      this.form = false;
    },
    deleteVault() {
      this.$store.dispatch("deleteVault", this.vaultData.id);
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