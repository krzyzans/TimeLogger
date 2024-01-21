using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Timelogger.Api.Extensions;
using Timelogger.Entities;
using Timelogger.Models;
using Timelogger.Validation;

namespace Timelogger.Api.Controllers
{
    [Route("api/[controller]")]
	public class ProjectsController : ControllerBase
	{
		private readonly ApiContext _context;
        private readonly ProjectModelValidation _validator;

        public ProjectsController(ApiContext context, ProjectModelValidation validator)
        {
            _context = context;
            _validator = validator;
        }

		[HttpGet]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IReadOnlyList<ProjectModel>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetProjects(
            [FromQuery] string orderby,
            CancellationToken token = default)
        {
            var projectsQuery = _context.Projects
                .Select(p => new ProjectModel(p));

            if (!string.IsNullOrWhiteSpace(orderby))
            {
                projectsQuery = projectsQuery.OrderDescending(orderby);
            }

            var projects = await projectsQuery.ToListAsync(token);

            return Ok(projects);
        }

		// GET api/projects
		[HttpGet]
        [Route("{projectId}")]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProjectModel), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetProject(
            [FromRoute] int projectId,
            CancellationToken token = default)
        {
            ProjectModel project = await _context.Projects
                .Where(p => p.Id == projectId)
                .Select(p => new ProjectModel(p))
                .FirstOrDefaultAsync(token);

            if (project == null)
            {
				return NotFound();
            }

            return Ok(project);
		}

        // POST api/projects
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> NewProject(
            [FromBody] ProjectModel model,
            CancellationToken token = default)
        {
            var validationResult = await _validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            Project project = new Project()
            {
                DeadlineTime = model.DeadLine,
                Name = model.Name
            };

            await _context.Projects.AddAsync(project, token);
            await _context.SaveChangesAsync(token);

            return Created(Url.Action("NewProject", "Projects", new { id = project.Id }), project.Id);
        }
	}
}
