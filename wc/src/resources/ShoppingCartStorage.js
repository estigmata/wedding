function saveShoppingCart (shoppingcart) {
	localStorage.setItem("shoppingcart", shoppingcart);
}

function getShoppingCart () {
	return localStorage.getItem("shoppingcart")
}

module.exports = {
	saveShoppingCart,
	getShoppingCart
}