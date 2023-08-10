namespace BoostProject.Common.Security;

public static class AppScopes
{
    public const string UserAccountAccess = "access_for_user_account"; 
    public const string MessageAccess = "access_for_messages"; 
    public const string MessageWrite = "access_to_write_messages"; 

    public const string FeedbackWrite = "access_to_write_feedbacks";

    public const string GameAccountsWrite = "access_to_write_gameaccounts";
    public const string GetUnverifiedGameAccounts = "access_for_getting_unverified_game_accounts";
    public const string VerifyAccount = "access_for_account_confirmations";

    public const string OrdersAccess = "access_for_orders";
}
