<template>
 <div>
  <form-wizard @on-complete="validateBeforeSubmit" color="#3498db" next-button-color="red ">
   <h4 slot="title">Purchase Present</h4>
   <tab-content title="Payment Details"
           icon="fa fa-credit-card" :before-change="onCompletePayment">
           <BuyPresent ref="BuyForm" :present="this.present"></BuyPresent>
   </tab-content>
   <tab-content title="Dedicatory Information"
      icon="fa fa-twitch">
      <Dedicatory ref="dedicatoryForm" :present="this.present"></Dedicatory>
   </tab-content>
    <el-button type="primary" class="btn btn-primary" slot="prev">Back</el-button>
    <el-button type="primary" class="btn btn-primary" slot="next">Next</el-button>
    <el-button type="primary" class="btn btn-success" slot="finish">Finish</el-button>
  </form-wizard>
 </div>
</template>

<script>
import BuyPresent from './BuyPresent'
import Dedicatory from './Dedicatory'
import purchaseResource from '../resources/purchaseResource'

export default {
  components: {
    BuyPresent,
    Dedicatory
  },
  data () {
    return {
      name: '',
      message: '',
      present: this.$route.params.present
    }
  },
  methods: {
    addDedycatory: function () {
    },
    validateBeforeSubmit: function () {
      let dedicatoryValidated = this.$refs.dedicatoryForm.Validate()
      dedicatoryValidated
        .then((result) => {
          if (result) {
            purchaseResource.buyProduct(this.present)
            this.$refs.dedicatoryForm.addDedicatory()
            this.$router.push({path: '/'})
          }
        })
        .catch(err => {
          console.log(err)
        })
    },
    onCompletePayment: function () {
      console.log('hi')
      return new Promise((resolve) => {
        let buyValidate = this.$refs.BuyForm.Validate()
        buyValidate
          .then((result) => {
            console.log(result)
            resolve(result)
          })
          .catch(err => {
            console.log(err)
          })
      })
    },
    onCompleteUserDedicatory: function () {
    }
  }
}
</script>

<style scoped></style>
