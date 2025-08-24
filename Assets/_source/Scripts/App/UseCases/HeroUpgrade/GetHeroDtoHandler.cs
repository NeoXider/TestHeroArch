using System.Threading;
using App.Messages.Dto.HeroUpgrade;
using App.Messages.Queries.HeroUpgrade;
using App.Ports.HeroUpgrade;
using Cysharp.Threading.Tasks;
using MessagePipe;

namespace App.UseCases.HeroUpgrade
{
    public sealed class GetHeroDtoHandler
        : IAsyncRequestHandler<GetHeroDtoQuery, HeroDto>
    {
        private readonly IHeroRepository _repo;

        public GetHeroDtoHandler(IHeroRepository repo)
        {
            _repo = repo;
        }

        public UniTask<HeroDto> InvokeAsync(GetHeroDtoQuery request, CancellationToken ct)
        {
            var hero = _repo.Get();
            return UniTask.FromResult(new HeroDto(hero.Name, hero.Level, hero.Strength));
        }
    }
}