# usergenerator
---------------------------Introduction----------------------
<br>
Tech stack:

	.Dev-Asp.net core2.2, EF core, LocalDB
	.Test-XUnit, Postman

	.FileServer:Image file storage,running on https 5003
	.UserInfo storage:LocalDB

	.Usergenerator:Webapi,running on https 5001
	.Contracts:Interface
	.Entities:Entities and Dtos
	.Logger:Logservice
	.Repository:DB manipulation
	.Usergenerator.Tests:Unit test---In progress...
	.Integeration.Tests:Postman test collection script
![](https://github.com/stanleyQi/usergenerator/raw/master/Usergenerator.Tests/result/tree.png)
<br>
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

