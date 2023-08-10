using System.Reflection;

namespace BoostProject.Common.Security;

public class ApiResourcesManager
{
    public static IEnumerable<string> GetAllResources()
        => GetAllFields()
            .Where(field => field.FieldType == typeof(string))
            .Select(field => (field.GetValue(field) as string)!)
            .ToList();

    private static FieldInfo[] GetAllFields() => typeof(ApiResources).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
}