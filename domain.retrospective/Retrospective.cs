using System;
using System.Collections.Generic;
using kaizen.domain.@base;
using kaizen.domain.retrospective.events;

// ReSharper disable UnusedMember.Local

namespace kaizen.domain.retrospective
{
    public class Retrospective : AggregateBase
    {
        public DateTime CreatedOn { get; private set; }
        public string Owner { get; private set; }
        public List<string> Participants { get; } = new List<string>();

        public Retrospective(Guid newRetrospectiveId, string owner)
        {
            ApplyChange(new RetrospectiveCreated(newRetrospectiveId, owner));
        }

        public void InviteAParticipant(string participant)
        {
            ApplyChange(new ParticipantAdded(participant));
        }

        #region Private Setters
        // Applied by Reflection when reading events (AggregateBase -> this.AsDynamic().Apply(@event);)
        // Ensures aggregates get their needed property values (e.g. Id) from events
        private void Apply(RetrospectiveCreated e)
        {
            Id = e.Id;
            CreatedOn = e.CreatedOn;
            Owner = e.Owner;
        }

        private void Apply(ParticipantAdded e)
        {
            Participants.Add(e.ParticipantIdentifier);
        }

        #endregion

    }
}