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

        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}