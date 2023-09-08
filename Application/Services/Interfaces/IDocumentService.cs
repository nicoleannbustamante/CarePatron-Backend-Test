namespace Application.Services.Interfaces
{
    public interface IDocumentService
    {
        Task SyncDocumentsFromExternalSource(string email);
    }
}