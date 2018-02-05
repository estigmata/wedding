export default {
  isAuthenticated: function () {
    if (localStorage.getItem('token')) {
      return true
    }
    return false
  },
  signOut: function () {
    localStorage.removeItem('token')
    localStorage.removeItem('role')
    localStorage.removeItem('firstName')
    localStorage.removeItem('firstNamePartner')
    localStorage.removeItem('productListId')
  },
  isInventoryManager: function () {
    if (localStorage.getItem('role') === 'inventory manager') {
      return true
    }
    return false
  },
  isCouple: function () {
    return localStorage.getItem('role') === 'couple'
  }
}
