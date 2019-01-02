using PrjManager.Business;
using PrjManager.Business.ServiceRequests;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PrjManager.Services.Controllers
{
    [RoutePrefix("v1/projects")]
    public class ProjectsController : ApiController
    {
        readonly IProjectBusiness _projectBusiness;
        public ProjectsController(IProjectBusiness projectBusiness)
        {
            _projectBusiness = projectBusiness;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<ProjectViewModel> GetAll()
        {
            return _projectBusiness.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            var user = _projectBusiness.GetAll().FirstOrDefault(e => e.ProjectId == id);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Save(ProjectViewModel model)
        {
            _projectBusiness.Save(model);
            return Ok();
        }

        [HttpPost]
        [Route("delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            _projectBusiness.Delete(id);
            return Ok();
        }
    }
}
