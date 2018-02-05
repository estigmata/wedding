<template>
  <div class="wrapper">
    <b-container>
      <b-col md="6" offset-md="3">
        <b-form>
          <br>
          <h3 class="text-center">Register a Product</h3>
          <b-form-group id="product-name-group"
            label="Product name"
            label-for="product-name">
            <b-form-input id="product-name"
              v-model="product.name"
              type="text"
              name="product-name"
              placeholder="Product name"
              v-validate="'required'"
              :class="{'input': true, 'is-danger is-invalid': errors.has('product-name') }"
              ></b-form-input>
            <span v-show="errors.has('product-name')" class="help is-danger invalid-feedback">{{ errors.first('product-name') }}</span>
          </b-form-group>

          <b-form-group id="product-image-group"
            label="Product image"
            label-for="product-image">
            <div class="custom-file">
              <input id="product-image"
                type="file"
                name="product-image"
                v-validate="'required|ext:jpg,png,jpeg|size:250'"
                @change="onFileChange"
                class="custom-file-input"
                :class="{'is-invalid': errors.has('product-image') }">
              <label for="product-image" class="custom-file-label">{{ product.imageName}}</label>
              <span v-show="errors.has('product-image')" class="invalid-feedback">{{ errors.first('product-image') }}</span>
            </div>
          </b-form-group>

          <b-form-group id="product-name-description"
            label="Product description"
            label-for="product-description">
            <b-form-textarea id="product-description"
              v-model="product.description"
              :rows="2"
              :max-rows="6"
              name="product-description"
              placeholder="Product description"
              v-validate="''"
              :class="{'input': true, 'is-danger is-invalid': errors.has('product-description') }"
              ></b-form-textarea>
              <span v-show="errors.has('product-description')" class="help is-danger invalid-feedback">{{ errors.first('product-description') }}</span>
          </b-form-group>
          <b-row>
            <b-col>
              <b-form-group id="product-price-group"
                label="Product price"
                label-for="product-price">
                <b-input-group>
                  <b-form-input id="product-price"
                    v-model="product.price"
                    type="text"
                    name="product-price"
                    placeholder="Product price"
                    v-validate="'required|decimal:2|min_value:0'"
                  :class="{'input': true, 'is-danger is-invalid': errors.has('product-price') }"
                  ></b-form-input>
                <span v-show="errors.has('product-price')" class="help is-danger invalid-feedback">{{ errors.first('product-price') }}</span>
                </b-input-group>
              </b-form-group>
            </b-col>
            <b-col sm="12" md="6">
              <b-form-group id="product-stock-group"
                label="Product stock"
                label-for="product-stock">
                <b-form-input id="prodcut-stock"
                  v-model="product.stock"
                  name="product-stock"
                  type="number"
                  placeholder="Product stock"
                  v-validate="'required|min_value:0'"
                  :class="{'input': true, 'is-danger is-invalid': errors.has('product-stock') }">
                </b-form-input>
                <span v-show="errors.has('product-stock')" class="help is-danger invalid-feedback">{{ errors.first('product-stock') }}</span>
              </b-form-group>
            </b-col>
          </b-row>
          <b-form-group id="product-category-group"
            label="Product category"
            label-for="product-category">
            <b-form-select id="product-category"
              v-model="product.category"
              name="product-category"
              :options="options"
              v-validate="'required'"
              :class="{'input': true, 'is-danger is-invalid': errors.has('product-category') }" >
            </b-form-select>
            <span v-show="errors.has('product-category')" class="help is-danger invalid-feedback">{{ errors.first('product-category') }}</span>
          </b-form-group>
          <b-form-group id="product-brand-group"
            label="Product brand"
            label-for="product-brand">
              <b-form-input id="product-brand"
              v-model="product.brand"
              name="product-brand"
              type="text"
              placeholder="Product brand"
              v-validate="'required'"
              :class="{'input': true, 'is-danger is-invalid': errors.has('product-brand') }"></b-form-input>
              <span v-show="errors.has('product-brand')" class="help is-danger invalid-feedback">{{ errors.first('product-brand') }}</span>
          </b-form-group>
          <b-alert :show="dismissCountDown"
                dismissible
                variant="success"
                @dismissed="dismissCountDown=0"
                @dismiss-count-down="countDownChanged">
            <p>The product was created succesfully</p>
          </b-alert>
          <b-button :disabled="!isValid" :block="true" variant="primary" @click="validate">Add Product</b-button>
        </b-form>
      </b-col>
    </b-container>
  </div>
</template>

<script>
import ProductResource from '../resources/productsResource'
import AuthService from '../services/authentication'

export default {
  name: 'AddProduct',
  data () {
    return {
      product: {
        name: '',
        description: '',
        price: '',
        category: '',
        image: '',
        imageName: '',
        stock: '1',
        brand: ''
      },
      options: [
        {
          value: null, text: 'Please select an option'
        },
        {
          value: 'White goods', text: 'White goods'
        },
        {
          value: 'Home entertainment', text: 'Home entertainment'
        },
        {
          value: 'Furnitures', text: 'Furnitures'
        },
        {
          value: 'Housewares', text: 'Housewares'
        }
      ],
      dismissSecs: 5,
      dismissCountDown: 0,
      showDismissibleAlert: false
    }
  },
  methods: {
    validate () {
      this.$validator.validateAll()
        .then((result) => {
          if (!result) {
          } else {
            this.addProduct()
          }
        })
        .catch(() => {
        })
    },
    addProduct () {
      return ProductResource.saveProduct(this.product)
        .then(res => {
          if (res.status === 201) {
            this.showAlert()
            this.resetForm()
          }
        })
        .catch(err => {
          console.log(err)
        })
    },
    countDownChanged (dismissCountDown) {
      this.dismissCountDown = dismissCountDown
    },
    showAlert () {
      this.dismissCountDown = this.dismissSecs
    },
    resetForm () {
      this.product = {
        name: '',
        description: '',
        price: '',
        category: '',
        image: '',
        imageName: '',
        stock: '1',
        brand: ''
      }
      this.$validator.reset()
    },
    onFileChange (e) {
      var files = e.target.files || e.dataTransfer.files
      if (!files.length) {
        return
      }
      this.product.image = files[0]
      this.product.imageName = files[0].name
    }
  },
  mounted () {
    if (AuthService.isAuthenticated()) {
      if (!AuthService.isInventoryManager()) {
        this.$router.push({path: '/'})
      }
    } else {
      this.$router.push({path: '/'})
    }
  },
  computed: {
    isValid () {
      return this.product.name && this.product.price &&
        this.product.category && this.product.stock &&
        this.product.brand && this.product.imageName &&
        !this.errors.any()
    }
  }
}
</script>
