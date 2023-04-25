/*Напишите программу, принимающую из консоли следующую информацию об автомобиле: марка, модель, количество, стоимость 
  одной единицы. После ввода наименований автомобилей, программа должна запросить у пользователя команду. При получении 
  команд программа должна выдать следующую информацию:

count types - количество марок автомобилей;

count all - общее количество автомобилей;

average price - средняя стоимость автомобиля;

average price type - средняя стоимость автомобилей каждой марки (марка задается пользователем), например average price 
volvo

При получении команды exit программа должна завершиться. Использовать паттерны проектирвоания Singleton, Command */

using OOP_design_pract_singleton_new;

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



class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter information about auto. Enter stop to continue to command");
        while (true)    //reading auto parameters 
        {
            Console.WriteLine("Brand:");
            string brand = Console.ReadLine();
            if (brand.ToLower() == "stop")
            {
                break;
            }
            Console.WriteLine("Model:");
            string model = Console.ReadLine();
            Console.WriteLine("Quaintity:");
            string quaintityToClean = Console.ReadLine();
            int quaintity = 0;
            while ((!int.TryParse(quaintityToClean, out quaintity)) || (Convert.ToInt32(quaintityToClean) <= 0))
            {
                Console.WriteLine("Quaintity must be positive int value \nQuaintity:");
                quaintityToClean = Console.ReadLine();
            }
            Console.WriteLine("Price: !!!разделитель запятая");
            string priceToClean = Console.ReadLine();
            double price = 0;
            while ((!double.TryParse(priceToClean, out price)) || (Convert.ToDouble(priceToClean) <= 0))
            {
                Console.WriteLine("Price must be positive double value \nPrice:");
                priceToClean = Console.ReadLine();
            }
            Car car = new Car(brand, model, quaintity, price);
            CarRegistry.Instance.Add(car);
        }

        bool commandCheck = false;  //exit from cicle
        do
        {
            Console.WriteLine("Enter command. Enter exit to exit");
            string command = Console.ReadLine().ToLower();

            if ((command.StartsWith("average price")) && (command != "average price"))
            {
                string modelToFind = command.Replace("average price ", "");
                Console.WriteLine($"Average price of {modelToFind} = " +
                    $"{CarRegistry.Instance.AveragePriceType(modelToFind)}");
            }
            else
            {
                switch (command)
                {
                    case ("count types"):
                        Console.WriteLine($"Count types: {CarRegistry.Instance.CountTypes()}");
                        break;
                    case ("count all"):
                        Console.WriteLine($"Count all: {CarRegistry.Instance.CountAll()}");
                        break;
                    case ("average price"):
                        Console.WriteLine($"Average price = {CarRegistry.Instance.AveragePrice()}");
                        break;
                    case ("exit"):
                        commandCheck = true;
                        break;
                    default:
                        Console.WriteLine("Invalid command. Try again");
                        break;
                }
            }
        }
        while (!commandCheck);
    }
}