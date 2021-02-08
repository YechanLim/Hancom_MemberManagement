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
    public class SearchBar : ViewModelBase
    {
        public SearchBar()
        {
            isFocused = false;
            searchBarVisibility = Visibility.Hidden;
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
                if(isFocused == null)
                {
                    Console.WriteLine("isFocused == null");
                    return;
                }

                Console.WriteLine("isFocus : " + isFocused);
                if (isFocused)
                {
                    SearchBarVisibility = Visibility.Visible;
                }
                else
                {
                    SearchBarVisibility = Visibility.Hidden;
                }

                OnPropertyChanged("IsFocused");
                Console.WriteLine("isFocused value changed");
            }
        }

        private Visibility searchBarVisibility;
        public Visibility SearchBarVisibility
        {
            get { return searchBarVisibility; }
            set
            {
                searchBarVisibility = value;
                OnPropertyChanged("SearchBarVisibility");
                Console.WriteLine("searchbarvisibility value changed");
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

        private ICommand keyUp;
        public ICommand KeyUp
        {
            get { return (this.keyUp) ?? (this.keyUp = new DelegateCommand(KeyUpFunction)); }
        }
        private void KeyUpFunction()
        {
            IEnumerable<MemberModel> searchedMember;
            if( (searchedMember = MemberModels.members.Where(member => member.Name.ToUpper().Equals(searchText.ToUpper()))).Count() != 0 )
            {
                SelectedMember = searchedMember.First();
            }
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
