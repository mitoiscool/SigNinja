namespace SigNinja.Core.Signatures;

public interface IIdentifier
{
    public int Identifier { get; }
    public void Serialize(BinaryWriter writer);
    public void Deserialize(BinaryReader reader);
    public int GetComparisonValue();
}