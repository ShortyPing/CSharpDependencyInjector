// See https://aka.ms/new-console-template for more information

using DependencyInjector;
using DependencyInjector.Annotation;


[Dependency]
class PersonService
{
    public void Test()
    {
        Console.WriteLine("Test123");
    }

}

[Dependency]
class UserService
{

    [Inject]
    public PersonService PersonService;

    [ExecuteOnInit]
    public void Init()
    {
        TestPerson();
    }
    
    public void TestPerson()
    {
        PersonService.Test();
    }
}

class Program
{
    public static void Main(string[] args)
    {
        DependencyHandler handeler = new DependencyHandler();
        handeler.Start();
    }
    
}