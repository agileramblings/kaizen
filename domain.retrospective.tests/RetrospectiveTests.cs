using System;
using System.Linq;
using kaizen.domain.retrospective.events;
using Xunit;

namespace kaizen.domain.retrospective.tests
{
    public class RetrospectiveTests
    {
        private const string OwnerName = "dave.white@gettyimages.com";

        [Fact]
        public void WhenARetrospectiveIsCreated()
        {
            // arrange

            // act
            var sut = new Retrospective(Guid.Empty, OwnerName);
            var events = sut.GetUncommittedChanges().ToList();

            // assert
            Assert.Single(events);
            Assert.Equal(typeof(RetrospectiveCreated), events.First().GetType());
            Assert.Equal(OwnerName, ((RetrospectiveCreated)events.First()).Owner);
            Assert.NotEqual(default(DateTime), ((RetrospectiveCreated)events.First()).CreatedOn);
        }
    }
}
