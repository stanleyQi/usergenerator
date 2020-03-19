using AutoMapper;
using Contracts;
using Entities.Dto;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using usergenerator.Controllers;
using Xunit;

namespace Usergenerator.Tests.Controllers
{
    public class UserGeneratorControllerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<ILoggerManager> _loggerMock;

        private readonly UserGeneratorController _userGeneratorController;

        #region initial setup
        
        public UserGeneratorControllerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _loggerMock = new Mock<ILoggerManager>();

            _userGeneratorController = new UserGeneratorController(
                _userRepositoryMock.Object,
                _mapperMock.Object,
                _httpContextAccessorMock.Object,
                _loggerMock.Object
            );
        }
        #endregion


        //Determines if Get returns a NotFoundObjectResult(HTTP Not Found) for NoUserMatching:
        //Test: 
        //  return type
        //  return content
        [Fact]
        public async Task Get_GetUsersByCondition_ReturnsNotFoundForNoUserMatching()
        {
            //Arrange
            var queryCount = "1";
            var searchName = "NoSuchName";

            _userRepositoryMock.Setup(repo => repo.GetUsersByCondition(Int32.Parse(queryCount), searchName))
                .ReturnsAsync(() =>
                        null
                    );

            //Act
            var result = await _userGeneratorController.Get(queryCount, searchName);

            //Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(queryCount, GetValueFromAnonymousObject(notFoundObjectResult.Value, "results"));
            Assert.Equal(searchName, GetValueFromAnonymousObject(notFoundObjectResult.Value, "search"));
        }

        //Determines if Get returns a list of userDto (<List<UserDTO>>) for a valid condition:
        //Test: 
        //  return type,value type
        //  return count,content
        [Fact]
        public async Task Get_GetUsersByCondition_ReturnsOneUserForSearchingCondition()
        {
            //Arrange
            var queryCount = "1";
            var searchName = "li";
            var userList = new List<User>();
            userList.Add(
                new User 
                { 
                    Id = 1,
                    Email="liqi@email.com",
                    Title="title",
                    LastName = "qi", 
                    FirstName = "li1",
                    Birthday="27-12-2020",
                    Phone="9999999",
                    Thumbnail="image1",
                    LargeImage="image2",
                    Other="other"
                });

            var userDtoList = new List<UserDto>();
            userDtoList.Add(
                new UserDto
                {
                    Id = 1,
                    Email = "liqi@email.com",
                    Title = "title",
                    LastName = "qi",
                    FirstName = "li1",
                    Birthday = "27-12-2020",
                    Phone = "9999999",
                    Thumbnail = "image1",
                    LargeImage = "image2",
                    Other = "other"
                });

            _userRepositoryMock.Setup(repo => repo.GetUsersByCondition(Int32.Parse(queryCount), searchName))
                .ReturnsAsync(
                        userList
                    );
            _mapperMock.Setup(map => map.Map<List<UserDto>>(userList))
                .Returns(
                        userDtoList
                    );

            //Act
            var result = await _userGeneratorController.Get(queryCount, searchName);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var users = Assert.IsType<List<UserDto>>(okResult.Value);
            Assert.Single(users);
            Assert.Contains("li", users[0].FirstName);
        }

        //Determines if GetUser returns a userDto for a valid id:
        //Test: 
        //  return type,value type
        //  return count,content
        [Fact]
        public async Task GetUser_GetUserById_ReturnsUserById()
        {
            //Arrange
            long id = 1;
            var user = new User
            {
                Id = 1,
                Email = "liqi@email.com",
                Title = "title",
                LastName = "qi",
                FirstName = "li1",
                Birthday = "27-12-2020",
                Phone = "9999999",
                Thumbnail = "image1",
                LargeImage = "image2",
                Other = "other"
            };
            var userDto = new UserDto
                {
                    Id = 1,
                    Email = "liqi@email.com",
                    Title = "title",
                    LastName = "qi",
                    FirstName = "li1",
                    Birthday = "27-12-2020",
                    Phone = "9999999",
                    Thumbnail = "image1",
                    LargeImage = "image2",
                    Other = "other"
                };

            _userRepositoryMock.Setup(repo => repo.GetUserById(id))
                .ReturnsAsync(
                        user
                    );
            _mapperMock.Setup(map => map.Map<UserDto>(user))
                .Returns(
                        userDto
                    );

            //Act
            var result = await _userGeneratorController.GetUser(id);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var userDtoResult = Assert.IsType<UserDto>(okResult.Value);
            Assert.Contains("li", userDtoResult.FirstName);
        }

        //Determines if GetUser returns not found for an invalid id:
        //Test: 
        //  return type
        //  return content
        [Fact]
        public async Task GetUser_GetUserById_ReturnsNotFoundForId()
        {
            //Arrange
            long id = 1;
            User user = null;

            _userRepositoryMock.Setup(repo => repo.GetUserById(id))
                .ReturnsAsync(
                        user
                    );

            //Act
            var result = await _userGeneratorController.GetUser(id);

            //Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(id, notFoundResult.Value);
        }

        //Determines if Put returns not found for an invalid id:
        //Test: 
        //  return type
        //  return content
        [Fact]
        public void Put_UpdateUserInfo_ReturnsNotFoundForId()
        {
            //Arrange
            long id = 1;
            var userDto = new UserDto
            {
                Id = 1,
                Email = "liqi@email.com",
                Title = "title",
                LastName = "qi",
                FirstName = "li1",
                Birthday = "27-12-2020",
                Phone = "9999999",
                Thumbnail = "image1",
                LargeImage = "image2",
                Other = "other"
            };
            var user = new User
            {
                Id = 1,
                Email = "liqi@email.com",
                Title = "title",
                LastName = "qi",
                FirstName = "li1",
                Birthday = "27-12-2020",
                Phone = "9999999",
                Thumbnail = "image1",
                LargeImage = "image2",
                Other = "other"
            };

            _mapperMock.Setup(map => map.Map<User>(userDto))
               .Returns(
                       user
                   );
            _userRepositoryMock.Setup(repo => repo.IsExistingUser(id))
                .Returns(
                    false
                );

            //Act
            var result =  _userGeneratorController.Put(id,userDto);

            //Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(id, notFoundResult.Value);
        }

        //Determines if Put returns updated info for UserDto specified:
        //Test: 
        //  return type,content type
        //  return content
        [Fact]
        public void Put_UpdateUserInfo_ReturnsUpdatedInfoForUserDtoSpecified()
        {
            //Arrange
            long id = 1;
            var userDto = new UserDto
            {
                Id = 1,
                Email = "liqi@email.com",
                Title = "title",
                LastName = "qi",
                FirstName = "li1",
                Birthday = "27-12-2020",
                Phone = "9999999",
                Thumbnail = "image1",
                LargeImage = "image2",
                Other = "other"
            };
            var user = new User
            {
                Id = 1,
                Email = "liqi@email.com",
                Title = "title",
                LastName = "qi",
                FirstName = "li1",
                Birthday = "27-12-2020",
                Phone = "9999999",
                Thumbnail = "image1",
                LargeImage = "image2",
                Other = "other"
            };

            _mapperMock.Setup(map => map.Map<User>(userDto))
               .Returns(
                       user
                   );
            _userRepositoryMock.Setup(repo => repo.IsExistingUser(id))
                .Returns(
                    true
                );
           _userRepositoryMock.Setup(repo => repo.Update(user))
               .Returns(
                  1
               );

            //Act
            var result = _userGeneratorController.Put(id, userDto);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var valueResult = Assert.IsType<int>(okResult.Value);
            Assert.Equal(1, valueResult);
        }

        //Determines if Delete returns not found for an invalid id:
        //Test: 
        //  return type
        //  return content
        [Fact]
        public void Delete_DeleteUser_ReturnsNotFoundForId()
        {
            //Arrange
            long id = 1;

            _userRepositoryMock.Setup(repo => repo.IsExistingUser(id))
                .Returns(
                    false
                );

            //Act
            var result = _userGeneratorController.Delete(id);

            //Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(id, notFoundResult.Value);
        }

        //Determines if Put returns deleted info for id specified:
        //Test: 
        //  return type,content type
        //  return content
        [Fact]
        public void Delete_DeleteUser_ReturnsDeletedInfoForIdSpecified()
        {
            //Arrange
            long id = 1;

            _userRepositoryMock.Setup(repo => repo.IsExistingUser(id))
                .Returns(
                    true
                );
            _userRepositoryMock.Setup(repo => repo.Delete(id))
                .Returns(
                   1
                );

            //Act
            var result = _userGeneratorController.Delete(id);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var valueResult = Assert.IsType<int>(okResult.Value);
            Assert.Equal(1, valueResult);
        }

        #region helper
        private static string GetValueFromAnonymousObject(Object obj, string attri)
        {
            string result = "";
            result = obj.GetType().GetProperty(attri).GetValue(obj).ToString();
            return result;
        }
        #endregion
    }
}
