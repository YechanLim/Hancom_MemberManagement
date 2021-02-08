using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemberManagement.Model;
namespace MemberManagement
{
    public static class TextInitializer
    {
        public static void InitializeWholeText(MemberModel memberModel)
        {
            memberModel.allowValidation = false;
            memberModel.Name = "";
            memberModel.Address = "";
            memberModel.Sex = "";
            memberModel.DateOfBirth = "";
            memberModel.Age = "";
            memberModel.PhoneNum = "";
            memberModel.Remark = "";
        }
        
    }
}
