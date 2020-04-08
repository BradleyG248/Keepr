<template>
  <div class="dashboard container">
    <div class="row">
      <div class="col-12 d-flex flex-row align-items-center justify-content-between pt-3">
        <div class="d-flex flex-row">
          <img class="rounded-pill profile img-fluid" :src="profile.img" v-if="profile.img" alt />
          <div class="pl-1">
            <h1>{{profile.name}}</h1>
            <p>{{profile.email}}</p>
          </div>
        </div>
        <edit :profile="profile" class="align-self-start" />
      </div>
    </div>
    <div class="row mt-3">
      <div class="col-12 d-flex text-center justify-content-around">
        <h2 class="clicker" @click="keep = true">Your Keeps</h2>
        <kcreate v-if="keep" />
        <vcreate v-if="!keep" />
        <h2 class="clicker" @click="keep = false">Your Vaults</h2>
      </div>
    </div>
    <div v-if="keep" class="row">
      <div class="col-6 col-md-3" v-for="keep in keeps" :key="keep.id">
        <keep :keepData="keep" :keepId="keep.id" />
      </div>
    </div>
    <div v-else class="row">
      <div class="col-12 col-md-6" v-for="vault in vaults" :key="vault.id">
        <vault :vaultData="vault" />
      </div>
    </div>
  </div>
</template>

<script>
import keep from "../components/Keep";
import edit from "../components/EditProfile";
import vault from "../components/Vault";
import kcreate from "../components/CreateKeep";
import vcreate from "../components/CreateVault";
export default {
  mounted() {
    this.$store.dispatch("getProfile");
    this.$store.dispatch("getUserKeeps");
    this.$store.dispatch("getVaults");
  },
  computed: {
    profile() {
      return this.$store.state.profile;
    },
    keeps() {
      return this.$store.state.userKeeps;
    },
    vaults() {
      return this.$store.state.vaults;
    }
  },
  methods: {
    async editProfile() {
      if (this.editedProfile) {
        await this.$store.dispatch("editProfile", this.editedProfile);
        this.editedProfile = {};
        this.form = false;
      }
    }
  },
  components: {
    keep,
    edit, 
    vault,
    kcreate,
    vcreate
  },
  data() {
    return {
      editedProfile: {},
      form: false,
      keep:true
    };
  }
};
</script>

<style scoped>
img.profile {
  max-height: 10rem;
}
div h2.clicker:hover{
  cursor: pointer;
}
</style>>