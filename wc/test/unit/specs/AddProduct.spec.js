import Vue from 'vue'
import sinon from 'sinon'
import AddProduct from '@/components/AddProduct'
import ProductResource from './../../../src/resources/productsResource'

let vm;

describe('AddProduct.vue', () => {
  beforeEach(() => {
    const Constructor = Vue.extend(AddProduct)
    vm = new Constructor().$mount()
  })
  describe('countDownChanged', () => {
    it('should set the value of dismissCountDown', () => {
      vm.countDownChanged(123)
      expect(vm.dismissCountDown).to.equal(123)
    })
  })

  describe('addProduct', () => {
    it('should save a new product', (done) => {
      sinon.stub(ProductResource, 'saveProduct').callsFake(() => {
        return Promise.resolve({ status: 201 })
      })
      sinon.stub(vm, 'showAlert')
      sinon.stub(vm, 'resetForm')

      vm.product = 'some_product'

      vm.addProduct().then(() => {
        sinon.assert.calledWith(ProductResource.saveProduct, 'some_product')
        sinon.assert.calledOnce(vm.showAlert)
        sinon.assert.calledOnce(vm.resetForm)
        done()
      })
    })
  })
})
