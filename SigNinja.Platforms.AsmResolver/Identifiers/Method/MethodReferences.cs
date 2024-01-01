using AsmResolver.DotNet;
using SigNinja.Core.Signatures;

namespace SigNinja.Platforms.AsmResolver.Identifiers.Method;

public class MethodReferences : IIdentifier
{

    private int _methodHash;
    
    public MethodReferences(MethodDefinition def)
    {
        if (def.CilMethodBody == null)
        {
            _methodHash = -1;
            return;
        }

        foreach (var instr in def.CilMethodBody.Instructions)
        {
            if (instr.Operand is IMethodDescriptor desc)
                _methodHash = (int)(desc.FullName.GetHashCode() + _methodHash);
        }
        
    }


    public int Identifier => 1;
    public void Serialize(BinaryWriter writer)
    {
        writer.Write(_methodHash);
    }

    public void Deserialize(BinaryReader reader)
    {
        _methodHash = reader.ReadInt32();
    }

    public int GetComparisonValue()
    {
        return _methodHash;
    }
}