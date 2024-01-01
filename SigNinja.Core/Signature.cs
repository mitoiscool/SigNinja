namespace SigNinja.Core;

public class Signature<T>
{
    
    public bool HasIdentifiers => _identifiers is { Count: > 0 };
    public IIdentifier[] Identifiers => _identifiers.ToArray();
    public int IdentifierCount => _identifiers.Count;

    
    public Signature<T> WithIdentifier(IIdentifier id)
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

    private HashSet<IIdentifier> _identifiers;
}