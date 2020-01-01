using System;

namespace Yandex.Music.Api.Tests.Enviromental
{
    public static class Credentials
    {
        const string UserNamePath = "Yandex_Secret_User";
        const string PasswordPath = "Yandex_Secret_Pass";

        public static (string username, string password) GetCredentials()
        {
            var username = Environment.GetEnvironmentVariable(UserNamePath);
            var password = Environment.GetEnvironmentVariable(PasswordPath);

            return (username, password);
        }
    }
}
