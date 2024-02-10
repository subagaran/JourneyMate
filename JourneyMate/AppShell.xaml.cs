using JourneyMate.MVVM.Views.Home;
using JourneyMate.MVVM.Views.User;

namespace JourneyMate
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(StartingPage), typeof(StartingPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));

        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}