using System.Threading;
using App.Messages.Commands.HeroUpgrade;
using App.Messages.Dto.HeroUpgrade;
using App.Messages.Events.HeroUpgrade;
using App.Ports.HeroUpgrade;
using Cysharp.Threading.Tasks;
using Domain.Models.HeroUpgrade;
using MessagePipe;

namespace App.UseCases.HeroUpgrade
{
    public sealed class UpgradeHeroHandler
        : IRequestHandler<UpgradeHeroCommand, UpgradeResultDto>
    {
        private readonly IHeroRepository _repository;
        private readonly IHeroUpgrade _plan;
        private readonly IPublisher<HeroChangedDto> _publisher;

        public UpgradeHeroHandler(
            IHeroRepository repository,
            IHeroUpgrade plan,
            IPublisher<HeroChangedDto> publisher)
        {
            _repository = repository;
            _plan = plan;
            _publisher = publisher;
        }

        public UpgradeResultDto Invoke(UpgradeHeroCommand request)
        {
            var hero = _repository.Get();
            var delta = _plan.CalculateUpgrade(hero);

            var updated = new Hero(
                hero.Name,
                hero.Level + delta.LevelDelta,
                hero.Strength + delta.StrengthDelta);

            _repository.Save(updated);

            var dto = new HeroDto(updated.Name, updated.Level, updated.Strength);

            _publisher.Publish(new HeroChangedDto(dto));

            return new UpgradeResultDto(true, dto);
        }
    }
}