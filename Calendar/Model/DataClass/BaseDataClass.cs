/*
 * INotifyPropertyChanged가 필요한 DataClass들의 부모 클래스
 */
using Calendar.ViewModel.Base;

namespace Calendar.Model.DataClass
{
    public abstract class BaseDataClass : NotifyObject
    {
        public BaseDataClass() { }
    }
}
