/// Інтерфейси Транспортний Засіб, 
/// Абстрактні класи Автомобіль, Двоколісний Транспорт
/// СабКласи Велосипед, Мопед, Мотоцикл, Потяг, Cargo автомобіль, Passenger автомобіль,
/// Також 2 класи(+1 mother-class) вагонів для потягу
/// Рахувати час/вартість перевезення людей/речей


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Lab2_5
{
    interface ITransport
    {
        double CalculateCost(double distance);
        double CalculateTime(double distance);
    }

    abstract class TwoWheels : ITransport
    {
        public double Consumption { get; protected set; }
        public int MaxSpeed { get; protected set; }
        public int Seats { get; protected set; }

        public abstract double CalculateCost(double distance);
        public abstract double CalculateTime(double distance);

        public virtual void Info()
        {
            Console.WriteLine($"Fuel Consumption: {Consumption} L/100km, " +
                $"\nMax Speed: {MaxSpeed} km/h " +
                $"\nSeats: {Seats}");
        }
    }

    abstract class Car : ITransport
    {
        public string Model { get; protected set; }
        public double Consumption { get; protected set; }
        public decimal Price { get; protected set; }
        public int MaxSpeed { get; protected set; }

        public abstract double CalculateCost(double distance);
        public abstract double CalculateTime(double distance);

        public virtual void Info()
        {
            Console.WriteLine($"Model: {Model}, " +
                $"\nFuel Consumption: {Consumption} L/100km, " +
                $"\nPrice: {Price}$, " +
                $"\nMax Speed: {MaxSpeed} km/h");
        }
    }

    class PassengerCar : Car
    {
        public int Seats { get; protected set; }

        public PassengerCar(string m, double c, decimal p, int mS, int seats)
        {
            Model = m;
            Consumption = c;
            Price = p;
            MaxSpeed = mS;
            Seats = seats;
        }

        public override double CalculateCost(double distance)
        {
            return distance / 100 * Consumption * 1.3;
        }

        public override double CalculateTime(double distance)
        {
            return distance / (2 * MaxSpeed); 
        }

        public override void Info()
        {
            base.Info();
            Console.WriteLine($"Number of Seats: {Seats}\n");
        }
    }

    class CargoCar : Car
    {
        public double LoadCapacity { get; protected set; }

        public CargoCar(string m, double c, decimal p, int mS, double lC)
        {
            Model = m;
            Consumption = c;
            Price = p;
            MaxSpeed = mS;
            LoadCapacity = lC;
        }

        public override double CalculateCost(double distance)
        {
            return distance / 100 * Consumption * 1.3;
        }

        public override double CalculateTime(double distance)
        {
            return distance / (3 * MaxSpeed);
        }

        public override void Info()
        {
            base.Info();
            Console.WriteLine($"Load Capacity: {LoadCapacity} kg\n");
        }
    }

    class Carriage
    {
        public double Weight { get; set; }
    }

    class PassengerCarriage : Carriage
    {
        public int Seats { get; protected set; }

        public PassengerCarriage()
        {
            Seats = 75;
            Weight = 16000;
        }
    }

    class CargoCarriage : Carriage
    {
        public double CargoSize { get; protected set; }

        public CargoCarriage()
        {
            CargoSize = 60000;
            Weight = 27000;
        }
    }

    class Train : ITransport
    {
        private List<Carriage> Carriages;
        public double Speed {  get; protected set; }

        public Train(int PCarriages, int CCarriages, double speed)
        {
            Carriages = new List<Carriage>();
            for (int i = 0; i < PCarriages; i++)
            {
                Carriages.Add(new PassengerCarriage());
            }
            for (int i = 0; i < CCarriages; i++)
            {
                Carriages.Add(new CargoCarriage());
            }
            Speed = speed;
        }

        public double CalculateCost(double distance)
        {
            double totalCost = 0;
            foreach (var carriage in Carriages)
            {
                if (carriage is PassengerCarriage)
                {
                    totalCost += distance * 75 * 100;
                }
                else if (carriage is CargoCarriage)
                {
                    totalCost += distance * 60000 * 50;
                }
            }
            return totalCost;
        }

        public double CalculateTime(double distance)
        {
            return distance / Speed;
        }
    }

    class Moped : TwoWheels
    {
        public Moped(double c, int mS, int seats)
        {
            Consumption = c;
            MaxSpeed = mS;
            Seats = seats;
        }

        public override double CalculateCost(double distance)
        {
            return distance / 100 * Consumption * 1.3;
        }

        public override double CalculateTime(double distance)
        {
            return distance / (1.5 * MaxSpeed);
        }
    }

    class Motorcycle : TwoWheels
    {
        public Motorcycle(double c, int mS, int seats)
        {
            Consumption = c;
            MaxSpeed = mS;
            Seats = seats;
        }

        public override double CalculateCost(double distance)
        {
            return distance / 100 * Consumption * 1.3;
        }

        public override double CalculateTime(double distance)
        {
            return distance / MaxSpeed;
        }
    }

    class Bicycle : TwoWheels
    {
        public Bicycle(int mS)
        {
            Consumption = 0;
            MaxSpeed = mS;
            Seats = 1;
        }

        public override double CalculateCost(double distance)
        {
            return 0;
        }

        public override double CalculateTime(double distance)
        {
            return distance / (2 * MaxSpeed);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PassengerCar car = new PassengerCar("Ford Focus", 8.5, 25000, 250, 5);
            double carCost = car.CalculateCost(100);
            double carTime = car.CalculateTime(100);
            Console.WriteLine("Cost for Car: " + carCost + "$");
            Console.WriteLine("Time for Car: " + carTime + " hours");
            car.Info();

            CargoCar cargoCar = new CargoCar("Volvo FH Aero", 14.2, 42000, 200, 1800);
            double cCarCost = cargoCar.CalculateCost(100);
            double cCarTime = cargoCar.CalculateTime(100);
            Console.WriteLine("Cost for Cargo Car: " + cCarCost + "$");
            Console.WriteLine("Time for Cargo Car: " + cCarTime + " hours");
            cargoCar.Info();

            Train train = new Train(5, 6, 200);
            double trainCost = train.CalculateCost(100);
            double trainTime = train.CalculateTime(100);
            Console.WriteLine("Cost for Train: " + trainCost + "$");
            Console.WriteLine("Time for Train: " + trainTime + " hours");
        }
    }
}
