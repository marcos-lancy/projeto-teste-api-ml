using Bogus;

namespace TesteMeli.Tests.Fakes;

public static class BogusConfig
{
    public static Faker Faker => new Faker("pt_BR");

    public static Faker<T> CreateFaker<T>() where T : class
    {
        return new Faker<T>("pt_BR");
    }
}
