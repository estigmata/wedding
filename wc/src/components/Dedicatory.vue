<template>
    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
      <br>
      <h3 class="text-center">Add a Dedicatory</h3>
      <form class="form-position">
        <div class="form-group">
          <input type="text" class="form-control" name="namefield" id="credit-dedicatory-name" placeholder="Name or Names"  v-model="dedicatory.name" v-validate="'required|max:100'">
          <i v-show="errors.has('namefield')" class="fa fa-warning"></i>
          <span v-show="errors.has('namefield')" class="help is-danger">
            {{ errors.first('namefield') }}
          </span>
        </div>
        <div class="form-group">
          <textarea class="form-control" name="message" placeholder="Add your Dedicatory" v-model="dedicatory.message" v-card-focus v-validate="'max:255'"></textarea>
          <i v-show="errors.has('message')" class="fa fa-warning"></i>
          <span v-show="errors.has('message')" class="help is-danger">
            {{ errors.first('message') }}
          </span>
        </div>
      </form>
    </div>
</template>

<script>
import PurchaseResource from '../resources/purchaseResource'
export default {
  props: ['present'],
  data () {
    return {
      dedicatory: {
        name: '',
        message: ''
      }
    }
  },
  methods: {
    addDedicatory: function () {
      PurchaseResource.saveDedicatory({name: this.dedicatory.name, message: this.dedicatory.message, PresentListProductID: this.present.id})
    },
    Validate () {
      return new Promise((resolve, reject) => {
        this.$validator.attach({ namefield: 'namefield', rules: 'required|max:100' })
        this.$validator.attach({ message: 'message', rules: 'max:255' })
        this.$validator.validateAll({
          namefield: this.dedicatory.name,
          message: this.dedicatory.message
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

<style>

</style>
