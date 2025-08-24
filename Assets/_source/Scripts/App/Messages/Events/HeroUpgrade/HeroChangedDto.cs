using App.Messages.Dto.HeroUpgrade;

namespace App.Messages.Events.HeroUpgrade
{
    public readonly struct HeroChangedDto
    {
        public readonly HeroDto Dto;
        public HeroChangedDto(HeroDto dto) => Dto = dto;
    }
}