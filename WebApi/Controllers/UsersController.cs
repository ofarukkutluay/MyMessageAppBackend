using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstracts;
using Core.Entities.Concretes;
using Core.WebApi;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : Controller
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Add(User user)
        {
            var result = _userService.Add(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _userService.GetAll();
            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);
        }

        [HttpGet]
        public IActionResult GetById(string id)
        {
            var result = _userService.GetById(id);
            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);
        }

        [HttpGet]
        public IActionResult GetPersonByUserId(string userId)
        {
            var result = _userService.GetPersonByUserId(userId);
            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);
        }

        [HttpGet]
        public IActionResult GetPersonByEmail(string email)
        {
            var result = _userService.GetPersonByEmail(email);
            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);
        }

        [HttpGet]
        public IActionResult GetPersonByMobileNumber(string mobileNumber)
        {
            var result = _userService.GetPersonByMobileNumber(mobileNumber);
            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);
        }

        [HttpDelete]
        public IActionResult Delete(User user)
        {
            var result = _userService.Delete(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut]
        public IActionResult Update(User user)
        {
            var result = _userService.Update(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut]
        public IActionResult UpdatePerson(Person person)
        {
            var result = _userService.UpdatePerson(person);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut]
        public IActionResult ActiveUser(string id)
        {
            var result = _userService.ActivateUser(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
