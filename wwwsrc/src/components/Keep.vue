<template>
  <div class="rounded mb-1 p-1 bg-dark text-light d-flex flex-column align-items-center">
    <img class="img-fluid rounded" :src="keepData.img" alt />
    <div v-if="!form" class="align-self-start">
      <h5 class="keep-title">{{keepData.name}}</h5>
      <p>{{keepData.description}}</p>
    </div>
    <div v-show="$route.name == 'dashboard'">
      <div v-if="form" class>
        <form class="d-flex justify-content-center flex-column align-items-center">
          <input
            v-model="editedKeep.name"
            class="form-control mt-1"
            placeholder="Title"
            type="text"
          />
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
        <button class="btn btn-danger mx-1" @click="remove()">Delete</button>
        <button class="btn btn-warning mx-1" @click="form=!form">Edit</button>
        <router-link :to="{name:'keep', params:{keepId: keepData.id}}">
          <button class="btn btn-primary">View</button>
        </router-link>
      </div>
    </div>
    <div v-show="$route.name == 'vault'">
      <button class="btn btn-primary" @click="removeKeepVault()">Remove</button>
      <router-link :to="{name:'keep', params:{keepId: keepData.id}}">
        <button class="btn btn-primary">View</button>
      </router-link>
    </div>
    <div v-show="$route.name == 'home'">
      <button class="btn btn-primary">Share</button>
      <button
        type="button"
        class="btn btn-primary"
        data-toggle="modal"
        :data-target="'#editKeep' + keepData.id"
      >Keep</button>
      <router-link :to="{name:'keep', params:{keepId: keepData.id}}">
        <button class="btn btn-primary">View</button>
      </router-link>
      <div
        class="modal fade"
        :id="'editKeep' + keepData.id"
        tabindex="-1"
        role="dialog"
        aria-labelledby="exampleModalLabel"
        aria-hidden="true"
      >
        <div class="modal-dialog" role="document">
          <div class="modal-content">
            <div class="modal-body d-flex justify-content-center">
              <h5>{{this.keep.name}}</h5>
              <form class="d-flex flex-column align-items-center w-100">
                <select
                  v-model="vault"
                  class="btn btn-primary text-light mb-3"
                  name="vaults"
                  id="vault-select"
                >
                  <option selected value>No Vault</option>
                  <option :value="vault.id" v-for="vault in vaults" :key="vault.id">{{vault.name}}</option>
                </select>
                <button
                  type="submit"
                  class="btn btn-warning align-self-start"
                  data-dismiss="modal"
                  @click.prevent="setVault()"
                >Submit Changes</button>
              </form>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
            </div>
          </div>
        </div>
      </div>
      <div class="d-flex flex-row justify-content-around">
        <p>Views: {{keepData.views}}</p>
        <p>Keeps: {{keepData.keeps}}</p>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  props: ["keepData", "keepId"],
  computed: {
    profile() {
      return this.$store.state.profile;
    },
    vaults() {
      return this.$store.state.vaults;
    }
  },
  data() {
    return {
      form: false,
      editedKeep: {
        name: this.keepData.name,
        description: this.keepData.description,
        id: this.keepData.id
      },
      vault: null,
      keep: {}
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
    },
    async setVault() {
      if (this.vault) {
        await this.$store.dispatch("setKeepVault", {
          keepId: this.keepData.id,
          vaultId: this.vault
        });
        this.vault = null;
      }
    },
    remove() {
      this.$store.dispatch("deleteKeep", this.keepData.id);
    },
    removeKeepVault() {
      this.$store.dispatch("deleteKeepVault", {
        keepId: this.keepData.id,
        vaultKeepId: this.keepData.vaultKeepId
      });
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