using Gym.Data;
using Gym.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Services.Client
{
    public class ClientService : IClientService
    {

        private GymDbContext db = new GymDbContext();


        public async Task AddClient(string firstName, string lastName, DateTime startDate, DateTime endDate)
        {
            var client = new Gym.Data.Models.Client()
            {
                FirstName = firstName,
                LastName = lastName,
                StartDate = startDate,
                EndDate = endDate,
                Number = this.GenerateNumber()
            };

            var allNumbers = this.db.Clients.Select(x => x.Number).ToList();

            while (allNumbers.Any(x => x == client.Number))
            {
                client.Number = this.GenerateNumber();
            }

            this.db.Clients.Add(client);

            await this.db.SaveChangesAsync();

        }

        public async Task<bool> EditClient(int number, string firstName, string lastName, DateTime startDate, DateTime endDate)
        {
            var client = this.db.Clients.FirstOrDefault(x => x.Number == number);

            if (client == null)
            {
                return false;
            }

            client.FirstName = firstName;
            client.LastName = lastName;
            client.StartDate = startDate;
            client.EndDate = endDate;

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditClient(int number, DateTime startDate, DateTime endDate)
        {
            var client = this.db.Clients.FirstOrDefault(x => x.Number == number);

            if (client == null)
            {
                return false;
            }

            client.StartDate = startDate;
            client.EndDate = endDate;

            await this.db.SaveChangesAsync();

            return true;
        }

        public SearchedClient SearchClientByName(string firstName, string lastName)
        => this.db.Clients
            .Where(client => client.FirstName.ToLower() == firstName.ToLower() && client.LastName.ToLower() == lastName.ToLower())
            .Select(client => new SearchedClient()
            {
                FullName = client.FirstName + " " + client.LastName,
                Number = client.Number,
                StartDate = client.StartDate,
                EndDate = client.EndDate
            })
            .FirstOrDefault();



        public SearchedClient SearchClientByNumber(int number)
        => this.db.Clients
            .Where(client => client.Number == number)
            .Select(client => new SearchedClient()
            {
                FullName = client.FirstName + " " + client.LastName,
                Number = client.Number,
                StartDate = client.StartDate,
                EndDate= client.EndDate
            })
            .FirstOrDefault();

        private int GenerateNumber()
        => new Random().Next(0, 10000);
    }
}
