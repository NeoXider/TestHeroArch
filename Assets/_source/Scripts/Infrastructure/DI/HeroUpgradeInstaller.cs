using App.Messages.Commands.HeroUpgrade;
using App.Messages.Dto.HeroUpgrade;
using App.Messages.Queries.HeroUpgrade;
using App.Ports.HeroUpgrade;
using App.UseCases.HeroUpgrade;
using Infrastructure.Config.Hero;
using Infrastructure.Persistence.HeroUpgrade;
using Infrastructure.Policies.HeroUpgrade;
using MessagePipe;
using Presentation.Presenters.HeroUpgrade;
using Presentation.ViewModels.HeroUpgrade;
using Presentation.Views.HeroUpgrade;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.DI
{
    public class HeroUpgradeInstaller : LifetimeScope
    {
        [Header("Configs")]
        [SerializeField] private HeroInitialStatsSO _initialStats;
        [SerializeField] private HeroUpgradeSO _upgradeRecipe;

        [Header("Views")]
        [SerializeField] private HeroView _heroView;

        protected override void Configure(IContainerBuilder builder)
        {
            var options = builder.RegisterMessagePipe();
            builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));

            RegisterConfig(builder);
            RegisterPorts(builder);
            RegisterUseCases(builder, options);
            RegisterViewAndPresenter(builder);
        }

        private void RegisterViewAndPresenter(IContainerBuilder builder)
        {
            builder.Register<HeroViewModel>(Lifetime.Singleton);
            builder.RegisterComponent(_heroView).As<IHeroUpgradeView>();
            builder.RegisterEntryPoint<HeroPanel>();
        }

        private void RegisterUseCases(IContainerBuilder builder, MessagePipeOptions options)
        {
            builder.RegisterAsyncRequestHandler<GetHeroDtoQuery, HeroDto, GetHeroDtoHandler>(options);
            builder.RegisterRequestHandler<UpgradeHeroCommand, UpgradeResultDto, UpgradeHeroHandler>(options);
        }

        private void RegisterPorts(IContainerBuilder builder)
        {
            builder.Register<IHeroRepository, InMemoryHeroRepository>(Lifetime.Singleton);
            builder.Register<IHeroUpgrade, UpgradePolicyFromSO>(Lifetime.Singleton);
        }

        private void RegisterConfig(IContainerBuilder builder)
        {
            if (_initialStats == null)
            {
                Debug.LogWarning("[DI] HeroInitialStatsSO is null");
            }

            if (_upgradeRecipe == null)
            {
                Debug.LogError("[DI] HeroUpgradeSO is null");
            }

            builder.RegisterInstance(_initialStats);
            builder.RegisterInstance(_upgradeRecipe);
        }
    }
}
