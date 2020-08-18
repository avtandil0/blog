import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    grid: localStorage.grid === 'true'? true: false,
    loading: false,
    // currentUser: null
  },
  mutations: {
    changeCardsMode(state) {
      state.grid = !state.grid;
      localStorage.grid = state.grid;
    },
    loadData(state, value) {
      console.log('load')
      state.loading = value;
    },
    // fillCurrentUser(state) {
    //   state.grid = !state.grid;
    //   localStorage.grid = state.grid;
    // },
  },
  actions: {
  },
  modules: {
  }
})
