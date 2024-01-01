namespace SigNinja.Core.Signatures;

public abstract class SignatureBase
{
    public bool HasIdentifiers => _identifiers is { Count: > 0 };
    public IIdentifier[] Identifiers => _identifiers.ToArray();
    public int IdentifierCount => _identifiers.Count;

    
    public SignatureBase WithIdentifier(IIdentifier id)
    {
        _identifiers.Add(id);
        return this;
    }

    public IIdentifier? GetAssignableIdentifier(Type t)
    {
        foreach (var identifier in _identifiers)
            if (identifier.GetType().IsAssignableTo(t))
                return identifier;

        return null;
    }

    private readonly HashSet<IIdentifier> _identifiers = new HashSet<IIdentifier>();
}