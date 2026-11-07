namespace HundDagiset
{
    public class ConsoleMenu
    {
        private readonly DogService dogService;

        public ConsoleMenu(DogService dogService)
        {
            this.dogService = dogService;
        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("");
                Console.WriteLine("--- HUNDDAGIS ---");
                Console.WriteLine("1. Visa hundar");
                Console.WriteLine("2. Visa lediga platser");
                Console.WriteLine("3. Checka in hund");
                Console.WriteLine("4. Checka ut hund");
                Console.WriteLine("5. Visa foderlager");
                Console.WriteLine("6. Registrera foder");
                Console.WriteLine("7. Ange hundens foder");
                Console.WriteLine("8. Mata hund");
                Console.WriteLine("9. Visa matstatus");
                Console.WriteLine("0. Avsluta");
                Console.WriteLine("-----------------");
                Console.Write("Val: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        ShowDogs();
                        break;

                    case "2":
                        ShowAvailableCapacity();
                        break;

                    case "3":
                        CheckIn();
                        break;

                    case "4":
                        CheckOut();
                        break;

                    case "5":
                        ShowFoodStock();
                        break;

                    case "6":
                        AddFood();
                        break;

                    case "7":
                        SetDogDiet();
                        break;

                    case "8":
                        FeedDog();
                        break;

                    case "9":
                        ShowFeedingStatus();
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Ogiltigt menyval.");
                        break;
                }
            }
        }

        private void CheckIn()
        {
            Console.Write("Hundens namn: ");
            string name = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Hunden måste ha ett namn.");
                return;
            }

            Console.Write("Storlek (1 = Liten, 2 = Medel, 3 = Stor): ");

            if (!int.TryParse(Console.ReadLine(), out int size)|| size < 1 || size > 3)
            {
                Console.WriteLine("Ogiltig storlek.");
                return;
            }

            bool success = dogService.CheckIn(name, (DogSize)size);

            Console.WriteLine(success
                ? $"{name} är incheckad."
                : "Det finns inte tillräckligt med plats.");
        }

        private void CheckOut()
        {
            Console.Write("Hundens ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ogiltigt ID!");
                return;
            }

            bool success = dogService.CheckOut(id);

            Console.WriteLine(success
                ? "Hunden är utcheckad."
                : "Hunden hittades inte.");
        }

        private void ShowAvailableCapacity()
        {
            Console.WriteLine("-----------------");
            Console.WriteLine($"Använd kapacitet: {dogService.UsedCapacity:0.#}");
            Console.WriteLine($"Max kapacitet: {dogService.MaxCapacity:0.#}");
            Console.WriteLine($"Ledig kapacitet: {dogService.AvailableCapacity:0.#}");
            Console.WriteLine("-----------------");
        }

        private void ShowDogs()
        {
            Console.WriteLine("-----------------");
            Console.WriteLine($"Antal hundar: {dogService.Dogs.Count}");

            foreach (Dog dog in dogService.Dogs)
            {
                Console.WriteLine($"ID {dog.Id}: {dog.Name} | {dog.Size}");
            }

            Console.WriteLine(
                $"Belastning: {dogService.GetLoadPercentage():0.#} %");
            Console.WriteLine("-----------------");
        }
        private void ShowFoodStock()
        {
            Console.WriteLine("\n--- FODERLAGER ---");

            if (dogService.FoodStock.Count == 0)
            {
                Console.WriteLine("Det finns inget registrerat foder.");
                return;
            }

            foreach (var food in dogService.FoodStock)
            {
                Console.WriteLine($"{food.Key}: {food.Value} portioner");
            }
        }
        private void AddFood()
        {
            Console.Write("Fodrets namn: ");
            string name = (Console.ReadLine() ?? "").Trim();

            Console.Write("Antal portioner: ");

            if (!int.TryParse(Console.ReadLine(), out int portions))
            {
                Console.WriteLine("Ogiltigt antal.");
                return;
            }

            bool success = dogService.AddFood(name, portions);

            Console.WriteLine(success
                ? $"{name} har registrerats."
                : "Kunde inte registrera fodret. Kontrollera namn och antal.");
        }
        private void FeedDog()
        {
            Console.Write("Hundens ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ogiltigt ID.");
                return;
            }

            bool success = dogService.TryFeedDog(id, out string message);

            Console.WriteLine(message);

        }
        private void SetDogDiet()
        {
            Console.Write("Hundens ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ogiltigt ID.");
                return;
            }

            Console.Write("Vilket foder ska hunden ha? ");
            string foodName = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(foodName))
            {
                Console.WriteLine("Du måste ange ett fodernamn.");
                return;
            }

            bool success = dogService.SetDogDiet(id, foodName);

            Console.WriteLine(success
                ? "Hundens foder har registrerats."
                : "Kunde inte registrera hundens foder.");
        }
        private void ShowFeedingStatus()
        {
            Console.WriteLine("\n--- MATSTATUS ---");

            foreach (Dog dog in dogService.Dogs)
            {
                Console.WriteLine($"ID {dog.Id}: {dog.Name}");

                Console.WriteLine(
                    $"Foder: {(string.IsNullOrWhiteSpace(dog.FoodName) ? "Ej registrerat" : dog.FoodName)}");

                Console.WriteLine(
                    dog.HasEaten ? "Status: Har fått mat" : "Status: Har inte fått mat");

                Console.WriteLine("-----------------");
            }
        }
    }
}