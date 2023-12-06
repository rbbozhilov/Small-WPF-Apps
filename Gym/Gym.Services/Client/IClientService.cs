using Gym.Services.DTO;
using System;
using System.Threading.Tasks;

namespace Gym.Services.Client
{
    public interface IClientService
    {

        Task AddClient(string firstName, string lastName, DateTime startDate, DateTime endDate);

        Task<bool> EditClient(int number, string firstName, string lastName, DateTime startDate, DateTime endDate);

        Task<bool> EditClient(int number, DateTime startDate, DateTime endDate);

        SearchedClient SearchClientByNumber(int number);

        SearchedClient SearchClientByName(string firstName,string lastName);

    }
}
