<template>
  <div class="container-fluid">
    <h2 class="my-3">My Gifts List</h2>
    <div class="row" v-if="!this.giftsList">
      <b-alert show variant="danger" class="full-row">NO PRODUCTS ON THE LIST</b-alert>
    </div>
    <div class="row" v-else>
      <ProductCard v-for="(present, index) in giftsList"
        :key="index"
        :products="present.product"
        :present="present"
        :showMyList="showMyList"></ProductCard>
    </div>
    <div id="shopping-cart" class="fix-shopping-cart btn btn-primary"><i class="fa fa-list-alt fa-2x"></i><b-badge variant="light" class="fix-shopping-cart-number"><span class="text-info">{{this.countShoppingCartIcon}}</span></b-badge></div>
    <b-popover target="shopping-cart" title="Gifts List">
      <div v-if="this.giftsList.length <= 0">
        <p>Don't have a products in this gits list.</p>
      </div>
      <div v-else>
        <hr class="my-1">

        <p class="text-center"><span class="text-primary">Quantity:</span> {{this.giftsList.length}} / {{this.listSize}}</p>

        <p class="text-center"><span class="text-primary">Low Stock:</span> {{this.shoppingCart.quantityLessStock}} / {{this.limitStock}} <span class="text-info" v-b-popover.hover.top="popoverDescription"><i class="fa fa-info-circle"></i></span></p>
        <ShoppingCart v-for="(product, index) in this.shoppingCart.products" :key='index' :productsList='product'></ShoppingCart>
        <hr class="my-1">

        <div class="btn btn-primary text-center" @click="goToCatalog">Update List</div>
      </div>
    </b-popover>
  </div>
</template>

<script>
import ProductCard from './../components/ProductCard'
import PresentListResource from '../resources/PresentListResource'
import ShoppingCart from './ShoppingCart'
import AuthService from '../services/authentication'

export default {
  name: 'ShowMyList',
  components: { ProductCard, ShoppingCart },
  data () {
    return {
      search: '',
      giftsList: [],
      showMyList: 0,
      listSize: 0,
      countShoppingCartIcon: 0,
      shoppingCart: {
        products: [],
        quantityLessStock: 0,
        save: false
      },
      limitStock: 10,
      isSendedList: false,
      popoverDescription: {
        title: 'Stock',
        content: 'Quantity that you can add only products with stock between 1 to 10.'
      }
    }
  },
  computed: {
  },
  methods: {
    GetAllProducts () {
      PresentListResource.GetPresentListProduct(this.$route.params.presentListId)
        .then(result => {
          this.giftsList = result['body']
          this.shoppingCart.products = result['body']
          if (this.giftsList.length === 0) {
            this.$router.push({path: '/products'})
          }
          this.showMyList = localStorage.getItem('productListId')
        })
        .catch(err => {
          console.log(err)
        })
    },
    goToCatalog () {
      this.$router.push({path: '/products'})
    }
  },
  mounted () {
    if (AuthService.isAuthenticated() && AuthService.isCouple()) {
      this.GetAllProducts()
      PresentListResource.GetPresentList(this.$route.params.presentListId)
        .then(result => {
          this.countShoppingCartIcon = result.body.presents.length
          this.listSize = result.body.size
        })
    } else {
      this.$router.push({path: '/'})
    }
  },
  beforeUpdate () {
    this.$on('removePresent', function (myPresent) {
      for (let index = 0; index < this.giftsList.length; index++) {
        if (this.giftsList[index].id === myPresent.id) {
          this.giftsList.splice(index, 1)
          this.countShoppingCartIcon = this.giftsList.length
        }
      }
      if (this.giftsList.length === 0) {
        this.$router.push({path: '/products'})
      }
    })
  }
}
</script>
