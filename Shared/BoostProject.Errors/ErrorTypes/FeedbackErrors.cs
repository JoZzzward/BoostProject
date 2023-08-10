namespace BoostProject.Errors.ErrorTypes;

public class FeedbackErrors
{
    public readonly string FeedbackContentIsRequired = "ContentIsRequired";

    public readonly string FeedbackContentMustBeLessThan250Symbols = "FeedbackContentMustBeLessThan250Symbols";

    public readonly string UserNotAllowToCreateTheFeedback = "UserNotAllowToCreateTheFeedback";
}