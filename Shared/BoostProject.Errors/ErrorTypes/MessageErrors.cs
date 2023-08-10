namespace BoostProject.Errors.ErrorTypes;

public class MessageErrors
{
    public readonly string MessageContentIsRequired = "ContentIsRequired";

    public readonly string MessageContentMustBeLessThan250Symbols = "MessageContentMustBeLessThan250Symbols";

    public readonly string UserIdIsIncorrect = "UserIdIsIncorrect";

    public readonly string MessageBelongsToAnotherUser = "MessageBelongsToAnotherUser";
}