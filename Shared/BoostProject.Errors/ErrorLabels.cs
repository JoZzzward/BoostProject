using BoostProject.Errors.ErrorTypes;

namespace BoostProject.Errors;

public static class ErrorLabels
{
    public readonly static GameAccountErrors GameAccount = new();
    public readonly static OrderErrors Order = new();
    public readonly static UserAccountErrors UserAccount = new();
    public readonly static FeedbackErrors Feedback = new();
    public readonly static MessageErrors Message = new();

    public const string IncorrectParameter = "IncorrectParameter";
    public const string ElementNotFound = "ElementNotFound";

    internal const string LabelNotFound = "Specific label was not found";
}
