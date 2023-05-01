using CommunityToolkit.Mvvm.ComponentModel;
using LessonProj.Modal;

namespace LessonProj.ViewModal
{
    public partial class UserViewModal : ObservableObject
    {
        public static implicit operator UserViewModal (User v)
        {
            throw new NotImplementedException();
        }
    }
}
