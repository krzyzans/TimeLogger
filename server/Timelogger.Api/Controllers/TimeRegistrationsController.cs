using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Timelogger.Entities;
using Timelogger.Models;
using Timelogger.Validation;

namespace Timelogger.Api.Controllers
{
    [Route("api/[controller]")]
    public class TimeRegistrationsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly TimeRegistrationModelValidation _validator;

        public TimeRegistrationsController(ApiContext context, TimeRegistrationModelValidation validator)
        {
            _context = context;
            _validator = validator;
        }

        [HttpGet]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IReadOnlyList<TimeRegistrationModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTimeRegistrations(
            CancellationToken token = default)
        {
            var timeRegistrations = await _context.TimeRegistrations
                .Select(tr => new TimeRegistrationModel(tr))
                .ToListAsync(token);

            return Ok(timeRegistrations);
        }

        // GET api/timeregistration
        [HttpGet]
        [Route("{projectId}")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IReadOnlyList<TimeRegistrationModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTimeRegistration(
            [FromRoute] int projectId,
            CancellationToken token = default)
        {
            var timeRegistrations = await _context.TimeRegistrations
                .Where(p => p.ProjectId == projectId)
                .Select(tr => new TimeRegistrationModel(tr))
                .ToListAsync(token);

            return Ok(timeRegistrations);
        }

        // POST api/timeregistration
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> NewTimeRegistration(
            [FromBody] TimeRegistrationModel timeRegistration,
            CancellationToken token = default)
        {
            var validationResult = await _validator.ValidateAsync(timeRegistration);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            TimeRegistration model = new TimeRegistration()
            {
                ProjectId = timeRegistration.ProjectId,
                Minutes = timeRegistration.Minutes,
                InvoiceId = timeRegistration.InvoiceId
            };

            await _context.TimeRegistrations.AddAsync(model, token);
            await _context.SaveChangesAsync(token);

            return Created(Url.Action("NewTimeRegistration", "TimeRegistrations", new { id = model.Id }), model.Id);
        }
    }
}
