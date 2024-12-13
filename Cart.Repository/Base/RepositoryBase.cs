using Cart.Contract.Interfaces;
using Cart.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Repository.Base;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    private readonly RepositoryContext _context;

    public RepositoryBase(RepositoryContext context) => _context = context;

    public void Create(T entity) => _context.Set<T>().Add(entity);

    public void Delete(T entity) => _context.Set<T>().Remove(entity);

    public void Update(T entity) => _context.Set<T>().Update(entity);

    public IQueryable<T> FindAll() => _context.Set<T>();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition) => _context.Set<T>().Where(condition);
}