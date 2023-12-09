using GreenThumb.Database;
using GreenThumb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GreenThumb.Views
{
    /// <summary>
    /// Interaction logic for AddPlantWindow.xaml
    /// </summary>
    public partial class AddPlantWindow : Window
    {
        
        public Plant PlantToEdit { get; set; }

        public AddPlantWindow()
        {
            InitializeComponent();
            InitUi();
        }

        public AddPlantWindow(Plant plant)
        {
            InitializeComponent();
            PlantToEdit = plant;
            InitUi();
        }
        

        private async void InitUi()
        {
            txtName.Text = "";
            txtInstruction.Text = "";
            lstInstructions.Items.Clear();

            //om man ska edita en planta
            if(PlantToEdit != null) { 
                //hämta den korrekta plantan från 
                txtName.Text = PlantToEdit.Name;
                
                foreach(Instruction instruction in PlantToEdit.Instructions)
                {
                    ListViewItem listViewItem = new();
                    listViewItem.Content = instruction.Description;
                    listViewItem.Tag = instruction;
                    listViewItem.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4D4D4D"));
                    listViewItem.Foreground = new SolidColorBrush(Colors.White);
                   
                    lstInstructions.Items.Add(listViewItem);
                }
            }

        }


        private void TxtInstructionEnter(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter && txtInstruction.Text != "")
            {
                //lägg till en ny instruction till listview:n

                ListViewItem instruction = new();
                instruction.Content = txtInstruction.Text;
                instruction.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4D4D4D"));
                instruction.Foreground = new SolidColorBrush(Colors.White);
                lstInstructions.Items.Add(instruction);

                txtInstruction.Text = "";
            }
        }

        private async void BtnAddPlant_Click(object sender, RoutedEventArgs e)
        {
            //skapa en ny planta.
            //kolla om man skrivit in nånting 
            if(txtName == null)
            {
                return;
            }
            
            //jag måste först skapa en planta med det namnet.


            using(GreenThumbDbContext context = new())
            {
                UnitOfWorkRepository uow = new(context);

                //skapar en planta 
                Plant newPlant = new()
                {
                    Name = txtName.Text,
                };
                await uow.PlantRepository.CreatePlantAsync(newPlant);
                uow.Complete();

                //save:ar så att det skapas ett id till plantan

                //hämta alla inlagda descriptions och skapa en instruction med description och lägg ti instructions

                foreach (ListViewItem item in lstInstructions.Items)
                {
                    string description = (string)item.Content;
                    Instruction newInstruction = new()
                    {
                        Description = description,
                        PlantId = newPlant.PlantId,
                    };
                    //lägga till instruction
                    await uow.InstructionRepository.CreateInstructionAsync(newInstruction);
                    uow.Complete();
                }
            }

            InitUi();

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            //lås upp alla fields
        }

        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {
            MyGardenWindow myGardenWindow = new();
            Close();
            myGardenWindow.Show();
        }
    }
}
