using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary;
using TheLittleOnesLibrary.Controllers;
using TheLittleOnesLibrary.Handler;
public partial class PasswordRecovery : BasePageTLO
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void BTNRecover_Click(object sender, EventArgs e)
    {
        LogController.LogLine(MethodBase.GetCurrentMethod().Name);
        string accountEmail = TBAccountEmail.Text.Trim();
        if (accountCtrler.checkEmailAddressExist(accountEmail))
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.Subject = "TheLittleOnes - Password Recovery";
                mail.From = new MailAddress("slimybacon@gmail.com");
                mail.To.Add("slimybacon@gmail.com");
                mail.Body = "This is for password recovery request from TheLittleOnes. Your password is: " + accountCtrler.getPassword(accountEmail) ;
                mail.IsBodyHtml = true;
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.EnableSsl = true;
                SmtpServer.Credentials = new System.Net.NetworkCredential("slimybacon@gmail.com", "iambacon");
                SmtpServer.Send(mail);
                MessageHandler.SuccessMessage(LBLErrorMsg, "Recovery mail has been sent to your email address");
            }
            catch (Exception ex)
            {
                MessageHandler.ErrorMessage(LBLErrorMsg, ex.Message);
            }
        }
        else {
            MessageHandler.ErrorMessage(LBLErrorMsg, "Invalid email address");
        }
    }
}