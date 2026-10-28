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
                Console.WriteLine("Ogiltigt ID.");
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
            Console.WriteLine($"Använd kapacitet: {dogService.UsedCapacity}");
            Console.WriteLine($"Max kapacitet: {dogService.MaxCapacity}");
            Console.WriteLine($"Ledig kapacitet: {dogService.AvailableCapacity}");
            Console.WriteLine("-----------------");
        }

        private void ShowDogs()
        {
            Console.WriteLine("-----------------");
            Console.WriteLine($"Antal hundar: {dogService.Dogs.Count}");

            foreach (Dog dog in dogService.Dogs)
            {
                Console.WriteLine(
                    $"ID {dog.Id}: {dog.Name} | {dog.Size}");
            }

            Console.WriteLine(
                $"Belastning: {dogService.GetLoadPercentage():0.#} %");
            Console.WriteLine("-----------------");
        }
    }
}