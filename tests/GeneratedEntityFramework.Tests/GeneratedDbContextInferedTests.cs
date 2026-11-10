using GeneratedEntityFramework.Tests.Common;
using SourceGeneratorTestHelpers.XUnit;

namespace GeneratedEntityFramework.Tests;

public class GeneratedDbContextInferedTests
{
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

        await result.VerifyAsync(
            EntityFrameworkGenerator.AttributesHint,
            snapshotName: $"GeneratedDbContextInferedTests.Attributes_{nameof(UsingDbSets)}_With{(withNamespace ? "" : "out")}Namespace",
            assertOnWarnings: true,
            cancellationToken: TestContext.Current.CancellationToken
        );
        await result.VerifyAsync(
            "BloggingContext.g.cs",
            snapshotName: $"GeneratedDbContextInferedTests.DbContext_{nameof(UsingDbSets)}_With{(withNamespace ? "" : "out")}Namespace",
            assertOnWarnings: true,
            cancellationToken: TestContext.Current.CancellationToken
        );
        await result.VerifyAsync(
            EntityFrameworkGenerator.RegistrationClassHint,
            snapshotName: $"GeneratedDbContextInferedTests.RegisterServices_{nameof(UsingDbSets)}_With{(withNamespace ? "" : "out")}Namespace",
            assertOnWarnings: true,
            cancellationToken: TestContext.Current.CancellationToken
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

        await result.VerifyAsync(
            EntityFrameworkGenerator.AttributesHint,
            snapshotName: $"GeneratedDbContextInferedTests.Attributes_{nameof(UsingDbSetsAndIQueryables)}_With{(withNamespace ? "" : "out")}Namespace",
            assertOnWarnings: true,
            cancellationToken: TestContext.Current.CancellationToken
        );
        await result.VerifyAsync(
            "BloggingContext.g.cs",
            snapshotName: $"GeneratedDbContextInferedTests.DbContext_{nameof(UsingDbSetsAndIQueryables)}_With{(withNamespace ? "" : "out")}Namespace",
            assertOnWarnings: true,
            cancellationToken: TestContext.Current.CancellationToken
        );
        await result.VerifyAsync(
            EntityFrameworkGenerator.RegistrationClassHint,
            snapshotName: $"GeneratedDbContextInferedTests.RegisterServices_{nameof(UsingDbSetsAndIQueryables)}_With{(withNamespace ? "" : "out")}Namespace",
            assertOnWarnings: true,
            cancellationToken: TestContext.Current.CancellationToken
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

        await result.VerifyAsync(
            EntityFrameworkGenerator.AttributesHint,
            snapshotName: $"GeneratedDbContextInferedTests.Attributes_{nameof(UsingIQueryables)}_With{(withNamespace ? "" : "out")}Namespace",
            assertOnWarnings: true,
            cancellationToken: TestContext.Current.CancellationToken
        );
        await result.VerifyAsync(
            "BloggingContext.g.cs",
            snapshotName: $"GeneratedDbContextInferedTests.DbContext_{nameof(UsingIQueryables)}_With{(withNamespace ? "" : "out")}Namespace",
            assertOnWarnings: true,
            cancellationToken: TestContext.Current.CancellationToken
        );
        await result.VerifyAsync(
            EntityFrameworkGenerator.RegistrationClassHint,
            snapshotName: $"GeneratedDbContextInferedTests.RegisterServices_{nameof(UsingIQueryables)}_With{(withNamespace ? "" : "out")}Namespace",
            assertOnWarnings: true,
            cancellationToken: TestContext.Current.CancellationToken
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

        await result.VerifyAsync(
            EntityFrameworkGenerator.AttributesHint,
            snapshotName: $"GeneratedDbContextInferedTests.Attributes_{nameof(UsingInterfaceAttributes)}_With{(withNamespace ? "" : "out")}Namespace",
            assertOnWarnings: true,
            cancellationToken: TestContext.Current.CancellationToken
        );
        await result.VerifyAsync(
            "BloggingContext.g.cs",
            snapshotName: $"GeneratedDbContextInferedTests.DbContext_{nameof(UsingInterfaceAttributes)}_With{(withNamespace ? "" : "out")}Namespace",
            assertOnWarnings: true,
            cancellationToken: TestContext.Current.CancellationToken
        );
        await result.VerifyAsync(
            EntityFrameworkGenerator.RegistrationClassHint,
            snapshotName: $"GeneratedDbContextInferedTests.RegisterServices_{nameof(UsingInterfaceAttributes)}_With{(withNamespace ? "" : "out")}Namespace",
            assertOnWarnings: true,
            cancellationToken: TestContext.Current.CancellationToken
        );
    }
}
