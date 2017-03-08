using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using TheLittleOnesLibrary.DataAccessObject;
using TheLittleOnesLibrary.Entities;
using TheLittleOnesLibrary.Handler;

namespace TheLittleOnesLibrary.Controllers
{
    public class AccountController
    {

        private static AccountController accountCtrl;
        private static AccountEntity loggedInAccount;

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
        }

        // Create account
        public AccountEntity createAccount(AccountEntity accountEntity)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                if (accountEntity.ShopInfoEntity == null)
                {
                    oleDbCommand.CommandText = string.Concat("INSERT INTO ACCOUNT ( ACCOUNTEMAIL,ACCOUNTPASSWORD,ACCOUNTTYPE, DATEJOINED)",
                                                         "VALUES (@ACCOUNTEMAIL,@ACCOUNTPASSWORD,@ACCOUNTTYPE, NOW());");
                }
                else
                {
                    oleDbCommand.CommandText = string.Concat("INSERT INTO ACCOUNT ( SHOPINFOID, ACCOUNTEMAIL,ACCOUNTPASSWORD,ACCOUNTTYPE, DATEJOINED)",
                                                         "VALUES (@SHOPINFOID, @ACCOUNTEMAIL,@ACCOUNTPASSWORD,@ACCOUNTTYPE, NOW());");
                    oleDbCommand.Parameters.AddWithValue("@SHOPINFOID", accountEntity.ShopInfoEntity.ShopInfoID);
                }
                oleDbCommand.Parameters.AddWithValue("@ACCOUNTEMAIL", accountEntity.AccountEmail);
                oleDbCommand.Parameters.AddWithValue("@ACCOUNTPASSWORD", accountEntity.AccountPassword);
                oleDbCommand.Parameters.AddWithValue("@ACCOUNTTYPE", accountEntity.AccountType);

                int insertID = dao.createRecord(oleDbCommand);
                if (insertID > 0)
                {
                    accountEntity.AccountID = insertID.ToString();
                    ProfileController.getInstance().createProfile(accountEntity.ProfileEntity, accountEntity.AccountID);
                    return accountEntity;
                }
                else
                {
                    return null;
                }
            }
        }

        // Update account password
        public AccountEntity changePassword(AccountEntity accountEntity)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("UPDATE ACCOUNT SET ACCOUNTPASSWORD = @ACCOUNTPASSWORD WHERE ACCOUNTID = @ACCOUNTID");
                oleDbCommand.Parameters.AddWithValue("@ACCOUNTPASSWORD", accountEntity.AccountPassword);
                oleDbCommand.Parameters.AddWithValue("@ACCOUNTID", accountEntity.AccountID);

                int insertID = dao.updateRecord(oleDbCommand);
                if (insertID > 0)
                {
                    // return edited accountEntity
                    loggedInAccount = accountEntity;
                }
                // return unedited accountEntity
                return loggedInAccount;
            }
        }

        // Retrieve account info
        public AccountEntity getAccount(string accountID)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM ACCOUNT WHERE ACCOUNTID = @ACCOUNTID");
                oleDbCommand.Parameters.AddWithValue("@ACCOUNTID", accountID);

                dataSet = dao.getRecord(oleDbCommand);
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    // logged in, instantial account 
                    loggedInAccount = new AccountEntity(
                        dataSet.Tables[0].Rows[0]["accountID"].ToString(),
                        dataSet.Tables[0].Rows[0]["accountEmail"].ToString(),
                        dataSet.Tables[0].Rows[0]["accountPassword"].ToString(),
                        dataSet.Tables[0].Rows[0]["accountType"].ToString(),
                        ProfileController.getInstance().logInProfile(accountID),
                        ShopInfoController.getInstance().getShopInfo(dataSet.Tables[0].Rows[0]["shopInfoID"].ToString()),
                        DateTime.Parse(dataSet.Tables[0].Rows[0]["dateJoined"].ToString()));
                    return loggedInAccount;
                }
                else
                {
                    // cannot login, return null object
                    return loggedInAccount = null;
                }
            }
        }

        // Check if email is being used before
        public bool checkEmailAddressExist(string emailAddress)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
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
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = string.Concat("SELECT * FROM ACCOUNT WHERE (ACCOUNTPASSWORD = @ACCOUNTPASSWORD) AND (ACCOUNTID = @ACCOUNTID)");
                oleDbCommand.Parameters.AddWithValue("@ACCOUNTPASSWORD", accountOldPassword);
                oleDbCommand.Parameters.AddWithValue("@ACCOUNTID", accountID);

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

        // Get logged in account
        public AccountEntity getLoggedInAccount()
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            return loggedInAccount;
        }

        // Sign in account
        public AccountEntity loginAccount(string emailAddress, string password)
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
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
                        ProfileController.getInstance().logInProfile(dataSet.Tables[0].Rows[0]["accountID"].ToString()),
                        string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["shopInfoID"].ToString()) ? null :
                        ShopInfoController.getInstance().getShopInfo(dataSet.Tables[0].Rows[0]["shopInfoID"].ToString()),
                        DateTime.Parse(dataSet.Tables[0].Rows[0]["dateJoined"].ToString()));
                    return loggedInAccount;
                }
                else
                {
                    // cannot login, return null object
                    return loggedInAccount = null;
                }
            }
        }

        // Sign out account
        public void SignOut()
        {
            LogController.LogLine(MethodBase.GetCurrentMethod().Name);
            loggedInAccount = null;
            ProfileController.getInstance().SignOut();
            // signout account

        }

        // Filter Data
        public DataTable filterAccountInfoData(string filterAccountType, string tbSearchValue, Label LBLSearchResultSystemAccount)
        {
            LBLSearchResultSystemAccount.ForeColor = Utility.getColorWhite();
            using (oleDbCommand = new OleDbCommand())
            {
                oleDbCommand.CommandType = CommandType.Text;
                string sqlQuery = string.Concat("SELECT Account.shopInfoID, Account.accountID, Account.accountEmail, Account.accountType, Account.dateJoined, Profile.profileID, Profile.profileName, Profile.profileContact, Profile.profileAddress ",
                    " FROM ACCOUNT INNER JOIN PROFILE ON ACCOUNT.ACCOUNTID = PROFILE.ACCOUNTID WHERE ",
                    "( ACCOUNTEMAIL LIKE @SEARCHVALUE OR ",
                    "  PROFILENAME LIKE @SEARCHVALUE OR ",
                    "  PROFILECONTACT LIKE @SEARCHVALUE ) "
                    );

                LBLSearchResultSystemAccount.Text = "Records for Account Info ";
                if (!string.IsNullOrEmpty(tbSearchValue))
                {
                    LBLSearchResultSystemAccount.Text += string.Concat("\"", tbSearchValue, "\" ");
                }

                if (!string.IsNullOrEmpty(filterAccountType))
                {
                    LBLSearchResultSystemAccount.Text += string.Concat("\"", filterAccountType, "\" ");
                    sqlQuery += string.Concat(" AND (ACCOUNTTYPE LIKE '", filterAccountType, "') ");
                }

                oleDbCommand.CommandText = string.Concat(sqlQuery, "ORDER BY ACCOUNT.ACCOUNTID DESC");
                oleDbCommand.Parameters.AddWithValue("@SEARCHVALUE", string.Concat("%", tbSearchValue, "%"));

                dataSet = dao.getRecord(oleDbCommand);
                return dataSet.Tables[0];
            }
        }
    }
}
