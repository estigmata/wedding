
export default {
  filterProducts (products, search) {
    return products.filter(product => {
      let nameFilter = this.filterProductsByName(product, search)
      let brandFilter = this.filterProductsByBrand(product, search)
      let categoryFilter = this.filterProductsByCategory(product, search)
      let priceFilter = null
      if (search[0] === '<') {
        priceFilter = this.filterProductsByPricesUpper(product, search)
      } else if (search[0] === '>') {
        priceFilter = this.filterProductsByPricesLower(product, search)
      }
      return nameFilter || brandFilter || categoryFilter || priceFilter
    })
  },
  filterPresents (presents, search) {
    return presents.filter(present => {
      let nameFilter = this.filterProductsByName(present.product, search)
      let brandFilter = this.filterProductsByBrand(present.product, search)
      let categoryFilter = this.filterProductsByCategory(present.product, search)
      let priceFilter = null
      if (search[0] === '<') {
        priceFilter = this.filterProductsByPricesUpper(present.product, search)
      } else if (search[0] === '>') {
        priceFilter = this.filterProductsByPricesLower(present.product, search)
      }
      return nameFilter || brandFilter || categoryFilter || priceFilter
    })
  },
  filterProductsByName (product, search) {
    return product.name.toLowerCase().includes(search.toLowerCase())
  },
  filterProductsByBrand (product, search) {
    return product.brand.toLowerCase().includes(search.toLowerCase())
  },
  filterProductsByCategory (product, search) {
    return product.category.toLowerCase().includes(search.toLowerCase())
  },
  filterProductsByPricesUpper (product, search) {
    let price = ''
    let priceCast
    for (var i = 1; i < search.length; i++) {
      price = price + search[i]
    }
    priceCast = parseFloat(price)
    if (priceCast >= product.price) {
      return priceCast
    }
  },
  filterProductsByPricesLower (product, search) {
    let price = ''
    let priceCast
    for (var i = 1; i < search.length; i++) {
      price = price + search[i]
    }
    priceCast = parseFloat(price)
    if (priceCast <= product.price) {
      return priceCast
    }
  }
}
