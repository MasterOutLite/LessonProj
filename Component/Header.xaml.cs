using CommunityToolkit.Mvvm.ComponentModel;
using LessonProj.ViewModal;

namespace LessonProj.Component
{
    public partial class Header : ContentView
    {       
        public Header ()
        {           
            InitializeComponent();       
        }

        protected override void OnBindingContextChanged ()
        {
            base.OnBindingContextChanged();

           var context = BindingContext as HeaderViewModal;
            if (context != null)
            {
                Button button = new Button ();

                RightGroup.Add(button);
            }
        }
    }
}
