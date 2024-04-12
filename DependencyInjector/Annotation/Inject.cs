namespace DependencyInjector.Annotation;

[AttributeUsage(System.AttributeTargets.Field)]
public class Inject : Attribute
{
    private string?_discriminator;
    
    public string? Discriminator => _discriminator;


    public Inject(string? discriminator = null)
    {
        this._discriminator = discriminator;
    } 
    
}