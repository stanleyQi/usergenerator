# usergenerator
---------------------------Introduction----------------------
<br>
Tech stack:
	Dev-Asp.net core2.2, EF core, LocalDB
	Test-XUnit, Postman

FileServer:Image file storage,running on https 5003
UserInfo storage:LocalDB

Usergenerator:Webapi,running on https 5001
Contracts:Interface
Entities:Entities and Dtos
Logger:Logservice
Repository:DB manipulation
Usergenerator.Tests:Unit test---In progress...
Integeration.Tests:Postman test collection script
![](https://github.com/stanleyQi/usergenerator/raw/master/Usergenerator.Tests/result/tree.png)
-------------------------------Dev--------------------------------
<br>
Points to note for Asp.NET Core Web API practice

	.Project Organization-OK
		Split application into smaller projects

	.Environment Based Settings-OK
		"Development, Staging, Production"

	.Startup Class and the Service Configuration-OK
		Create an extension class with the static method

	.Data Access Layer-OK
		Seprated, and the repository logic is based on interfaces

	.Controllers-OK
		Controllers should be responsible for accepting the service instances 
		through the constructor injection and for organizing HTTP action methods 
		(GET, POST, PUT, DELETE, PATCH...)
	
	.Action-OK
		Their responsibilities include handling HTTP requests, validating models, 
		catching errors and returning responses

	.Handling Errors Globally-OK
		Implement exception handling globally by using built-in and ready to use middleware

	.Using ActionFilters to Remove Duplicated Code-OK
		Avoid any code repetition as much as we can

	.Using DTOs to Return Results and to Accept Inputs-OK
	
	.Routing-OK
		Use Attribute Routing instead of Conventional Routing

	.Logging-OK
		Have a logging mechanism in place

-----------------------Test------------------------------
<br>
.UT:xUnit
	These unit tests are focus on the actions of the controller's behaviors.
	Those such as filters, routing, model binding will be validated in the integration test.
![](https://github.com/stanleyQi/usergenerator/raw/master/Usergenerator.Tests/result/XUnit-ut-result.png)  
	Controller-OK
		.Get([FromQuery(Name = "results")] string queryCount, [FromQuery(Name = "search")] string searchName)	
		.GetUser(long id)
		.Put(long id, [FromBody] UserDto userDto)
		.Delete(long id)

	Repository-In progress...
		.GetUsersByCondition-OK
		.GetUserById
		.Update
		.Delete
		.IsExistingUser

.Integration:
![](https://github.com/stanleyQi/usergenerator/raw/master/Integeration.Tests/Postman-integrationtesting-result.png)
	Postman-OK
		.GET: api/UserGenerator/?results=5&search=liqi
		.GET: api/UserGenerator/5
		.PUT: api/UserGenerator/5
		.DELETE: api/UserGenerator/5

////////////////////////////////Question and Answer///////////////////////////

>1. What is your  favourite design pattern, and why?
-It can be seen that the implementations of most of the design patterns 
-when I work with frameworks. Such as "Singleton" for the HttpApplication, 
-"Chain of Responsibility" for the middleware and filter in asp.net, and 
-"Mediator" for the state module in react, etc.
-My favourite design pattern is   "Observer" because it is widely used in as Notification
-function, and be adapted in most of the frameworks both frontend and backend.

>2. For your favourite programming language, tell me about a new (or upcoming) language 
>feature that has you excited. Why is it exciting for you?
-For C#8.0, there was a new feature I was excited for, it is "Default implementations of interface members".
-It can be used for extending new features(module) based on existing interface added default implementations,
-but no need to modify the modules has implemented the interface.  

>3. What do you NOT like to see when you're reviewing your own or another colleague's code?
-I like to use proven best practice code, such as being readable, testable, and maintainable. 
-It is hoped that the newly introduced method can be used to a minimum unless this method can 
-significantly increase development efficiency.

>4. Tell me about a time you fixed a performance issue.
-Sorry, no relevant experience in recent years.
-I think this needs to be analyzed and improved layer by layer from the beginning of the architecture, 
-and it involves many aspects. 
-As far as programming is concerned, we can start with 
-framework performance verification, managed and unmanaged resource management, 
-programming algorithms and logic, database connection management, SQL optimization, 
-caching, and more.
