using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using xxAMIDOxx.xxSTACKSxx.Application.Integration;
using xxAMIDOxx.xxSTACKSxx.Domain;

namespace xxAMIDOxx.xxSTACKSxx.Infrastructure.Repositories
{
    public class DynamoDbMenuRepository : IMenuRepository
    {
        private readonly DynamoDBContext context;

        public DynamoDbMenuRepository(IAmazonDynamoDB dynamoDbClient)
        {
            context = new DynamoDBContext(dynamoDbClient);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Menu> GetByIdAsync(Guid id)
        {
            return await context.LoadAsync<Menu>(id.ToString());
        }

        public async Task<bool> SaveAsync(Menu entity)
        {
            try
            {
                await context.SaveAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
