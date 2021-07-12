using MarsRover.Domain.Objects.Entities;
using MarsRover.Domain.Service.Interfaces;
using System;
using System.Threading.Tasks;

namespace MarsRover.Domain.Service
{
    public class ForwardMoveStrategy : IMoveStrategy
    {
        private readonly int XAxisSize;
        private readonly int YAxisSize;

        public ForwardMoveStrategy(int XSize, int YSize)
        {
            this.XAxisSize = XSize;
            this.YAxisSize = YSize;
        }

        public async Task<MovementType> GetMovementTypeAsync()
        {
            return MovementType.M;
        }

        public async Task<Coordinate> MoveAsync(Coordinate coordinate)
        {
            switch (coordinate.Direction)
            {
                case Directions.N:
                    if (coordinate.Y >= YAxisSize)
                        throw new Exception("Can't move. Already reached max Y axis size");
                    else
                        coordinate.Y += 1;
                    break;

                case Directions.E:
                    if (coordinate.X >= XAxisSize)
                        throw new Exception("Can't move. Already reached max X axis size");
                    else
                        coordinate.X += 1;
                    break;

                case Directions.S:
                    if (coordinate.Y != 0)
                        coordinate.Y -= 1;
                    else
                        throw new Exception("Can't move. Already reached Y zero coordinate");
                    break;

                case Directions.W:
                    if (coordinate.X != 0)
                        coordinate.X -= 1;
                    else
                        throw new Exception("Can't move. Already reached X zero coordinate");
                    break;
            }
            return coordinate;
        }
    }
}
