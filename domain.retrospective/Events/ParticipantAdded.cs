using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class ParticipantAdded : Event
    {
        public string ParticipantIdentifier { get; set; }
        public ParticipantAdded(string participantId)
        {
            ParticipantIdentifier = participantId;
        }
    }
}