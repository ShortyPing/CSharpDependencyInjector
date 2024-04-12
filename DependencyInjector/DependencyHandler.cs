using System.Reflection;
using DependencyInjector.Annotation;

namespace DependencyInjector;

public class DependencyHandler
{

    private Dictionary<String, Object> _registeredDependencies = new();

    public void Start()
    {
        foreach (Type type in Assembly.GetCallingAssembly().GetTypes().Where(t => t.GetCustomAttributes(typeof(Dependency)).ToList().Count >= 1 ))
        {
            AddDependency(type);
        }
    }

    private void AddDependency(Type type)
    {
        var annotation = (Dependency) type.GetCustomAttributes(typeof(Dependency), true).First();

        string discriminator = AnnotationDiscriminator(type, annotation);
        var instance = Activator.CreateInstance(type) ??
                       throw new NullReferenceException("Created instance cannot be null.");
        
        foreach (var injectField in type.GetFields().Where(f => f.GetCustomAttributes(typeof(Inject)).ToList().Count >= 1))
        {
            var injectAnnotation = (Inject) injectField.GetCustomAttributes(typeof(Inject), true).First();

            string injectDiscriminator = AnnotationDiscriminatorField(injectField.FieldType, injectAnnotation);

            if (!_registeredDependencies.ContainsKey(injectDiscriminator))
            {
                AddDependency(injectField.FieldType);
            }
            
            injectField.SetValue(instance, _registeredDependencies[injectDiscriminator]);
        }

        

        _registeredDependencies[discriminator] = instance;
        
        foreach (var method in type.GetMethods().Where(m => m.GetCustomAttributes(typeof(ExecuteOnInit)).ToList().Count >= 1))
        {
            method.Invoke(instance, null);
        }

    }

    private static string AnnotationDiscriminator(Type type, Dependency annotation)
    {
        return annotation.Discriminator ?? type.Name;
    }
    
    private static string AnnotationDiscriminatorField(Type type, Inject annotation)
    {
        return annotation.Discriminator ?? type.Name;
    }
}