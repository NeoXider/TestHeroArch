using App.Messages.Dto.HeroUpgrade;
using App.Ports.HeroUpgrade;
using Domain.Models.HeroUpgrade;
using Infrastructure.Config.Hero;

namespace Infrastructure.Policies.HeroUpgrade
{
    public class UpgradePolicyFromSO : IHeroUpgrade
    {
        private readonly HeroUpgradeSO _recipe;

        public UpgradePolicyFromSO(HeroUpgradeSO recipe)
        {
            _recipe = recipe;
        }
        
        public HeroUpgradeDto CalculateUpgrade(Hero hero)
        {
            var lvl = _recipe != null? _recipe.LevelDelta : 0;
            var str = _recipe != null? _recipe.StrengthDelta : 0;

            return new HeroUpgradeDto(lvl, str);
        }
    }
}

