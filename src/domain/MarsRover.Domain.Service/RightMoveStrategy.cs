using MarsRover.Domain.Objects.Entities;
using MarsRover.Domain.Service.Interfaces;
using System.Threading.Tasks;

namespace MarsRover.Domain.Service
{
    public class RightMoveStrategy : IMoveStrategy
    {
        public async Task<MovementType> GetMovementTypeAsync()
        {
            return MovementType.R;
        }

        public async Task<Coordinate> MoveAsync(Coordinate coordinate)
        {
            switch (coordinate.Direction)
            {
                case Directions.E:
                    coordinate.Direction = Directions.S;
                    break;
                case Directions.W:
                    coordinate.Direction = Directions.N;
                    break;
                case Directions.N:
                    coordinate.Direction = Directions.E;
                    break;

                case Directions.S:
                    coordinate.Direction = Directions.W;
                    break;
            }
            return coordinate;
        }
    }
}
