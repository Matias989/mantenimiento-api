using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using mantenimiento_api.Controllers.RR;
using mantenimiento_api.Controllers.VM;
using mantenimiento_api.Models;
using mantenimiento_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mantenimiento_api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly ILogger _logger;
        readonly IUsersServices _services;
        readonly IMapper _mapper;
        public UsersController(ILogger<UsersController> logger, IUsersServices services, IMapper mapper)
        {
            _logger = logger;
            _services = services;
            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<ApiResponseBase<IEnumerable<UserVM>>>> GetUsers()
        {
            ApiResponseBase<IEnumerable<UserVM>> resp = new ApiResponseBase<IEnumerable<UserVM>>();
            resp.Successful();
            try
            {
                IEnumerable<User> res = _services.GetUsers();
                resp.Data = _mapper.Map<IEnumerable<User>, IEnumerable<UserVM>>(res);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
            return Ok(resp);
        }

        // GET: api/Users/5
        [HttpGet("{email}")]
        public async Task<ActionResult<ApiResponseBase<UserVM>>> GetUser([FromQuery] string email)
        {
            ApiResponseBase<UserVM> resp = new ApiResponseBase<UserVM>();
            resp.Successful();
            try
            {
                var res = _services.GetUser(email);

                if (res == null)
                {
                    resp.Error("Usuario no encontrado");
                    return BadRequest(resp);
                }

                resp.Data = _mapper.Map<User, UserVM>(res);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

            return Ok(resp);
        }
        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponseBase<UserVM>>> GetUser([FromQuery]int id)
        {
            ApiResponseBase<UserVM> resp = new ApiResponseBase<UserVM>();
            resp.Successful();
            try
            {
                var res = _services.GetUser(id);

                if (res == null)
                {
                    resp.Error("Usuario no encontrado");
                    return BadRequest(resp);
                }

                resp.Data = _mapper.Map<User, UserVM>(res);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

            return Ok(resp);
        }

        // PUT: api/Users/5
        [HttpPut]
        public async Task<ActionResult<ApiResponseBase<string>>> PutUser([FromBody] UserVM userVM)
        {
            ApiResponseBase<string> resp = new ApiResponseBase<string>();
            resp.Successful();
            resp.Data = string.Empty;
            try
            {
                if (userVM == null)
                {
                    resp.Error("Debe enviar un usuario correcto");
                    return BadRequest(resp);
                }

                var user = _services.GetUser(userVM.Id);

                if (user == null)
                {
                    resp.Error("Usuario inexistente");
                    return BadRequest(resp);
                }

                user = _mapper.Map<UserVM, User>(userVM);

                if (!_services.UpdateUser(user))
                {
                    return Problem("Ocurrio un problema al actualizar el usuario");
                }
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

            return Ok(resp);

        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<ApiResponseBase<int>>> PostUser([FromBody] UserVM userVM)
        {
            ApiResponseBase<int> resp = new ApiResponseBase<int>();
            resp.Successful();
            try
            {
                if (userVM == null)
                {
                    resp.Error("Usuario no valido");
                    return BadRequest(resp);
                }

                // TODO - esto deberia activarse por mail
                userVM.Active = true;

                var user = _mapper.Map<UserVM, User>(userVM);

                resp.Data = _services.InsertUser(user);
                if (resp.Data == 0)
                {
                    return Problem("No se pudo insertar el usuario");
                }
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
            return Ok(resp);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseBase<string>>> DeleteUser([FromQuery] int id)
        {
            ApiResponseBase<string> resp = new ApiResponseBase<string>();
            resp.Successful();
            resp.Data = string.Empty;
            try
            {
                if (id > 0)
                {
                    resp.Error("Datos invalidos");
                    return BadRequest();
                }
                if(_services.DeleteUser(id))
                {
                    return Problem("no se pudo eliminar usuario");
                }
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
            return Ok(resp);
        }
    }
}