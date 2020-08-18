import Vue from 'vue';
import Vuetify, { VSnackbar } from 'vuetify/lib';
import VuetifyToast from 'vuetify-toast-snackbar'


Vue.use(VuetifyToast, {
	x: 'right', // default
	y: 'bottom', // default
	timeout: 3000, // default
	dismissable: true, // default
	autoHeight: false, // default
	multiLine: false, // default
	vertical: false, // default
	shorts: {
		custom: {
			color: 'purple'
		}
	},
	property: '$toast' // default
})

Vue.use(Vuetify, {
    components: {
      VSnackbar
    }
  })

Vue.use(VuetifyToast)
export default new Vuetify({

});
