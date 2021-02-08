using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemberManagement.Model;
namespace MemberManagement
{
    public static class SelectedItemDisplayer
    {
        public static void DisplaySelectedItem(MemberModel ListPageMemberModel, MemberModel selectedMemberModel)
        {
            ListPageMemberModel.Name = selectedMemberModel.Name;
            ListPageMemberModel.Address = selectedMemberModel.Address;
            ListPageMemberModel.Sex = selectedMemberModel.Sex;
            ListPageMemberModel.DateOfBirth = selectedMemberModel.DateOfBirth;
            ListPageMemberModel.Age = selectedMemberModel.Age;
            ListPageMemberModel.PhoneNum = selectedMemberModel.PhoneNum;
            ListPageMemberModel.Remark = selectedMemberModel.Remark;
        }
    }
}
