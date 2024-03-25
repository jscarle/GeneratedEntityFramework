using Basic.Reference.Assemblies;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using SourceGeneratorTestHelpers;
using SourceGeneratorTestHelpers.XUnit;

namespace GeneratedEntityFramework.Tests;

[UsesVerify]
public class EntityFrameworkGeneratorTests
{
    public EntityFrameworkGeneratorTests()
    {
        ModuleInitializer.Initialize();
    }

    #region Using DbSets

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task GeneratedDbContext_UsingDbSets(bool withNamespace)
    {
        var sources = GetSources(
            """
            [GeneratedDbContext]
            public partial class BloggingContext : DbContext, IBlogsContext, IPostsContext;

            public interface IBlogsContext
            {
                public DbSet<Blog> Blogs { get; }
            }

            public interface IPostsContext
            {
                public DbSet<Post> Posts { get; }
            }

            public sealed class Blog;

            public sealed class Post;
            """,
            withNamespace
        );
        var result = RunGenerator(sources);

        await result.VerifyAsync(EntityFrameworkGenerator.AttributesHint)
            .UseMethodName($"Attributes_{nameof(GeneratedDbContext_UsingDbSets)}_With{(withNamespace ? "" : "out")}Namespace");
        await result.VerifyAsync("BloggingContext.g.cs")
            .UseMethodName($"DbContext_{nameof(GeneratedDbContext_UsingDbSets)}_With{(withNamespace ? "" : "out")}Namespace");
        await result.VerifyAsync(EntityFrameworkGenerator.RegistrationClassHint).UseMethodName(
            $"RegisterServices_{nameof(GeneratedDbContext_UsingDbSets)}_With{(withNamespace ? "" : "out")}Namespace"
        );
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task GeneratedDbContext_WithInterface_UsingDbSets(bool withNamespace)
    {
        var sources = GetSources(
            """
            [GeneratedDbContext<IBlogsContext>]
            [GeneratedDbContext<IPostsContext>]
            public partial class BloggingContext : DbContext;

            public interface IBlogsContext
            {
                public DbSet<Blog> Blogs { get; }
            }

            public interface IPostsContext
            {
                public DbSet<Post> Posts { get; }
            }

            public sealed class Blog;

            public sealed class Post;
            """,
            withNamespace
        );
        var result = RunGenerator(sources);

        await result.VerifyAsync(EntityFrameworkGenerator.AttributesHint).UseMethodName(
            $"Attributes_{nameof(GeneratedDbContext_WithInterface_UsingDbSets)}_With{(withNamespace ? "" : "out")}Namespace"
        );
        await result.VerifyAsync("BloggingContext.g.cs").UseMethodName(
            $"DbContext_{nameof(GeneratedDbContext_WithInterface_UsingDbSets)}_With{(withNamespace ? "" : "out")}Namespace"
        );
        await result.VerifyAsync(EntityFrameworkGenerator.RegistrationClassHint).UseMethodName(
            $"RegisterServices_{nameof(GeneratedDbContext_WithInterface_UsingDbSets)}_With{(withNamespace ? "" : "out")}Namespace"
        );
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task DbContext_UsingDbSets(bool withNamespace)
    {
        var sources = GetSources(
            """
            public partial class BloggingContext : DbContext;

            [DbContext<BloggingContext>]
            public interface IBlogsContext
            {
                public DbSet<Blog> Blogs { get; }
            }

            [DbContext<BloggingContext>]
            public interface IPostsContext
            {
                public DbSet<Post> Posts { get; }
            }

            public sealed class Blog;

            public sealed class Post;
            """,
            withNamespace
        );
        var result = RunGenerator(sources);

        await result.VerifyAsync(EntityFrameworkGenerator.AttributesHint)
            .UseMethodName($"Attributes_{nameof(DbContext_UsingDbSets)}_With{(withNamespace ? "" : "out")}Namespace");
        await result.VerifyAsync("BloggingContext.g.cs")
            .UseMethodName($"DbContext_{nameof(DbContext_UsingDbSets)}_With{(withNamespace ? "" : "out")}Namespace");
        await result.VerifyAsync(EntityFrameworkGenerator.RegistrationClassHint)
            .UseMethodName($"RegisterServices_{nameof(DbContext_UsingDbSets)}_With{(withNamespace ? "" : "out")}Namespace");
    }

    #endregion

    #region Using DbSets and IQueryables

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task GeneratedDbContext_UsingDbSetsAndIQueryables(bool withNamespace)
    {
        var sources = GetSources(
            """
            [GeneratedDbContext]
            public partial class BloggingContext : DbContext, IBloggingContext;

            public interface IBloggingContext
            {
                public DbSet<Blog> Blogs { get; }
                [AsNoTracking]
                public IQueryable<Blog> BlogsAsNoTracking { get; }
                public DbSet<Post> Posts { get; }
                [AsNoTracking]
                public IQueryable<Post> PostsAsNoTracking { get; }
            }

            public sealed class Blog;

            public sealed class Post;
            """,
            withNamespace
        );
        var result = RunGenerator(sources);

        await result.VerifyAsync(EntityFrameworkGenerator.AttributesHint).UseMethodName(
            $"Attributes_{nameof(GeneratedDbContext_UsingDbSetsAndIQueryables)}_With{(withNamespace ? "" : "out")}Namespace"
        );
        await result.VerifyAsync("BloggingContext.g.cs").UseMethodName(
            $"DbContext_{nameof(GeneratedDbContext_UsingDbSetsAndIQueryables)}_With{(withNamespace ? "" : "out")}Namespace"
        );
        await result.VerifyAsync(EntityFrameworkGenerator.RegistrationClassHint).UseMethodName(
            $"RegisterServices_{nameof(GeneratedDbContext_UsingDbSetsAndIQueryables)}_With{(withNamespace ? "" : "out")}Namespace"
        );
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task GeneratedDbContext_WithInterface_UsingDbSetsAndIQueryables(bool withNamespace)
    {
        var sources = GetSources(
            """
            [GeneratedDbContext<IBloggingContext>]
            public partial class BloggingContext : DbContext, IBloggingContext;

            public interface IBloggingContext
            {
                public DbSet<Blog> Blogs { get; }
                [AsNoTracking]
                public IQueryable<Blog> BlogsAsNoTracking { get; }
                public DbSet<Post> Posts { get; }
                [AsNoTracking]
                public IQueryable<Post> PostsAsNoTracking { get; }
            }

            public sealed class Blog;

            public sealed class Post;
            """,
            withNamespace
        );
        var result = RunGenerator(sources);

        await result.VerifyAsync(EntityFrameworkGenerator.AttributesHint).UseMethodName(
            $"Attributes_{nameof(GeneratedDbContext_WithInterface_UsingDbSetsAndIQueryables)}_With{(withNamespace ? "" : "out")}Namespace"
        );
        await result.VerifyAsync("BloggingContext.g.cs").UseMethodName(
            $"DbContext_{nameof(GeneratedDbContext_WithInterface_UsingDbSetsAndIQueryables)}_With{(withNamespace ? "" : "out")}Namespace"
        );
        await result.VerifyAsync(EntityFrameworkGenerator.RegistrationClassHint).UseMethodName(
            $"RegisterServices_{nameof(GeneratedDbContext_WithInterface_UsingDbSetsAndIQueryables)}_With{(withNamespace ? "" : "out")}Namespace"
        );
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task DbContext_UsingDbSetsAndIQueryables(bool withNamespace)
    {
        var sources = GetSources(
            """
            public partial class BloggingContext : DbContext, IBloggingContext;

            [DbContext<BloggingContext>]
            public interface IBloggingContext
            {
                public DbSet<Blog> Blogs { get; }
                [AsNoTracking]
                public IQueryable<Blog> BlogsAsNoTracking { get; }
                public DbSet<Post> Posts { get; }
                [AsNoTracking]
                public IQueryable<Post> PostsAsNoTracking { get; }
            }

            public sealed class Blog;

            public sealed class Post;
            """,
            withNamespace
        );
        var result = RunGenerator(sources);

        await result.VerifyAsync(EntityFrameworkGenerator.AttributesHint).UseMethodName(
            $"Attributes_{nameof(DbContext_UsingDbSetsAndIQueryables)}_With{(withNamespace ? "" : "out")}Namespace"
        );
        await result.VerifyAsync("BloggingContext.g.cs")
            .UseMethodName($"DbContext_{nameof(DbContext_UsingDbSetsAndIQueryables)}_With{(withNamespace ? "" : "out")}Namespace");
        await result.VerifyAsync(EntityFrameworkGenerator.RegistrationClassHint).UseMethodName(
            $"RegisterServices_{nameof(DbContext_UsingDbSetsAndIQueryables)}_With{(withNamespace ? "" : "out")}Namespace"
        );
    }

    #endregion

    #region Using IQueryables

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task GeneratedDbContext_UsingIQueryables(bool withNamespace)
    {
        var sources = GetSources(
            """
            [GeneratedDbContext]
            public partial class BloggingContext : DbContext, IBlogsContext, IPostsContext;

            public interface IBlogsContext
            {
                public IQueryable<Blog> Blogs { get; }
            }

            public interface IPostsContext
            {
                public IQueryable<Post> Posts { get; }
            }

            public sealed class Blog;

            public sealed class Post;
            """,
            withNamespace
        );
        var result = RunGenerator(sources);

        await result.VerifyAsync(EntityFrameworkGenerator.AttributesHint).UseMethodName(
            $"Attributes_{nameof(GeneratedDbContext_UsingIQueryables)}_With{(withNamespace ? "" : "out")}Namespace"
        );
        await result.VerifyAsync("BloggingContext.g.cs")
            .UseMethodName($"DbContext_{nameof(GeneratedDbContext_UsingIQueryables)}_With{(withNamespace ? "" : "out")}Namespace");
        await result.VerifyAsync(EntityFrameworkGenerator.RegistrationClassHint).UseMethodName(
            $"RegisterServices_{nameof(GeneratedDbContext_UsingIQueryables)}_With{(withNamespace ? "" : "out")}Namespace"
        );
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task GeneratedDbContext_WithInterface_UsingIQueryables(bool withNamespace)
    {
        var sources = GetSources(
            """
            [GeneratedDbContext<IBlogsContext>]
            [GeneratedDbContext<IPostsContext>]
            public partial class BloggingContext : DbContext;

            public interface IBlogsContext
            {
                public IQueryable<Blog> Blogs { get; }
            }

            public interface IPostsContext
            {
                public IQueryable<Post> Posts { get; }
            }

            public sealed class Blog;

            public sealed class Post;
            """,
            withNamespace
        );
        var result = RunGenerator(sources);

        await result.VerifyAsync(EntityFrameworkGenerator.AttributesHint).UseMethodName(
            $"Attributes_{nameof(GeneratedDbContext_WithInterface_UsingIQueryables)}_With{(withNamespace ? "" : "out")}Namespace"
        );
        await result.VerifyAsync("BloggingContext.g.cs").UseMethodName(
            $"DbContext_{nameof(GeneratedDbContext_WithInterface_UsingIQueryables)}_With{(withNamespace ? "" : "out")}Namespace"
        );
        await result.VerifyAsync(EntityFrameworkGenerator.RegistrationClassHint).UseMethodName(
            $"RegisterServices_{nameof(GeneratedDbContext_WithInterface_UsingIQueryables)}_With{(withNamespace ? "" : "out")}Namespace"
        );
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task DbContext_UsingIQueryables(bool withNamespace)
    {
        var sources = GetSources(
            """
            public partial class BloggingContext : DbContext;

            [DbContext<BloggingContext>]
            public interface IBlogsContext
            {
                public IQueryable<Blog> Blogs { get; }
            }

            [DbContext<BloggingContext>]
            public interface IPostsContext
            {
                public IQueryable<Post> Posts { get; }
            }

            public sealed class Blog;

            public sealed class Post;
            """,
            withNamespace
        );
        var result = RunGenerator(sources);

        await result.VerifyAsync(EntityFrameworkGenerator.AttributesHint)
            .UseMethodName($"Attributes_{nameof(DbContext_UsingIQueryables)}_With{(withNamespace ? "" : "out")}Namespace");
        await result.VerifyAsync("BloggingContext.g.cs")
            .UseMethodName($"DbContext_{nameof(DbContext_UsingIQueryables)}_With{(withNamespace ? "" : "out")}Namespace");
        await result.VerifyAsync(EntityFrameworkGenerator.RegistrationClassHint)
            .UseMethodName($"RegisterServices_{nameof(DbContext_UsingIQueryables)}_With{(withNamespace ? "" : "out")}Namespace");
    }

    #endregion

    #region Helper Methods

    private static GeneratorDriverRunResult RunGenerator(IEnumerable<string> sources)
    {
        var cSharpParseOptions = new CSharpParseOptions(LanguageVersion.CSharp11).WithPreprocessorSymbols("NET7_0_OR_GREATER");
        var cSharpCompilationOptions = new CSharpCompilationOptions(OutputKind.NetModule).WithNullableContextOptions(NullableContextOptions.Enable);
        return IncrementalGenerator.Run<EntityFrameworkGenerator>(sources, cSharpParseOptions, ReferenceAssemblies.Net80, cSharpCompilationOptions);
    }

    private static IEnumerable<string> GetSources(string source, bool withNamespace)
    {
        const string usingStatements = """
                                       using System.Linq;
                                       using Microsoft.EntityFrameworkCore;
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

    #endregion
}
