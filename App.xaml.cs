using cfloresS3TC.Views;

namespace cfloresS3TC
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new vPrincipal());
        }
    }
}