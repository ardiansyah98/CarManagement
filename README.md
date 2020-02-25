# Car Management

Simple apps with three pages using Asp.Net Core 3.1: 
  - Register
  - Login
  - Manage Car

# Feature
- Migrate database on startup
- Using Cookie for authentication
- Register new user
- Login
- Create log in every action
- CRUD Car (Only authenticated user)

# Register
```
[Route("api/signup")]
[HttpPost]
public ActionResult SignUp([FromBody] RegisterModel registerModel)
{
...
}
```
Register new user will hit api with parameter RegisterModel. RegisterModel data class is like code below
```
public class RegisterModel
{
    public string szEmail { get; set; }
    public string szFullName { get; set; }
    public string szPassword { get; set; }
    public string szConfirmPassword { get; set; }
}
```

Password will be saved in MD5 hash.

# Login
```
[Route("api/signin")]
[HttpPost]
public async Task<ActionResult> SignIn([FromBody] SignInRequest signInRequest)
{
...
}
```
Login will hit api with parameter SignInRequest. SignInRequest data class is like code below
```
public class SignInRequest
{
    public string szEmail { get; set; }
    public string szPassword { get; set; }
}
```
