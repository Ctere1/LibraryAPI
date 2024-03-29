<h1 align="center">
  Library API
  
  ![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
  ![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)
  ![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
  <br>
</h1>

<p align="center">
  <a href="#introduction">Introduction</a> •
  <a href="#installation-guide">Installation Guide</a> •
  <a href="#api">API Reference</a> •
  <a href="#credits">Credits</a> •
  <a href="#license">License</a> •
  <a href="#contributors">Contributors</a> 
</p>

<div align="center">

![GitHub Repo stars](https://img.shields.io/github/stars/Ctere1/LibraryMVC)
![GitHub forks](https://img.shields.io/github/forks/Ctere1/LibraryMVC)
![GitHub watchers](https://img.shields.io/github/watchers/Ctere1/LibraryMVC)

</div>

## ℹ️Introduction
Library API that you can manage the books and it's users using ASP-NET WEB API. It has 2 roles which are `Admin` and `User`. 

It uses **MS-SQL** | **EntityFramework** on DB side. Authentication and authorization processes are performed using `Bearer Token`.

| Admin Actions                               | User Actions                       |                                     
| :----------------------------------------   | :-------------------------------   |        
| `Create, Update and Delete a User`          | `Borrow a Book`                    |         
| `Create, Update, Delete and Return a book`  | `Return the Book`                  |        
| `Get book(s)`                               | `Get book(s)`                      |        
| `Get and Delete all auth Logs`              | `Get own profile auth Logs`        |        
| `Update own profile credentials`            | `Update own profile credentials`   |      

## 💾Installation Guide

To clone and run this application, you'll need [Git](https://git-scm.com), [ASP NET](https://dotnet.microsoft.com/en-us/apps/aspnet), [MS-SQL](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) and [Visual Studio](https://visualstudio.microsoft.com/downloads/) installed on your computer. From your command line:

```bash
# Clone this repository
$ git clone https://github.com/Ctere1/LibraryAPI
# Go into the repository
$ cd LibraryAPI
```
> After these steps,  you should be able to open the project/solution with Visual Studio, build it and run it from there.

## ⚡API
> See Postman Collection Json for detailed information. Also Swagger Doc included in this project.

### **Authentication Endpoint**

| HTTP Verb   | Endpoint                    | Description                                         |Admin Access  | User Access | 
| :---------- | :-----------------------    |:----------------------------------                  |:-----------  |:----------- |
| `GET`       | `api/Auth/GetToken`         |  Returns Bearer Token                               | ☑️          |  ☑️         |

### **Signup Endpoint**

| HTTP Verb   | Endpoint                    | Description                                         |Admin Access  | User Access | 
| :---------- | :------------------------   |:----------------------------------                  |:-----------  |:----------- |
| `POST`      | `api/Signup`                |  To create a new User                               | ☑️          |  ☑️         |

### **User Endpoints**

| HTTP Verb   | Endpoint                    | Description                                         |Admin Access  | User Access | 
| :---------- | :-----------------------    |:----------------------------------                  |:-----------  |:----------- |
| `GET`       | `api/User`                  |  Returns User(s)                                    | ☑️           |  ❌        |
| `PUT`       | `api/User`                  |  To update a User                                   | ☑️           |  ❌        |
| `GET`       | `api/User/MyProfile`        |  Returns user's or admin's profile credentials      | ☑️           |  ☑️        |
| `POST`      | `api/User`                  |  To create a User                                   | ☑️           |  ❌        |
| `PUT`       | `api/User/AdminProfile`     |  To update admin profile                            | ☑️           |  ❌        |
| `DELETE`    | `api/User`                  |  To delete a user                                   | ☑️           |  ❌        |

### **Book Endpoints**

| HTTP Verb   | Endpoint                    | Description                                         |Admin Access  | User Access | 
| :---------- | :------------------------   |:----------------------------------                  |:-----------  |:----------- |
| `GET`       | `api/Book`                  |  Returns Book(s)                                    | ☑️           |  ☑️        |
| `PUT`       | `api/Book`                  |  To update Book                                     | ☑️           |  ❌        |
| `GET`       | `api/Book/MyBooks`          |  Returns User's books                               | ❌           |  ☑️        |
| `POST`      | `api/Book`                  |  To create a Book                                   | ☑️           |  ❌        |
| `PUT`       | `api/Book/Return`           |  To return a Book                                   | ☑️           |  ☑️        |
| `PUT`       | `api/Book/Borrow`           |  To borrow a Book                                   | ❌           |  ☑️        |
| `DELETE`    | `api/Book`                  |  To delete a Book                                   | ☑️           |  ❌        |

### **Log Endpoints**

| HTTP Verb   | Endpoint                    | Description                                         |Admin Access  | User Access | 
| :---------- | :-----------------------    |:----------------------------------                  |:-----------  |:----------- |
| `GET`       | `api/Log`                   |  Returns Log(s)                                     | ☑️           |  ☑️        |
| `DELETE`    | `api/Log`                   |  To delete a Log                                    | ☑️           |  ❌        |

### 📃**Swagger Doc**
> When the server is up, it will automatically redirect the web page to Swagger.

![ss](https://user-images.githubusercontent.com/62745858/208373891-02eafe99-4b71-429f-b327-858bcb0e1071.png)

### 👤**User Data Example**

```json
{
    "id": 2,
    "name": "new",
    "email": "new@gmail.com",
    "password": "1234"
}
```

### 📚**Book Data Example**

```json
{
    "id": 1,
    "name": "new book2",
    "genre": "novel",
    "language": "english",
    "publisherName": "oxford press",
    "authorName": "John",
    "description": "lorem",
    "isActive": false,
    "issuedFrom": "2022-12-13T15:53:18.097",
    "issuedTo": "2022-12-15T00:00:00",
    "borrowedBy": "user@user.com"
}
```
### 🧾**Log Data Example**

```json
{
    "id": 92,
    "user_email": "user@user.com",
    "time": "2022-12-18T20:58:49.993",
    "message": "user@user.com borrowed Book: new book5 via API"
}
```
### 🚥**Get Token Data Example**

```json
{
    "access_token": "9iaB18wzzG6-XRGJrYonDEEaQgMIqMNBO-AfS1gT-MsotDcyFVygpXlnG3asRFSJRPHuHKAUDlkK962XUkXnhrcHgOVMJhuEr05Emxtwf-vYRn_YnvcfdqKqLhbOQloHZiphLNSNSFByuHiRk8mWtWi_jMs",
    "token_type": "bearer",
    "expires_in": 899
}
```

## 📝Credits

This software uses the following packages:

- Microsoft.AspNet.WebApi
- Microsoft.Owin
- Swashbuckle
- EntityFramework

## ©License
![GitHub](https://img.shields.io/github/license/Ctere1/LibraryAPI?style=flat-square)

```
Copyright (c) 2022 Cemil TAN

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

## 📌Contributors

<a href="https://github.com/Ctere1/">
  <img src="https://contrib.rocks/image?repo=Ctere1/Ctere1" />
</a>

