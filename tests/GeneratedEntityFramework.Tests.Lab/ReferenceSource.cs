using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

// Resharper disable CheckNamespace
namespace Microsoft.EntityFrameworkCore
{
    public class DbContext;

    public abstract class DbSet<TEntity> : IQueryable<TEntity>
        where TEntity : class
    {
        public IEnumerator<TEntity> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Type ElementType { get; } = default!;
        public Expression Expression { get; } = default!;
        public IQueryProvider Provider { get; } = default!;
    }

    public static class EntityFrameworkQueryableExtensions
    {
        public static IQueryable<TEntity> AsNoTracking<TEntity>(this IQueryable<TEntity> source)
            where TEntity : class
        {
            throw new NotImplementedException();
        }
    }
}

// Resharper disable CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public interface IServiceCollection;

    public enum ServiceLifetime
    {
        Singleton = 0,
        Scoped = 1,
        Transient = 2,
    }

    public static class ServiceCollectionServiceExtensions
    {
        public static IServiceCollection AddSingleton<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory)
            where TService : class
        {
            throw new NotImplementedException();
        }

        public static IServiceCollection AddScoped<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory)
            where TService : class
        {
            throw new NotImplementedException();
        }

        public static IServiceCollection AddTransient<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory)
            where TService : class
        {
            throw new NotImplementedException();
        }

        public static T GetRequiredService<T>(this IServiceProvider provider)
            where T : notnull, new()
        {
            throw new NotImplementedException();
        }
    }
}
