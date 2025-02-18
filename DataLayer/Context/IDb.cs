﻿namespace DataLayer
{
    public interface IDb<T, K>
    {
        Task CreateAsync(T entity);

        Task<T> ReadAsync(K key, bool useNavigationalProperties = false, bool isReadOnly = true);

        Task<List<T>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true);

        Task UpdateAsync(T entity, bool useNavigationalProperties = false);

        Task DeleteAsync(K key);
    }
}
