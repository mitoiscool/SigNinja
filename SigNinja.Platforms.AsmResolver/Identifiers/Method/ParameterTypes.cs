using AsmResolver.DotNet;
using SigNinja.Core.Signatures;

namespace SigNinja.Platforms.AsmResolver.Identifiers.Method;

public class ParameterTypes : IIdentifier
{
    private int _hash = 0;
    
    public ParameterTypes(MethodDefinition meth)
    {
        if (meth.Signature.ParameterTypes.Count == 0)
        {
            _hash = -1;
            return;
        }

        foreach (var typesig in meth.Signature.ParameterTypes)
        {
            _hash += typesig.GetHashCode();
        }
        
    }
    
    public int Identifier => 2;
    public void Serialize(BinaryWriter writer)
    {
        writer.Write(_hash);
    }

    public void Deserialize(BinaryReader reader)
    {
        _hash = reader.ReadInt32();
    }

    public int GetComparisonValue()
    {
        return _hash;
    }
}