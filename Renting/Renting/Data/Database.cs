using Renting.Entities;
using System.Text.Json;

namespace Renting.Data
{
    // TODO: Change colors. Can be done via hex / RGB, dono how, but google it!
    // TODO: Make Costume view scrolable, there is a parameter in Panel that influences that. Ask ChatGpt how to make panel
    // scrolable and set this property in code (same as width / height and color)
    // TODO: Write proper costumes inside Data/costumes.json (now they have names like name1 and name 2). NAMES SHOULD NOT REPEAT!

    public class Database
    {
        public List<Costume> AvailableCostumes { get; set; } = default!;

        public List<Costume> RentedConstumes { get; set; } = default!;

        public static Database CreateDatabase()
        {
            // var automaticaly defines type of variable
            // otherwise we would use Database db = new Database();
            var db = new Database();

            var costumes = LoadCostumes();

            db.AvailableCostumes = costumes.Where(x => x.RentingTime == null)
                .ToList();

            db.RentedConstumes = costumes.Where(x => x.RentingTime != null)
                .ToList();

            return db;
        }

        private static List<Costume> LoadCostumes()
        {
            try
            {
                // Read JSON file content
                string jsonContent = File.ReadAllText(Constants.PathToFile);

                // Deserialize JSON to your object
                var costumes = JsonSerializer.Deserialize<List<Costume>>(jsonContent);

                return costumes ?? new List<Costume>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Costume>();
            }
        }

        public static void SavedCostumes(List<Costume> costumes)
        {
            try
            {
                // Serialize the object to JSON
                string jsonContent = JsonSerializer.Serialize(costumes, new JsonSerializerOptions
                {
                    WriteIndented = true // sformats JSON for readability
                });

                // Write the JSON to the file
                File.WriteAllText(Constants.PathToFile, jsonContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public Costume RelocatedCostumeToRentedList(string costumeName)
        {
            var costume = AvailableCostumes.FirstOrDefault(x => x.Name == costumeName);

            if (costume == null)
                throw new ArgumentException($"Costume with name {costumeName} does not exist!");

            costume.RentingTime = DateTime.UtcNow;
            AvailableCostumes.RemoveAll(c => c.Name == costumeName);

            RentedConstumes.Add(costume);

            // TODO: Add Save button, and call those 2 methods on its click. The button can AND should be located on a window
            // that means -> Add the button manualy via UI
            // Remove those methods from here after that
            var costumes = GetAllCostumes();
            SavedCostumes(costumes);
            return costume;

        }

        public Costume RelocatedCostumeToAvailableList(string costumeName)
        {
            var costume = RentedConstumes.FirstOrDefault(x => x.Name == costumeName);


            if (costume == null)
                throw new ArgumentException($"Costume with name {costumeName} does not exist!");

            costume.RentingTime = default;
            RentedConstumes.RemoveAll(c => c.Name == costumeName);

            AvailableCostumes.Add(costume);

            // TODO: Add Save button, and call those 2 methods on its click. The button can AND should be located on a window
            // that means -> Add the button manualy via UI
            // Remove those methods from here after that
            var costumes = GetAllCostumes();
            SavedCostumes(costumes);
            return costume;
        }

        private List<Costume> GetAllCostumes()
        {
            var costumes = new List<Costume>();

            costumes.AddRange(AvailableCostumes);
            costumes.AddRange(RentedConstumes);

            return costumes;
        }
    }
}
