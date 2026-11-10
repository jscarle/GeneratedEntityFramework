using GeneratedEntityFramework.Tests.Common;
using SourceGeneratorTestHelpers.XUnit;

namespace GeneratedEntityFramework.Tests;

public class ServiceLifetimeTests
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task Singleton(bool withNamespace)
    {
        var sources = TestHelpers.GetSources(
            """
            [GeneratedDbContext]
            [DbContextInterfaceLifetime(ServiceLifetime.Singleton)]
            public partial class BloggingContext : DbContext, IBloggingContext;

            [AsNoTracking]
            public interface IBloggingContext
            {
                public IQueryable<Blog> BlogsAsNoTracking { get; }
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
            snapshotName: $"ServiceLifetimeTests.Attributes_{nameof(Singleton)}_With{(withNamespace ? "" : "out")}Namespace",
            assertOnWarnings: true,
            cancellationToken: TestContext.Current.CancellationToken
        );
        await result.VerifyAsync(
            "BloggingContext.g.cs",
            snapshotName: $"ServiceLifetimeTests.DbContext_{nameof(Singleton)}_With{(withNamespace ? "" : "out")}Namespace",
            assertOnWarnings: true,
            cancellationToken: TestContext.Current.CancellationToken
        );
        await result.VerifyAsync(
            EntityFrameworkGenerator.RegistrationClassHint,
            snapshotName: $"ServiceLifetimeTests.RegisterServices_{nameof(Singleton)}_With{(withNamespace ? "" : "out")}Namespace",
            assertOnWarnings: true,
            cancellationToken: TestContext.Current.CancellationToken
        );
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task Scoped(bool withNamespace)
    {
        var sources = TestHelpers.GetSources(
            """
            [GeneratedDbContext]
            [DbContextInterfaceLifetime(ServiceLifetime.Scoped)]
            public partial class BloggingContext : DbContext, IBloggingContext;

            [AsNoTracking]
            public interface IBloggingContext
            {
                public IQueryable<Blog> BlogsAsNoTracking { get; }
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
            snapshotName: $"ServiceLifetimeTests.Attributes_{nameof(Scoped)}_With{(withNamespace ? "" : "out")}Namespace",
            assertOnWarnings: true,
            cancellationToken: TestContext.Current.CancellationToken
        );
        await result.VerifyAsync(
            "BloggingContext.g.cs",
            snapshotName: $"ServiceLifetimeTests.DbContext_{nameof(Scoped)}_With{(withNamespace ? "" : "out")}Namespace",
            assertOnWarnings: true,
            cancellationToken: TestContext.Current.CancellationToken
        );
        await result.VerifyAsync(
            EntityFrameworkGenerator.RegistrationClassHint,
            snapshotName: $"ServiceLifetimeTests.RegisterServices_{nameof(Scoped)}_With{(withNamespace ? "" : "out")}Namespace",
            assertOnWarnings: true,
            cancellationToken: TestContext.Current.CancellationToken
        );
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task Transient(bool withNamespace)
    {
        var sources = TestHelpers.GetSources(
            """
            [GeneratedDbContext]
            [DbContextInterfaceLifetime(ServiceLifetime.Transient)]
            public partial class BloggingContext : DbContext, IBloggingContext;

            [AsNoTracking]
            public interface IBloggingContext
            {
                public IQueryable<Blog> BlogsAsNoTracking { get; }
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
            snapshotName: $"ServiceLifetimeTests.Attributes_{nameof(Transient)}_With{(withNamespace ? "" : "out")}Namespace",
            assertOnWarnings: true,
            cancellationToken: TestContext.Current.CancellationToken
        );
        await result.VerifyAsync(
            "BloggingContext.g.cs",
            snapshotName: $"ServiceLifetimeTests.DbContext_{nameof(Transient)}_With{(withNamespace ? "" : "out")}Namespace",
            assertOnWarnings: true,
            cancellationToken: TestContext.Current.CancellationToken
        );
        await result.VerifyAsync(
            EntityFrameworkGenerator.RegistrationClassHint,
            snapshotName: $"ServiceLifetimeTests.RegisterServices_{nameof(Transient)}_With{(withNamespace ? "" : "out")}Namespace",
            assertOnWarnings: true,
            cancellationToken: TestContext.Current.CancellationToken
        );
    }
}
