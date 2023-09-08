using Infrastructure.Repositories.Interfaces;
using Infrastructure.Utilities;
using Microsoft.Extensions.Logging;
using System;

namespace Infrastructure.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly ILogger<DocumentRepository> _logger;
        public DocumentRepository(ILogger<DocumentRepository> logger)
        {
            _logger = logger;
        }

        public async Task SyncDocumentsFromExternalSource(string _)
        {
            try
            {
                // simulates random errors that occur with external services
                // leave this to emulate real life
                ChaosUtility.RollTheDice();

                // this simulates sending an email
                // leave this delay as 10s to emulate real life
                await Task.Delay(10000);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error syncing documents: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}

