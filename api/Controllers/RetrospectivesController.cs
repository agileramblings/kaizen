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
        public async Task<IActionResult> Post(string ownerIdentifier)
        {
            var newRetrospectiveId = Guid.NewGuid();
            var cmd = new CreateRetrospective(newRetrospectiveId, ownerIdentifier);
            await _cmdSender.Send(cmd);
            return RedirectToAction("Get", new { retrospectiveId = newRetrospectiveId });
        }

        [HttpPost("{retrospectiveId}/participant/{participantId}")]
        public async Task<IActionResult> Put(Guid retrospectiveId, string participantId)
        {
            var retro = await _aggRepo.GetById<Retrospective>(retrospectiveId);
            var cmd = new AddParticipant(retrospectiveId, participantId);
            await _cmdSender.Send(cmd);
            return RedirectToAction("Get", new { retrospectiveId = retrospectiveId });
        }

        [HttpPost("{retrospectiveId}/like/{participantId}")]
        public async Task<IActionResult> CreateLike(Guid retrospectiveId, string participantId, [FromBody] string description)
        {
            var retro = await _aggRepo.GetById<Retrospective>(retrospectiveId);
            return RedirectToAction("Get", new { retrospectiveId = retrospectiveId });
        }

        [HttpPost("{retrospectiveId}/dislike/{participantId}")]
        public async Task<IActionResult> CreateDislike(Guid retrospectiveId, string participantId, [FromBody] string description)
        {
            var retro = await _aggRepo.GetById<Retrospective>(retrospectiveId);
            return RedirectToAction("Get", new { retrospectiveId = retrospectiveId });
        }

        [HttpPost("{retrospectiveId}/actionitem/{participantId}")]
        public async Task<IActionResult> CreateActionItem(Guid retrospectiveId, string participantId, [FromBody] string description)
        {
            var retro = await _aggRepo.GetById<Retrospective>(retrospectiveId);
            return RedirectToAction("Get", new { retrospectiveId = retrospectiveId });
        }

        [HttpPost("{retrospectiveId}/like/{participantId}")]
        public async Task<IActionResult> UpdateLike(Guid retrospectiveId, string participantId, [FromBody] string description)
        {
            var retro = await _aggRepo.GetById<Retrospective>(retrospectiveId);
            return RedirectToAction("Get", new { retrospectiveId = retrospectiveId });
        }

        [HttpPost("{retrospectiveId}/dislike/{participantId}")]
        public async Task<IActionResult> UpdateDislike(Guid retrospectiveId, string participantId, [FromBody] string description)
        {
            var retro = await _aggRepo.GetById<Retrospective>(retrospectiveId);
            return RedirectToAction("Get", new { retrospectiveId = retrospectiveId });
        }

        [HttpPost("{retrospectiveId}/actionitem/{participantId}")]
        public async Task<IActionResult> UpdateActionItem(Guid retrospectiveId, string participantId, [FromBody] string description)
        {
            var retro = await _aggRepo.GetById<Retrospective>(retrospectiveId);
            return RedirectToAction("Get", new { retrospectiveId = retrospectiveId });
        }

        [HttpDelete("{retrospectiveId}")]
        public IActionResult Delete(Guid retrospectiveId)
        {
            return Ok();
        }
    }
}
