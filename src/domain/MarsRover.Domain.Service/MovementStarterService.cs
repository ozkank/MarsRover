using MarsRover.Domain.Objects.Entities;
using MarsRover.Domain.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarsRover.Domain.Service
{
    public class MovementStarterService : IMovementStarterService
    {
        private readonly IEnumerable<IMoveStrategy> moveStrategies;

        public MovementStarterService(IEnumerable<IMoveStrategy> moveStrategies)
        {
            this.moveStrategies = moveStrategies;
        }

        public async Task<Coordinate> Run(string movementCommands, Coordinate coordinate)
        {
            if (string.IsNullOrEmpty(movementCommands))
            {
                throw new NullReferenceException(movementCommands);
            }

            foreach (var command in movementCommands.ToCharArray())
            {
                foreach (var strategy in moveStrategies)
                {
                    var movementType = await strategy.GetMovementTypeAsync();

                    if (movementType.ToString() == command.ToString())
                    {
                        var result =  await strategy.MoveAsync(coordinate);
                        coordinate.X = result.X;
                        coordinate.Y = result.Y;
                        coordinate.Direction = result.Direction;

                        break;
                    }
                }
            }

            return coordinate;
        }
    }
}
