namespace Infrastructure.Repositories.Interfaces
{
    public interface IDocumentRepository
    {
        Task SyncDocumentsFromExternalSource(string email);
    }
}
