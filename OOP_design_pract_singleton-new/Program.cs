using OOP_design_pract_singleton_new;

public class Program
{
    public static void Main(string[] args)
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

            Console.WriteLine("Price: use \",\" as divider");
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

        bool commandCheck = false;    //exit from cicle
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
        } while (!commandCheck);
    }
}