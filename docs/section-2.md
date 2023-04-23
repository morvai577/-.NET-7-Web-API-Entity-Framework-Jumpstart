# Table of Contents
- [Model-View-Controller (MVC) pattern](#model-view-controller-mvc-pattern)
- [HTTP Request Methods](#http-request-methods)
- [Best Practice: Web API Structure](#best-practice-web-api-structure)
  - [Service Layer](#service-layer)
  - [Data Transfer Objects (DTOs)](#data-transfer-objects-dtos)

# Model-View-Controller (MVC) pattern
The Model-View-Controller (MVC) pattern is a way of organizing code in a software application that separates the data, user interface, and control logic into **three** interconnected components. This helps to make the code more modular, easier to maintain, and more straightforward to understand.

Imagine you're at a restaurant with a menu, a chef, and a waiter.

**Model**: The chef represents the Model. The chef is responsible for managing the ingredients and preparing the dishes. In the context of a software application, the Model represents the data and the business logic that manipulates that data. In a .NET Web API, this would include database access and any algorithms or rules specific to your application.

**View**: The menu represents the View. It's the way you, as a customer, interact with the restaurant's offerings. The View is the user interface that presents the data to the user and receives their inputs. In a .NET Web API, this might be an HTML, CSS, and JavaScript web page or a mobile app that displays the data and allows users to interact with it.

**Controller**: The waiter represents the Controller. The waiter takes your order, communicates it to the chef (Model), and brings the prepared dish back to you. The Controller manages the flow of data between the Model and the View. In a .NET Web API, this would involve receiving requests from the View, processing them, and returning the appropriate data or response.

So, in the restaurant scenario, you (the user) interact with the menu (View), place an order, and the waiter (Controller) takes the order to the chef (Model). The chef prepares the dish and gives it back to the waiter, who then serves it to you.

In the context of .NET Web API, a user might submit a request to update their profile information. The request is received by the Controller, which then communicates with the Model to update the data in the database. Once the data has been updated, the Controller sends a response back to the View, indicating whether the operation was successful or not.

By separating the concerns of data management (Model), user interface (View), and control logic (Controller), the MVC pattern makes it easier to develop, maintain, and understand complex software applications, such as those built using .NET Web API.

# HTTP Request Methods
In layman terms, HTTP request methods are like verbs that tell a server what action to perform on a specified resource. In the context of a .NET Web API, there are four key HTTP request methods: GET, POST, PUT, and DELETE. They allow clients, such as web browsers or mobile apps, to interact with the server in different ways.

GET: The GET method is used to request data from a server. It's like asking a librarian for a specific book. When you use the GET method, you're essentially saying, "Please give me the information about this resource." In a .NET Web API, the GET method retrieves data from the server without modifying it.
Example: A user wants to view their profile information on a website. The client sends an HTTP GET request to the server, which then retrieves the user's profile data and sends it back as a response.

POST: The POST method is used to submit data to a server to create a new resource. It's like filling out a form and submitting it to create a new account. When you use the POST method, you're saying, "Please create a new resource with this data." In a .NET Web API, the POST method adds new data to the server.
Example: A user wants to register for a new account on a website. The client sends an HTTP POST request to the server with the user's information. The server then creates a new account with the provided data and sends a response confirming the successful creation.

PUT: The PUT method is used to update an existing resource on the server with new data. It's like editing a document and saving the changes. When you use the PUT method, you're saying, "Please update this existing resource with this new data." In a .NET Web API, the PUT method modifies existing data on the server.
Example: A user wants to update their profile information on a website. The client sends an HTTP PUT request to the server with the updated information. The server then modifies the user's profile data and sends a response confirming the successful update.

DELETE: The DELETE method is used to remove a resource from the server. It's like throwing a document into a trash bin. When you use the DELETE method, you're saying, "Please delete this resource." In a .NET Web API, the DELETE method removes data from the server.
Example: A user wants to delete their account on a website. The client sends an HTTP DELETE request to the server. The server then removes the user's account and sends a response confirming the successful deletion.

In summary, the four key HTTP request methods in the context of a .NET Web API are like verbs that tell the server what action to perform on a resource: GET to retrieve data, POST to create new resources, PUT to update existing resources, and DELETE to remove resources. These methods allow clients to interact with the server in various ways, depending on the desired outcome.

# Best Practice: Web API Structure

## Service Layer
Imagine a .NET Web API project with a single controller called BooksController that currently handles all the logic for creating, retrieving, updating, and deleting books. As the project grows, it becomes harder to maintain the code, and it's not efficient to copy and paste the same code in other controllers if needed.

To improve the project's structure, we can separate the logic into different classes called services. In our case, we'll create a BookService class.

Now, the BooksController should only forward data to the BookService and return the result to the client. To inject the BookService into the controller, we'll use dependency injection. This way, if we change the implementation of the BookService, we don't need to modify every controller that uses it.

## Data Transfer Objects (DTOs)
Additionally, we'll introduce Data Transfer Objects (DTOs) for better communication between the client and server. In our case, we could have a Book model that represents a database table and a BookDTO that is used to communicate with the client. For example, if the Book model has a DateCreated field that we don't want to send to the client, we can exclude it from the BookDTO.

We can use a library like AutoMapper to map data between the Book model and the BookDTO easily. DTOs can be used for both sending and receiving data, allowing more flexibility in what is sent to or received from the client.

![image](SCR-20230423-pcw.jpeg)

By implementing these changes, the project becomes more organized and easier to maintain, allowing for better separation of concerns and more efficient use of services and DTOs.

# Dependency Injection
Dependency injection is a software design pattern that allows us to implement loosely coupled code. In other words, it allows us to write code that is loosely coupled to other components, making it easier to maintain and test. Dependency injection allows us to inject services into controllers, repositories, and other classes. This way, we can easily swap out the implementation of a service without having to modify the code that uses it.

## Constructor Injection
 constructor injection is a technique used to provide a class with its dependencies, like services, through its constructor. It's a way of telling a class, "Hey, here's everything you need to do your job!" This approach helps keep your code organized, maintainable, and testable.

In the context of a .NET Web API project, you often have controllers that depend on services to handle the business logic. Instead of creating these service instances directly within the controller, you can "inject" them through the controller's constructor. This way, the controller doesn't need to know how to create the service – it just receives a ready-to-use instance.

Constructor injection relies on an Inversion of Control (IoC) container, which manages the creation and lifetime of objects for you. In .NET, the built-in dependency injection system handles this role.

Let's consider an example using a `BooksController` and a `BookService`:

1. First, define an interface for the `BookService`:
  
      ```csharp
      public interface IBookService
      {
          IEnumerable<Book> GetBooks();
          // Other methods for handling books
      }
      ```
2. Implements the `BookService`:

      ```csharp
      public class BookService : IBookService
      {
          public IEnumerable<Book> GetBooks()
          {
              // Implementation for getting books from a database, for example.
          }
          // Implement other methods for handling books
      }
      ```
3. In the `BooksController`, inject the `IBookService` through the constructor:

      ```csharp
      [ApiController]
      [Route("api/[controller]")]
      public class BooksController : ControllerBase
      {
          private readonly IBookService _bookService;

          public BooksController(IBookService bookService)
          {
              _bookService = bookService;
          }

          [HttpGet]
          public IActionResult GetAllBooks()
          {
              var books = _bookService.GetBooks();
              return Ok(books);
          }

          // Other action methods (POST, PUT, DELETE) would follow the same pattern.
      }
      ```
4. Register the service in the IoC container (usually in the Startup.cs file):

      ```csharp
        services.AddScoped<IBookService, BookService>();
      ```
In this example, the `BooksController` receives an instance of `IBookService` through its constructor. The .NET dependency injection system takes care of creating the `BookService` instance and provides it to the controller.

Constructor injection helps you adhere to the Dependency Inversion Principle, which states that high-level modules should not depend on low-level modules, but both should depend on abstractions (in this case, the `IBookService` interface). This makes your code more flexible, maintainable, and easier to test.