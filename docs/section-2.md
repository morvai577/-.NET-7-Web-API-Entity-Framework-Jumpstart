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
