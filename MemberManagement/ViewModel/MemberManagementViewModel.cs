﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MemberManagement.Model;
using MemberManagement.Command;
using MemberManagement;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;

namespace MemberManagement.ViewModel
{
    class MemberManagementViewModel : ViewModelBase
    {
        const string registerCompletedMessage = "등록이 완료되었습니다.";
        const string deleteConfirmMessage = "정말 삭제하시겠습니까?";
        const string deleteConfirmCaption = "삭제";

        public MemberModel RegisterPageMemberModel { get; set; }
        public MemberModel ListPageMemberModel { get; set; }


        private TreeItemModel treeItemModel;
        public static TreeItemModel TreeItemModel { get; set; }

        public SearchBar SearchBar { get; set; }

        private MemberAdder memberAdder;
        public MemberManagementViewModel()
        {
            RegisterPageMemberModel = new MemberModel();
            ListPageMemberModel = new MemberModel();
            TreeItemModel = new TreeItemModel();
            memberAdder = new MemberAdder(RegisterPageMemberModel, TreeItemModel);
            SearchBar = new SearchBar();
            TreeViewInitializer.InitializeTreeView(TreeItemModel);
        }

        private ICommand registerCommand;
        public ICommand RegisterCommand
        {
            get { return (this.registerCommand) ?? (this.registerCommand = new DelegateCommand(Register)); }
        }

        private void Register()
        {
            string error = memberAdder.EnableValidationAndGetError();
            OnPropertyChanged("");

            if (error != null) return;

            memberAdder.AddMember();
            RegisterPageMemberModel.Clear();
            MessageBoxService.MessageBoxServiceShow(registerCompletedMessage);
        }

        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get { return (this.cancelCommand) ?? (this.cancelCommand = new DelegateCommand(Cancel)); }
        }
        private void Cancel()
        {
            RegisterPageMemberModel.Clear();
        }

        private ICommand selectItemCommand;
        public ICommand SelectItemCommand
        {
            get
            {
                return selectItemCommand ?? (selectItemCommand = new RelayCommand(param => this.ShowMemberList(param)));
            }
        }
        private void ShowMemberList(object selectedMenuItem)
        {
            var selectedMemberModel = selectedMenuItem as MemberModel;
            if (selectedMemberModel == null)
            {
                return;
            }
            ListPageMemberModel.CopyFrom(selectedMemberModel);
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get { return (this.deleteCommand) ?? (this.deleteCommand = new RelayCommand(param => Delete(param))); }
        }
        private void Delete(object selectedMenuItem)
        {
            MemberModel selectedMemberModel = selectedMenuItem as MemberModel;
            if (selectedMemberModel == null)
            {
                return;
            }
            MessageBoxResult result = MessageBoxService.MessageBoxServiceShow(deleteConfirmMessage, deleteConfirmCaption);

            if (result == MessageBoxResult.No)
            {
                return;
            }

            TreeCategory treeCategory = TreeViewInitializer.FindNewParent(TreeItemModel, Int32.Parse(selectedMemberModel.Age));
            if (treeCategory == null)
            {
                return;
            }
            treeCategory.Children.Remove(selectedMemberModel);
            MemberModels.members.Remove(selectedMemberModel);
            TextInitializer.InitializeWholeText(ListPageMemberModel);
            OnPropertyChanged("");
        }

        private ICommand applyCommand;
        public ICommand ApplyCommand
        {
            get { return (this.applyCommand) ?? (this.applyCommand = new RelayCommand(param => this.Apply(param))); }
        }
        private void Apply(object selectedMenuItem)
        {
            MemberModel selectedModel = selectedMenuItem as MemberModel;
            selectedModel.Age = ListPageMemberModel.Age;
            TreeCategory oldParent = selectedModel.Parent;
            TreeCategory newParent = TreeViewInitializer.FindNewParent(TreeItemModel, Int32.Parse(ListPageMemberModel.Age));
            if (oldParent.Equals(newParent))
            {
                return;
            }
            oldParent.Children.Remove(selectedModel);
            newParent.Children.Add(selectedModel);
            newParent.IsExpanded = true;
            selectedModel.IsSelected = true;
        }

    }
}
