using System;

namespace Domain.Models.HeroUpgrade
{
    public sealed class Hero : IHero
    {
        public string Name { get; }
        public int Level { get; }
        public int Strength { get; }

        public Hero(string name = "Hero", int level = 1, int strength = 0)
        {
            Name = string.IsNullOrWhiteSpace(name) ? "New Hero" : name;
            Level = Math.Max(level, 1);
            Strength = Math.Max(strength, 0);
        }
    }
}