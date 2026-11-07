namespace HundDagiset
{
    public enum DogSize
    {
        Liten = 1,
        Medel = 2,
        Stor = 3
    }

    public class Dog
    {
        public int Id { get; }
        public string Name { get; }
        public DogSize Size { get; }
        public string FoodName { get; private set; } = "";
        public bool HasEaten { get; private set; }
        public decimal SpaceRequired => Size switch
        {
            DogSize.Liten => 0.5m,
            DogSize.Medel => 1m,
            DogSize.Stor => 1.5m,
            _ => throw new ArgumentOutOfRangeException()
        };

        public Dog(int id, string name, DogSize size)
        {
            Id = id;
            Name = name;
            Size = size;
        }

        public void MarkAsFed()
        {
            HasEaten = true;
        }
    }

    public class DogService
    {
        private readonly List<Dog> dogs = new();
        private int nextId = 1;

        public IReadOnlyList<Dog> Dogs { get; }
        public decimal MaxCapacity { get; }

        public decimal UsedCapacity
        {
            get
            {
                decimal total = 0;

                foreach (Dog dog in dogs)
                    total += dog.SpaceRequired;

                return total;
            }
        }

        public decimal AvailableCapacity => MaxCapacity - UsedCapacity;

        public DogService(decimal maxCapacity)
        {
            if (maxCapacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxCapacity));

            MaxCapacity = maxCapacity;
            Dogs = dogs.AsReadOnly();
        }

        public bool CheckIn(string name, DogSize size)
        {
            if (String.IsNullOrWhiteSpace(name))
                return false;

            if (size != DogSize.Liten && size != DogSize.Medel && size != DogSize.Stor)
                return false;

            Dog dog = new Dog(nextId, name.Trim(), size);

            if (dog.SpaceRequired > AvailableCapacity)
                return false;

            dogs.Add(dog);
            nextId++;

            return true;
        }

        public bool CheckOut(int id)
        {
            Dog? dog = dogs.Find(d => d.Id == id);

            if (dog == null)
                return false;

            dogs.Remove(dog);
            return true;
        }

        public decimal GetLoadPercentage()
        {
            return UsedCapacity / MaxCapacity * 100m;
        }
        private readonly Dictionary<string, int> foodStock = new(StringComparer.OrdinalIgnoreCase);

        public IReadOnlyDictionary<string, int> FoodStock => foodStock;
        public bool AddFood(string name, int portions)
        {
            if (string.IsNullOrWhiteSpace(name) || portions <= 0)
                return false;

            name = name.Trim();

            if (foodStock.ContainsKey(name))
                return false;

            foodStock.Add(name, portions);
            return true;
        }
        public bool TryFeedDog(int id, out string message)
        {
            Dog? dog = dogs.Find(d => d.Id == id);

            if (dog == null)
            {
                message = "Hunden hittades inte.";
                return false;
            }

            if (dog.HasEaten)
            {
                message = "Hunden har redan fått mat.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(dog.FoodName))
            {
                message = "Hunden har inget registrerat foder.";
                return false;
            }

            if (!foodStock.ContainsKey(dog.FoodName) ||
                foodStock[dog.FoodName] <= 0)
            {
                message = "Det finns inte tillräckligt med foder.";
                return false;
            }

            foodStock[dog.FoodName]--;

            dog.MarkAsFed();

            message = $"{dog.Name} har fått mat.";
            return true;
        }
    }
}