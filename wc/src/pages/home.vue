<template>
  <div class="container">
    <nav class="navbar fixed-top navbar-expand-lg navbar-dark bg-primary">
      <div class="container-fluid">
        <router-link class="navbar-brand" to="/"> Wedding APP </router-link>
        <button class="navbar-toggler collapsed" type="button" data-toggle="collapse" data-target="#navbarColor02" aria-controls="navbarColor02" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="navbar-collapse collapse" id="navbarColor02" style="">
          <ul class="navbar-nav mr-auto">
            <li class="nav-item" v-if="showNavbar">
              <router-link class="nav-link" to="/register-couple">Register</router-link>
            </li>
            <li class="nav-item" v-if="!showNavbar">
              <router-link v-if="!isCouple" class="nav-link"  to="/UpdateStock">Update Stock</router-link>
            </li>
             <li class="nav-item" v-if="!showNavbar">
              <router-link v-if="!isCouple" class="nav-link"  to="/add-product">Add Product</router-link>
            </li>
            <li class="nav-item" v-if="!showNavbar">
              <router-link v-if="isCouple" class="nav-link" to="/products">Catalog</router-link>
            </li>
            <li class="nav-item" v-if="!showNavbar">
              <router-link v-if="isCouple" class="nav-link" :to="{ name: 'ShowMyList', params: { presentListId: presentListId }}">My Gifts List</router-link>
            </li>
          </ul>

           <form class="form-inline" v-if="showNavbar">
            <div>
              <input type="text" name="Account" class="form-control form-control-sm  mr-sm-2" id="inlineFormInput"
              v-validate="'required'" v-model="credentials.account"
              :class="{'input': true, 'is-danger is-invalid': errors.has('Account') }"
              placeholder="Couple Account">
            </div>
            <div>
              <input type="password" name="Password" class="form-control form-control-sm mr-sm-2" id="inlineFormInputGroup"
              v-validate="'required'" v-model="credentials.password"
              :class="{'input': true, 'is-danger is-invalid': errors.has('Password') }" placeholder="Password">
            </div>
            <button class="btn btn-sm align-middle btn-success my-2 my-sm-0" type="button" @click="validate">Sign In</button>
          </form>
          <div class=""  v-if="!showNavbar">
            <font v-if="isCouple" color="white">{{firstName}} & {{firstNamePartner}}</font>
            <font v-else color="white">{{firstName}}</font>
            <div class="text-right small text-secondary">
              <a href="#" @click="signOut">Sign Out</a>
            </div>
          </div>
        </div>
      </div>
    </nav>
    <div v-if="showError" class="col-3" style="position: absolute; right:0px; top:56px; z-index: 100; padding: 0;">
      <b-alert show variant="danger">{{ this.errorMessage }}</b-alert>
    </div>
    <router-view> </router-view>
  </div>
</template>

<script>
import SearchCoupleList from './../components/SearchCoupleList'
import Authentication from '../resources/authenticate'
import AuthService from '../services/authentication'

export default {
  name: 'Home',
  props: ['SearchCoupleList'],
  components: { SearchCoupleList },
  data () {
    return {
      credentials: {
        account: '',
        password: ''
      },
      showNavbar: true,
      firstName: '',
      firstNamePartner: '',
      isCouple: null,
      isAuthenticated: false,
      showError: false,
      errorMessage: '',
      presentListId: localStorage.getItem('productListId')
    }
  },
  methods: {
    validate () {
      this.$validator.validateAll()
        .then((result) => {
          if (!result) {
          } else {
            this.request()
          }
        })
        .catch(() => {
        })
    },
    request () {
      Authentication.authenticate(this.credentials.account, this.credentials.password)
        .then(res => {
          if (res.status === 401) {
            this.showError = true
            this.errorMessage = 'The account has expired'
          } else {
            if (res.status === 200) {
              localStorage.setItem('token', res.body.token)
              localStorage.setItem('role', res.body.role)

              if (res.body.role === 'couple') {
                localStorage.setItem('productListId', res.body.presentListId)
                localStorage.setItem('firstName', res.body.firstName)
                localStorage.setItem('firstNamePartner', res.body.firstNamePartner)
                var weddingDate = this.$moment(res.body.weddingDate)
                localStorage.setItem('weddingDate', weddingDate.format('YYYY-MM-DD'))
                this.presentListId = res.body.presentListId
                this.$router.push({name: 'ShowMyList', params: {presentListId: res.body.presentListId}})
              } else {
                this.$router.push({path: 'add-product'})
              }
            }
          }
        })
        .catch(err => {
          console.log(err)
          if (err.status === 401) {
            this.showError = true
            this.errorMessage = 'The account has expired'
          } else {
            this.showError = true
            this.errorMessage = 'The username or password is invalid'
          }

          setTimeout(() => {
            this.showError = false
          }, 3000)
        })
    },
    signOut: function () {
      AuthService.signOut()
      this.$router.push({path: '/'})
    }
  },
  mounted () {
    this.$router.push({path: '/'})
  },
  beforeUpdate () {
    var isAuth = AuthService.isAuthenticated()

    if (isAuth) {
      this.showNavbar = false
      if (AuthService.isInventoryManager()) {
        this.firstName = 'Inventory Manager'
        this.isCouple = false
      } else {
        this.isCouple = true
        this.firstName = localStorage.getItem('firstName')
        this.firstNamePartner = localStorage.getItem('firstNamePartner')
      }
    } else {
      this.showNavbar = true
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1, h2 {
  font-weight: normal;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  /* color: #42b983; */
  color: #fff;
  text-decoration: none;
}

.container{
  padding-top: 56px;
  max-width: 100%;
  height: 100%;
  position: fixed;
  overflow-y: auto;
}
</style>
