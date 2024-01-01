namespace SigNinja.Core.Signatures;

public interface IIdentifier
{
    public int Identifier { get; }
    public void Serialize(BinaryWriter writer);
    public IIdentifier Deserialize(BinaryReader reader);
    // know that GetHashCode is used for the default comparison method, so devs should override.
}