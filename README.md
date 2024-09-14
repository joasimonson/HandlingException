# Global Exception Handler

A **Global Exception Handler** is a centralized mechanism to catch and handle unhandled exceptions throughout an application.
This approach simplifies error management by preventing unhandled exceptions from crashing the application and provides a uniform way to log errors or present error messages to users.

# Middleware Exception Handler

**Middleware** in ASP.NET Core allows you to create a custom pipeline to handle exceptions.
By implementing middleware, you can catch exceptions globally, log them, and return a consistent error response.
This approach helps separate error handling logic from your application’s core functionality and ensures that all exceptions are handled uniformly.

# Result Pattern

The **Result Pattern** is used to encapsulate the outcome of an operation, including both success and failure states.
It typically involves returning a result object that contains information about the operation's success or failure, error messages, and any additional data.
This pattern helps in managing and communicating the results of operations in a clear and consistent manner.

# Libraries

- [XUnit](https://github.com/xunit/xunit)
- [FluentAssertions](https://github.com/fluentassertions/fluentassertions)
- [FluentAssertions.Web](https://github.com/adrianiftode/FluentAssertions.Web)