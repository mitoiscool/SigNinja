namespace SigNinja.Core;

public class SignatureComparer<T>
{
    private readonly int _minimumSureness;
    public SignatureComparer(int sureness)
    {
        _minimumSureness = sureness;
    }

    public SignatureComparer()
    {
        _minimumSureness = 100; // default 100% sure
    }

    public bool Compare(Signature<T> sig1, Signature<T> sig2)
    {
        if (!sig1.HasIdentifiers || !sig2.HasIdentifiers)
            throw new ArgumentException("Provided signature has no identifying factors.");
        
        Signature<T> comparerSignature = sig1.IdentifierCount > sig2.IdentifierCount ? sig2 : sig1; // whichever has the lower identifier count
        Signature<T> compareeSignature = comparerSignature == sig1 ? sig2 : sig1; // opposite of comparer

        int sureness = 0;
        int perCorrectPercentage = 100 / comparerSignature.IdentifierCount;

        for (int i = 0; i < comparerSignature.IdentifierCount; i++)
        {
            IIdentifier current = comparerSignature.Identifiers[i];
            
            // first locate an equivalent identifier
            IIdentifier? equivalent = compareeSignature.GetAssignableIdentifier(current.GetType());
            
            if(equivalent == null)
                continue; // do not continue if could not resolve an equivalent signature - does this necessarily mean they are not equal though?

            if (current.GetIdentificationCode() == equivalent.GetIdentificationCode())
                sureness += perCorrectPercentage;
        }
        
        return sureness > _minimumSureness;
    }
    
    
}