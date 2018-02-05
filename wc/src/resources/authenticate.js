import Vue from 'vue'
import VueResource from 'vue-resource'
import api from '../config/api'

Vue.use(VueResource)

export default {
  authenticate (account, password) {
    var credentials = {
      account: account,
      password: password
    }
    return Vue.http.post(api.url + '/accounts/authenticate', credentials)
  }
}
