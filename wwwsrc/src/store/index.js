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
    vaults: [],
    activeVault: {},
    activeKeep: {},
    activeProfile: {}
  },
  mutations: {
    setPublicKeeps(state, keeps) {
      state.publicKeeps = keeps;
    },
    setUserKeeps(state, keeps) {
      state.userKeeps = keeps;
    },
    setActiveKeep(state, keep) {
      state.activeKeep = keep;
    },
    editUserKeeps(state, keep) {
      let index = state.userKeeps.findIndex(k => k.id == keep.id);
      state.userKeeps[index] = keep;
    },
    editPublicKeeps(state, keep) {
      let index = state.publicKeeps.findIndex(k => k.id == keep.id);
      state.publicKeeps[index] = keep;
    },
    addKeep(state, keep) {
      state.publicKeeps.push(keep);
      state.userKeeps.push(keep);
    },
    deleteKeep(state, keepId) {
      state.publicKeeps = state.publicKeeps.filter(k => k.id != keepId);
      state.userKeeps = state.userKeeps.filter(k => k.id != keepId);
    },
    setVaults(state, vaults) {
      state.vaults = vaults;
    },
    setActiveVault(state, vault) {
      state.activeVault = vault;
    },
    addVault(state, vault) {
      state.vaults.push(vault);
    },
    editVault(state, vault) {
      let index = state.vaults.findIndex(v => v.id == vault.id);
      state.vaults[index] = vault;
    },
    deleteVault(state, vaultId) {
      state.vaults = state.vaults.filter(v => v.id != vaultId);
    },
    setProfile(state, profile) {
      state.profile = profile;
    },
    setActiveProfile(state, profile) {
      state.activeProfile = profile;
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
    async getKeepById({ commit }, keepId) {
      let keep = await api.get(`keeps/${keepId}`);
      commit("setActiveKeep", keep.data);
    },
    async getUserKeeps({ commit }) {
      let keeps = await api.get("profile/keeps");
      commit("setUserKeeps", keeps.data);
    },
    async getKeepsByVault({ commit }, vaultId) {
      let res = await api.get(`vaults/${vaultId}/keeps`);
      commit("setUserKeeps", res.data);
    },
    async editUserKeep({ commit }, keep) {
      let res = await api.put(`keeps/${keep.id}`, keep);
      commit("editUserKeeps", res.data);
    },
    async editPublicKeep({ commit }, keep) {
      let res = await api.put(`keeps/${keep.id}`, keep);
      commit("editPublicKeeps", res.data);
    },
    async setKeepVault({ commit }, dict) {
      await api.post("vaultkeeps", dict);
    },
    async deleteKeepVault({ commit }, dict) {
      await api.delete(`vaultkeeps/${dict.vaultKeepId}`);
      commit("deleteKeep", dict.keepId);
    },
    async createKeep({ commit }, keep) {
      let res = await api.post("keeps", keep);
      commit("addKeep", res.data);
    },
    async deleteKeep({ commit }, keepId) {
      await api.delete(`keeps/${keepId}`);
      commit("deleteKeep", keepId);
    },
    async getVaults({ commit }) {
      let res = await api.get("vaults");
      commit("setVaults", res.data);
    },
    async getVaultById({ commit }, vaultId) {
      let res = await api.get(`vaults/${vaultId}`);
      commit("setActiveVault", res.data);
    },
    async editVault({ commit }, vault) {
      let res = await api.put(`vaults/${vault.id}`, vault);
      commit("editVault", res.data);
    },
    async createVault({ commit }, vault) {
      let res = await api.post("vaults", vault);
      commit("addVault", res.data);
    },
    async deleteVault({ commit }, vaultId) {
      await api.delete(`vaults/${vaultId}`);
      commit("deleteVault", vaultId);
    },
    async getProfile({ commit }) {
      let profile = await api.get("profile");
      commit("setProfile", profile.data);
    },
    async getProfileById({ commit }, userId) {
      let profile = await api.get(`profile/${userId}`);
      commit("setActiveProfile", profile.data);
    },
    async editProfile({ commit }, newProfile) {
      let profile = await api.put("profile", newProfile);
      commit("setProfile", profile.data);
    }
  }
});
