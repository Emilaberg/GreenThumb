using GreenThumb.Views;

namespace GreenThumb.Managers
{
    public static class ViewManager
    {
        public static AddPlantWindow AddPlantWindow()
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
        public static PlantWindow PlantWindow()
        {
            return new PlantWindow();
        }

        public static RegistrationWindow RegistrationWindow()
        {
            return new RegistrationWindow();
        }

    }
}
