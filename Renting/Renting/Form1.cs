using Renting.Controls;
using Renting.Data;

namespace Renting
{
    public partial class Form1 : Form
    {
        private Database _database;
        private CostumeView _availableCostumeView = default!;
        private CostumeView _rentedCostumeView = default!;

        public Form1()
        {
            _database = Database.CreateDatabase();

            _availableCostumeView = new CostumeView(_database.AvailableCostumes, RentCostume)
            {
                Location = new Point(12, 12)
            };

            _rentedCostumeView = new CostumeView(_database.RentedConstumes, ReturnCostume)
            {
                Location = new Point(12, 300)
            };

            this.Controls.Add(_availableCostumeView);
            this.Controls.Add(_rentedCostumeView);

            InitializeComponent();
        }

        private void RentCostume(string costumeName) 
        {
            var costume = _database.RelocatedCostumeToRentedList(costumeName);
            _rentedCostumeView.AddCostume(costume, addToInternalList: true);
        }

        private void ReturnCostume(string costumeName)
        {
            var costume = _database.RelocatedCostumeToAvailableList(costumeName);
            _availableCostumeView.AddCostume(costume, addToInternalList: true);
        }
    }
}
