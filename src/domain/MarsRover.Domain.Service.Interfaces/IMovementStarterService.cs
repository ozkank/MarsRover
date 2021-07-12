using MarsRover.Domain.Objects.Entities;
using System.Threading.Tasks;

namespace MarsRover.Domain.Service.Interfaces
{
    public interface IMovementStarterService
    {
        Task<Coordinate> Run(string movementCommands, Coordinate coordinate);
    }
}
