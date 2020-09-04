using Microsoft.Extensions.Logging;

namespace Backend.Infrastructure.Persistence
{
    public static class Logging
    {
        public static void IndexCreatorCreateIndexFor(this ILogger logger, string fieldName, string entityFullName, string collectionName, string databaseName)
        {
            logger.LogInformation(new EventId(1, typeof(Logging).Namespace), "Create new index. FieldName: {fieldName}, EntityFullName: {entityFullName}, CollectionName: {collectionName}, DatabaseName: {databaseName}", fieldName, entityFullName, collectionName, databaseName);
        }
    }
}