namespace Domain.Models.HeroUpgrade
{
    public interface IHero
    {
        string Name { get; }
        int Level { get; }
        int Strength { get; }
    }
}
