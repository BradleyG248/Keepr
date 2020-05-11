<template>
  <div class="dashboard container">
    <div class="row">
      <div class="col-12">
        <h1>{{vault.name}}</h1>
        <h2>{{vault.description}}</h2>
      </div>
    </div>
    <div class="row">
      <div class="col-12 text-center">
        <h2>Keeps</h2>
      </div>
    </div>
    <div class="row">
      <div class="grid-item col-6 col-md-3" v-for="keep in keeps" :key="keep._id">
        <keep :keepData="keep" />
      </div>
    </div>
  </div>
</template>

<script>
import keep from "../components/Keep";
export default {
  async mounted() {
    if (await this.$auth.isAuthenticated) {
      this.$store.dispatch("getVaultById", this.$route.params.vaultId);
      this.$store.dispatch("getKeepsByVault", this.$route.params.vaultId);
    }
  },
  computed: {
    vault() {
      return this.$store.state.activeVault;
    },
    keeps() {
      return this.$store.state.userKeeps;
    }
  },
  methods: {},
  components: {
    keep
  },
  data() {
    return {
      form: false
    };
  }
};
</script>

<style scoped>
img.profile {
  max-height: 10rem;
}
</style>