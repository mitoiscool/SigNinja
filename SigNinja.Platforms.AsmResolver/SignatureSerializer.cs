using SigNinja.Core.Serialization;
using SigNinja.Core.Signatures;

namespace SigNinja.Platforms.AsmResolver;

public class SignatureSerializer : ISignatureSerializer
{
    public byte[] Serialize(SignatureBase signature)
    {
        using var ms = new MemoryStream();
        using var writer = new BinaryWriter(ms);

        int sigIdentifier = Platform.SignatureIdMap.Single(x => x.Value == signature.GetType()).Key;
        
        writer.Write(sigIdentifier);
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
        using var ms = new MemoryStream(bytes);
        using var reader = new BinaryReader(ms);

        int sigIdentifier = reader.ReadInt32();
        SignatureBase signature = (SignatureBase)Activator.CreateInstance(Platform.SignatureIdMap[sigIdentifier])!;
        
        // read signature name
        signature.SignatureName = reader.ReadString();

        int identifierCount = reader.ReadInt32();

        for (int i = 0; i < identifierCount; i++)
        {
            // read type
            int identifierType = reader.ReadInt32();
            
            // resolve identifier
            IIdentifier identifier = (IIdentifier)Activator.CreateInstance(Platform.IdentifierIdMap[identifierType])!;

            identifier.Deserialize(reader);

            signature.WithIdentifier(identifier);
        }

        return signature;
    }
}