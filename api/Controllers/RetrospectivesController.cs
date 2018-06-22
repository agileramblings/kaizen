using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kaizen.domain.@base;
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
            var cmd = new AddLike(retrospectiveId, description, participantId);
            await _cmdSender.Send(cmd);
            return RedirectToAction("Get", new { retrospectiveId = retrospectiveId });
        }

        [HttpPost("{retrospectiveId}/like/{likeId}/{participantId}")]
        public async Task<IActionResult> UpdateLike(Guid retrospectiveId, Guid likeId, string participantId, [FromBody] string description)
        {
            var retro = await _aggRepo.GetById<Retrospective>(retrospectiveId);
            var cmd = new UpdateLike(retrospectiveId, likeId, description, participantId);
            await _cmdSender.Send(cmd);
            var response = RedirectToAction("Get", new { retrospectiveId = retrospectiveId });
            return response;
        }

        [HttpDelete("{retrospectiveId}/actionitem/{actionItemId}/{participantId}")]
        public async Task<IActionResult> DeleteActionItem(Guid retrospectiveId, Guid actionItemId, string participantId)
        {
            var retro = await _aggRepo.GetById<Retrospective>(retrospectiveId);
            var cmd = new DeleteActionItem(retrospectiveId, actionItemId, participantId);
            await _cmdSender.Send(cmd);
            return RedirectToAction("Get", new { retrospectiveId = retrospectiveId });
        }

        [HttpDelete("{retrospectiveId}/dislike/{dislikeId}/{participantId}")]
        public async Task<IActionResult> DeleteDislike(Guid retrospectiveId, Guid dislikeId, string participantId)
        {
            var retro = await _aggRepo.GetById<Retrospective>(retrospectiveId);
            var cmd = new DeleteDislike(retrospectiveId, dislikeId, participantId);
            await _cmdSender.Send(cmd);
            return RedirectToAction("Get", new { retrospectiveId = retrospectiveId });
        }

        [HttpDelete("{retrospectiveId}/like/{likeId}/{participantId}")]
        public async Task<IActionResult> DeleteLike(Guid retrospectiveId, Guid likeId, string participantId)
        {
            var retro = await _aggRepo.GetById<Retrospective>(retrospectiveId);
            var cmd = new DeleteLike(retrospectiveId, likeId, participantId);
            await _cmdSender.Send(cmd);
            return RedirectToAction("Get", new { retrospectiveId = retrospectiveId });
        }

        [HttpPost("{retrospectiveId}/dislike/{participantId}")]
        public async Task<IActionResult> CreateDislike(Guid retrospectiveId, string participantId, [FromBody] string description)
        {
            var retro = await _aggRepo.GetById<Retrospective>(retrospectiveId);
            var cmd = new AddDislike(retrospectiveId, description, participantId);
            await _cmdSender.Send(cmd);
            return RedirectToAction("Get", new { retrospectiveId = retrospectiveId });
        }

        [HttpPost("{retrospectiveId}/dislike/{dislikeId}/{participantId}")]
        public async Task<IActionResult> UpdateDislike(Guid retrospectiveId, Guid dislikeId, string participantId, [FromBody] string description)
        {
            var retro = await _aggRepo.GetById<Retrospective>(retrospectiveId);
            var cmd = new UpdateDislike(retrospectiveId, dislikeId, description, participantId);
            await _cmdSender.Send(cmd);
            var response = RedirectToAction("Get", new { retrospectiveId = retrospectiveId });
            return response;
        }

        [HttpPost("{retrospectiveId}/actionitem/{participantId}")]
        public async Task<IActionResult> CreateActionItem(Guid retrospectiveId, string participantId, [FromBody] string description)
        {
            var retro = await _aggRepo.GetById<Retrospective>(retrospectiveId);
            var cmd = new AddActionItem(retrospectiveId, description, participantId);
            await _cmdSender.Send(cmd);
            return RedirectToAction("Get", new { retrospectiveId = retrospectiveId });
        }

        [HttpPost("{retrospectiveId}/actionitem/{actionItemId}/{participantId}")]
        public async Task<IActionResult> UpdateActionItem(Guid retrospectiveId, Guid actionItemId, string participantId, [FromBody] string description)
        {
            var retro = await _aggRepo.GetById<Retrospective>(retrospectiveId);
            var cmd = new UpdateActionItem(retrospectiveId, actionItemId, description, participantId);
            await _cmdSender.Send(cmd);
            var response = RedirectToAction("Get", new { retrospectiveId = retrospectiveId });
            return response;
        }

        [HttpPost("{retrospectiveId}/state")]
        public async Task<IActionResult> UpdateRetroState(string participantId, Guid retrospectiveId, RetrospectiveState state)
        {
            var cmd = new UpdateRetroState(retrospectiveId, participantId, state);
            await _cmdSender.Send(cmd);
            var response = RedirectToAction("Get", new { retrospectiveId = retrospectiveId });
            return response;
        }

        [HttpPost("{retrospectiveId}/like/{likeId}/tooglevote")]
        public async Task<IActionResult> ToogleLikeVote(string participantId, Guid retrospectiveId, Guid likeId)
        {
            var cmd = new Vote(retrospectiveId, likeId, Vote.VoteItemType.Like, participantId);
            await _cmdSender.Send(cmd);
            var response = RedirectToAction("Get", new { retrospectiveId = retrospectiveId });
            return response;
        }

        [HttpPost("{retrospectiveId}/dislike/{dislikeId}/tooglevote")]
        public async Task<IActionResult> ToogleDisLikeVote(string participantId, Guid retrospectiveId, Guid dislikeId)
        {
            var cmd = new Vote(retrospectiveId, dislikeId, Vote.VoteItemType.DisLike, participantId);
            await _cmdSender.Send(cmd);
            var response = RedirectToAction("Get", new { retrospectiveId = retrospectiveId });
            return response;
        }

        [HttpPost("{retrospectiveId}/actionitem/{actionItemId}/tooglevote")]
        public async Task<IActionResult> ToogleActionItemVote(string participantId, Guid retrospectiveId, Guid actionItemId)
        {
            var cmd = new Vote(retrospectiveId, actionItemId, Vote.VoteItemType.ActionItem, participantId);
            await _cmdSender.Send(cmd);
            var response = RedirectToAction("Get", new { retrospectiveId = retrospectiveId });
            return response;
        }

        [HttpDelete("{retrospectiveId}")]
        public IActionResult Delete(string participantId, Guid retrospectiveId)
        {
            return Ok();
        }
    }
}
