using Basic.Reference.Assemblies;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using SourceGeneratorTestHelpers;

namespace GeneratedEntityFramework.Tests.Common;

public static class TestHelpers
{
    public static GeneratorDriverRunResult RunGenerator(IEnumerable<string> sources)
    {
        var cSharpParseOptions = new CSharpParseOptions(LanguageVersion.CSharp11).WithPreprocessorSymbols("NET7_0_OR_GREATER");
        var cSharpCompilationOptions = new CSharpCompilationOptions(OutputKind.NetModule).WithNullableContextOptions(NullableContextOptions.Enable);
        return IncrementalGenerator.Run<EntityFrameworkGenerator>(sources, cSharpParseOptions, ReferenceAssemblies.Net80, cSharpCompilationOptions);
    }

    public static IEnumerable<string> GetSources(string source, bool withNamespace)
    {
        const string usingStatements = """
                                       using System.Linq;
                                       using Microsoft.EntityFrameworkCore;
                                       using Microsoft.Extensions.DependencyInjection;
                                       using GeneratedEntityFramework;
                                       """;

        if (withNamespace)
            yield return $"""
                          {usingStatements}

                          namespace EntityFrameworkGeneratorTests;

                          {source}
                          """;
        else
            yield return $"""
                          {usingStatements}

                          {source}
                          """;

        yield return """
                     using System;
                     using System.Collections;
                     using System.Collections.Generic;
                     using System.Linq;
                     using System.Linq.Expressions;

                     namespace Microsoft.EntityFrameworkCore
                     {
                         public class DbContext
                         {
                         }

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

                             public Type ElementType { get; }
                             public Expression Expression { get; }
                             public IQueryProvider Provider { get; }
                         }
                     }

                     namespace Microsoft.Extensions.DependencyInjection
                     {
                         public interface IServiceCollection
                         {
                         }

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
                     """;
    }
}
