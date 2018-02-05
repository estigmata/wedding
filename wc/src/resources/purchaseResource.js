import Vue from 'vue'
import VueResource from 'vue-resource'
import api from '../config/api'

Vue.use(VueResource)

export default {
  saveDedicatory (dedicatory) {
    return Vue.http.post(api.url + '/dedicatory', dedicatory)
  },
  buyProduct (present) {
    return Vue.http.post(api.url + '/presentList/buy', present)
  }
}
