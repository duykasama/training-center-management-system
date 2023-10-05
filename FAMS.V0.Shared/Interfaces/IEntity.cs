using System.ComponentModel.DataAnnotations.Schema;

namespace FAMS.V0.Shared.Interfaces;

public interface IEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
}