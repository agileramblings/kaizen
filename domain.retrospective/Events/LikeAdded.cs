using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class LikeAdded : Event
    {
        public string Description;
        public string ParticipantId;

        public LikeAdded(string description, string participantId)
        {
            Description = description;
            ParticipantId = participantId;
        }
    }
}