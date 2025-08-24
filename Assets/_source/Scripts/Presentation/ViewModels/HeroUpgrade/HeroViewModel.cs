using App.Messages.Dto.HeroUpgrade;
using R3;

namespace Presentation.ViewModels.HeroUpgrade
{
    public sealed class HeroViewModel
    {
        public ReactiveProperty<string> Name { get; } = new("");
        public ReactiveProperty<int> Level { get; } = new(1);
        public ReactiveProperty<int> Strength { get; } = new(0);

        public void Apply(HeroDto s)
        {
            Name.Value = s.Name ?? "";
            Level.Value = s.Level;
            Strength.Value = s.Strength;
        }
    }
}