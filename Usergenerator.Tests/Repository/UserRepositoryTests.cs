using Contracts;
using Entities.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Repository.DataContext;
using Repository.Functions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Usergenerator.Tests.Repository
{
    public class UserRepositoryTests
    {
        //This test might be fail on the sqlite env, because of it is using a row sql in the code for sqlserver(top/limit)
        [Fact]
        public async Task GetUsersByCondition_WhenHaveNoConditionsSpecified_ReturnsOnlyOneUserExisting()
        {
            //Arrange
            var connection = new SqliteConnection();
            try
            {
                IUserRepository sut = GetSqliteInMemoryUserRepository(out connection);

                //Act
                var users = await sut.GetUsersByCondition();

                //Assert
                Assert.IsType<List<User>>(users);
                Assert.Single(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                //After
                connection.Close();
            }
        }

        //This test might be fail on the sqlite env, because of it is using a row sql in the code for sqlserver(top/limit)
        [Fact]
        public async Task GetUserById_WhenResultsAndSearchSpecified_ReturnsMultiUsersExisting()
        {
            //Arrange
            var connection = new SqliteConnection();
            try
            {
                IUserRepository sut = GetSqliteInMemoryUserRepository(out connection);
                var results = 2;
                var search = "li";

                //Act
                var users = await sut.GetUsersByCondition(results, search);

                //Assert
                Assert.IsType<List<User>>(users);
                Assert.Equal(2, users.Count);
                Assert.Equal(1, users[0].Id);
                Assert.Equal(2, users[1].Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                //After
                connection.Close();
            }
        }

        private IUserRepository GetSqliteInMemoryUserRepository(out SqliteConnection connection)
        {

            // In-memory database only exists while the connection is open
            connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlite(connection)
                    .Options;

            AppDbContext appDbContext = new AppDbContext(options);
            appDbContext.Database.EnsureDeleted();
            appDbContext.Database.EnsureCreated();

            //Import inital data
            var userList =new List<User>();
            userList.Add(new User
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
            userList.Add(new User
            {
                Id = 2,
                Email = "liqi@email.com",
                Title = "title",
                LastName = "qi",
                FirstName = "li2",
                Birthday = "27-12-2020",
                Phone = "9999999",
                Thumbnail = "image1",
                LargeImage = "image2",
                Other = "other"
            });
            userList.Add(new User
            {
                Id = 3,
                Email = "liqi@email.com",
                Title = "title",
                LastName = "qi",
                FirstName = "li3",
                Birthday = "27-12-2020",
                Phone = "9999999",
                Thumbnail = "image1",
                LargeImage = "image2",
                Other = "other"
            });

            var userRepository = new UserRepository(appDbContext);
            foreach (var user in userList)
            {
                userRepository.Add(user);
            }

            return userRepository;
        }
    }
}
