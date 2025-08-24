using App.Messages.Dto.HeroUpgrade;

namespace App.Ports.HeroUpgrade
{
    using Domain.Models.HeroUpgrade;
    
    public interface IHeroUpgrade
    {
        HeroUpgradeDto CalculateUpgrade(Hero hero);
    }
}