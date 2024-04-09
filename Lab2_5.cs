/// Інтерфейси Транспортний Засіб, Двоколісний Транспорт
/// Абстрактні класи Автомобіль
/// СабКласи Велосипед, Мопед, Мотоцикл, Потяг, Cargo автомобіль, Passenger автомобіль,
/// Рахувати час/вартість перевезення людей/речей

using System;

namespace Lab2_5
{
    interface ITransport
    {
        double CalculateCost(double distance);
        double CalculateTime(double distance);
    }

    interface ITwoWheels : ITransport
    {
        
    }

    abstract class Car : ITransport
    {
        public double Weight { get; protected set; }
        public double Price { get; protected set; }
        public double Size { get; protected set; }

        public abstract double CalculateCost(double distance);
        public abstract double CalculateTime(double distance);
    }

    class Passenger_Car : Car
    {
        public Passenger_Car()
        {
            Weight = 1500;
            Price = 20000;
            Size = 4;
        }

        public override double CalculateCost(double distance)
        {
            return distance * 0.1;
        }

        public override double CalculateTime(double distance)
        {
            return distance / 60;
        }
    }

    class Cargo_Car : Car
    {
        public Cargo_Car()
        {
            Weight = 5000;
            Price = 50000;
            Size = 8;
        }

        public override double CalculateCost(double distance)
        {
            return distance * 0.2;
        }

        public override double CalculateTime(double distance)
        {
            return distance / 50;
        }
    }

    class Moped : ITwoWheels
    {
        public double CalculateCost(double distance)
        {
            return distance * 0.05;
        }

        public double CalculateTime(double distance)
        {
            return distance / 30;
        }
    }

    class Motorcycle : ITwoWheels
    {
        public double CalculateCost(double distance)
        {
            return distance * 0.08;
        }

        public double CalculateTime(double distance)
        {
            return distance / 40;
        }
    }

    class Bicycle : ITwoWheels
    {
        public double CalculateCost(double distance)
        {
            return distance * 0.02;
        }

        public double CalculateTime(double distance)
        {
            return distance / 15;
        }
    }

    class Train : ITransport
    {
        private int numberOfCars;

        public Train(int numberOfCars)
        {
            this.numberOfCars = numberOfCars;
        }

        public double CalculateCost(double distance)
        {
            return distance * 0.05 * numberOfCars;
        }

        public double CalculateTime(double distance)
        {
            return distance / 100;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Passenger_Car car = new Passenger_Car();
            double carCost = car.CalculateCost(100);
            double carTime = car.CalculateTime(100);
            Console.WriteLine("Cost for Car: " + carCost);
            Console.WriteLine("Time for Car: " + carTime);

            Train train = new Train(5);
            double trainCost = train.CalculateCost(100);
            double trainTime = train.CalculateTime(100);
            Console.WriteLine("Cost for Train: " + trainCost);
            Console.WriteLine("Time for Train: " + trainTime);
        }
    }
}