using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Dto;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Usergenerator.Filter;

namespace usergenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGeneratorController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILoggerManager _logger;

        public UserGeneratorController(IUserRepository userRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ILoggerManager logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        // GET: api/UserGenerator/
        // GET: api/UserGenerator/?results=5
        // GET: api/UserGenerator/?search=liqi
        // GET: api/UserGenerator/?results=5&search=liqi
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name = "results")] string queryCount, [FromQuery(Name = "search")] string searchName)
        {
            //Handle the request parameter
            int count=1;
            if (!string.IsNullOrEmpty(queryCount))
                Int32.TryParse(queryCount, out count);

            string searchNameByKey = "";
            if (!string.IsNullOrEmpty(searchName))
                searchNameByKey = searchName;

            //Perform logic and catching errors
            var users = await _userRepository.GetUsersByCondition(count, searchNameByKey);
            if (users==null)
                return NotFound(new { results = queryCount, search = searchName });

            //Return response
            var usersResult = _mapper.Map<List<UserDto>>(users);
            return Ok(usersResult);
        }

        // GET: api/UserGenerator/GetUser/5
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(long id)
        {
            //Handle the request parameter:Using global filter for 400 exception

            //Perform logic and catching errors
            var user = await _userRepository.GetUserById(id);
            if (user == null)
                return NotFound(id);

            //Return response
            var userResult = _mapper.Map<UserDto>(user);
            return Ok(userResult);
        }

        // PUT: api/UserGenerator/5
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult Put(long id, [FromBody] UserDto userDto)
        {

            //Validating model:Using ActionFilter and using global filter for 400 exception

            //Perform logic and catching errors
            if (!_userRepository.IsExistingUser(id))
                return NotFound(id);

            var user = _mapper.Map<User>(userDto);

            //Return response
            var result = _userRepository.Update(user);
            return Ok(result);
        }

        // DELETE: api/UserGenerator/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            //Handle the request parameter:using global filter for 400 exception

            //Perform logic and catching errors
            if (!_userRepository.IsExistingUser(id))
                return NotFound(id);

            //Return response
            var result = _userRepository.Delete(id);
            return Ok(result);
        }
    }
}
