import Vue from 'vue'
import VueResource from 'vue-resource'
import api from '../config/api'

Vue.use(VueResource)

export default {
  saveCouple (couple) {
    return Vue.http.post(api.url + '/couples', couple)
  }
}
