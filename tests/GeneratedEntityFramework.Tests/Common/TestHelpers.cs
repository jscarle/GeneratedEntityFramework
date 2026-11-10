using System.Collections.Immutable;
using Basic.Reference.Assemblies;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using SourceGeneratorTestHelpers;

namespace GeneratedEntityFramework.Tests.Common;

public static class TestHelpers
{
    public static (ImmutableArray<Diagnostic> CompilationDiagnostics, GeneratorDriverRunResult Result) RunGenerator(IEnumerable<string> sources)
    {
        var cSharpParseOptions = new CSharpParseOptions(LanguageVersion.CSharp13).WithPreprocessorSymbols("NET7_0_OR_GREATER", "NET11_0_OR_GREATER");
        var cSharpCompilationOptions = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary).WithNullableContextOptions(NullableContextOptions.Enable);
        var (_, result, diagnostics) = IncrementalGenerator.RunWithDiagnostics<EntityFrameworkGenerator>(
            sources, cSharpParseOptions, Net110.References.All, cSharpCompilationOptions);

        return (diagnostics, result);
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
                         public static class EntityFrameworkQueryableExtensions
                         {
                             public static IQueryable<TEntity> AsNoTracking<TEntity>(this IQueryable<TEntity> source)
                                 where TEntity : class
                             {
                                 return source;
                             }
                         }

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

                             public Type ElementType { get; } = default!;
                             public Expression Expression { get; } = default!;
                             public IQueryProvider Provider { get; } = default!;
                         }
                     }

                     """;
    }
}
