# WarehouseManager
## What is the WarehouseManager?
Student project. Simple WPF app for managing warehouses, suppliers and supplies. First this kind project in my life.

## What are the main concepts/tech stack?
- Trying to keep MVVM design
- Basic validation (length, only numbers for account/nip number)
- EF Core
- .NET 5
- SQLite under the hood

## How to add a supplier/employee/product
Just fill the fields.

## How to add a warehouse?
You need to create an employee first, then just fill the fields.

## How to add a supply?
You need to have added warehouse and supplier first.

## How to add a product to the supply?
Go to Supplies > Zawartość > + and fill the fields. You can add/remove products from the supply only if the supply's status is 'Dodana'.
