namespace BoostProject.Common.Application_Urls;

public static class AppUrls
{
    public static class Api
    {
        private static string Http = "https://";
        private static string Host = "localhost";
        private static string Port = "7000";
        private static string Version = "v1";


        public static string MainUri = $"{Http}{Host}:{Port}";
        public static string FullMainUri = $"{MainUri}/api/{Version}";
        
        public static class Accounts
        {
            public static string MainRoute = $"{FullMainUri}/accounts";

            private static string RegisterRoute = "register";
            public static string Register = $"{MainRoute}/{RegisterRoute}";

            private static string LoginRoute = "login";
            public static string Login = $"{MainRoute}/{LoginRoute}";

            private static string SendConfirmEmailRoute = "send-confirm-email";
            public static string SendConfirmEmail = $"{MainRoute}/{SendConfirmEmailRoute}";

            private static string ConfirmEmailRoute = "confirm-email";
            public static string ConfirmEmail = $"{MainRoute}/{ConfirmEmailRoute}";

            private static string SendRecoverPasswordRoute = "send-recover-password";
            public static string SendRecoverPassword = $"{MainRoute}/{SendRecoverPasswordRoute}";

            private static string RecoverPasswordRoute = "recover-password";
            public static string RecoverPassword = $"{MainRoute}/{RecoverPasswordRoute}";

            private static string ChangePasswordRoute = "change-password";
            public static string ChangePassword = $"{MainRoute}/{ChangePasswordRoute}"; 
        }

        public static class Feedbacks
        {
            public static string MainRoute = $"{FullMainUri}/feedbacks";

            public static string GetFeedbacks = $"{MainRoute}";

            public static string CreateFeedbacks = $"{MainRoute}";
        }

        public static class GameAccounts
        {
            public static string MainRoute = $"{FullMainUri}/gameaccounts";

            private static string GetVerifiedGameAccountsRoute = "verified";
            public static string GetVerifiedGameAccounts = $"{MainRoute}/{GetVerifiedGameAccountsRoute}";

            private static string GetUnverifiedGameAccountsRoute = "unverified";
            public static string GetUnverifiedGameAccounts = $"{MainRoute}/{GetUnverifiedGameAccountsRoute}";

            /// <summary>
            /// Must contain id in route
            /// </summary>
            public static string GetGameAccountById = $"{MainRoute}";

            /// <summary>
            /// Must contain id in route
            /// </summary>
            private static string VerifyGameAccountRoute = "verify";
            public static string VerifyGameAccount = $"{MainRoute}/{VerifyGameAccountRoute}";

            public static string CreateGameAccount = $"{MainRoute}";

            /// <summary>
            /// Must contain id in route
            /// </summary>
            public static string UpdateGameAccount = $"{MainRoute}";

            /// <summary>
            /// Must contain id in route
            /// </summary>
            public static string DeleteGameAccount = $"{MainRoute}";
        }

        public static class Orders
        {
            public static string MainRoute = $"{FullMainUri}/orders";

            public static string GetAllOrders = $"{MainRoute}";

            private static string GetUnassignedOrdersRoute = "unassigned-orders";
            public static string GetUnassignedOrders = $"{MainRoute}/{GetUnassignedOrdersRoute}";
            
            /// <summary>
            /// Must contain id in route
            /// </summary>
            public static string GetOrderById = $"{MainRoute}";

            public static string CreateOrder = $"{MainRoute}";

            /// <summary>
            /// Must contain id in route
            /// </summary>
            public static string UpdateOrder = $"{MainRoute}";

            /// <summary>
            /// Must contain id in route
            /// </summary>
            public static string DeleteOrder = $"{MainRoute}";

            /// <summary>
            /// Must contain OrderId in route and BoosterId in query
            /// </summary>
            public static string AssignOrderToBooster = $"{MainRoute}";
        }
    }

    public static class ChatsApi
    {
        private static string Http = "https://";
        private static string Host = "localhost";
        private static string Port = "7001";
        private static string Version = "v1";


        public static string MainUri = $"{Http}{Host}:{Port}";
        public static string FullMainUri = $"{MainUri}/api/{Version}";

        public static class Messages
        {
            public static string MainRoute = $"{FullMainUri}/orders";

            /// <summary>
            /// Must contain SenderId
            /// </summary>
            public static string GetMessages = $"{MainRoute}";

            public static string DeleteMessage = $"{MainRoute}";
        }

        public static class Hubs
        {
            public static string ChatHub = $"{FullMainUri}/chathub";
        }
    }

    public static class AuthorizationServer
    {
        private static string Http = "https://";
        private static string Host = "localhost";
        private static string Port = "8000";

        public static string MainUri = $"{Http}{Host}:{Port}";

        public static string AuthenticatePage = "authenticate";
        public static string ConsentPage = "consent";

        /// <summary>
        /// connect/authorize
        /// </summary>
        public static string Authorization = "connect/authorize";
        /// <summary>
        /// connect/token
        /// </summary>
        public static string Token = "connect/token";
        /// <summary>
        /// connect/logout
        /// </summary>
        public static string Logout = "connect/logout";
        /// <summary>
        /// connect/userinfo
        /// </summary>
        public static string UserInfo = "connect/userinfo";
        /// <summary>
        /// connect/introspect
        /// </summary>
        public static string Introspection = "connect/introspect";
    }

    public static class ResourceOwnerServer
    {
        private static string Http = "https://";
        private static string Host = "localhost";
        private static string Port = "8001";

        public static string MainUri = $"{Http}{Host}:{Port}";

        public static string Resources = "resources";
    }

    public static class EmailWorker
    {
        private static string Http = "https://";
        private static string Host = "localhost";
        private static string Port = "9000";

        public static string MainUri = $"{Http}{Host}:{Port}";
    }

    public static class Web
    {
        private static string Http = "https://";
        private static string Host = "localhost";
        private static string Port = "10000";

        public static string MainUri = $"{Http}{Host}:{Port}";
    }
}
