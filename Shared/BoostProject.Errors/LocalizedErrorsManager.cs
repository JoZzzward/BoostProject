using System.Globalization;
using System.Resources;

namespace BoostProject.Errors;

public static class LocalizedErrorsManager
{
    private static readonly ResourceManager _resourceManager;
    private static readonly CultureInfo _cultureInfo = CultureInfo.CurrentCulture;

    static LocalizedErrorsManager()
    {
        var en = _cultureInfo.TwoLetterISOLanguageName;
        _resourceManager = new($"Errors.{en}", typeof(LocalizedErrorsManager).Assembly);
    }

    public static string GetMessage(string errorLabel)
    {
        try
        {
            if (!IsExist(errorLabel))
                return ErrorLabels.LabelNotFound;

            var message = _resourceManager.GetString(errorLabel, _cultureInfo);

            return message;
        }
        catch (Exception)
        { 
            return "Internal server error while getting error label.";  
        }
    }

    static bool IsExist(string errorLabel)
    {
        var result = _resourceManager.GetString(errorLabel);

        return !string.IsNullOrEmpty(result);
    }
}