import Vue from 'vue'
import App from './App.vue'
import router from './router'
import vuetify from './plugins/vuetify';
import moment from 'moment'
import store from './store'
import Editor from 'vue-editor-js'
import axios from 'axios';
import webApi from './webApi'
import VueYouTubeEmbed from 'vue-youtube-embed'
import VueGeolocation from 'vue-browser-geolocation';
import VueSocialSharing from 'vue-social-sharing'

Vue.use(VueSocialSharing);

moment.locale('ka');
Vue.use(VueGeolocation);
Vue.use(VueYouTubeEmbed)

Vue.use(Editor)
Vue.prototype.$http = webApi
Vue.config.productionTip = false

axios.interceptors.request.use(function(config) {
  const authToken = localStorage.getItem('user-token')
  if (authToken) {
    config.headers.Authorization = `Bearer ${authToken}`;
  }
  return config;
}, function(err)  {
  return Promise.reject(err);
});

Vue.filter('dateToDayMonthYear', date => {
  if (!date) {
    return undefined
  }
  const momentDate = moment(date, 'YYYY-MM-DDTHH:mm:ss.SSSS')

  return momentDate.isValid() ? momentDate.format('LL') : null
})

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')
