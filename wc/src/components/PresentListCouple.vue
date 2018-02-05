<template>
    <div class="container-fluid">
  <b-tooltip target="tooltip" placement="right">
        You can input the name of present, brand of present, category of present or the range of prices [Eg: &#10094;500 or &#10095;500]
      </b-tooltip>
  <h2 class="my-3">Present List of {{this.husband}} and {{this.wife}}
<i id="tooltip" class="fa fa-info-circle little"></i></h2>
    <input type="text"  class="form-control" v-model="search" placeholder="Search..."/>
  <hr class="my-3">
  <div class="row" v-if="!this.presents">
    <b-alert show variant="danger" class="full-row">NO PRODUCTS ON THE LIST</b-alert>
  </div>
  <div class="row" v-else>
    <ProductCard v-for="(present, index) in filteredList" :showMyList="0" :key='index' :products='present.product' :present="present"></ProductCard>
  </div>
 </div>
</template>

<script>
import ProductCard from './../components/ProductCard'
import PresentListResource from '../resources/PresentListResource'
import FilterProduct from '../services/filterProduct'
import AuthService from '../services/authentication'
export default {
  components: { ProductCard },
  data () {
    return {
      husband: this.$route.params.husband,
      wife: this.$route.params.wife,
      presentListId: this.$route.params.presentListId,
      search: '',
      presents: []
    }
  },
  computed: {
    filteredList () {
      return FilterProduct.filterPresents(this.presents, this.search)
    }
  },
  methods: {
    GetAllProducts () {
      PresentListResource.GetPresentListProduct(this.$route.params.presentListId)
        .then(result => {
          this.presents = result['body']
        })
        .catch(err => {
          console.log(err)
        })
    }
  },
  mounted () {
    this.GetAllProducts()
    var isAuth = AuthService.isAuthenticated()
    if (isAuth && AuthService.isInventoryManager()) {
      this.$router.push({path: 'add-product'})
    }
  }
}
</script>

<style scoped>
  .little {
    font-size: 25px;
  }
  .full-row {
    width: 100%;
    text-align: center;
  }
</style>
