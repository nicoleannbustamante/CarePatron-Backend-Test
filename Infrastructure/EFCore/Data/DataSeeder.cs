using Domain.Aggregates.Clients;

namespace Infrastructure.EFCore.Data
{
    public class DataSeeder
    {
        private readonly DataContext dataContext;

        public DataSeeder(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void Seed()
        {
            var client = new Client( "John", "Smith", "john@gmail.com", "+18202820232");

            dataContext.Add(client);
            dataContext.SaveChanges();
        }
    }
}

