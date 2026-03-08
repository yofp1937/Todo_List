/*
 * SettingWindowView 담당
 */
using Calendar.Common.Commands;
using Calendar.Common.Interface;
using Calendar.Common.Service;
using Calendar.Model;
using Calendar.Model.Enum;
using Calendar.ViewModel.Base;
using System.Windows.Input;

namespace Calendar.ViewModel
{
    public class SettingViewModel : WindowBaseViewModel
    {
        #region Property
        private readonly AutoStartService _autoStartService;
        private bool _isAutoStartEnabled;
        public bool IsAutoStartEnabled
        {
            get => _isAutoStartEnabled;
            set => SetProperty(ref _isAutoStartEnabled, value);
        }

        private MinimizeMode _currentMinimizeMode;
        public MinimizeMode CurrentMinimizeMode
        {
            get => _currentMinimizeMode;
            set => SetProperty(ref _currentMinimizeMode, value);
        }

        public ICommand? ApplyCommand { get; private set; }
        public ICommand? SubmitCommand { get; private set; }
        #endregion

        #region 생성자, override
        public SettingViewModel(ISettingRepository settingRepository) : base(null, settingRepository)
        {
            MainBarTitleText = "설정";

            AppSettings data = settingRepository.GetSettings();
            CurrentMinimizeMode = data.MinimizeMode;

            _autoStartService = new AutoStartService("Calendar");
            IsAutoStartEnabled = _autoStartService.IsAutoStartEnabled();
        }

        protected override void RegisterICommands()
        {
            base.RegisterICommands();

            ApplyCommand = new RelayCommand(ApplyExecute);
            SubmitCommand = new RelayCommand(SubmitExecute);
        }
        #endregion

        #region 메서드
        /// <summary>
        /// 적용 버튼 누를시 동작
        /// </summary>
        private void ApplyExecute(object? obj)
        {
            _autoStartService.SetAutoStart(IsAutoStartEnabled);

            AppSettings settings = SettingRepository.GetSettings();
            settings.MinimizeMode = CurrentMinimizeMode;

            _ = SettingRepository.SaveSettings_AsyncSave(settings);
        }
        /// <summary>
        /// 저장 버튼 누를시 동작
        /// </summary>
        private void SubmitExecute(object? obj)
        {
            ApplyExecute(obj);
            CloseWindow();
        }
        #endregion
    }
}
