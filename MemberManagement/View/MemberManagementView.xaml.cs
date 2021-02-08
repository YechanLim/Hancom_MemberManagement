
using System.Windows.Controls;
using MemberManagement.ViewModel;

namespace MemberManagement.View
{
    /// <summary>
    /// Interaction logic for MemberManagementView.xaml
    /// </summary>
    public partial class MemberManagementView : UserControl
    {
        public MemberManagementView()
        {
            InitializeComponent();
            this.DataContext = new MemberManagementViewModel();

        }
    }
}
