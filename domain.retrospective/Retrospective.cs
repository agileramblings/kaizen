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
        public List<Like> Likes { get; } = new List<Like>();

        public Retrospective(Guid newRetrospectiveId, string owner)
        {
            ApplyChange(new RetrospectiveCreated(newRetrospectiveId, owner));
        }

        public void InviteAParticipant(string participant)
        {
            ApplyChange(new ParticipantAdded(participant));
        }
        public void AddLikeItem(string description, string participantId)
        {
            ApplyChange(new LikeAdded(description, participantId));
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

        private void Apply(LikeAdded e)
        {
            Likes.Add(new Like{Description = e.Description, ParticipantId = e.ParticipantId});
        }

        #endregion
    }

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