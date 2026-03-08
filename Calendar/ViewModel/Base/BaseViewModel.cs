/*
 * ViewModel들의 기반 클래스
 */
namespace Calendar.ViewModel.Base
{
    public abstract class BaseViewModel : NotifyObject
    {
        #region abstract 메서드
        /// <summary>
        /// BaseViewModel을 상속받는 ViewModel중에서 ICommand를 사용할경우 초기화하는 로직을 구현하는 곳<br/>
        /// RegisterICommands는 사용할시 생성자에서 호출해줘야함 
        /// </summary>
        protected virtual void RegisterICommands()
        {
        }
        #endregion
    }
}
