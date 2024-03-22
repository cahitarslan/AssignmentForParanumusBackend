## Part 5: Advanced C# and Software Design

### Task 5.1: Dependency Injection and Services

- The in-memory services were implemented for all necessary entities. 
- A generic class, ImEntityRepositoryBase, was derived to ensure uniformity and efficiency.
- Extensive testing confirmed that all operations functioned seamlessly within the in-memory context. 
- To integrate these services with the controllers using dependency injection, a separate service class was created, and the necessary dependencies were injected into the controllers. 
- This refactor enhances code organization, promotes reusability, and facilitates easier maintenance in the long run.

### Task 5.2: Unit Testing
- Unit tests were written for the ProductService class to validate that all CRUD operations are working as expected. 
- NUnit and Moq were used for the testing process. 
- The tests were executed successfully, as shown in the screenshot below.

![devenv_wWQ3syLVLg](https://github.com/cahitarslan/AssignmentForParanumusBackend/assets/96558672/09cf42af-61a7-43a7-b3eb-f757479a978a)

