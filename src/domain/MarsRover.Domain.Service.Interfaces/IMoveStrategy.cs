using MarsRover.Domain.Objects.Entities;
using System.Threading.Tasks;

namespace MarsRover.Domain.Service.Interfaces
{
    public interface IMoveStrategy
    {
        Task<MovementType> GetMovementTypeAsync();
        Task<Coordinate> MoveAsync(Coordinate coordinate);
    }
}
