using MarsRover.Domain.Objects.Entities;
using MarsRover.Domain.Service.Interfaces;
using System.Threading.Tasks;

namespace MarsRover.Domain.Service
{
    public class LeftMoveStrategy : IMoveStrategy
    {
        public async Task<MovementType> GetMovementTypeAsync()
        {
            return MovementType.L;
        }

        public async Task<Coordinate> MoveAsync(Coordinate coordinate)
        {
            switch (coordinate.Direction)
            {
                case Directions.E:
                    coordinate.Direction = Directions.N;
                    break;
                case Directions.W:
                    coordinate.Direction = Directions.S;
                    break;
                case Directions.N:
                    coordinate.Direction = Directions.W;
                    break;
                case Directions.S:
                    coordinate.Direction = Directions.E;
                    break;
            }
            return coordinate;
        }
    }
}
