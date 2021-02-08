using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemberManagement.Command;
namespace MemberManagement.Model
{
    public class TreeItemModel
    {
        private ObservableCollection<TreeCategory> treeCategories;
        public ObservableCollection<TreeCategory> TreeCategories
        {
            get { return treeCategories; }
            set { treeCategories = value; }
        }

        public TreeItemModel()
        {
            treeCategories = new ObservableCollection<TreeCategory>();
        }
    }
    public class TreeCategory : ViewModelBase
    {
        public TreeCategory()
        {
            children = new ObservableCollection<MemberModel>();
            this.Children.CollectionChanged += Children_CollectionChanged;
        }

        private bool isExpanded = false;
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                OnPropertyChanged("IsExpanded");
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
            }
        }

        private ObservableCollection<MemberModel> children;
        public ObservableCollection<MemberModel> Children
        {
            get { return children; }
            set
            {
                children = value;
            }
        }

        public void Children_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var newChild in e.NewItems.Cast<MemberModel>())
                {
                    newChild.Parent = this;
                }
            }
        }
    }
}
