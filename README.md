<h1 align="center">
  Library API
  <br>
</h1>

<p align="center">
  <a href="#introduction">Introduction</a> •
  <a href="#installation-guide">Installation Guide</a> •
  <a href="#api">API Reference</a> •
  <a href="#credits">Credits</a> 
</p>

## Introduction
Library API that you can manage the books and it's users. As **User** you can:
* Borrow a Book 
* Return the Book
* Get book(s) 
* Get your profile auth Logs
* Update your profile credentials

As **Admin** you can:
* Create a User 
* Update the User 
* Delete the User 
* Return the Book
* Get book(s) 
* Delete a book
* Update a book
* Get all auth Logs
* Delete auth Logs
* Update your profile credentials

## Installation Guide

To clone and run this application, you'll need [Git](https://git-scm.com), [ASP NET](https://dotnet.microsoft.com/en-us/apps/aspnet), [MS-SQL](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) and [Visual Studio](https://visualstudio.microsoft.com/downloads/) installed on your computer. From your command line:

```bash
# Clone this repository
$ git clone https://github.com/Ctere1/LibraryAPI
# Go into the repository
$ cd LibraryAPI
```
> After these steps,  you should be able to open the project/solution with Visual Studio, build it and run it from there.

## API

### Authentication Endpoint

| HTTP Verb   | Endpoint                    | Description                                         |Admin Access  | User Access | 
| :---------- | :-----------                |:----------------------------------                  |:-----------  |:----------- |
| `GET`       | `api/Auth/GetToken`         |  Returns Bearer Token                               | ☑️          |  ☑️         |
> See Postman Collection Json for detailed information. Also Swagger Doc included in this project.

### Signup Endpoint

| HTTP Verb   | Endpoint                    | Description                                         |Admin Access  | User Access | 
| :---------- | :-----------                |:----------------------------------                  |:-----------  |:----------- |
| `POST`      | `api/Signup`                |  To create a new User                               | ☑️          |  ☑️         |
> See Postman Collection Json for detailed information. Also Swagger Doc included in this project.

### User Endpoints

| HTTP Verb   | Endpoint                    | Description                                         |Admin Access  | User Access | 
| :---------- | :-----------                |:----------------------------------                  |:-----------  |:----------- |
| `GET`       | `api/User`                  |  Returns User(s)                                    | ☑️           |  ❌        |
| `PUT`       | `api/User`                  |  To update a User                                   | ☑️           |  ❌        |
| `GET`       | `api/User/MyProfile`        |  Returns user's or admin's profile credentials      | ☑️           |  ☑️        |
| `POST`      | `api/User`                  |  To create a User                                   | ☑️           |  ❌        |
| `PUT`       | `api/User/AdminProfile`     |  To update admin profile                            | ☑️           |  ❌        |
| `DELETE`    | `api/User`                  |  To delete a user                                   | ☑️           |  ❌        |
> See Postman Collection Json for detailed information. Also Swagger Doc included in this project.

### Book Endpoints

| HTTP Verb   | Endpoint                    | Description                                         |Admin Access  | User Access | 
| :---------- | :-----------                |:----------------------------------                  |:-----------  |:----------- |
| `GET`       | `api/Book`                  |  Returns Book(s)                                    | ☑️           |  ☑️        |
| `PUT`       | `api/Book`                  |  To update Book                                     | ☑️           |  ❌        |
| `GET`       | `api/Book/MyBooks`          |  Returns User's books                               | ☑️           |  ☑️        |
| `POST`      | `api/Book`                  |  To create a Book                                   | ☑️           |  ❌        |
| `PUT`       | `api/Book/Return`           |  To return a Book                                   | ☑️           |  ☑️        |
| `PUT`       | `api/Book/Borrow`           |  To borrow a Book                                   | ☑️           |  ☑️        |
| `DELETE`    | `api/Book`                  |  To delete a Book                                   | ☑️           |  ❌        |
> See Postman Collection Json for detailed information. Also Swagger Doc included in this project.

### Log Endpoints

| HTTP Verb   | Endpoint                    | Description                                         |Admin Access  | User Access | 
| :---------- | :-----------                |:----------------------------------                  |:-----------  |:----------- |
| `GET`       | `api/Log`                   |  Returns Log(s)                                     | ☑️           |  ☑️        |
| `DELETE`    | `api/Log`                   |  To delete a Log                                    | ☑️           |  ❌        |

> See Postman Collection Json for detailed information. Also Swagger Doc included in this project.

## Credits

This software uses the following packages:

- Microsoft.AspNet.WebApi
- Microsoft.Owin
- Swashbuckle
- EntityFramework
