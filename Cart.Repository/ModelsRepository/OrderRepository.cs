using Cart.Contract.Interfaces;
using Cart.Entities.Models;
using Cart.Repository.Base;
using Cart.Repository.Contexts;
using System.Linq.Expressions;

namespace Cart.Repository.ModelsRepository;

public class OrderRepository : RepositoryBase<Entities.Models.Cart>, IOrderRepository
{
    public OrderRepository(RepositoryContext context)
        : base(context)
    {
    }

    public void Create(Order entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Order entity)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Order> FindByCondition(Expression<Func<Order, bool>> condition)
    {
        throw new NotImplementedException();
    }

    public void Update(Order entity)
    {
        throw new NotImplementedException();
    }

    IQueryable<Order> IRepositoryBase<Order>.FindAll()
    {
        throw new NotImplementedException();
    }
}