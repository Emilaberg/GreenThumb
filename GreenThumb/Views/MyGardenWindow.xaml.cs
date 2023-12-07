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
        //initials the ui, get the data async and sets it to the ui.
        private void InitUi()
        {
            //Om det är första init så kör vi detta annars hämtar vi från "cache"
            if (AuthManager.UserGarden == null)
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(async () =>
                {
                    await GetUsersGardens();
                    await GetAllPlants();
                    try
                    {
                        //loopa igenom alla garden, 
                        //sätt alla gardens till ett listviewitem.
                        foreach (Plant plant in AuthManager.UserGarden.Plants)
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

                        foreach (Plant plant in AuthManager.AllPlants)
                        {
                            ListViewItem listViewItem = new();
                            listViewItem.Content = $"Name: {plant.Name}";
                            listViewItem.Tag = plant;
                            listViewItem.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4D4D4D"));
                            listViewItem.Foreground = new SolidColorBrush(Colors.White);
                            lstPlants.Items.Add(listViewItem);
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        return;
                    }

                }));
            } else
            {
                try
                {
                    //loopa igenom alla garden, 
                    //sätt alla gardens till ett listviewitem.
                    foreach (Plant plant in AuthManager.UserGarden.Plants)
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

                    foreach (Plant plant in AuthManager.AllPlants)
                    {
                        ListViewItem listViewItem = new();
                        listViewItem.Content = $"Name: {plant.Name}";
                        listViewItem.Tag = plant;
                        listViewItem.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4D4D4D"));
                        listViewItem.Foreground = new SolidColorBrush(Colors.White);
                        lstPlants.Items.Add(listViewItem);
                        
                    }
                }
                catch (Exception ex)
                {
                    return;
                }

            }
            
        }

        private async Task GetAllPlants()
        {
            //hämta alla plantor och sätt det till all plants i authcontrollern.
            using (GreenThumbDbContext context = new())
            {
                UnitOfWorkRepository uow = new(context);
                AuthManager.AllPlants = await uow.PlantRepository.GetAllAsync();

            }
        }

        //hämta userns garden
        private async Task GetUsersGardens()
        {
            
            using (GreenThumbDbContext context = new())
            {
                UnitOfWorkRepository uow = new(context);
                AuthManager.UserGarden = await uow.GardenRepository.GetGardenByIdAsync(AuthManager.UserSessionId);
                
            }
        }

        //initials the ui, get the data async and sets it to the ui.




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
        private void BtnAddPlant_Click(object sender, RoutedEventArgs e)
        {
            //FÖRST VILL JAG KOLLA OM PLANTAN REDAN FINNS I GARDEN, ALTERNATIVT ATT JAG TRY ATT LÄGGA TILL DEN I GARDEN OCH OM DET INTE FUNKAR SÅ FINNS DEN REDAN DÄR.
            


        }
        private void BtnPlantDetails_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRemoveGarden_Click(object sender, RoutedEventArgs e)
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

        
    }
}
