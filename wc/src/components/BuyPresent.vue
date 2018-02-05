<template>
<div>
 <div class="row">
  <div class="hidden-xs ol-xs-12 col-sm-3 col-md-3 col-lg-3 separator-border">
    <div class="product-box">

        <h6 class="text-primary">You are purchasing:</h6>
        <h6 class="text-secondary">{{this.present.product.name}}</h6>
        <h6 class="text-primary">Total:</h6><h6 class="text-secondary">Bs. {{this.present.product.price}}</h6>
        <h6 class="text-primary">Description:</h6>
        <h6 class="text-secondary">{{this.present.product.description}}</h6>
    </div>
  </div>
  <div class="col-xs-12 col-sm-9 col-md-9 col-lg-9">
    <div>
        <h6 class="text-primary">We accept:</h6>
        <img src="../../image/AllCreditCards.jpg" class="img-thumbnail image-cards">
        <div class="card-wrapper">
          <card v-model="cardDetail"></card>
        </div>
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <form class="form-position">

                  <div class="form-group">
                    <input type="text" class="form-control" name="name-card" id="credit-card-name" placeholder="Name On Card"  v-model="cardDetail.name" v-card-focus v-validate="'required|alpha_spaces'">
                    <i v-show="errors.has('name-card')" class="fa fa-warning"></i>
                    <span v-show="errors.has('name-card')" class="help is-danger">
                      {{ errors.first('name-card') }}
                    </span>
                  </div>

                  <div class="form-group">
                    <input type="tel" class="form-control" name="number" placeholder="Card Number" v-model="cardDetail.number" v-card-focus v-validate="'required|credit_card'">
                    <i v-show="errors.has('number')" class="fa fa-warning"></i>
                    <span v-show="errors.has('number')" class="help is-danger">
                      {{ errors.first('number') }}
                    </span>
                  </div>

                  <div class="row">
                    <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                      <div class="form-group">
                        <input type="month" class="form-control" name="date" id="credit-card-expiry" placeholder="YY/MM" v-model="cardDetail.expiry" v-card-focus v-validate="'required'">
                        <i v-show="errors.has('date')" class="fa fa-warning"></i>
                        <span v-show="errors.has('date')" class="help is-danger">
                          {{ errors.first('date') }}
                        </span>
                      </div>
                    </div>

                    <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                      <div class="form-group">
                        <input name="cvc" class="form-control" id="credit-card-cvc" placeholder="CVV" type="number" v-model="cardDetail.cvc" v-card-focus v-validate="'required'">
                        <i v-show="errors.has('cvc')" class="fa fa-warning"></i>
                        <span v-show="errors.has('cvc')" class="help is-danger">
                          {{ errors.first('cvc') }}
                        </span>
                      </div>
                    </div>

                  </div>
                </form>
            </div>
        </div>
    </div>
  </div>
 </div>
</div>
</template>

<script>
import card from 'vue-credit-card'
export default {
  props: ['present'],
  components: {
    card
  },
  data () {
    return {
      cardDetail: {
        number: '',
        name: '',
        expiry: '',
        cvc: ''
      }
    }
  },
  methods: {
    Validate () {
      return new Promise((resolve, reject) => {
        this.$validator.attach({ name: 'name-card', rules: 'required' })
        this.$validator.attach({ number: 'number', rules: 'required' })
        this.$validator.attach({ date: 'date', rules: 'required' })
        this.$validator.attach({ cvc: 'cvc', rules: 'required' })
        this.$validator.validateAll({
          name: this.cardDetail.name,
          number: this.cardDetail.number,
          date: this.cardDetail.expiry,
          cvc: this.cardDetail.cvc
        })
          .then((result) => {
            resolve(result)
          })
      })
    }
  },
  computed: {
    isValid () {
      return !this.errors.any()
    }
  }
}
</script>

<style scoped>
    .product-box {
        width: 100%;
        margin-top: 25px;
    }

    .separator-border {
        border-right: solid 2px #428bca;
    }

    .image-box {
        width: 60%;
        height: 100px;
        box-sizing: border-box;
        margin-left: 30px;
        margin-bottom: 15px;
    }

    .form-position {
        margin-top: 30px;
        margin-left: 5%;
        margin-bottom: 30px;

        box-sizing: border-box;
        padding: 25px;

        border: solid 2px #428bca;
        border-radius: 15px 15px;

        float: right;
    }

    .image-cards {
      width: 120px;
      height: 35px;
      float: left;
    }

    .card-wrapper {
      float: left;
      clear: both;
    }
</style>
