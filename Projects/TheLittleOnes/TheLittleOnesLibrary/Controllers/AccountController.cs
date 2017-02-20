using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLittleOnesLibrary.DataAccessObject;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.Handler;

namespace TheLittleOnesLibrary.Controllers
{
    public class AccountController
    {

        private static AccountController accountCtrl;
        private static AccountEntity loggedInAccount;
        private ProfileController profileCtrl;
        private ProfileEntity profileEntity;
        public static AccountController getInstance()
        {
            if (accountCtrl == null)
                accountCtrl = new AccountController();
            return accountCtrl;
        }

        // Data Access Object
        private DAO dao;
        private OleDbCommand oleDbCommand;
        private DataSet dataSet;
        // Default Constructor
        public AccountController()
        {
            dao = DAO.getInstance();
            profileCtrl = ProfileController.getInstance();
        }

        // Create account
        public AccountEntity createAccount(AccountEntity accountEntity)
        {
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("INSERT INTO ACCOUNT (ACCOUNTEMAIL,ACCOUNTPASSWORD,ACCOUNTTYPE,DATEJOINED)",
                                                         "VALUES (@ACCOUNTEMAIL,@ACCOUNTPASSWORD,@ACCOUNTTYPE, NOW());");
                oleDbCommand.Parameters.AddWithValue("@ACCOUNTEMAIL", accountEntity.AccountEmail);
                oleDbCommand.Parameters.AddWithValue("@ACCOUNTPASSWORD", accountEntity.AccountPassword);
                oleDbCommand.Parameters.AddWithValue("@ACCOUNTTYPE", accountEntity.AccountType);

                int insertID = dao.createRecord(oleDbCommand);
                if (insertID > 0)
                {
                    return new AccountEntity(insertID.ToString(),
                        accountEntity.AccountEmail,
                        accountEntity.AccountPassword,
                        accountEntity.AccountType,
                        accountEntity.DateJoined);
                }
                else
                {
                    return null;
                }
            }
        }

        // Check if email is being used before
        public bool checkEmailAddressExist(string emailAddress)
        {
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM ACCOUNT WHERE ACCOUNTEMAIL LIKE @ACCOUNTEMAIL ");
                oleDbCommand.Parameters.AddWithValue("@ACCOUNTEMAIL", string.Concat("%", emailAddress, "%"));
                if (string.IsNullOrEmpty(dao.getValue(oleDbCommand)))
                {
                    // empty = no email exists
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        // Check if password provided is the same as DB. Change password function.
        public bool checkPassword(string accountID, string accountOldPassword)
        {
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM ACCOUNT WHERE (ACCOUNTPASSWORD = @ACCOUNTPASSWORD) AND (ACCOUNTID = @ACCOUNTID)");
                //oleDbCommand.Parameters.AddWithValue("@ACCOUNTPASSWORD", string.Concat("'", accountOldPassword, "'"));
                //oleDbCommand.Parameters.AddWithValue("@ACCOUNTID", string.Concat("'", accountID, "'"));
                oleDbCommand.Parameters.AddWithValue("@ACCOUNTPASSWORD", accountOldPassword);
                oleDbCommand.Parameters.AddWithValue("@ACCOUNTID", accountID);
                LogController.LogLine(oleDbCommand.CommandText);
                if (string.IsNullOrEmpty(dao.getValue(oleDbCommand)))
                {
                    // empty = no password provided is invalid
                    return false;
                }
                else
                {
                    // password provided is valid
                    return true;
                }
            }
        }

        // Sign in account
        public AccountEntity loginAccount(string emailAddress, string password)
        {
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM ACCOUNT WHERE ACCOUNTEMAIL = @ACCOUNTEMAIL AND ACCOUNTPASSWORD = @ACCOUNTPASSWORD");
                oleDbCommand.Parameters.AddWithValue("@ACCOUNTEMAIL", emailAddress);
                oleDbCommand.Parameters.AddWithValue("@ACCOUNTPASSWORD", password);

                dataSet = dao.getRecord(oleDbCommand);
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    // logged in, instantial account 
                    loggedInAccount = new AccountEntity(
                        dataSet.Tables[0].Rows[0]["accountID"].ToString(),
                        dataSet.Tables[0].Rows[0]["accountEmail"].ToString(),
                        dataSet.Tables[0].Rows[0]["accountPassword"].ToString(),
                        dataSet.Tables[0].Rows[0]["accountType"].ToString(),
                        DateTime.Parse(dataSet.Tables[0].Rows[0]["dateJoined"].ToString()));
                    // instantial profile
                    profileEntity = profileCtrl.logInProfile(loggedInAccount);

                    return loggedInAccount;
                }
                else
                {
                    // cannot login, return null object
                    return loggedInAccount = null;
                }
            }
        }

        // Get logged in account
        public AccountEntity getLoggedInAccount()
        {
            return loggedInAccount;
        }

        // Sign out account
        public void SignOut()
        {
            loggedInAccount = null;
            // signout account

        }

        // Save edited account
        public bool saveEditedAccount(AccountEntity accountEntity)
        {
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("UPDATE ACCOUNT SET ",
                                                        "ACCOUNTPASSWORD = @ACCOUNTPASSWORD ",
                                                        "WHERE ACCOUNTID = @ACCOUNTID");
                oleDbCommand.Parameters.AddWithValue("@ACCOUNTNAME", string.Concat(accountEntity.AccountPassword));
                oleDbCommand.Parameters.AddWithValue("@ACCOUNTID", string.Concat(accountEntity.AccountID));

                int insertID = dao.updateRecord(oleDbCommand);
                if (insertID > 0)
                {
                    // reutrn edited ACCOUNTEntity
                    loggedInAccount = accountEntity;
                    return true;
                }
                else
                {
                    // reutrn unedited profileEntity
                    return false;
                }
            }
        }
    }
}
