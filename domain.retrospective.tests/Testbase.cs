using System;
using System.Collections.Generic;
using System.Text;

namespace kaizen.domain.retrospective.tests
{
    public abstract class TestBase
    {
        protected const string OwnerName = "dave.white@gettyimages.com";
        protected readonly string[] _participants = { "tuan.nguyen@gettyimages.com", "david.chen@gettyimages.com" };
        protected readonly Guid _defaultRetroId = Guid.Parse("12345678-1234-1234-1234-123456789012");

        #region Test Helpers
        protected Retrospective GetDefaultRetrospectiveSut() => new Retrospective(_defaultRetroId, OwnerName);
        #endregion
    }
}
