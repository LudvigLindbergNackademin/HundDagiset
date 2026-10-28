namespace HundDagiset
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DogService dogService = new DogService(10m);
            dogService.CheckIn("Bella", DogSize.Liten);
            dogService.CheckIn("Charlie", DogSize.Medel);
            dogService.CheckIn("Max", DogSize.Stor);
            ConsoleMenu menu = new ConsoleMenu(dogService);
            menu.Run();
        }
    }
}