using App.Messages.Dto.HeroUpgrade;
using Cysharp.Threading.Tasks;

namespace App.UseCases.HeroUpgrade
{
    public interface IUpgradeHeroHandler
    {
        UniTask<UpgradeResultDto> ExecuteAsync();
    }
}