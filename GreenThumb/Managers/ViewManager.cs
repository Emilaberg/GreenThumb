using GreenThumb.Models;
using GreenThumb.Views;

namespace GreenThumb.Managers
{
    public static class ViewManager
    {
        public static AddPlantWindow AddPlantWindow(Plant plant)
        {
            return new AddPlantWindow(plant);
        }
        public static AddPlantWindow EditPlantWindow()
        {
            return new AddPlantWindow();
        }
        
        public static LoginWindow LoginWindow()
        {
            return new LoginWindow();
        }

        public static MyGardenWindow MyGardenWindow()
        {
            return new MyGardenWindow();
        }
        public static PlantDetailsWindow PlantDetailsWindow()
        {
            return new PlantDetailsWindow();
        }

        public static RegistrationWindow RegistrationWindow()
        {
            return new RegistrationWindow();
        }

    }
}
