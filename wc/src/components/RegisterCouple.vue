<template>
  <form-wizard @on-complete="validateBeforeSubmit">
    <h4 slot="title">Register Account</h4>
    <tab-content title="Your Details"
      icon="fa fa-user"
      :before-change="onCompleteUser">
      <div class="input">
        <input name="FirstName"
          class="form-control"
          v-model="FirstName"
          v-validate="'required|alpha'"
          :class="{'input': true, 'is-danger': errors.has('FirstName') }"
          type="text"
          placeholder="First Name" />
          <i v-show="errors.has('FirstName')" class="fa fa-warning"></i>
          <span v-show="errors.has('FirstName')" class="help is-danger">
            {{ errors.first('FirstName') }}
          </span>
      </div>
      <div class="input">
        <input name="LastName"
          class="form-control"
          v-model="LastName"
          v-validate="'required|alpha'"
          :class="{'input': true, 'is-danger': errors.has('LastName') }"
          type="text"
          placeholder="Last Name" />
          <i v-show="errors.has('LastName')" class="fa fa-warning"></i>
          <span v-show="errors.has('LastName')" class="help is-danger">
            {{ errors.first('LastName') }}
          </span>
      </div>
      <div class="input">
        <input name="Id"
          class="form-control"
          v-model="Id"
          v-validate="'required|numeric|min:7|min_value:1'"
          :class="{'input': true, 'is-danger': errors.has('Id') }"
          type="text"
          placeholder="CI" />
          <i v-show="errors.has('Id')" class="fa fa-warning"></i>
          <span v-show="errors.has('Id')" class="help is-danger">
            {{ errors.first('Id') }}
          </span>
      </div>
      <div class="input">
        <input name="Telephone"
          class="form-control"
          v-model="Telephone"
          v-validate="'required|numeric'"
          :class="{'input': true, 'is-danger': errors.has('Telephone') }"
          type="text"
          placeholder="Telephone" />
          <i v-show="errors.has('Telephone')" class="fa fa-warning"></i>
          <span v-show="errors.has('Telephone')" class="help is-danger">
            {{ errors.first('Telephone') }}
          </span>
      </div>
      <div class="input">
        <input name="Mail"
          class="form-control"
          v-model="Mail"
          v-validate="'required|email'"
          data-vv-delay="1000"
          :class="{'input': true, 'is-danger': errors.has('Mail') }"
          type="text"
          placeholder="Email" />
        <i v-show="errors.has('Mail')" class="fa fa-warning"></i>
        <span v-show="errors.has('Mail')" class="help is-danger">
          {{ errors.first('Mail') }}
        </span>
      </div>
      <div class="input">
        <input name="Address"
          class="form-control"
          v-model="Address"
          v-validate="'required'"
          :class="{'input': true, 'is-danger': errors.has('Address') }"
          type="text"
          placeholder="Address" />
        <i v-show="errors.has('Address')" class="fa fa-warning"></i>
        <span v-show="errors.has('Address')" class="help is-danger">{{ errors.first('Address') }}</span>
      </div>
    </tab-content>
    <tab-content title="My significant other"
      icon="fa fa-heart"
      :before-change="onCompleteUserPartner">
      <div class="input">
        <input name="FirstNamePartner"
          class="form-control"
          v-model="FirstNamePartner"
          v-validate="'required|alpha'"
          :class="{'input': true, 'is-danger': errors.has('FirstNamePartner') }"
          type="text"
          placeholder="First Name" />
        <i v-show="errors.has('FirstNamePartner')" class="fa fa-warning"></i>
        <span v-show="errors.has('FirstNamePartner')" class="help is-danger">
          {{ errors.first('FirstNamePartner') }}
        </span>
      </div>
      <div class="input">
        <input name="LastNamePartner"
          class="form-control"
          v-model="LastNamePartner"
          v-validate="'required|alpha'"
          :class="{'input': true, 'is-danger':errors.has('LastNamePartner') }"
          type="text"
          placeholder="Last Name" />
          <i v-show="errors.has('LastNamePartner')" class="fa fa-warning"></i>
          <span v-show="errors.has('LastNamePartner')" class="help is-danger">{{ errors.first('LastNamePartner') }}</span>
      </div>
      <div class="input">
        <input name="IdPartner"
          class="form-control"
          v-model="IdPartner"
          v-validate="'required|numeric|min:7|min_value:1'"
          :class="{'input': true, 'is-danger': errors.has('IdPartner') }"
          type="text"
          placeholder="CI" />
          <i v-show="errors.has('IdPartner')" class="fa fa-warning"></i>
          <span v-show="errors.has('IdPartner')" class="help is-danger">{{ errors.first('IdPartner') }}</span>
      </div>
      <div class="input">
        <input name="TelephonePartner"
          class="form-control"
          v-model="TelephonePartner"
          v-validate="'required|numeric'"
          :class="{'input': true, 'is-danger': errors.has('TelephonePartner') }"
          type="text"
          placeholder="Telephone" />
          <i v-show="errors.has('TelephonePartner')" class="fa fa-warning"></i>
          <span v-show="errors.has('TelephonePartner')" class="help is-danger">{{ errors.first('TelephonePartner') }}</span>
      </div>
      <div class="input">
        <input name="MailPartner"
          class="form-control"
          v-model="MailPartner"
          v-validate="'required|email'"
          data-vv-delay="1000"
          :class="{'input': true, 'is-danger': errors.has('MailPartner') }"
          type="text"
          placeholder="Email" />
        <i v-show="errors.has('MailPartner')" class="fa fa-warning"></i>
        <span v-show="errors.has('MailPartner')" class="help is-danger">{{ errors.first('MailPartner') }}</span>
      </div>
      <div class="input">
        <input name="AddressPartner"
          class="form-control"
          v-model="AddressPartner"
          v-validate="'required'"
          :class="{'input': true, 'is-danger': errors.has('AddressPartner') }"
          type="text"
          placeholder="Address" />
          <i v-show="errors.has('AddressPartner')" class="fa fa-warning"></i>
          <span v-show="errors.has('AddressPartner')" class="help is-danger">{{ errors.first('AddressPartner') }}</span>
      </div>
    </tab-content>
    <tab-content title="Wedding Details"
      icon="fa fa-calendar"
      :before-change="validateWeddingDate">
      <label>Wedding Date</label>
      <div class="input">
        <input name="WeddingDate"
          class="form-control"
          type="date"
          v-model="WeddingDate"
          v-validate="'required|date_format:YYYY-MM-DD'"
          :class="{'input': true, 'is-danger': errors.has('WeddingDate') }" />
        <i v-show="errors.has('WeddingDate')" class="fa fa-warning"></i>
        <span v-show="errors.has('WeddingDate')" class="help is-danger">
          {{ errors.first('WeddingDate') }}
        </span>
        <div>
          {{this.messageWedding}}
        </div>
      </div>
    </tab-content>
    <tab-content title="Delivery Address"
      icon="fa fa-map-marker">
      <b-row>
        <b-col md="4">
          <label>Delivery Date</label>
          <div class="input">
            <input name="DeliveryDate"
              class="form-control"
              type="date"
              v-model="DeliveryDate"
              v-validate="'required|date_format:YYYY-MM-DD'"
              :class="{'input': true, 'is-danger': errors.has('DeliveryDate') }" />
              <i v-show="errors.has('DeliveryDate')" class="fa fa-warning"></i>
              <span v-show="errors.has('DeliveryDate')" class="help is-danger">
                {{ errors.first('DeliveryDate') }}
              </span>
              <div>
                {{this.messageDelivery}}
              </div>
          </div>
          <label>Delivery Address</label>
          <div class="input">
            <input name="AddressDelivery"
              class="form-control"
              v-model="AddressDelivery"
              v-validate="'required'"
              :class="{'input': true, 'is-danger': errors.has('AddressDelivery') }"
              type="text"
              placeholder="Delivery Address" />
              <i v-show="errors.has('AddressDelivery')" class="fa fa-warning"></i>
              <span v-show="errors.has('AddressDelivery')" class="help is-danger">
                {{ errors.first('AddressDelivery') }}
              </span>
          </div>
        </b-col>
        <b-col md="8">
          <label>Delivery Address</label>
          <div class="map">
            <gmap-map
              :center="center"
              :zoom="16"
              style="width: 100%; height: 100%"
              @click="addMarker"
              ref="example">
              <gmap-marker
                :position="deliveryAddress"
                :draggable="true"></gmap-marker>
            </gmap-map>
            <div>
            </div>
          </div>
        </b-col>
      </b-row>
    </tab-content>
  </form-wizard>
