using Application.Services.Interfaces;
using Core.Exceptions;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly ILogger<DocumentService> _logger;
        public DocumentService(IDocumentRepository documentRepository, ILogger<DocumentService> logger)
        {
            _documentRepository = documentRepository;
            _logger = logger;
        }

        public async Task SyncDocumentsFromExternalSource(string email)
        {
            try
            {
                await _documentRepository.SyncDocumentsFromExternalSource(email);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error in syncing the document: " + ex.Message);
                throw new DocumentException("Error in syncing the document: ", ex);
            }
        }
    }
}
