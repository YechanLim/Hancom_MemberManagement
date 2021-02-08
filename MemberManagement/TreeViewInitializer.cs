using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemberManagement.Model;

namespace MemberManagement
{
    public static class TreeViewInitializer
    {
        public static void InitializeTreeView(TreeItemModel treeItemModel)
        {
            treeItemModel.TreeCategories.Add(new TreeCategory() { Name = "10대" });
            treeItemModel.TreeCategories.Add(new TreeCategory() { Name = "20대" });
            treeItemModel.TreeCategories.Add(new TreeCategory() { Name = "그 외" });

            foreach (MemberModel member in MemberModels.members)
            {
                TreeCategory treeCategory = FindNewParent(treeItemModel, int.Parse(member.Age));
                treeCategory.Children.Add(member);
            }
        }

        public static TreeCategory FindNewParent(TreeItemModel treeItemModel, int age)
        {
            if (age <= 19)
            {
                return treeItemModel.TreeCategories[0];
            }
            else if (age <= 29)
            {
                return treeItemModel.TreeCategories[1];
            }
            else
            {
                return treeItemModel.TreeCategories[2];
            }
        }

        public static TreeCategory FindParent(TreeItemModel treeItemModel, MemberModel searchModel)
        {
            if (searchModel != null)
            {
                foreach (TreeCategory treeCategory in treeItemModel.TreeCategories)
                {
                    if (treeCategory.Children.Count > 0)
                    {
                        foreach (MemberModel memberModel in treeCategory.Children)
                        {
                            if(memberModel.Equals(searchModel))
                            {
                                return treeCategory;
                            }
                        }
                    }
                }
            }
            return null;
        }

    }
}
