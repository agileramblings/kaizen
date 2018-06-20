using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class ActionItemAdded : Event
    {
        public string Description;
        public string ParticipantId;

        public ActionItemAdded(string description, string participantId)
        {
            Description = description;
            ParticipantId = participantId;
        }
    }
}
