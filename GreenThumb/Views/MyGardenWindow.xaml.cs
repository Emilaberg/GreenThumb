using GreenThumb.Database;
using GreenThumb.Managers;
using GreenThumb.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace GreenThumb.Views
{
    /// <summary>
    /// Interaction logic for MyGardenWindow.xaml
    /// </summary>
    public partial class MyGardenWindow : Window
    {
        
        public MyGardenWindow()
        {
            InitializeComponent();
            InitUi();

        }

        //when I want to update the ui I call initUi
        private async void InitUi()
        {

            await GetData();
            
            //skapa listviewitems
            if(AuthManager.UserGarden != null)
            {
                //kalla på populateListViews
                PopulateListViews();
            }else
            {
                MessageBox.Show("You have no garden, start by creating a garden");
                //kalla på create Gardenwindow eller bara automatiskt skapa en garden
                BtnCreateGarden.Visibility = Visibility.Visible;
                lblGardenName.Visibility = Visibility.Visible;
                txtGardenName.Visibility = Visibility.Visible;

                lblGarden.Text = "'Your future garden name :)'";


                //disable all the other buttons

            }
        }

        private async void BtnCreateGarden_Click(object sender, RoutedEventArgs e)
        {
            //skapa en garden med det namnet man skrivit in
            using (GreenThumbDbContext context = new())
            {
                UnitOfWorkRepository uow = new(context);
                Garden garden = new()
                {
                    Name = txtGardenName.Text,
                    Level = 1,
                    Size = 1,
                    UserId = AuthManager.CurrentUser!.UserId,
                };

                await uow.GardenRepository.CreateGardenAsync(garden);
                uow.Complete();
            }
            //göm creategarden
            BtnCreateGarden.Visibility = Visibility.Hidden;
            lblGardenName.Visibility = Visibility.Hidden;
            txtGardenName.Visibility = Visibility.Hidden;
            //Kör initUi
            InitUi();

        }

        //jag vill ha en function som är async void, den await sen funktion 1 och två, sen skapar listviewitems
        private async Task GetData()
        {
            using (GreenThumbDbContext context = new())
            {
                //hämtar och sätter userGarden
                UnitOfWorkRepository uow = new(context);
                AuthManager.UserGarden = await uow.GardenRepository.GetGardenByIdAsync(AuthManager.CurrentUser!.UserId);

                //sätta allplants
                AuthManager.AllPlants = await uow.PlantRepository.GetAllAsync();

            }

        }

        private void PopulateListViews()
        {
            //rensa listviews
            lstGardens.Items.Clear();
            lstPlants.Items.Clear();

            lblGarden.Text = AuthManager.UserGarden!.Name;
            //lägg till userns plantor
            foreach (Plant plant in AuthManager.UserGarden!.Plants) 
            {
                ListViewItem listViewItem = new();
                listViewItem.Content = $"{plant.Name}";
                listViewItem.Tag = plant;
                listViewItem.FontSize = 18;
                listViewItem.FontWeight = FontWeights.Bold;
                listViewItem.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4D4D4D"));
                listViewItem.Foreground = new SolidColorBrush(Colors.White);
                lstGardens.Items.Add(listViewItem);
            }

            //lägg till alla plantor 
            foreach (Plant plant in AuthManager.AllPlants!)
            {
                ListViewItem listViewItem = new();
                listViewItem.Content = $"Name: {plant.Name}";
                listViewItem.Tag = plant;
                listViewItem.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4D4D4D"));
                listViewItem.Foreground = new SolidColorBrush(Colors.White);
                lstPlants.Items.Add(listViewItem);
            }

        }

        //SEARCH FUNCTIONALITY 

        //DETECTS WHEN WRITING SOMETHING
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //jag vill kalla på getSearchResult och skicka med det man skrev in.
            GetSearchResult(txtSearch.Text);
            if(AuthManager.FilteredPlants != null)
            {
                //sen vill jag rensa listan och fylla den med det nya.
                lstPlants.Items.Clear();
                foreach (Plant plant in AuthManager.FilteredPlants)
                {
                    ListViewItem listViewItem = new();
                    listViewItem.Content = $"Name: {plant.Name}";
                    listViewItem.Tag = plant;
                    listViewItem.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4D4D4D"));
                    listViewItem.Foreground = new SolidColorBrush(Colors.White);
                    lstPlants.Items.Add(listViewItem);
                    
                }
            }
        }

        private void GetSearchResult(string searchstring)
        {
            //så när man skriver in nånting, då vill jag hämta alla de plantor som stämmer överens med det man skrivit
            //för varje planta, vill jag kolla om namnet startar med det man skrivit in, isåfall 
            AuthManager.FilteredPlants = AuthManager.AllPlants?.Where(p => p.Name.StartsWith(searchstring) == true).ToList();

        }

        //SEARCH FUNCTIONALITY 





        //ADD PLANT TO GARDEN
        private async void BtnAddPlant_Click(object sender, RoutedEventArgs e)
        {
            if(lstPlants.SelectedItem == null)
            {
                return;
            }
            ListViewItem selectedItem = (ListViewItem)lstPlants.SelectedItem;
            Plant selectedPlant = (Plant)selectedItem.Tag;

            //först vill jag kolla om plantan finns hos användarens garden
            //jag vill kolla om plant man valt finns hos användaren trädgård i databasen
            if(AuthManager.UserGarden!.Plants.Contains(selectedPlant) == false)
            {
                using (GreenThumbDbContext context = new())
                {
                    UnitOfWorkRepository uow = new(context);
                    Garden? userGarden = await uow.GardenRepository.GetGardenByIdAsync(AuthManager.CurrentUser!.UserId);
                    if (userGarden != null)
                    {
                        userGarden.Plants.Add(selectedPlant);
                    }
                    
                    uow.Complete();
                    
                }
                InitUi();
            }

        }
        private void BtnPlantDetails_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            ViewManager.LoginWindow().Show();
            Close();
        }

        private void BtnAccount_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lstGardens_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //
        }

        

        private void BtnAddGarden_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void BtnRemoveGardenPlant_Click(object sender, RoutedEventArgs e)
        {
            //kolla om man valt nånting
            //sen vill jag hämta den valda plantan, 
            //sen vill jag ta bort den plantan från currentUsers plant lista.
            if(lstGardens.SelectedItem == null)
            {
                return;
            }

            ListViewItem selectedItem = (ListViewItem)lstGardens.SelectedItem;
            Plant plant = (Plant)selectedItem.Tag;

            using (GreenThumbDbContext context = new())
            {
                UnitOfWorkRepository uow = new(context);
                Garden? garden = await uow.GardenRepository.GetGardenByIdAsync(AuthManager.CurrentUser.UserId);
                garden.Plants.RemoveAll(p => p.PlantId == plant.PlantId);
                
                uow.Complete();
            }
            InitUi();
            
        }
    }
}
