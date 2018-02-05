<template>
  <div>
    <b-form-row>
      <b-table hover fixed :items="products" :fields="myfields">
        <template slot="image" slot-scope="row">
            <b-img class="img-thumbnail" :src="'data:image/jpeg;base64,' + row.item.image" alt="Responsive image" />
        </template>
        <template slot="newStock" slot-scope="row">
          <b-row>
            <b-form-group id="product-newStock-group"
              label-for="product-newStock">
              <b-form-input
                :key="row.item.id"
                v-model="newStock[row.item.id]"
                type="text"
                :name="'newStock_' + row.item.id"
                placeholder="100"
                v-validate="'numeric'"
                :class="{'input': true, 'is-danger is-invalid': errors.has('newStock_' + row.item.id) }"
                ></b-form-input>
              <span v-show="errors.has('newStock_' + row.item.id)" class="help is-danger invalid-feedback">Only numbers are allowed</span>
            </b-form-group>
          </b-row>
        </template>
        <template slot="stock" slot-scope="row">
          <span>{{ row.item.stock }}</span>
          <br>
          <div class="success-message" v-show="messages[row.item.id] == true">
            The stock was updated succesfully
          </div>
        </template>
        <template slot="operations" slot-scope="row">
          <div>
            <b-button
              :disabled="newStock[row.item.id] == null || newStock[row.item.id] === '' || errors.has('newStock_' + row.item.id) || newStock[row.item.id] <= 0" size="sm" class="mr-2" variant="link" @click="add(row)">Add</b-button>
            <b-button :disabled="newStock[row.item.id] == null || newStock[row.item.id] === '' || errors.has('newStock_' + row.item.id) || newStock[row.item.id] > row.item.stock || newStock[row.item.id] <= 0" size="sm" class="mr-2" variant="link" @click="remove(row)">Remove</b-button>
          </div>
        </template>
      </b-table>
    </b-form-row>
  </div>
</template>

<script>
import ProductResource from '../resources/productsResource'
import AuthService from '../services/authentication'

export default {
  data () {
    return {
      myfields: ['image', {
        key: 'name', label: 'Name', sortable: true
      }, {
        key: 'stock', label: 'Current Stock', sortable: false, 'class': 'text-center'
      }, 'newStock', 'operations'],
      products: [],
      sortBy: null,
      sortDesc: false,
      newStock: [],
      messages: [],
      notificationDuration: 2000
    }
  },
  methods: {
    add: function (row) {
      var product = this.products.find(p => p.id === row.item.id)
      product.newStock = this.newStock[row.item.id]

      ProductResource.updateStock(product)
        .then(res => {
          if (res.status === 200) {
            product.stock = res.body.stock
            this.newStock = []
            this.messages[row.item.id] = true

            return new Promise(resolve => {
              setTimeout(resolve, 2000)
            })
          }
        })
        .then(() => {
          this.messages = []
        })
        .catch(err => {
          console.log(err)
        })
    },
    remove: function (row) {
      var product = this.products.find(p => p.id === row.item.id)
      var newStock = this.newStock[row.item.id]

      product.newStock = newStock * (-1)

      ProductResource.updateStock(product)
        .then(res => {
          if (res.status === 200) {
            product.stock = res.body.stock
            this.newStock = []
            this.messages[row.item.id] = true

            return new Promise(resolve => {
              setTimeout(resolve, 2000)
            })
          }
        })
        .then(() => {
          this.messages = []
        })
        .catch(err => {
          console.log(err)
        })
    },
    delay: function (milliseconds, res) {
      return new Promise((resolve, reject) => {
        setTimeout(() => {
          resolve(res)
        }, milliseconds)
      })
    }
  },
  mounted () {
    var isAuth = AuthService.isAuthenticated()

    if (isAuth && AuthService.isInventoryManager()) {
      ProductResource.getCatalogueProduct()
        .then(res => {
          this.products = res.body
        })
        .catch(err => {
          console.log(err)
        })
    } else {
      this.$router.push({path: '/'})
    }
  }
}

</script>

<style scoped>
  table button {
    text-decoration: none !important;
  }

  .success-message {
    color: #28a745;
    font-size: 80%;
    width: 100%;
    margin-top: .25rem;
  }

  .img-thumbnail {
    max-width: 50%;
  }

  @media screen and (max-width: 900px) {
    .img-thumbnail {
      max-width: 100%;
    }
  }

</style>
