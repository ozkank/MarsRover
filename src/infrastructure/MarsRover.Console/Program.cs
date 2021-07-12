using MarsRover.Domain.Objects.Entities;
using MarsRover.Domain.Service;
using MarsRover.Domain.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MarsRover.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.Clear();

            System.Console.WriteLine("Please enter X axis dimension");
            var xSize = Convert.ToInt32(System.Console.ReadLine());
            System.Console.WriteLine("Please enter Y axis dimension");
            var ySize = Convert.ToInt32(System.Console.ReadLine());
            System.Console.WriteLine("Please enter current ranger coordinates (no spaces)");
            var currentCoordinate = System.Console.ReadLine().ToUpper();
            var currentCoordinateAsSplitted = currentCoordinate.ToCharArray();
            Enum.TryParse(currentCoordinateAsSplitted[2].ToString(), out Directions direction);

            var coordinate = new Coordinate
            {
                X = Convert.ToInt32(currentCoordinateAsSplitted[0].ToString()),
                Y = Convert.ToInt32(currentCoordinateAsSplitted[1].ToString()),
                Direction = direction
            };

            System.Console.WriteLine("Please enter full commands without spaces");
            var movementCommand = System.Console.ReadLine().ToUpper();

            var serviceProvider = RegisterServices(xSize, ySize);
            var movementStarterService = serviceProvider.GetService<IMovementStarterService>();

            try
            {
                var newCoordinate = movementStarterService.Run(movementCommand, coordinate).GetAwaiter().GetResult();
                System.Console.WriteLine($"New coordinate : X:{newCoordinate.X} Y:{newCoordinate.Y} Direction:{newCoordinate.Direction}");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            DisposeServices(serviceProvider);
        }

        private static ServiceProvider RegisterServices(int xSize, int ySize)
        {
            var services = new ServiceCollection();
            services.AddSingleton<IMoveStrategy, ForwardMoveStrategy>(x => new ForwardMoveStrategy(xSize, ySize));
            services.AddSingleton<IMoveStrategy, LeftMoveStrategy>();
            services.AddSingleton<IMoveStrategy, RightMoveStrategy>();
            services.AddSingleton<IMovementStarterService, MovementStarterService>();
            return services.BuildServiceProvider(true);
        }

        private static void DisposeServices(ServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                return;
            }
            if (serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
