using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GeneratedEntityFramework.Tests.Lab;

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
