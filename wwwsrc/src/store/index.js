import Vue from "vue";
import Vuex from "vuex";
import Axios from "axios";
import router from "../router";

Vue.use(Vuex);

let baseUrl = location.host.includes("localhost")
  ? "https://localhost:5001/"
  : "/";

let api = Axios.create({
  baseURL: baseUrl + "api/",
  timeout: 3000,
  withCredentials: true
});

export default new Vuex.Store({
  state: {
    profile: {},
    publicKeeps: [],
    userKeeps: [],
    vaults: []
  },
  mutations: {
    setPublicKeeps(state, keeps) {
      state.publicKeeps = keeps;
    },
    setUserKeeps(state, keeps) {
      state.userKeeps = keeps;
    },
    addKeep(state, keep) {
      state.publicKeeps.push(keep);
    },
    editUserKeeps(state, keep) {
      let index = state.userKeeps.findIndex(k => k.id == keep.id);
      state.userKeeps[index] = keep;
    },
    editPublicKeeps(state, keep) {
      let index = state.publicKeeps.findIndex(k => k.id == keep.id);
      state.publicKeeps[index] = keep;
    },
    deleteKeep(state, keepId) {
      let index = state.publicKeeps.findIndex(k => k.id == keepId);
      state.publicKeeps.splice(index, 1);
    },
    setVaults(state, vaults) {
      state.vaults = vaults;
    },
    addVault(state, vault) {
      state.vaults.push(vault);
    },
    editVault(state, vault) {
      let index = state.vaults.findIndex(v => v.id == vault.id);
      state.vaults[index] = vault;
    },
    deleteVault(state, vaultId) {
      let index = state.vaults.findIndex(k => k.id == vaultId);
      state.vaults.splice(index, 1);
    },
    setProfile(state, profile) {
      state.profile = profile;
    }
  },
  actions: {
    setBearer({ }, bearer) {
      api.defaults.headers.authorization = bearer;
    },
    resetBearer() {
      api.defaults.headers.authorization = "";
    },
    async getKeeps({ commit, dispatch }) {
      let keeps = await api.get("keeps");
      commit("setPublicKeeps", keeps.data);
    },
    async getUserKeeps({ commit }) {
      let keeps = await api.get("profile/keeps");
      commit("setUserKeeps", keeps.data);
    },
    async editUserKeep({ commit }, keep) {
      let res = await api.put(`keeps/${keep.id}`, keep);
      commit("editUserKeeps", res.data);
    },
    async editPublicKeep({ commit }, keep) {
      let res = await api.put(`keeps/${keep.id}`, keep);
      commit("editPublicKeeps", res.data);
    },
    async getVaults({commit}){
      let res = await api.get("vaults");
      commit("setVaults", res.data);
    },
    async editVault({commit}, vault){
      let res = await api.put(`vaults/${vault.id}`, vault);
      commit("editVault", res.data);
    },
    async getProfile({ commit }) {
      let profile = await api.get("profile");
      commit("setProfile", profile.data);
    },
    async editProfile({ commit }, newProfile) {
      let profile = await api.put("profile", newProfile);
      commit("setProfile", profile.data);
    }
  }
});
