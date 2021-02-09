using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemberManagement.Model;

namespace MemberManagement
{
    public static class TreeViewMaker
    {
        public static void InitializeTreeView(TreeItemModel treeItemModel)
        {
            treeItemModel.TreeCategories.Add(new TreeCategory() { Name = "10대" });
            treeItemModel.TreeCategories.Add(new TreeCategory() { Name = "20대" });
            treeItemModel.TreeCategories.Add(new TreeCategory() { Name = "그 외" });

            foreach (MemberModel member in MemberModels.members)
            {
                TreeCategory treeCategory = FindNewParent(treeItemModel.TreeCategories, member.Age);
                treeCategory.Children.Add(member);
            }
        }

        public static TreeCategory FindNewParent(ObservableCollection<TreeCategory> treeCategories, string ageValue)
        {
            int age = Int32.Parse(ageValue);
            if (age <= 19)
            {
                return treeCategories[0];
            }
            else if (age <= 29)
            {
                return treeCategories[1];
            }
            else
            {
                return treeCategories[2];
            }
        }
    }
}
