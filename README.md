[![Banner](https://raw.githubusercontent.com/jscarle/GeneratedEntityFramework/main/Banner.png)](https://github.com/jscarle/GeneratedEntityFramework)

# GeneratedEntityFramework - Generated interface implementations for Entity Framework Core

GeneratedEntityFramework is a.NET source generator that automatically generates the `DbSet` implementations for a
`DbContext` based on the properties of an interface. This greatly facilitates integration of Entity Framework within a
Clean Architecture (CA) or a Vertical Slice Architecture (VSA) by segmenting one or more DbContexts into one or more
interfaces.

[![main](https://img.shields.io/github/actions/workflow/status/jscarle/GeneratedEntityFramework/main.yml?logo=github)](https://github.com/jscarle/GeneratedEntityFramework)
[![nuget](https://img.shields.io/nuget/v/GeneratedEntityFramework)](https://www.nuget.org/packages/GeneratedEntityFramework)
[![downloads](https://img.shields.io/nuget/dt/GeneratedEntityFramework)](https://www.nuget.org/packages/GeneratedEntityFramework)

## Requirements

This source generator will generate source that is only compatible with
[EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore) and Microsoft's
[Dependency Injection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection) abstraction.

## Getting Started

### The Basics

Usually, a [DbContext](https://learn.microsoft.com/en-us/ef/core/#the-model) will contain or more DbSets:

```csharp
public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
}
```

In a typical Clean Architecture project, those DbSets are often presented to the
application [as an interface](https://github.com/jasontaylordev/CleanArchitecture/blob/cea275b3c5716fd48e1aaeda231f041f837e9be2/src/Application/Common/Interfaces/IApplicationDbContext.cs):

```csharp
public class BloggingContext : DbContext, IBloggingContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
}

public interface IBloggingContext
{
    public DbSet<Blog> Blogs { get; }
    public DbSet<Post> Posts { get; }
}
```

Keeping both the DbContext and the interface up to date with each other can quickly become tedious as each
DbSet added to the interface must also be duplicated by hand in the DbContext and vice versa.
[GeneratedEntityFramework](https://github.com/jscarle/GeneratedEntityFramework)'s source generator solves this
by source generating a `partial` implementation of the DbContext based on it's association with one or more
interfaces.

### How to use the source generator

The are three different ways for the source generator to detect which implementations to generate for the DbContext.

#### Inheriting from the interface

If the DbContext is marked with the `GeneratedDbContext` attribute, it will generate the implementations for all
of the inherited interfaces.

```csharp
[GeneratedDbContext]
public partial class BloggingContext : DbContext, IBloggingContext
{
}

public interface IBloggingContext
{
    public DbSet<Blog> Blogs { get; }
    public DbSet<Post> Posts { get; }
}
```

#### Specifying the interface

If the DbContext is marked with the `GeneratedDbContext` attribute and the interface is specified as the constructor
parameter, it will generate the implementations for the specified interface and automatically add the interface as
an inherited interface to the partial DbContext.

```csharp
[GeneratedDbContext<IBloggingContext>]
public partial class BloggingContext : DbContext
{
}

public interface IBloggingContext
{
    public DbSet<Blog> Blogs { get; }
    public DbSet<Post> Posts { get; }
}
```

#### Specifying the DbContext

If the interface is marked with the `DbContext` attribute and the DbContext is specified as the constructor
parameter, it will generate the implementations for the specified interface and automatically add the interface as
an inherited interface to the partial DbContext.

```csharp
public partial class BloggingContext : DbContext
{
}

[DbContext<BloggingContext>]
public interface IBloggingContext
{
    public DbSet<Blog> Blogs { get; }
    public DbSet<Post> Posts { get; }
}
```

### Notes about the attributes

Generic attributes are available from .NET 7.0 onwards, in order to use the `GeneratedDbContext` and `DbContext`
attributes with .NET 6.0 and prior, simply use the constructor with a `typeof` value of the interface or DbContext
as follows:

```csharp
[GeneratedDbContext(typeof(IBloggingContext))]
public partial class BloggingContext : DbContext { }

[DbContext(typeof(BloggingContext))]
public interface IBloggingContext { }
```

The `GeneratedDbContext` and `DbContext` attributes can be used multiple times in any combinations, the source generator
will automatically eliminate any duplication and redundancies. This is incredibly useful for Vertical Sliced Architecture
(VSA) as this allows different slices to define segmented interfaces while merging of all the implementations over a single
DbContext. For example, the following will produce a single implementation of each of the DbSets on the DbContext:

```csharp
[GeneratedDbContext<IBloggingContext>]
[GeneratedDbContext<IBlogsContext>]
[GeneratedDbContext<IPostsContext>]
public partial class BloggingContext : DbContext
{
}

[DbContext<BloggingContext>]
public interface IBloggingContext
{
    public DbSet<Blog> Blogs { get; }
    public DbSet<Post> Posts { get; }
}


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
```

### IQueryable properties

One of the issues with specifying `DbSet` in the interface is that it creates high coupling with Entity Framework and
negates the benefits of using an abstraction to access the database. As an alternative, if the interface specifies an
`IQueryable` property instead of a `DbSet` one, then a private DbSet property will be generated as a backing field
within the DbContext. Thus, the following:

```csharp
[GeneratedDbContext<IBloggingContext>]
public partial class BloggingContext : DbContext
{
}

public interface IBloggingContext
{
    public IQueryable<Blog> Blogs { get; }
    public IQueryable<Post> Posts { get; }
}
```

Would generate the following partial DbContext:

```csharp
public partial class BloggingContext : IBloggingContext
{
    private DbSet<Blog> DbSet__Blogs { get; set; } = default!;
    public IQueryable<Blog> Blogs => DbSet__Blogs;

    private DbSet<Post> DbSet__Posts { get; set; } = default!;
    public IQueryable<Post> Posts => DbSet__Posts;
}
```

If a `DbSet` exists on the interface for the same entity type as the `IQueryable`, that will be used instead of
creating a backing field. This allows both DbSet and IQueryable properties to be combined as needed like so:

```csharp
[GeneratedDbContext<IBloggingContext>]
public partial class BloggingContext : DbContext
{
}

public interface IBloggingContext
{
    public DbSet<Blog> BlogsDbSet { get; }
    public IQueryable<Blog> Blogs { get; }
}
```

Would instead generate the following partial DbContext:

```csharp
public partial class BloggingContext : IBloggingContext
{
    public DbSet<Blog> BlogsDbSet { get; set; } = default!;
    public IQueryable<Blog> Blogs => BlogsDbSet;
}
```

### AsNoTracking on IQueryable properties

The `AsNoTracking` attribute can be added to `IQueryable` properties and `.AsNoTracking()` will added to the implementation.

```csharp
[GeneratedDbContext<IBloggingContext>]
public partial class BloggingContext : DbContext
{
}

public interface IBloggingContext
{
    public DbSet<Blog> BlogsDbSet { get; }

    [AsNoTracking]
    public IQueryable<Blog> Blogs { get; }
}
```

Will generate the following partial DbContext:

```csharp
public partial class BloggingContext : IBloggingContext
{
    public DbSet<Blog> BlogsDbSet { get; set; } = default!;
    public IQueryable<Blog> Blogs => BlogsDbSet.AsNoTracking();
}
```

### Registering the interfaces

In the `GeneratedEntityFramework` namespace, an extension method named `AddDbContextInterfaces` is added which can be used
with the `IServiceCollection` to register all of the interfaces against their respective DbContexts.

```csharp
public static void AddDbContextInterfaces(this IServiceCollection services)
{
    services.AddScoped<IBlogsContext>(sp => sp.GetRequiredService<BloggingContext>());
    services.AddScoped<IPostsContext>(sp => sp.GetRequiredService<BloggingContext>());
}
```

With a host builder, simply call the extension method on the service collection.

```csharp
builder.Services.AddDbContextInterfaces();
```

By default, the service lifetime is the same as the Entity Framework default of Scoped. To specify a different service lifetime,
add the `DbContextInterfaceLifetime` attribute to the DbContext and any associated interfaces will be registered with the
specified lifetime.

```csharp
[GeneratedDbContext<IBloggingContext>]
[DbContextInterfaceLifetime(ServiceLifetime.Transient)]
public partial class BloggingContext : DbContext
{
}
```
