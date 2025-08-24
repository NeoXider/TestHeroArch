using System;

namespace App.Messages.Dto.HeroUpgrade
{
    public readonly struct HeroDto
    {
        public readonly string Name;
        public readonly int Level;
        public readonly int Strength;

        public HeroDto(string name, int level, int strength)
        {
            Name = string.IsNullOrWhiteSpace(name) ? "Hero" : name;
            Level = Math.Max(level, 1);
            Strength = Math.Max(strength, 0);
        }
    }
}