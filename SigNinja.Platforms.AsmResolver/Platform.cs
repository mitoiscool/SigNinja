using SigNinja.Core.Signatures;
using SigNinja.Platforms.AsmResolver.Signatures;

namespace SigNinja.Platforms.AsmResolver;

public class Platform
{
    public static readonly Dictionary<int, Type> SignatureIdMap = new Dictionary<int, Type>()
    {
        {0, typeof(MethodSignature)}
    };

    public static readonly Dictionary<int, Type> IdentifierIdMap = new Dictionary<int, Type>();

    static Platform()
    {
        var types = typeof(Platform).Assembly.GetExportedTypes();

        foreach (var type in types)
            if(type.IsAssignableFrom(typeof(IIdentifier)))
                IdentifierIdMap.Add(((IIdentifier)Activator.CreateInstance(type)!).Identifier, type);
    }
}