namespace EShopProject.Repositories;
using EShopProject.Entities;

public interface IRepository<T> where T : IEntityId
{
    T? GetSingle(int id);
    void Add(T item);
    T Update(T item);
    IQueryable<T> GetAll();
    void Delete(T item);
    bool Save();
}