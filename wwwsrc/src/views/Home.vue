<template>
  <div class="container">
    <div class="row">
      <div class="home col-12">
        <h1>Welcome Home</h1>
      </div>
    </div>
    <div class="row">
      <div class="col-6 col-md-4" v-for="keep in keeps" :key="keep._id">
        <keep :keepData="keep" :keepId="keep.id" />
      </div>
    </div>
  </div>
</template>

<script>
import keep from "../components/Keep";
export default {
  name: "home",
  computed: {
    user() {
      return this.$store.state.user;
    },
    keeps() {
      return this.$store.state.publicKeeps;
    }
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    }
  },
  mounted() {
    this.$store.dispatch("getKeeps");
    this.$store.dispatch("getVaults");
  },
  components: {
    keep
  }
};
</script>
