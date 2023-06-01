using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_design_pract_singleton_new
{
    public class CarRegistry
    {
        private static CarRegistry instance = null;
        private List<Car> cars = new List<Car> { };

        private CarRegistry() { }

        public static CarRegistry Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CarRegistry();
                }

                return instance;
            }
        }

        public void Add(Car car)
        {
            cars.Add(car);
        }

        public double AveragePriceType(string modelToFind)
        {
            var findedModels = from c in cars
                               where c.brand.ToLower() == modelToFind
                               select c.price;
            try
            {
                double result = findedModels.Average();
                return result;
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine($"There is no {modelToFind} cars in list");
                return 0;
            }
        }

        public double AveragePrice()
        {
            try
            {
                var result = cars.Select(car => car.price).Average();
                Console.WriteLine(result);
                return result;
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Error. Car list is empty");
                return 0;
            }
        }

        public int CountAll()
        {
            var result = cars.Select(car => car.quaintity).Sum();
            return result;
        }

        public int CountTypes()
        {
            var result = cars.Select(car => car.brand).Distinct().Count();
            return result;
        }
    }
}
