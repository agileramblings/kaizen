using System;
using System.Threading.Tasks;

namespace kaizen.domain.@base.persistence
{
    public interface IReadModelPersistence
    {
        Task Put<T>(T t) where T : ReadModelBase;
        Task Delete<T>(Guid id) where T : ReadModelBase;
    }
}