using BoostProject.Common.Extensions;
using BoostProject.Common.Helpers;

namespace BoostProject.Services.UserAccountService.Helpers;

public static partial class ContentReader
{
    public static string ReadFromFile(string fileName, string userEmail, string token)
    {
        // TODO: Create common constants for default developer and production paths
        var content = PathReader.ReadContent(
                                Path.Combine(Directory.GetCurrentDirectory(), $"\\EmailPages\\{fileName}"),
                                $"/app/emailpages/{fileName}");

        content = content.Replace("QUERYEMAIL", userEmail)
                         .Replace("QUERYTOKEN", token)
                         .Replace("DATENOW", DateTimeOffset.UtcNow.ToShortStringFormat().ToString());

        return content;
    }
}
