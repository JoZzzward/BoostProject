namespace BoostProject.Common.Extensions
{
    public static class GuidExtensions
    {
        public static string Shrink(this Guid guid)
            => guid.ToString().Replace("-", "");

        public static bool IsNullOrDefault(this Guid? guid)
        {
            return guid == null || guid.Value == Guid.Empty;
        }

        public static bool IsValidGuid(this string guidString)
        {
            if (string.IsNullOrEmpty(guidString))
                return false;

            Guid result;
            return Guid.TryParse(guidString, out result);
        }
    }
}
