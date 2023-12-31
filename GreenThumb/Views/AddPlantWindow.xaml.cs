﻿using GreenThumb.Controllers;
using GreenThumb.Database;
using GreenThumb.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GreenThumb.Views
{
    /// <summary>
    /// Interaction logic for AddPlantWindow.xaml
    /// </summary>
    public partial class AddPlantWindow : Window
    {
        bool isEditing = false;
        public Plant? PlantToEdit { get; set; }

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

        private void InitUi()
        {
            txtName.Text = "";
            txtInstruction.Text = "";
            lstInstructions.Items.Clear();

            //om man ska edita en planta
            if (PlantToEdit != null)
            {
                //sätta alla saker till edit mode
                BtnEdit.Visibility = Visibility.Visible;
                BtnRemoveInstruction.Visibility = Visibility.Visible;

                txtName.IsReadOnly = true;
                txtName.Opacity = 0.5;
                txtInstruction.IsReadOnly = true;
                lstInstructions.Opacity = 0.5;
                lstInstructions.IsHitTestVisible = false;
                BtnAddPlant.Content = "Edit";
                BtnAddPlant.Opacity = 0.5;
                BtnAddPlant.IsHitTestVisible = false;
                BtnRemoveInstruction.IsHitTestVisible = false;
                BtnRemoveInstruction.Opacity = 0.5;
                BtnEdit.BorderBrush = new SolidColorBrush(Colors.Transparent);
                btnShadow.Color = (Color)ColorConverter.ConvertFromString("#6ACA20");

                //hämta den korrekta plantan från 
                txtName.Text = PlantToEdit.Name;
                BtnRemoveInstruction.Visibility = Visibility.Visible;
                foreach (Instruction instruction in PlantToEdit.Instructions)
                {
                    ListViewItem listViewItem = new();
                    listViewItem.Content = instruction.Description;
                    listViewItem.Tag = instruction;
                    listViewItem.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4D4D4D"));
                    listViewItem.Foreground = new SolidColorBrush(Colors.White);

                    lstInstructions.Items.Add(listViewItem);
                }

                return;
            }

        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if(isEditing == false)
            {
                //lås upp alla fields
                txtName.IsReadOnly = false;
                txtName.Opacity = 1;

                txtInstruction.IsReadOnly = false;
                lstInstructions.Opacity = 1;
                lstInstructions.IsHitTestVisible = true;

                BtnAddPlant.Content = "Save";
                BtnAddPlant.Opacity = 1;
                BtnAddPlant.IsHitTestVisible = true;

                isEditing = true;

                BtnRemoveInstruction.IsHitTestVisible = true;
                BtnRemoveInstruction.Opacity = 1;

                btnShadow.Color = Colors.Orange;


                BtnEdit.BorderBrush = new SolidColorBrush(Colors.Orange);
            }else
            {
                InitUi();
                isEditing = false;
            }
            
        }

        private async void TxtInstructionEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && txtInstruction.Text != "")
            {
                //lägg till en ny instruction till listview:n

                ListViewItem instruction = new();
                instruction.Content = txtInstruction.Text;
                instruction.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4D4D4D"));
                instruction.Foreground = new SolidColorBrush(Colors.White);
                lstInstructions.Items.Add(instruction);
                if (isEditing)
                {
                    await UpdateInstructions(txtInstruction.Text);
                }
                txtInstruction.Text = "";


            }
        }

        private async Task UpdateInstructions(string description)
        {
            using (GreenThumbDbContext context = new())
            {


                UnitOfWorkRepository uow = new(context);

                Plant? plant = await uow.PlantRepository.GetPlantByIdAsync(PlantToEdit.PlantId);

                Instruction instruction = new()
                {
                    Description = description,
                    PlantId = plant!.PlantId
                };

                Instruction? exits = plant.Instructions.FirstOrDefault(i => i.Description.ToLower() == instruction.Description.ToLower());

                if (exits == null)
                {
                    await uow.InstructionRepository.CreateInstructionAsync(instruction);
                    uow.Complete();
                    PlantToEdit = await uow.PlantRepository.GetPlantByIdAsync(PlantToEdit.PlantId);

                }
                else
                {

                }
                isEditing = false;
                InitUi();

            }
        }

        private async void BtnAddPlant_Click(object sender, RoutedEventArgs e)
        {

            //om man uppdaterar en planta
            if (isEditing)
            {
                await UpdateEditedPlant();
            }
            else
            {
                //skapa en ny planta.
                //kolla om man skrivit in nånting 
                if (txtName.Text == "")
                {
                    MessageBox.Show("The plant must have a name");
                    return;
                }
                if (await ValidationController.ContainsPlant(txtName.Text.ToLower()))
                {
                    MessageBox.Show($"The plant with the name {txtName.Text.ToLower()} already exits, please choose a different name");
                    return;
                }

                await AddNewPlant();
                MyGardenWindow myGardenWindow = new();
                Close();
                myGardenWindow.Show();
            }
            isEditing = false;
            InitUi();
        }

        private async Task AddNewPlant()
        {
            await CreatePlant();

        }

        private async Task CreatePlant()
        {
            using (GreenThumbDbContext context = new())
            {
                UnitOfWorkRepository uow = new(context);

                //skapar en planta 
                Plant newPlant = new()
                {
                    Name = txtName.Text.ToLower(),
                };

                await uow.PlantRepository.CreatePlantAsync(newPlant);
                uow.Complete();
                //save:ar så att det skapas ett id till plantan

                //hämta alla inlagda descriptions och skapa en instruction med description och lägg till instructions

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
        }

        private async Task UpdateEditedPlant()
        {
            //kolla om man editar
            if (isEditing == false)
            {
                return;
            }
            // skapa en ny planta med det nya namnet, och sen uppdatera den plantan jag har nu till det nya namnet, sen spara
            await UpdatePlant(PlantToEdit.PlantId);


            //kolla om man har man har lagt till en ny instruction.
            //då kollar jag om plantToedit instrucktion är lika lång som listview:n
            //sen skapa nya instruktions som har plantans id. och save:a för varje.

            //tillslut uppdatera ui:t.
        }

        private async Task UpdatePlant(int plantId)
        {
            // skapa en ny planta med det nya namnet, och sen uppdatera den plantan jag har nu till det nya namnet, sen spara
            Plant plant = new()
            {
                Name = txtName.Text,
            };
            using (GreenThumbDbContext context = new())
            {
                UnitOfWorkRepository uow = new(context);

                //uppdaterar den nuvarande plantans namn
                await uow.PlantRepository.UpdateSelectedPlantAsync(plantId, plant);
                uow.Complete();

                //uppdaterar plantanToEdit
                PlantToEdit = await uow.PlantRepository.GetPlantByIdAsync(plantId);
            }
        }

        

        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {
            MyGardenWindow myGardenWindow = new();
            Close();
            myGardenWindow.Show();
        }

        private async void BtnRemoveInstruction_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem selectedItem = (ListViewItem)lstInstructions.SelectedItem;
            Instruction selectedInstruction = (Instruction)selectedItem.Tag;

            using (GreenThumbDbContext context = new())
            {
                UnitOfWorkRepository uow = new(context);

                await uow.InstructionRepository.RemoveSelectedInstructionAsync(selectedInstruction.InstructionId);
                uow.Complete();
                PlantToEdit = await uow.PlantRepository.GetPlantByIdAsync(PlantToEdit.PlantId);
            };

            isEditing = false;
            InitUi();
            BtnEdit_Click(null, null);

        }
    }
}
