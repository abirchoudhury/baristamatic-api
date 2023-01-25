# baristamatic-api

## note: please use the 'review' branch for all reviews. 'main' is left untouched for safety.

to run this app, clone the repository

launch visual studio and hit play, it should work as is.
If for some reason it doesn't like the url, for me it was "https://localhost:7109"


example request flows: 

1. call BaristamaticMenu/GetDrinksMenu to get a list of the menu items from the original requirement document
2. call GetAvailableDrinks, to show a list of all drinks from the menu, and whether or not they can be made based on remaining inventory
3. call PlaceOrder
4. call GetCurrentStock, verify that the ingredients used to place order is being subtracted from existing Ingredients inventory
5. call GetAvailableDrinks if you wish, to re-check #2
6. call GetAllOrders, to list all orders currently placed
7. call RestoreIngredients, to restore all quantity to 10
8. call GetCurrentStock & GetAvailableDrinks, verify stocks are replenished
