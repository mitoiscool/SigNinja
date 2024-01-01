using SigNinja.Core.Signatures;

namespace SigNinja.Platforms.AsmResolver.Identifiers.Method;

public class InstructionCount : IIdentifier
{
    public int Identifier => 0;

    private int _instructionCount;
    
    public void Serialize(BinaryWriter writer)
    {
        writer.Write(_instructionCount);
    }

    public void Deserialize(BinaryReader reader)
    {
        _instructionCount = reader.ReadInt32();
    }

    public int GetComparisonValue()
    {
        return _instructionCount;
    }

}