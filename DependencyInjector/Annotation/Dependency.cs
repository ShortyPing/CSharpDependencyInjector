namespace DependencyInjector.Annotation;

[AttributeUsage(System.AttributeTargets.Class)]
public class Dependency : Attribute
{
    private string? _discriminator;

    public string? Discriminator => _discriminator;

    public Dependency(string? discriminator = null)
    {
        _discriminator = discriminator;
    }
}