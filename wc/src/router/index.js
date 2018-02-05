import Vue from 'vue'
import Router from 'vue-router'
import foundedcoupleslist from '@/components/FoundedCouplesList'
import searchcouplelist from '@/components/SearchCoupleList'
import home from '@/pages/home'
import AddProduct from '@/components/AddProduct'
import RegisterCouple from '@/components/RegisterCouple'
import notificationregister from '@/components/NotificationRegister'
import VeeValidate from 'vee-validate'
import products from '@/pages/products'
import presentList from '@/components/PresentListCouple'
import BuyPresent from '@/components/BuyPresent'
import UpdateStock from '@/components/UpdateStock'
import PurchasePresent from '@/components/PurchasePresent'
import ShowMyList from '@/components/ShowMyList'

Vue.use(VeeValidate)
Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'home',
      component: home,
      children: [
        {
          path: '',
          name: 'searchcouplelist',
          component: searchcouplelist
        },
        {
          path: '/founded-couples-list',
          name: 'foundedcoupleslist',
          component: foundedcoupleslist
        },
        {
          path: '/register-couple',
          name: 'RegisterCouple',
          component: RegisterCouple
        },
        {
          path: '/add-product',
          name: 'AddProduct',
          component: AddProduct
        },
        {
          path: '/notificationregister/:acc',
          name: 'notificationregister',
          component: notificationregister
        },
        {
          path: '/products',
          name: 'products',
          component: products
        },
        {
          path: '/present-list/:husband/:wife/:presentListId',
          name: 'presentlist',
          component: presentList
        },
        {
          path: '/BuyPresent',
          name: 'BuyPresent',
          component: BuyPresent
        },
        {
          path: '/UpdateStock',
          name: 'UpdateStock',
          component: UpdateStock
        },
        {
          path: '/PurchasePresent/:present',
          name: 'PurchasePresent',
          component: PurchasePresent
        },
        {
          path: '/show-my-list/:presentListId',
          name: 'ShowMyList',
          component: ShowMyList
        }
      ]
    }
  ]
})
