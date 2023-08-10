namespace BoostProject.Data.Entities.Base;

public interface IBaseEntity
{
    public Guid Id { get; set; }

    public DateTime CreationDateTime { get; init; }

    public DateTime ModificationDateTime { get; set; }

    void Touch();
}