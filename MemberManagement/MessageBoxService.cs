using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MemberManagement
{
    public static class MessageBoxService
    {
        public static void MessageBoxServiceShow(string message)
        {
            MessageBox.Show(message);
        }

        public static MessageBoxResult MessageBoxServiceShow(string message, string header)
        {
            MessageBoxResult result = MessageBox.Show(message, header, MessageBoxButton.YesNo);
            return result;
        }
    }
}
