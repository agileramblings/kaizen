using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class DislikeAdded : Event
    {
        public string Description;
        public string ParticipantId;

        public DislikeAdded(string description, string participantId)
        {
            Description = description;
            ParticipantId = participantId;
        }
    }
}