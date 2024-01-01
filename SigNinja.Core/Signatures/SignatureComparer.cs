namespace SigNinja.Core.Signatures;

public class SignatureComparer
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

    public bool Compare(SignatureBase sig1, SignatureBase sig2)
    {
        if (!sig1.HasIdentifiers || !sig2.HasIdentifiers)
            throw new ArgumentException("Provided signature has no identifying factors.");

        if (!sig1.GetType().IsAssignableTo(sig2.GetType()))
            return false; // they are different types, therefore they cannot be equal
        
        SignatureBase comparerSignature = sig1.IdentifierCount > sig2.IdentifierCount ? sig2 : sig1; // whichever has the lower identifier count
        SignatureBase compareeSignature = comparerSignature == sig1 ? sig2 : sig1; // opposite of comparer

        int sureness = 0;
        int perCorrectPercentage = 100 / comparerSignature.IdentifierCount;

        for (int i = 0; i < comparerSignature.IdentifierCount; i++)
        {
            IIdentifier current = comparerSignature.Identifiers[i];
            
            // first locate an equivalent identifier
            IIdentifier? equivalent = compareeSignature.GetAssignableIdentifier(current.GetType());
            
            if(equivalent == null)
                continue; // do not continue if could not resolve an equivalent signature - does this necessarily mean they are not equal though?

            if (current.GetHashCode() == equivalent.GetHashCode())
                sureness += perCorrectPercentage;
        }
        
        return sureness > _minimumSureness;
    }
    
    
}