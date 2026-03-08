/*
 * 일간 반복에서 사용하는 ViewModel
 * 현재는 DataTemplate을 값에따라 변경하는 용도로만 사용
 */
using Calendar.Common.Interface;
using Calendar.ViewModel.Base;

namespace Calendar.ViewModel.TodoWindow.Routine
{
    public partial class DailyRoutineViewModel : BaseViewModel, IRoutineViewModel
    {
        #region 상속 구현
        public object GetRoutineData()
        {
            return new object();
        }
        public void SetRoutineData(object data)
        {
        }
        public bool GetEnteredRequireData()
        {
            return true;
        }
        #endregion
    }
}
