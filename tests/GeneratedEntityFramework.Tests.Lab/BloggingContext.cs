using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GeneratedEntityFramework.Tests.Lab;

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
