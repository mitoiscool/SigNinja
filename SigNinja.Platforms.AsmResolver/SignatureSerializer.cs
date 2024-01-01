using SigNinja.Core.Serialization;
using SigNinja.Core.Signatures;

namespace SigNinja.Platforms.AsmResolver;

public class SignatureSerializer : ISignatureSerializer
{
    public byte[] Serialize(SignatureBase signature)
    {
        using var ms = new MemoryStream();
        using var writer = new BinaryWriter(ms);

        writer.Write(signature.SignatureName);

        if (!signature.HasIdentifiers)
            throw new ArgumentException("Signature has no identifiers.");
        
        writer.Write(signature.IdentifierCount);

        foreach (var identifier in signature.Identifiers)
        {
            writer.Write(identifier.Identifier);
            identifier.Serialize(writer);
        }

        return ms.ToArray();
    }

    public SignatureBase Deserialize(byte[] bytes)
    {
        throw new NotImplementedException();
    }
}