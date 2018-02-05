<template>
    <div class="col-xs-12 col-sm-6 col-md-4 col-lg-3">
      <b-card no-body class="text-center style-padding-card">
        <div class="style-product-card">
          <div class="text-left text-primary"><h5 class="text-truncate font-weight-bold" v-b-popover.hover.top="popoverName">{{this.products.name}}</h5></div>
          <div class="text-left text-dark"><h6 class="text-truncate font-weight-normal" v-b-popover.hover.top="popoverBrand">{{this.products.brand}}</h6></div>
          <img :src="'data:image/jpeg;base64,' + this.products.image" class="img-thumbnail image-style">
          <div class="row">
            <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6 text-left"><h6 class="text-truncate"><span class="text-dark">Bs. </span><span class="text-secondary">{{this.products.price}}</span></h6></div>
            <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6 text-center">
              <span v-if="this.products.stock <= 10 && this.products.stock > 0" class="text-warning" v-b-popover.hover.top="popoverStockWarning"><i class="fa fa-exclamation-triangle"></i></span>
              <span v-if="this.products.stock == 0" class="text-danger" v-b-popover.hover.top="popoverStockDanger"><i class="fa fa-exclamation-triangle"></i></span>
              <span class="text-info" v-b-popover.hover.top="popoverDescription"><i class="fa fa-info-circle"></i> Info.</span></div>
          </div>
          <div v-if="this.showMyList !== 0">
            <div v-if="this.present.message !== 'Already Bought' && this.showRemove">
              <button
                class="btn btn-sm btn-danger style-button-card"
                @click="removeProduct(present)">
                Remove from my list
              </button>
            </div>
            <div v-else>
              <b-alert show variant="success">{{this.present.message}}</b-alert>
            </div>
          </div>
          <div v-else>
            <div v-if="this.present === undefined">
              <div v-if="!alreadyInCart(this.products)">
                <button v-if="this.products.stock > 0" type="button" class="btn btn-primary style-button-card" @click="sharedProduct(products.id, products.name, products.stock)"><span class="fa fa-plus"></span> Add To Gift</button>
                <button v-if="this.products.stock == 0" type="button" class="btn btn-primary style-button-card disabled"><span class="fa fa-plus"></span> Add To Gift</button>
              </div>
              <div v-else class="alert">
                Added to Gift
              </div>
            </div>
            <div v-else>
              <div v-if="this.present.message != 'Available'" class="alert">
                {{this.present.message}}
              </div>
              <div v-else>
                <button v-if="this.products.stock > 0" type="button" class="btn btn-primary style-button-card" @click="buyProduct(products)"><span class="fa fa-shopping-cart"></span> Buy</button>
              </div>
            </div>
          </div>
        </div>
      </b-card>
    </div>
</template>

<script>
import ProductResource from '../resources/productsResource'

export default {
  props: ['products', 'present', 'showMyList', 'productsInCart'],
  data () {
    return {
      childSharedProduct: '',
      popoverDescription: {
        title: 'Description',
        content: this.products.description
      },
      popoverName: {
        title: 'Name',
        content: this.products.name
      },
      popoverBrand: {
        title: 'Brand',
        content: this.products.brand
      },
      popoverStockWarning: {
        title: 'Stock',
        content: 'This product have less or equal than 10 products in stock.'
      },
      popoverStockDanger: {
        title: 'Stock',
        content: 'This product dont have stock.'
      },
      showRemove: true
    }
  },
  methods: {
    sharedProduct (id, name, stock) {
      this.childSharedProduct = {id: id, name: name, stock: stock}
      this.$root.$emit('AddShoppingCart', this.childSharedProduct)
    },
    buyProduct () {
      this.$router.push({name: 'PurchasePresent', params: {present: this.present}})
    },
    removeProduct (myPresent) {
      ProductResource.removeProduct(parseInt(localStorage.getItem('productListId')), myPresent.product.id)
        .then(result => {
          if (result.status === 200) {
            this.$parent.$emit('removePresent', myPresent)
          }
        })
    },
    alreadyInCart (product) {
      for (let index = 0; index < this.productsInCart.length; index++) {
        if (this.productsInCart[index].id === product.id) {
          return true
        }
      }
      return false
    }
  },
  mounted () {
    var nowFormat = this.$moment().format('YYYY-MM-DD')
    var weddingDate = this.$moment(localStorage.getItem('weddingDate'))
    var now = this.$moment(nowFormat)

    if (now > weddingDate.subtract(7, 'day')) {
      this.showRemove = false
    }
  }
}
</script>

<style>
  .padding-letters {
    padding-bottom: 2px;
  }

  .style-product-card {
    box-sizing: border-box;
    padding: 7px;
  }

  .style-button-card {
    border-radius: 25px;
    box-sizing: border-box;
    margin-top: 15px;
    margin-bottom: 10px;
  }

  .style-padding-card {
    margin-bottom: 10px;
  }
  .alert {
    color: #e13a00;
    margin: 0px;
    height: 63px;
  }

  .image-style {
    width: 100%;
    height: 180px;
    max-height: 180px;
  }
</style>
