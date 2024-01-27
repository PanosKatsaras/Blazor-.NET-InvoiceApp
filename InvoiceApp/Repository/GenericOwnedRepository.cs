using AutoMapper;
using InvoiceApp.Data;
using InvoiceApp.DTOS;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using System.Security.Claims;

//Generic Repository of logged In User that owns the objects.
namespace InvoiceApp.Repository
{
    //The main criteria of the class based on the Interfaces.
    public class GenericOwnedRepository <TEntity, TDTO> : IGenericOwnedRepository <TEntity, TDTO>
        where TEntity : class, IEntity, IOwnedEntity
        where TDTO : class, IDTO, IOwnedDTO

    {
        
        public readonly ApplicationDbContext context;
        public readonly IMapper mapper;

        //Injection of ApplicationDbContext and IMapper
        public GenericOwnedRepository(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        //Method that gets the user Id of current user with the use of ClaimsPrincipal.
        protected string? getMyUserId(ClaimsPrincipal User)
        {
            Claim? uid = User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (uid is not null)
                return uid.Value;
            else
                return null;
        }

        //All the asynchronous tasks of DTO with the use of ClaimsPrincipal.
        public virtual async Task<List<TDTO>> GetAllEntities(ClaimsPrincipal User)
        {
            string? userId = getMyUserId(User);
            if (userId is not null)
            {
                List<TEntity> entities =
                    await context.Set<TEntity>().Where(e => e.UserId == userId).ToListAsync();
                List<TDTO> result = mapper.Map<List<TDTO>>(entities);
                return result;
            }
            else
            {
                return new List<TDTO>();
            }
        }

        public virtual async Task<TDTO> GetEntityById(ClaimsPrincipal User, string id)
        {
            string? userId = getMyUserId(User);
            if (userId is not null)
            {
                TEntity? entity = await
                    context.Set<TEntity>().Where(entity => entity.Id == id && entity.UserId == userId).FirstOrDefaultAsync(); 
                TDTO result = mapper.Map<TDTO>(entity);
                return result;
            }
            else
            {
                return null!;
            }
        }

        public virtual async Task<TDTO> UpdateEntity(ClaimsPrincipal User, TDTO dto)
        {
            string? userId = getMyUserId(User);
            if (userId is not null)
            {
                TEntity? entityToUpdate =
                    await context.Set<TEntity>().Where(entity => entity.Id == dto.Id && entity.UserId == userId).FirstOrDefaultAsync();

                if (entityToUpdate is not null)
                {
                    mapper.Map<TDTO, TEntity>(dto, entityToUpdate);
                    context.Entry(entityToUpdate).State = EntityState.Modified;
                    TDTO result = mapper.Map<TDTO>(entityToUpdate);
                    return result;

                }
                else
                {
                    return null!;
                }
            }
            else
            {
                return null!;
            }
        }

        public virtual async Task<bool> DeleteEntity(ClaimsPrincipal User, string id)
        {
            string? userId = getMyUserId(User);
            if (userId is not null)
            {
                TEntity? entity = await context.Set<TEntity>().Where(entity => entity.Id == id && entity.UserId == userId).FirstOrDefaultAsync();
                if (entity is not null)
                {
                    context.Remove(entity);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public virtual async Task<string> AddEntity(ClaimsPrincipal User, TDTO dto)
        {
            string? userId = getMyUserId(User);
            if (userId is not null)
            {
                dto.UserId = userId;
                dto.Id = System.Guid.NewGuid().ToString();
                TEntity entityToAdd = mapper.Map<TEntity>(dto);
                await context.Set<TEntity>().AddAsync(entityToAdd);
                return entityToAdd.Id;
            }
            else
            {
                return null!;
            }
        }

        protected async Task<List<TDTO>> GenericQuery(ClaimsPrincipal User, Expression<Func<TEntity, bool>>? expression, List<Expression<Func<TEntity, object>>> includes)
        {
            string? userId = getMyUserId(User);
            if (userId is not null)
            {
                IQueryable<TEntity> query = context.Set<TEntity>().Where(e => e.UserId == userId);
                if (expression is not null)
                    query = query.Where(expression);

                if (includes is not null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }
                List<TEntity> entities = await query.ToListAsync();

                List<TDTO> result = mapper.Map<List<TDTO>>(entities);
                return result;
            }
            else
            {
                return new List<TDTO>();
            }
        }


    }
}
