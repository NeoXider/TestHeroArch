using System;
using R3;

namespace Presentation.Views.HeroUpgrade
{
    public interface IHeroUpgradeView
    {
        Observable<Unit> UpgradeClicks { get; }

        void ShowName(string name);
        void ShowLevel(int level);
        void ShowStrength(int strength);

        void SetUpgradeInteractable(bool enabled);
    }
}