<template>
  <div class="row">
    <div class="col-0 col-md-7 weeding-image">
        <div class="legend">
          <h3><small>Enjoy with your Presents on the most Beutiful day of your Lifes </small></h3>
          <button type="button" class="btn btn-success btn-lg btn-block" @click="register" >
              Get Started
            </button>
        </div>
    </div>
     <div class="col-12 col-md-5">
      <div class="search-form">
        <div class="tittle">
          <h3>Search a List of Presents</h3>
        </div>
        <b-container fluid="">
          <b-row class="my-1">
            <b-col sm="3">
              <label for="input-large">*Name:</label>
            </b-col>
            <b-col sm="9">
              <b-form-input id="name" size="lg" type="text" v-model="name" placeholder="Enter Partner Name"></b-form-input>
            </b-col>
          </b-row>
          <b-row class="my-1">
            <b-col sm="3">
              <label for="input-large">*Last Name:</label>
            </b-col>
            <b-col sm="9">
              <b-form-input id="last-name" size="lg" type="text" v-model="lastName" placeholder="Enter Partner Last Name"></b-form-input>
            </b-col>
          </b-row>
          <b-row class="my-1">
            <b-col sm="12" class="error" v-for="(message, index) in this.errorMessage" :key='index'>
              {{message}}
            </b-col>
          </b-row>
        </b-container>
        <div class="submit">
            <button type="button" class="btn btn-primary btn-lg btn-block" @click="submitSearch" >
              Search
            </button>
        </div>
      </div>
      <foundedcoupleslist ref="form"></foundedcoupleslist>
      <div class="logo"></div>
    </div>
  </div>
</template>

<script>
import foundedcoupleslist from '@/components/FoundedCouplesList'
import AuthService from '../services/authentication'
export default {
  props: ['foundedcoupleslist'],
  components: { foundedcoupleslist },
  data () {
    return {
      name: '',
      lastName: '',
      errorMessage: []
    }
  },
  methods: {
    submitSearch: function () {
      this.validate()
      if (this.errorMessage.length === 0) {
        this.$refs.form.search(this.name, this.lastName)
      }
    },
    validate: function () {
      this.errorMessage = []
      if (this.name.trim().length === 0) {
        this.errorMessage.push('* Name cannot be empty \n')
      }
      if (this.lastName.trim().length === 0) {
        this.errorMessage.push('* Last Name cannot be empty \n')
      }
    },
    register: function () {
      this.$router.push({path: '/register-couple'})
    }
  },
  mounted () {
    if (AuthService.isAuthenticated()) {
      if (AuthService.isCouple()) {
        this.$router.push({path: 'products'})
      }

      if (AuthService.isInventoryManager()) {
        this.$router.push({path: 'add-product'})
      }
    }
  }
}

</script>

<style scoped>
.search-form{
  text-align: center;
  width: 100%;
  height: auto;
  margin-top: 50px;
  position: relative;
  bottom: 15px;

}

.field{
  display: inline;
}

.error{
  color: red;
  font-size: 12px;
}

.weeding-image{
  background-image: url(../../image/background.jpg);
  background-size: 100% 100%;
  background-repeat: no-repeat;
}

.row {
  height: 100%;
}

.legend{
  margin-left: 25px;
  color: black;
  text-align: center;
  width: 45%;
  height: 100px;
  position: relative;
  top: 200px;
  text-shadow: 1px 1px #878787;
}

.logo{
  position: absolute;
  bottom: 0px;
  right: 0px;
  background-image: url(../../image/logo.png);
  background-size: 100% 100%;
  width: 150px;
  height: 50px;
  margin: 25px;
  background-repeat: no-repeat;
}

@media screen and (max-width: 770px){
  .weeding-image{
    display: none;
  }
}
</style>
