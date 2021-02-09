using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MemberManagement.Command;
using MemberManagement.Model;
namespace MemberManagement
{
    public class SearchBarViewModel : ViewModelBase
    {
        const string searchResultText = "검색 결과가 없습니다.";
        public SearchBarViewModel()
        {
            isFocused = false;
            searchResultVisibility = Visibility.Hidden;
        }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged("SearchText");
                OnPropertyChanged("FilteredMembers");
            }
        }

        private bool isFocused;
        public bool IsFocused
        {
            get { return isFocused; }
            set
            {
                isFocused = value;

                if (isFocused == null)
                {
                    return;
                }

                if (isFocused)
                {
                    SearchResultVisibility = Visibility.Visible;
                }
                else
                {
                    SearchResultVisibility = Visibility.Hidden;
                }

                OnPropertyChanged("IsFocused");
            }
        }

        private Visibility searchResultVisibility;
        public Visibility SearchResultVisibility
        {
            get { return searchResultVisibility; }
            set
            {
                searchResultVisibility = value;
                OnPropertyChanged("SearchResultVisibility");
            }
        }

        public IEnumerable<MemberModel> FilteredMembers
        {
            get
            {
                if (searchText == "" || searchText == null)
                {
                    return MemberModels.members;
                }

                return MemberModels.members.Where(member => member.Name.ToUpper().Contains(searchText.ToUpper()));
            }
        }

        private MemberModel selectedMember;
        public MemberModel SelectedMember
        {
            get
            {
                return selectedMember;
            }
            set
            {
                selectedMember = value;
                if (selectedMember != null)
                {
                    if (selectedMember.Parent != null)
                    {
                        selectedMember.Parent.IsExpanded = true;
                    }
                    selectedMember.IsSelected = true;
                }
            }
        }

        private ICommand returnKeyUp;
        public ICommand ReturnKeyUp
        {
            get { return (this.returnKeyUp) ?? (this.returnKeyUp = new DelegateCommand(returnKeyUpFunction)); }
        }
        private void returnKeyUpFunction()
        {
            IEnumerable<MemberModel> searchedMember;
            if ((searchedMember = MemberModels.members.Where(member => member.Name.ToUpper().Equals(searchText.ToUpper()))).Count() != 0)
            {
                SelectedMember = searchedMember.First();
                return;
            }
            MessageBoxService.MessageBoxServiceShow(searchResultText);
        }

        private ICommand gotFocus;
        public ICommand GotFocus
        {
            get { return (this.gotFocus) ?? (this.gotFocus = new DelegateCommand(ChangeFocusToTrue)); }
        }
        private void ChangeFocusToTrue()
        {
            IsFocused = true;
        }

        private ICommand lostFocus;
        public ICommand LostFocus
        {
            get { return (this.lostFocus) ?? (this.lostFocus = new DelegateCommand(ChangeFocusToFalse)); }
        }
        private void ChangeFocusToFalse()
        {
            IsFocused = false;
        }

    }
}
