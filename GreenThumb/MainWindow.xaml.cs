using GreenThumb.Database;
using GreenThumb.Models;
using System.Windows;

namespace GreenThumb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            using (GreenThumbDbContext context = new())
            {
                UnitOfWorkRepository uow = new(context);

                Plant plant = uow.PlantRepository.GetPlantById(2);
                MessageBox.Show($"{plant.Name}");
            }
        }
    }
}