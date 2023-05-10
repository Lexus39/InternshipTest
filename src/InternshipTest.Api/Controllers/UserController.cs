using InternshipTest.Application.Services;
using InternshipTest.Domain.UserAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternshipTest.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Возвращает пользователя по id
        /// </summary>
        /// <returns>Возвращает User</returns>
        /// <response code="200">Если пользователь существет</response>
        /// <response code="404">Если пользователя с таким id не удалось найти</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id) 
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }

        /// <summary>
        /// Добавляет нового пользователя
        /// </summary>
        /// <remarks>
        /// Для админов groupid=1.
        /// Для пользователей groupid=2.
        /// Логин должен быть длиной от 4 до 20 символов, начинаться с буквы и состоять из букв, цифр и знаков подчеркивания.
        /// Пароль должен быть длиной от 8 до 20 символов и состоять из букв, цифр и знаков подчеркивания.
        /// Пользователь всегда создаётся с статусом Active.
        /// </remarks>
        /// <returns>Возвращает добавленный объект User</returns>
        /// <response code="200">Если добавление прошло успешно</response>
        /// <response code="400">Если переданы некоректные параметры</response>
        [HttpPost("create")]
        public async Task<ActionResult<User>> CreateUser([FromBody]UserCreateParameters parameters)
        {
            return Ok(await _userService.CreateUserAsync(parameters));
        }

        /// <summary>
        /// Удаляет пользователя по id
        /// </summary>
        /// <returns>Ничего не возвращает</returns>
        /// <response code="200">Если пользователь существет и удаление прошло успешно</response>
        /// <response code="404">Если пользователя с таким id не удалось найти</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok();
        }

        /// <summary>
        /// Возвращает всех пользователей
        /// </summary>
        /// <returns>Возвращает список объектов User</returns>
        /// <response code="200"></response>
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return Ok(await _userService.GetUsersAsync());
        }

        /// <summary>
        /// Обновляет данные пользователя
        /// </summary>
        /// <remarks>
        /// Для админов groupid=1. 
        /// Для пользователей groupid=2
        /// Пароль должен быть длиной от 8 до 20 символов и состоять из букв, цифр и знаков подчеркивания.
        /// </remarks>
        /// <returns>Ничего не возвращает</returns>
        /// <response code="200">Если пользователь с таким id существет и его обновление прошло успешно</response>
        /// <response code="400">Если переданы некоректные параметры</response>
        /// <response code="404">Если пользователя с таким id не удалось найти</response>
        [HttpPut("update")]
        public async Task<ActionResult> UpdateUser([FromBody]UserUpdateParameters parameters)
        {
            await _userService.UpdateUserAsync(parameters);
            return Ok();
        }
    }
}
