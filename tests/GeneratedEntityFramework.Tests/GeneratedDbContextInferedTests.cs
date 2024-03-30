using GeneratedEntityFramework.Tests.Common;
using SourceGeneratorTestHelpers.XUnit;

namespace GeneratedEntityFramework.Tests;

[UsesVerify]
public class GeneratedDbContextInferedTests
{
    public GeneratedDbContextInferedTests()
    {
        ModuleInitializer.Initialize();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task UsingDbSets(bool withNamespace)
    {
        var sources = TestHelpers.GetSources(
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
        var result = TestHelpers.RunGenerator(sources);

        await result.VerifyAsync(EntityFrameworkGenerator.AttributesHint)
            .UseMethodName($"Attributes_{nameof(UsingDbSets)}_With{(withNamespace ? "" : "out")}Namespace");
        await result.VerifyAsync("BloggingContext.g.cs").UseMethodName($"DbContext_{nameof(UsingDbSets)}_With{(withNamespace ? "" : "out")}Namespace");
        await result.VerifyAsync(EntityFrameworkGenerator.RegistrationClassHint).UseMethodName(
            $"RegisterServices_{nameof(UsingDbSets)}_With{(withNamespace ? "" : "out")}Namespace"
        );
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task UsingDbSetsAndIQueryables(bool withNamespace)
    {
        var sources = TestHelpers.GetSources(
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
        var result = TestHelpers.RunGenerator(sources);

        await result.VerifyAsync(EntityFrameworkGenerator.AttributesHint).UseMethodName(
            $"Attributes_{nameof(UsingDbSetsAndIQueryables)}_With{(withNamespace ? "" : "out")}Namespace"
        );
        await result.VerifyAsync("BloggingContext.g.cs").UseMethodName(
            $"DbContext_{nameof(UsingDbSetsAndIQueryables)}_With{(withNamespace ? "" : "out")}Namespace"
        );
        await result.VerifyAsync(EntityFrameworkGenerator.RegistrationClassHint).UseMethodName(
            $"RegisterServices_{nameof(UsingDbSetsAndIQueryables)}_With{(withNamespace ? "" : "out")}Namespace"
        );
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task UsingIQueryables(bool withNamespace)
    {
        var sources = TestHelpers.GetSources(
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
        var result = TestHelpers.RunGenerator(sources);

        await result.VerifyAsync(EntityFrameworkGenerator.AttributesHint).UseMethodName(
            $"Attributes_{nameof(UsingIQueryables)}_With{(withNamespace ? "" : "out")}Namespace"
        );
        await result.VerifyAsync("BloggingContext.g.cs").UseMethodName($"DbContext_{nameof(UsingIQueryables)}_With{(withNamespace ? "" : "out")}Namespace");
        await result.VerifyAsync(EntityFrameworkGenerator.RegistrationClassHint).UseMethodName(
            $"RegisterServices_{nameof(UsingIQueryables)}_With{(withNamespace ? "" : "out")}Namespace"
        );
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task UsingInterfaceAttributes(bool withNamespace)
    {
        var sources = TestHelpers.GetSources(
            """
            [GeneratedDbContext]
            public partial class BloggingContext : DbContext, IBloggingContext;

            [AsNoTracking]
            public interface IBloggingContext
            {
                public DbSet<Blog> Blogs { get; }
                public IQueryable<Blog> BlogsAsNoTracking { get; }
                public DbSet<Post> Posts { get; }
                public IQueryable<Post> PostsAsNoTracking { get; }
            }

            public sealed class Blog;

            public sealed class Post;
            """,
            withNamespace
        );
        var result = TestHelpers.RunGenerator(sources);

        await result.VerifyAsync(EntityFrameworkGenerator.AttributesHint).UseMethodName(
            $"Attributes_{nameof(UsingInterfaceAttributes)}_With{(withNamespace ? "" : "out")}Namespace"
        );
        await result.VerifyAsync("BloggingContext.g.cs").UseMethodName(
            $"DbContext_{nameof(UsingInterfaceAttributes)}_With{(withNamespace ? "" : "out")}Namespace"
        );
        await result.VerifyAsync(EntityFrameworkGenerator.RegistrationClassHint).UseMethodName(
            $"RegisterServices_{nameof(UsingInterfaceAttributes)}_With{(withNamespace ? "" : "out")}Namespace"
        );
    }
}
