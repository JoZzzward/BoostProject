using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BoostProject.Data.Entities.Base;

[Index("Id", IsUnique = true)]
public class BaseEntity : IBaseEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreationDateTime { get; init; } = DateTime.Now;
    public DateTime ModificationDateTime { get; set; } = DateTime.Now;

    public void Touch() => ModificationDateTime = DateTime.Now;
}