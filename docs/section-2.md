# Table of Contents
- [New controllers & models]
- [Asynchrounous implementations with async/await]
- [Data Transfer Objects (DTOs)]
- Best practices
- [Model-View-Controller (MVC) pattern](#model-view-controller-mvc-pattern)

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