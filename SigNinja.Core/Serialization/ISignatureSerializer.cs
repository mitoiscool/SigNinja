using SigNinja.Core.Signatures;

namespace SigNinja.Core.Serialization;

public interface ISignatureSerializer
{
    public byte[] Serialize(Signature signature);
    public Signature Deserialize(byte[] bytes);
}