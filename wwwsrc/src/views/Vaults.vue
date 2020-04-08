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
        <edit class="align-self-start" />
      </div>
    </div>
    <div class="row">
      <div class="col-12 text-center">
        <h2>Your Keeps</h2>
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
export default {
  mounted() {},
  computed: {
    profile() {
      return this.$store.state.profile;
    },
    vaults() {
      return this.$store.state.vaults;
    }
  },
  methods: {},
  components: {},
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
</style>>