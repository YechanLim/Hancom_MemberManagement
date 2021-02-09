using System;
using MemberManagement.Command;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace MemberManagement.Model
{
    public static class MemberModels
    {
        public static ObservableCollection<MemberModel> members = new ObservableCollection<MemberModel>
            (new MemberModel[]
            {
                new MemberModel(){Name = "Yechan", Address = "야탑", Age = "27", DateOfBirth = "1995.04.26", PhoneNum = "010-332x-217x", Remark = "게임 잘함 \n좋아하는 영화: August Rush, Good Will Hunting", Sex = "남"},
                new MemberModel(){Name = "Potato", Address = "강남", Age = "50", DateOfBirth = "1973.01.16", PhoneNum = "010-332x-217x", Remark = "nothing", Sex = "남"},
                new MemberModel(){Name = "Carrot", Address = "제주도", Age = "12", DateOfBirth = "2010.01.16", PhoneNum = "010-332x-217x", Remark = "잼민", Sex = "여"},
                new MemberModel(){Name = "Rice", Address = "거제", Age = "33", DateOfBirth = "1995.04.26", PhoneNum = "010-332x-217x", Remark = "게임 잘함 \n좋아하는 영화: August Rush, Good Will Hunting", Sex = "남"},
                new MemberModel(){Name = "RiceBowl", Address = "야탑", Age = "27", DateOfBirth = "1995.04.26", PhoneNum = "010-332x-217x", Remark = "게임 잘함 \n좋아하는 영화: August Rush, Good Will Hunting", Sex = "남"},
            });
    }

    public class MemberModel : ViewModelBase, IDataErrorInfo
    {
        public bool allowValidation = false;

        public TreeCategory Parent { get; set; }

        private ObservableCollection<MemberModel> children;
        public ObservableCollection<MemberModel> Children
        {
            get { return children; }
            set
            {
                children = value;
            }
        }

        private string name = "";
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private string address = "";
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }

        private string sex = "";
        public string Sex
        {
            get { return sex; }
            set
            {
                sex = value;
                OnPropertyChanged("Sex");
            }
        }

        private string dateOfBirth = "";
        public string DateOfBirth
        {
            get { return dateOfBirth; }
            set
            {
                dateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
            }
        }

        private string age = "";
        public string Age
        {
            get { return age; }
            set
            {
                age = value;
                OnPropertyChanged("Age");
            }
        }

        private string phoneNum;
        public string PhoneNum
        {
            get { return phoneNum; }
            set
            {
                phoneNum = value;
                OnPropertyChanged("PhoneNum");
            }
        }

        private string remark;
        public string Remark
        {
            get { return remark; }
            set
            {
                remark = value; OnPropertyChanged("Remark");
            }
        }

        private bool isSelected = false;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        private bool isExpanded = false;
        public bool IsExpanded
        {
            get { return false; }
            set
            {
                isExpanded = value;
            }
        }

        public string Error
        {
            get
            {
                if (!allowValidation) return null;

                IDataErrorInfo me = (IDataErrorInfo)this;
                string error =
                    me["Name"] +
                    me["Address"] +
                    me["Sex"] +
                    me["DateOfBirth"] +
                    me["Age"] +
                    me["PhoneNum"];
                if (!string.IsNullOrEmpty(error))
                    return "Please check inputted data.";
                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (!allowValidation) return null;

                string result = string.Empty;
                int tempNum;
                switch (columnName)
                {
                    case "Name": if (string.IsNullOrEmpty(Name)) result = "필수 항목입니다!"; break;
                    case "Address": if (string.IsNullOrEmpty(Address)) result = "필수 항목입니다!"; break;
                    case "Sex": if (string.IsNullOrEmpty(Sex)) result = "필수 항목입니다!"; break;
                    case "DateOfBirth": if (string.IsNullOrEmpty(DateOfBirth)) result = "필수 항목입니다!"; break;
                    case "Age":
                        if (string.IsNullOrEmpty(Age)) { result = "필수 항목입니다!"; }
                        else if (!Int32.TryParse(Age, out tempNum)) { result = "숫자만 입력해주세요!"; }
                        break;
                    case "PhoneNum": if (string.IsNullOrEmpty(PhoneNum)) result = "필수 항목입니다!"; break;
                }
                return result;
            }
        }

        public string EnableValidationAndGetError()
        {
            this.allowValidation = true;
            string error = this.Error;
            Console.WriteLine("error : " + error);
            OnPropertyChanged("");

            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }
            return null;
        }

        public void Clear()
        {
            allowValidation = false;
            Name = "";
            Address = "";
            Sex = "";
            DateOfBirth = "";
            Age = "";
            PhoneNum = "";
            Remark = "";
        }

        public void CopyFrom(MemberModel sourceMemberModel)
        {
            Name = sourceMemberModel.Name;
            Address = sourceMemberModel.Address;
            Sex = sourceMemberModel.Sex;
            DateOfBirth = sourceMemberModel.DateOfBirth;
            Age = sourceMemberModel.Age;
            PhoneNum = sourceMemberModel.PhoneNum;
            Remark = sourceMemberModel.Remark;
        }



    }
}
