using InvoiceApp.Data;
using InvoiceApp.DTOS;
using System.Security.Claims;

namespace InvoiceApp.Repository
{
    //Generic Repository Interface of logged In User with the main criteria.
    public interface IGenericOwnedRepository<TEntity, TDTO>
        where TEntity : class, IEntity, IOwnedEntity
        where TDTO : class, IDTO, IOwnedDTO
    {
        //All the asynchronous tasks of DTO with the use of ClaimsPrincipal.
        Task<TDTO> GetEntityById(ClaimsPrincipal User, string id);
        Task<List<TDTO>> GetAllEntities(ClaimsPrincipal User);
        Task<string> AddEntity(ClaimsPrincipal User, TDTO dto);
        Task<TDTO> UpdateEntity(ClaimsPrincipal User, TDTO dto);
        Task<bool> DeleteEntity(ClaimsPrincipal User, string id);

    }
}
