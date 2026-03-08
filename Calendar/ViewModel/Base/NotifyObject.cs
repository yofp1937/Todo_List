/*
 * INotifyPropertyChaged를 사용하는 Model, ViewModel들의 부모 클래스
 */
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Calendar.ViewModel.Base
{
    public abstract class NotifyObject : INotifyPropertyChanged
    {
        #region Property
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        public NotifyObject() { }

        #region INotifyPropertyChanged 메서드
        /// <summary>
        /// 값이 변할때 UI를 갱신시키기위한 알림 함수<br/>
        /// 매개변수에 CallerMemberName을 사용해서 호출한 속성이나 메서드의 이름을 자동으로 가져옴
        /// </summary>
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///  새로 입력받은 값이 기존 값과 다르다면 이벤트 호출 이후 True 반환
        /// </summary>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
        {
            // 기존 값과 새 값이 같다면 아무것도 하지 않고 false 반환
            if (Equals(storage, value))
            {
                return false;
            }

            //Debug.WriteLine($"BaseViewModel: {propertyName} - 기존값: {storage}, 입력값: {value}");
            // 값이 다르다면 storage에 새 값 저장
            storage = value;

            // UI 업데이트를 위해 PropertyChanged 이벤트 발생
            //Debug.WriteLine($"[{propertyName}] - OnPropertyChanged 호출");
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion
    }
}
