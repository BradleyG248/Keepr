<template>
  <div class="container">
    <div class="row">
      <div class="col-7">
        <img :src="keep.img" alt class="img-fluid" />
        <h1>{{keep.name}}</h1>
        <h2>{{keep.description}}</h2>
        <h5>Keeps: {{keep.keeps}}</h5>
        <h5>Views: {{keep.views}}</h5>
        <button
          type="button"
          class="btn btn-primary"
          data-toggle="modal"
          :data-target="'#editKeep' + keep.id"
        >Keep</button>
        <div
          class="modal fade"
          :id="'editKeep' + keep.id"
          tabindex="-1"
          role="dialog"
          aria-labelledby="exampleModalLabel"
          aria-hidden="true"
        >
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-body d-flex justify-content-center">
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
      </div>
      <div class="col-5">
        <img v-show="profile.img" :src="profile.img" class="profile rounded-pill" alt />
        <div class="d-flex flex-column justify-content-around">
          <h2>Creator: {{profile.name}}</h2>
          <h4>Email: {{profile.email}}</h4>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "KeepView",
  async mounted() {
    await this.$store.dispatch("getKeepById", this.$route.params.keepId);
    this.$store.dispatch("getProfileById", this.keep.userId);
    this.$store.dispatch("getVaults");
  },
  computed: {
    keep() {
      return this.$store.state.activeKeep;
    },
    profile() {
      return this.$store.state.activeProfile;
    },
    vaults() {
      return this.$store.state.vaults;
    }
  },
  methods: {
    async setVault() {
      if (this.vault) {
        await this.$store.dispatch("setKeepVault", {
          keepId: this.keep.id,
          vaultId: this.vault
        });
        this.vault = null;
      }
    }
  },
  components: {},
  data() {
    return {
      form: false,
      vault: 0
    };
  }
};
</script>

<style scoped>
img.profile {
  max-height: 10rem;
}
</style>