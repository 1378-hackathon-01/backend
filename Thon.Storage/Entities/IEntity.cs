using Thon.Core.Models;

namespace Thon.Storage.Entities;

internal interface IEntity<T> where T : BaseModel
{
    public T GetModel();
}
