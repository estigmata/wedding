import Vue from 'vue'
import VueResource from 'vue-resource'
import api from '../config/api'

Vue.use(VueResource)

export default {
  saveProduct (product) {
    var formData = new FormData()
    formData.append('name', product.name)
    formData.append('stock', product.stock)
    formData.append('price', product.price)
    formData.append('category', product.category)
    formData.append('brand', product.brand)
    formData.append('description', product.description)
    formData.append('image', product.image)
    formData.append('imageName', product.imageName)

    return Vue.http.post(api.url + '/products', formData, {
      headers: {
        'Authorization': 'Basic ' + localStorage.getItem('token')
      }
    })
  },
  getCatalogueProduct () {
    const url = api.url + '/products'

    return Vue.http.get(url)
  },
  saveProductList (id, productslist) {
    var products = {PresentList: productslist, ListId: id}

    return Vue.http.post(api.url + '/presentlist', products, {
      headers: {
        'Authorization': 'Basic ' + localStorage.getItem('token')
      }
    })
  },
  updateStock (product) {
    return Vue.http.put(api.url + '/products', product)
  },
  removeProduct (listId, productId) {
    return Vue.http.delete(api.url + '/presentlist?listID=' + listId + '&productID=' + productId, {})
  }
}
