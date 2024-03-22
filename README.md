## Part 4: Business Scenario Implementation

>  I successfully implemented all business rules according to the scenario.


### Task 4.1: Design an API Endpoint
- You can send a POST request to PlaceOrder from the OrdersController to test. 
- The request model, named PlaceOrderRequest, contains user ID and order details within a DTO. 
- The response model includes OriginalPrice, DiscountAmount, and FinalPrice information.

### Task 4.2: Implement Business Logic
- Discounts are applied in two different ways: 
	- First, if the purchaser holds the role of an employee, they receive a 30% discount, and no other discount is applied. 
	- Second, if the purchaser is a premium customer, which is not checked by roles, conditions are evaluated before each purchase. If the total purchase amount within the last month equals or exceeds 100 units, a 10% discount is applied.
- Tested and working. I have a few steps for you to test as well.
- First, navigate to the Package Manager Console and execute the following command.
  
	```sh
	update-database "20240322075800_BusinessScenario"
	```
- Then, locate the ParanumusDb database in SQL Server Object Explorer. 
- After that, manually insert the pairs 1-1, 2-1, 3-2 into the AspNetUserRoles table. 
- The user with ID 1 has the regular role, and the user with ID 3 has the employee role.

### Task 4.3: Data Persistence
- I store my purchase records in the Orders and OrderDetails tables. 
- The Order table contains user ID, purchase date, total amount, discount amount, and a collection of OrderDetails. 
- OrderDetails includes Order ID, product ID, quantity, and unit price information.

>I retrieved the information about which roles the user with the given ID has from the UserRoles table. If the user is an employee, I applied a direct 30% discount. 
>Otherwise, based on whether the total order amount the user made within the last month is equal to or greater than 100, I applied either a 10% discount or no discount at all.

### Task 4.3: Documentation
>One key aspect of the thought process behind your design choices lies in the meticulous consideration of how to apply discounts based on user roles and purchase history. 
>By prioritizing clarity, efficiency, and scalability in the implementation of these rules, you've ensured that the system operates effectively while accommodating potential changes and expansions in the future. 
>This thoughtful approach not only enhances the functionality of the application but also lays a solid foundation for seamless maintenance and further development.
