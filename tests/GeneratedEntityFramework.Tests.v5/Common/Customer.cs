using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneratedEntityFramework.Tests.v5.Common;

public sealed class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [MaxLength(255)]
    public string Name { get; set; } = "";
}
