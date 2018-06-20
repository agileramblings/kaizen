using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kaizen.domain.retrospective;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RetrospectivesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Retrospective>> Get()
        {
            return new Retrospective[] {new Retrospective(Guid.NewGuid(), "dave.white@gettyimages.com"), new Retrospective(Guid.NewGuid(), "tuan.nguyen@gettyimages.com"), new Retrospective(Guid.NewGuid(), "david.chen@gettyimages.com") };
        }

        // GET api/values/5
        [HttpGet("{retrospectiveId}")]
        public ActionResult<Retrospective> Get(Guid retrospectiveId)
        {
            return new Retrospective(Guid.NewGuid(), "dave.white@gettyimages.com");
        }

        // POST api/values
        [HttpPost]
        public void Post()
        {

        }

        // Put pi/values/5
        [HttpPut("{retrospectiveId}/participant/{participantId}")]
        public void Put(int retrospectiveId, string participantId)
        {
        }

        // POST 
        [HttpPost("{retrospectiveId}/like/{participantId}")]
        public void CreateLike(int retrospectiveId, string participantId, [FromBody] string description)
        {
        }

        // POST 
        [HttpPost("{retrospectiveId}/dislike/{participantId}")]
        public void CreateDislike(int retrospectiveId, string participantId, [FromBody] string description)
        {
        }

        // POST 
        [HttpPost("{retrospectiveId}/actionitem/{participantId}")]
        public void CreateActionItem(int retrospectiveId, string participantId, [FromBody] string description)
        {
        }

        // PUT 
        [HttpPut("{retrospectiveId}/like/{participantId}")]
        public void UpdateLike(int retrospectiveId, string participantId, [FromBody] string description)
        {
        }

        // PUT 
        [HttpPut("{retrospectiveId}/dislike/{participantId}")]
        public void UpdateDislike(int retrospectiveId, string participantId, [FromBody] string description)
        {
        }

        // PUT 
        [HttpPut("{retrospectiveId}/actionitem/{participantId}")]
        public void UpdateActionItem(int retrospectiveId, string participantId, [FromBody] string description)
        {
        }



        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
