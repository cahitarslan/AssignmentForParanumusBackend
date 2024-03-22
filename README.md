# Assignment for Paranumus Backend

## Part 3: Expertise and Best Practices

### Task 3.1: RESTful API with CRUD Operations

>In addition to the in-memory data store, I have set up a local database on MSSQL Server using Entity Framework Core with the code-first approach.

Having the Data Storage and Processing workload package installed in Visual Studio 2022 would be beneficial.
 
![4](https://github.com/cahitarslan/AssignmentForParanumusBackend/assets/96558672/355a1bd7-f88f-44ca-a0d7-467dc0b41616)

- Paste the following code in the Package Manager Console
	```sh
	update-database "20240322075420_CustomIdentity"
	```
![5](https://github.com/cahitarslan/AssignmentForParanumusBackend/assets/96558672/0920002e-d8d5-497c-bb26-30eb996aff9b)

- After creating the database locally, you need to remove the Authorize attribute in the ProductsController.cs to view the products.
- Alternatively, you can log in with the email and password provided below in object format, and then authorize with the returned JWT to view the products.
	```sh
	{"email": "cahit@xyz.com, password: "Abc123!"}
	```

### Task 3.2: Authentication

>With .NET 8, I integrated login operations that come within the Identity architecture by creating Custom Identity classes for authentication processes.

- When creating the database, you can log in with the following three sets of credentials.
- Alternatively, you can register users in the database and authorize using JWT through Swagger.

```sh
	{"email": "cahit@xyz.com, password: "Abc123!"}
	{"email": "ahmet@xyz.com, password: "Abc123!"}
	{"email": "ayse@xyz.com, password: "Abc123!"}
```

### Task 3.3: Advanced Error Handling and Logging

>I created a middleware for global error handling. 
>Inside it, I logged the occurring errors using SeriLog. 
>I configured the logs to be written to both a file and the console.

### Task 3.4: Performance Optimization

>In the Business layer, I implemented an in-memory caching mechanism. 
>I utilized the Get, Set, and Delete cache operations in the service layers.

- Caching is a technique used to store frequently accessed data temporarily in a faster-accessible storage location, such as memory, to reduce the need for repeated computations or data retrievals from slower storage mediums like databases. 
- It improves system performance by providing faster response times and reducing resource utilization, making it beneficial for optimizing application performance and scalability.
