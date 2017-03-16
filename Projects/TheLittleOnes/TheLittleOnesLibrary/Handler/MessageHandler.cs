using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary.Controllers;

namespace TheLittleOnesLibrary.Handler
{
    public class MessageHandler
    {
        public static void DefaultMessage(Label label, string message)
        {
            label.Text = message;
            label.ForeColor = Utility.getDefaultColor();
            LogController.LogLine(string.Concat("MessageHandler : ", message));
        }
        public static void SuccessMessage(Label label, string message)
        {
            label.Text = message;
            label.ForeColor = Utility.getSuccessColor();
        }
        public static void WarningMessage(Label label, string message)
        {
            label.Text = message;
            label.ForeColor = Utility.getWarningColor();
        }
        public static void ErrorMessage(Label label, string message)
        {
            label.Text = message;
            label.ForeColor = Utility.getErrorColor();
            LogController.LogLine(string.Concat("MessageHandler : ", message));
        }
        public static void ErrorMessageAdmin(Label label, string message)
        {
            label.Text = message;
            label.ForeColor = Utility.getErrorColor();
            LogController.LogLine(string.Concat("MessageHandler : ", message, ". Please contact admin"));
        }

        public static void ClearMessage(Label label)
        {
            label.Text = string.Empty;
            label.ForeColor = Utility.getDefaultColor();
        }
        public static void ClearMessage(TextBox textbox)
        {
            textbox.Text = string.Empty;
            textbox.ForeColor = Utility.getDefaultColor();
        }

    }
}
