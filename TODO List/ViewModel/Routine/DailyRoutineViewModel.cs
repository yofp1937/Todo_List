/*
 * 일간 반복에서 사용하는 ViewModel
 * 현재는 DataTemplate을 값에따라 변경하는 용도로만 사용
 */
using TODO_List.Common.Interface;
using TODO_List.Model;

namespace TODO_List.ViewModel.Routine
{
    public partial class DailyRoutineViewModel : BaseViewModel, IRoutineViewModel
    {
        #region 상속 구현
        public object GetRoutineData()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
