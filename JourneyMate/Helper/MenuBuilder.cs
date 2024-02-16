
using JourneyMate.Controls;
using JourneyMate.Helper;
using JourneyMate.MVVM.Views.BRBO.Home;
using JourneyMate.MVVM.Views.BRUS.Home;

namespace JourneyMate.Helpers
{
    public static class MenuBuilder
    {
        public static void BuildMenu()
        {
            Shell.Current.Items.Clear();

            Shell.Current.FlyoutHeader = new FlyOutHeader();

            var AppRole = GlobalVariable.GetUserRole();

            var role = "SO"; //App.UserInfo.Role;

            if (role.Equals("SO"))
            {
                var flyOutItem = new FlyoutItem()
                {
                    Title = "Home",
                    Route = nameof(MainPage),
                    FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                    Items =
                    {
                        new ShellContent
                        {
                            Icon = "home.png",
                            Title = "Home",
                            ContentTemplate = new DataTemplate(typeof(HomePage))
                        },
                    },
                };
                 
                if (!Shell.Current.Items.Contains(flyOutItem))
                {
                    Shell.Current.Items.Add(flyOutItem); 
                }
            }
        }


    }
}
