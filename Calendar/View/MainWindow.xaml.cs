/*
 * 사용자가 보는 화면(View)을 담당
 */
using Calendar.Common.Interface;
using Calendar.Common.Messages;
using Calendar.Common.Util;
using Calendar.ViewModel;
using System.Windows;

namespace Calendar.View
{
    public partial class MainWindow : Window
    {
        private MainViewModel _mvm;

        public MainWindow(ITodoRepository todoRepository, ISettingRepository settingRepository)
        {
            _mvm = new MainViewModel(todoRepository, settingRepository);
            DataContext = _mvm;
            InitializeComponent();

            Messenger.Subscribe<WindowMessages.UpdateOpacityMessage>(this, msg =>
            {
                this.Opacity = msg.Opacity;
            });
        }
    }
}