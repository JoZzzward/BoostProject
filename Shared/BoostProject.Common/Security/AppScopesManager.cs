using System.Reflection;

namespace BoostProject.Common.Security;

public class AppScopesManager
{
    public static IEnumerable<string> GetAllScopes()
        => GetAllFields()
            .Where(field => field.FieldType == typeof(string))
            .Select(field => (field.GetValue(field) as string)!)
            .ToList();

    public static Dictionary<string, string> GenerateDictionary()
        => GetAllFields()
        .Where(field => field.FieldType == typeof(string))
        .ToDictionary(
            field => (field.GetValue(field) as string)!,
            field => field.Name
            );

    public static void ImplementAllByAction(Action<string> action) => GetAllScopes().ToList().ForEach(action.Invoke);

    private static FieldInfo[] GetAllFields() => typeof(AppScopes).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
}