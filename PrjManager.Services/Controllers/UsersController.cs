using PrjManager.Business;
using PrjManager.Business.ServiceRequests;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using PrjManager.Infrastructure.Logging;

namespace PrjManager.Services.Controllers
{
    [RoutePrefix("v1/users")]
    public class UsersController : ApiController
    {
        readonly IUserBusiness _userBusiness;
        readonly ILogger _logger;
        public UsersController(IUserBusiness userBusiness, ILogger logger)
        {
            _userBusiness = userBusiness;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<UserViewModel> GetAll()
        {
            return _userBusiness.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            var user = _userBusiness.GetAll().FirstOrDefault(e => e.UserId == id);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Save(UserViewModel model)
        {
            _userBusiness.Save(model);
            return Ok();
        }

        [HttpPost]
        [Route("delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            _userBusiness.Delete(id);
            return Ok();
        }
    }
}
