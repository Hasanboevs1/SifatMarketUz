﻿using System.Linq.Expressions;

namespace Cart.Contract.Interfaces;

public interface IRepositoryBase<T> where T : class
{
    IQueryable<T> FindAll();
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition);
    void Create(T entity);
    void Delete(T entity);
    void Update(T entity);
}
