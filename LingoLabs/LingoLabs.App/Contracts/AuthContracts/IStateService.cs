namespace LingoLabs.App.Contracts.AuthContracts
{
    public interface IStateService
    {
        Task<Guid> GetId();
        Task RemoveId();
        Task SetId(Guid token);
    }
}
