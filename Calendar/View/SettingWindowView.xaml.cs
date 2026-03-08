using Calendar.ViewModel;
using System.Windows;

namespace Calendar.View
{
    public partial class SettingWindowView : Window
    {
        public SettingWindowView(SettingViewModel settingVM)
        {
            this.DataContext = settingVM;
            InitializeComponent();
        }
    }
}
