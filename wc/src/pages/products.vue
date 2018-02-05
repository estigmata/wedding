<template>
 <div class="container-fluid">
    <b-tooltip target="tooltip" placement="right">
      You can input the name of product, brand of product, category of product or the range of prices [Eg: &#10094;500 or &#10095;500]
    </b-tooltip>
  <h2 class="my-3">Products List <i id="tooltip" class="fa fa-info-circle"></i></h2>
    <input type="text"  class="form-control" v-model="search" placeholder="Search..."/>
  <hr class="my-3">
   <div class="row">
    <ProductCard v-for="(product, index) in filteredList" :key='index' :products='product' :showMyList="showMyList" :productsInCart="shoppingCart.products"></ProductCard>
  </div>
  <div id="shopping-cart" class="fix-shopping-cart btn btn-primary"><i class="fa fa-gift fa-2x icon-style"></i><b-badge variant="light" class="fix-shopping-cart-number"><span class="text-info">{{this.countShoppingCartIcon}}</span></b-badge></div>
  <b-popover target="shopping-cart" title="Gifts List">
    <div v-if="this.shoppingCart.products.length <= 0">
      <p>Don't have a products in this gits list.</p>
    </div>
    <div v-else>
      <hr class="my-1">
      <p class="text-center"><span class="text-primary">Quantity:</span> {{this.shoppingCart.products.length}} / {{this.maxRangeQuantity}}</p>
      <p class="text-center"><span class="text-primary">Low Stock:</span> {{this.shoppingCart.quantityLessStock}} / {{this.limitStock}} <span class="text-info" v-b-popover.hover.top="popoverDescription"><i class="fa fa-info-circle"></i></span></p>
      <ShoppingCart v-for="(product, index) in this.shoppingCart.products" :key='index' :productsList='product'></ShoppingCart>
      <hr class="my-1">
      <div v-if="this.shoppingCart.save == true">
        <div v-if="isSendedList == false" class="btn btn-primary text-center" @click="PostProductsList">Add all Gifts</div>
        <div v-if="isSendedList == true" class="btn btn-primary text-center d-none">Add all Gifts</div>
        <div v-if="isSendedList == true" class="text-success">All this gifts was added successfully!</div>
      </div>
      <div v-else>
        <div class="btn btn-primary disabled text-center">Add all Gifts</div>
      </div>
    </div>
  </b-popover>
 </div>
</template>

<script>
import ProductCard from './../components/ProductCard'
import ProductResource from '../resources/productsResource'
import FilterProduct from '../services/filterProduct'
import AuthService from '../services/authentication'
import ShoppingCart from './../components/ShoppingCart'
import PresentListResource from '../resources/PresentListResource'
export default {
  components: { ProductCard, ShoppingCart },
  data () {
    return {
      products: [],
      popoverDescription: {
        title: 'Stock',
        content: 'Quantity that you can add only products with stock between 1 to 10.'
      },
      search: '',
      shoppingCart: {
        products: [],
        quantityLessStock: 0,
        save: false
      },
      limitStock: 10,
      countShoppingCartIcon: 0,
      maxRangeQuantity: 0,
      idList: '',
      isSendedList: false,
      showMyList: 0
    }
  },
  created () {
    this.$root.$on('AddShoppingCart', (sharedProduct) => {
      this.ListOfPresents(sharedProduct)
    })
  },
  computed: {
    filteredList () {
      return FilterProduct.filterProducts(this.products, this.search)
    }
  },
  methods: {
    GetAllProducts () {
      ProductResource.getCatalogueProduct()
        .then(products => {
          this.products = products['body']
        })
        .catch(err => {
          console.log(err)
        })
    },
    ListOfPresents (PressProductFromChild) {
      if (this.shoppingCart.products.length <= 0) {
        this.shoppingCart.products.push(PressProductFromChild)

        if (PressProductFromChild.stock <= this.limitStock) {
          this.shoppingCart.quantityLessStock++
        }
      } else {
        if ((this.shoppingCart.products.length < 100) && (this.isSendedList === false)) {
          var isFound = this.shoppingCart.products.find((element) => {
            return element.id === PressProductFromChild.id
          })

          if (isFound === undefined) {
            if (PressProductFromChild.stock > this.limitStock) {
              this.shoppingCart.products.push(PressProductFromChild)
            } else {
              if (this.shoppingCart.quantityLessStock < this.limitStock) {
                this.shoppingCart.products.push(PressProductFromChild)
                this.shoppingCart.quantityLessStock++
              } else {
                console.log('ya tiene 10 arituculos con un stock de 10 o menos')
              }
            }
          } else {
            console.log('ya se encuentra registrado: ' + PressProductFromChild.id)
          }
        } else {
          console.log('ya tiene un maximo de 100 arituculos.')
        }
      }

      if ((this.shoppingCart.products.length === 25) || (this.shoppingCart.products.length === 50) || (this.shoppingCart.products.length >= 100)) {
        this.shoppingCart.save = true
      } else {
        this.shoppingCart.save = false
      }

      if (this.shoppingCart.products.length <= 25) {
        this.maxRangeQuantity = 25
      }

      if ((this.shoppingCart.products.length) > 25 && (this.shoppingCart.products.length <= 50)) {
        this.maxRangeQuantity = 50
      }

      if (this.shoppingCart.products.length > 50) {
        this.maxRangeQuantity = 100
      }

      this.countShoppingCartIcon = this.shoppingCart.products.length
    },
    PostProductsList () {
      var listId = []
      this.isSendedList = true

      this.shoppingCart.products.find((element) => {
        listId.push(element.id)
      })

      PresentListResource.updatePresentList(this.idList, listId)
        .then(res => {
          if (res.status === 200) {
            this.$router.push({name: 'ShowMyList', params: {presentListId: this.idList}})
          }
        })
        .catch(err => {
          console.log(err)
        })
    }
  },
  mounted () {
    var listId = localStorage.getItem('productListId')

    PresentListResource.GetPresentList(listId)
      .then(res => {
        if (res.status === 200 && res.body.size > 0) {
          this.maxRangeQuantity = res.body.size
          var products = []

          res.body.presents.forEach(present => {
            var product = present.product
            products.push({
              id: product.id,
              name: product.name,
              stock: product.stock
            })
          })

          this.shoppingCart.products = products
          var productsWithLowStock = 0

          res.body.presents.forEach(present => {
            if (present.product.stock < this.limitStock) {
              productsWithLowStock++
            }
          })

          this.shoppingCart.quantityLessStock = productsWithLowStock
          this.countShoppingCartIcon = res.body.presents.length
          this.GetAllProducts()
        } else {
          this.GetAllProducts()
        }
      })
      .catch(err => {
        console.log(err)
      })

    var isAuth = AuthService.isAuthenticated()

    if (isAuth && AuthService.isInventoryManager()) {
      this.$router.push({path: 'add-product'})
    } else {
      if (!isAuth) {
        this.$router.push({path: '/'})
      }
    }
    this.idList = localStorage.getItem('productListId')
  }
}
</script>

<style>

.fix-shopping-cart {
  position: fixed;
  top: 85%;
  right: 5%;
  z-index: 999999;
  padding: 8px;
  color: #428bca;
  border-radius: 50%;
  border: 2px solid #428bca;
  background-color: white;
  float: left;
}

.fix-shopping-cart-number {
  float: right;
  background-color: white;
  top: 120px;
  left: 0px;
  border-radius: 50%;
  background-color: white;
  border: 1px solid #428bca;
}

.icon-style {
  float: left;
  margin-left: 5px;
}

</style>