</template>

<script>
import couplesResource from '../resources/couplesResource'
import AuthService from '../services/authentication'
export default {
  data () {
    return {
      completeInfo: false,
      completeInfoPartner: false,
      isValidWeddingDate: false,
      messageWedding: '',
      messageDelivery: '',
      FirstName: '',
      LastName: '',
      Id: '',
      Telephone: '',
      Mail: '',
      Address: '',
      FirstNamePartner: '',
      LastNamePartner: '',
      IdPartner: '',
      TelephonePartner: '',
      MailPartner: '',
      AddressPartner: '',
      WeddingDate: '',
      DeliveryDate: '',
      AddressDelivery: '',
      Account: {
        Name: '',
        Password: '',
        NamePerson: '',
        NamePersonPartner: ''
      },
      deliveryAddress: {
        lat: 0,
        lng: 0
      },
      center: {
        lat: -17.3664441,
        lng: -66.1755708
      },
      markable: true
    }
  },
  methods: {
    onCompleteUser: function () {
      return new Promise((resolve, reject) => {
        this.$validator.attach({ name: 'FirstName', rules: 'required|alpha' })
        this.$validator.attach({ name: 'LastName', rules: 'required|alpha' })
        this.$validator.attach({ name: 'Id', rules: 'required|numeric|min:7|min_value:1' })
        this.$validator.attach({ name: 'Telephone', rules: 'required|numeric' })
        this.$validator.attach({ name: 'Mail', rules: 'required|email' })
        this.$validator.attach({ name: 'Address', rules: 'required' })
        this.$validator.validateAll({
          FirstName: this.FirstName,
          LastName: this.LastName,
          Id: this.Id,
          Telephone: this.Telephone,
          Mail: this.Mail,
          Address: this.Address
        })
          .then((result) => {
            this.completeInfo = result
            resolve(result)
          })
      })
    },
    onCompleteUserPartner: function () {
      return new Promise((resolve, reject) => {
        this.$validator.attach({ name: 'FirstNamePartner', rules: 'required|alpha' })
        this.$validator.attach({ name: 'LastNamePartner', rules: 'required|alpha' })
        this.$validator.attach({ name: 'IdPartner', rules: 'required|numeric|min:7|min_value:1' })
        this.$validator.attach({ name: 'TelephonePartner', rules: 'required|numeric' })
        this.$validator.attach({ name: 'MailPartner', rules: 'required|email' })
        this.$validator.attach({ name: 'AddressPartner', rules: 'required' })
        this.$validator.validateAll({
          FirstNamePartner: this.FirstNamePartner,
          LastNamePartner: this.LastNamePartner,
          IdPartner: this.IdPartner,
          TelephonePartner: this.TelephonePartner,
          MailPartner: this.MailPartner,
          AddressPartner: this.AddressPartner
        })
          .then((result) => {
            this.completeInfoPartner = result
            resolve(result)
          })
      })
    },
    validateWeddingDate () {
      return new Promise((resolve, reject) => {
        this.$validator.validateAll({WeddingDate: this.WeddingDate})
          .then((result) => {
            this.isValidWeddingDate = result
            this.$gmapDefaultResizeBus.$emit('resize')
            resolve(result)
          })
      })
    },
    validateBeforeSubmit () {
      this.messageWedding = ''
      this.messageDelivery = ''
      this.$validator.attach({ name: 'WeddingDate', rules: 'required|date_format:YYYY-MM-DD' })
      this.$validator.attach({ name: 'DeliveryDate', rules: 'required|date_format:YYYY-MM-DD' })
      this.$validator.attach({ name: 'AddressDelivery', rules: 'required' })
      this.$validator.validateAll({
        WeddingDate: this.WeddingDate,
        DeliveryDate: this.DeliveryDate,
        AddressDelivery: this.AddressDelivery
      })
        .then((result) => {
          if (result && this.completeInfo && this.completeInfoPartner) {
            var couple = {
              FirstName: this.FirstName,
              LastName: this.LastName,
              Id: this.Id,
              Telephone: this.Telephone,
              Mail: this.Mail,
              Address: this.Address,
              FirstNamePartner: this.FirstNamePartner,
              LastNamePartner: this.LastNamePartner,
              IdPartner: this.IdPartner,
              TelephonePartner: this.TelephonePartner,
              MailPartner: this.MailPartner,
              AddressPartner: this.AddressPartner,
              WeddingDate: this.WeddingDate,
              DeliveryDate: this.DeliveryDate,
              AddressDelivery: this.AddressDelivery,
              Latitude: this.deliveryAddress.lat,
              Longitude: this.deliveryAddress.lng
            }
            var date = new Date()
            var dateWedding = new Date(this.WeddingDate)
            var dateDelivering = new Date(this.DeliveryDate)
            var actualDate = new Date(date.getFullYear(), date.getMonth(), date.getDate())

            if (dateWedding.getTime() >= actualDate.getTime()) {
              if (dateDelivering.getTime() - dateWedding.getTime() <= 1209600000 && dateDelivering.getTime() - dateWedding.getTime() > 0) {
                couplesResource.saveCouple(couple)
                  .then((account) => {
                    this.Account = account.body
                    this.$router.push({name: 'notificationregister', params: {acc: this.Account}})
                  })
                  .catch(error => {
                    console.log('This is a error')
                    console.log(error)
                  })
              } else {
                this.messageDelivery = 'Date Delivering not valid'
              }
            } else {
              this.messageWedding = 'Date Wedding not valid'
            }
          }
        })
    },
    addMarker: function (data) {
      if (this.markable) {
        this.deliveryAddress.lat = data.latLng.lat()
        this.deliveryAddress.lng = data.latLng.lng()
      }
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
  .input {
    margin-bottom:1%;
  }

  .map {
    height: 300px;
    width: 100%;
  }
</style>
