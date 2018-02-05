import Vue from 'vue'
import VueResource from 'vue-resource'
import api from '../config/api'

Vue.use(VueResource)

export default {
  SearchPresentList (name, lastName) {
    return Vue.http.get(api.url + '/PresentList?name=' + name + '&lastName=' + lastName)
  },
  GetPresentListProduct (PresentListId) {
    return Vue.http.get(api.url + '/presentlist/' + PresentListId + '/presents')
  },
  GetPresentList (PresentListId) {
    return Vue.http.get(api.url + '/presentlist/' + PresentListId)
  },
  updatePresentList (listId, presentsList) {
    var presentList = {
      listId: listId,
      presentList: presentsList
    }

    return Vue.http.put(api.url + '/presentlist/' + presentList.listId + '/presents', presentList)
  }
}
