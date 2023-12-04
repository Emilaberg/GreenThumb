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

            //using (GreenThumbDbContext context = new())
            //{
            //    User newUser = new()
            //    {
            //        Name = "Johannes",
            //        Password = "hej123"
            //    };

            //    context.User.Add(newUser);

            //    context.SaveChanges();
            //}

            //using (GreenThumbDbContext context = new())
            //{
            //    User? user = context.User.FirstOrDefault(u => u.UserId == 5);
            //    MessageBox.Show($"{user!.Name} {user.Password}");
            //}

            //using (GreenThumbDbContext context = new())
            //{
            //    Plant newPlant = new()
            //    {
            //        Name = "kaktus",
            //    };

            //    context.Plant.Add(newPlant);

            //    context.Garden.First(g => g.GardenId == 2).Plants.Add(newPlant);

            //    context.SaveChanges();
            //}
        }
    }
}