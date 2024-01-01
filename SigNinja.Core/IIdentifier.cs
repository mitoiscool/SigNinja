namespace SigNinja.Core;

public interface IIdentifier
{
    public byte[] Serialize();
    public int GetIdentificationCode(); // intention is this should be faster than comparing every raw serialized byte
}