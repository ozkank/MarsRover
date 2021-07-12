using MarsRover.Domain.Objects.Entities;
using MarsRover.Domain.Service;
using MarsRover.Domain.Service.Interfaces;
using System;
using System.Collections.Generic;
using Xunit;

namespace MarsRover.Tests
{
    public class MovementStarterServiceTest
    {
        private readonly IMovementStarterService movementStarterService;
        private static int xSize = 5;
        private static int ySize = 5;

        public MovementStarterServiceTest()
        {

            List<IMoveStrategy> moveStrategies = new List<IMoveStrategy>();
            var leftMoveStrategy = new LeftMoveStrategy();
            var rightMoveStrategy = new RightMoveStrategy();
            var forwardMoveStrategy = new ForwardMoveStrategy(xSize, ySize);
            moveStrategies.Add(leftMoveStrategy);
            moveStrategies.Add(rightMoveStrategy);
            moveStrategies.Add(forwardMoveStrategy);

            movementStarterService = new MovementStarterService(moveStrategies);
        }


        [Fact]
        public void RunMovementStarterServiceScenario1()
        {
            //Arrange
            string movementCommands = "LMLMLMLMM";
            var currentCoordinate = new Coordinate { X = 1, Y = 2, Direction = Directions.N };

            //Act
            var result = movementStarterService.Run(movementCommands, currentCoordinate).GetAwaiter().GetResult();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.X);
            Assert.Equal(3, result.Y);
            Assert.Equal(Directions.N, result.Direction);
        }


        [Fact]
        public void ShouldThrowExceptionWhenReachedYAxisDimension()
        {
            //Arrange
            string movementCommands = "LMLMLMLMMMMMMMMM";
            var currentCoordinate = new Coordinate { X = 1, Y = 2, Direction = Directions.N };
            var expectedDimension = new Exception("Can't move. Already reached max Y axis size");

            //Act
            var apiException = Assert.ThrowsAsync<Exception>(() => movementStarterService.Run(movementCommands, currentCoordinate));

            //Assert
            Assert.Equal(expectedDimension.Message, apiException.GetAwaiter().GetResult().Message);
        }
    }
}
