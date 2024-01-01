using SigNinja.Core.Signatures;

namespace SigNinja.Core.Serialization;

public interface ISignatureSerializer
{
    public byte[] Serialize(SignatureBase signature);
    public SignatureBase Deserialize(byte[] bytes);
}