// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import BootstrapVue from 'bootstrap-vue'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import VueResource from 'vue-resource'
import VueFormWizard from 'vue-form-wizard'
import 'vue-form-wizard/dist/vue-form-wizard.min.css'
import 'font-awesome/css/font-awesome.css'
import * as VueGoogleMaps from 'vue2-google-maps'
import Card from 'vue-credit-card'
import VueMoment from 'vue-moment'

Vue.config.productionTip = false

Vue.use(Card)
Vue.use(BootstrapVue)
Vue.use(VueResource)
Vue.use(VueFormWizard)
Vue.use(VueMoment)
Vue.use(VueGoogleMaps, {
  load: {
    key: 'AIzaSyCTAXFNuG_nqi1TRRkzKkujbOjb-JICNkE',
    libraries: 'places'
  }
})

/* eslint-disable no-new */
new Vue({
  el: '#app',
  data: { customBus: new Vue() },
  router,
  components: { App },
  template: '<App/>'
})
