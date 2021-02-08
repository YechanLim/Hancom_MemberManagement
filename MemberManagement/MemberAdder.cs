using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemberManagement.Model;
using MemberManagement.Command;
using System.Collections.ObjectModel;

namespace MemberManagement
{
    class MemberAdder : ViewModelBase
    {
        private MemberModel memberModel;
        private TreeItemModel treeItemModels;
        public MemberAdder(MemberModel memberModel, TreeItemModel treeItemModels)
        {
            this.memberModel = memberModel;
            this.treeItemModels = treeItemModels;
        }

        public void AddMember()
        {
            MemberModel tempMemberModel = new MemberModel()
            {
                Name = memberModel.Name,
                Address = memberModel.Address,
                Sex = memberModel.Sex,
                DateOfBirth = memberModel.DateOfBirth,
                Age = memberModel.Age,
                PhoneNum = memberModel.PhoneNum,
                Remark = memberModel.Remark
            };
            
            MemberModels.members.Add(tempMemberModel);

            if (int.Parse(memberModel.Age) <= 19)
            {
                treeItemModels.TreeCategories[0].Children.Add(tempMemberModel);
            }
            else if (int.Parse(memberModel.Age) <= 29)
            {
                treeItemModels.TreeCategories[1].Children.Add(tempMemberModel);
            }
            else
            {
                treeItemModels.TreeCategories[2].Children.Add(tempMemberModel);
            }
        }

        public string EnableValidationAndGetError()
        {
            memberModel.allowValidation = true;
            string error = memberModel.Error;

            if (!string.IsNullOrEmpty(error))
            {
                OnPropertyChanged("");
                return error;
            }
            return null;
        }
    }
}
