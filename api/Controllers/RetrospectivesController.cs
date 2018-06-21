using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using kaizen.domain.@base;
using kaizen.domain.@base.messaging;
using kaizen.domain.@base.persistence;
using kaizen.domain.retrospective;
using kaizen.domain.retrospective.commands;
using kaizen.domain.retrospective.readmodel;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/v1/retrospectives")]
    [ApiController]
    public class RetrospectivesController : ControllerBase
    {
        private readonly ICommandSender _cmdSender;
        private readonly IReadModelFacade _read;
        private readonly IAggregateRepository _aggRepo;

        public RetrospectivesController(IAggregateRepository agRepository,
                                        IReadModelFacade readModel,
                                        ICommandSender cmdSender)
        {
            _aggRepo = agRepository;
            _read = readModel;
            _cmdSender = cmdSender;
        }

        [HttpGet]
        public async Task<IEnumerable<RetrospectiveDetails>> GetAll()
        {
            var allDetails = await _read.GetAll<RetrospectiveDetails>();
            return allDetails;
        }

        [HttpGet("{retrospectiveId}")]
        public async Task<RetrospectiveDetails> Get(Guid retrospectiveId)
        {
            return await _read.Get<RetrospectiveDetails>(retrospectiveId);
        }

        [HttpPost("{ownerIdentifier}")]
        public IActionResult Post(string ownerIdentifier)
        {
            var newRetrospectiveId = Guid.NewGuid();
            var cmd = new CreateRetrospective(newRetrospectiveId, ownerIdentifier);
            _cmdSender.Send(cmd);
            return RedirectToAction("Get", new { retrospectiveId = newRetrospectiveId });
        }

        [HttpPut("{retrospectiveId}/participant/{participantId}")]
        public void Put(int retrospectiveId, string participantId)
        {
        }

        [HttpPost("{retrospectiveId}/like/{participantId}")]
        public void CreateLike(int retrospectiveId, string participantId, [FromBody] string description)
        {
        }

        [HttpPost("{retrospectiveId}/dislike/{participantId}")]
        public void CreateDislike(int retrospectiveId, string participantId, [FromBody] string description)
        {
        }

        [HttpPost("{retrospectiveId}/actionitem/{participantId}")]
        public void CreateActionItem(int retrospectiveId, string participantId, [FromBody] string description)
        {
        }

        [HttpPut("{retrospectiveId}/like/{participantId}")]
        public void UpdateLike(int retrospectiveId, string participantId, [FromBody] string description)
        {
        }

        [HttpPut("{retrospectiveId}/dislike/{participantId}")]
        public void UpdateDislike(int retrospectiveId, string participantId, [FromBody] string description)
        {
        }

        [HttpPut("{retrospectiveId}/actionitem/{participantId}")]
        public void UpdateActionItem(int retrospectiveId, string participantId, [FromBody] string description)
        {
        }

        [HttpDelete("{retrospectiveId}")]
        public void Delete(int retrospectiveId)
        {
        }
    }
}
