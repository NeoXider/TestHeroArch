namespace App.Messages.Dto.HeroUpgrade
{
    public readonly struct HeroUpgradeDto
    {
        public int LevelDelta { get; }
        public int StrengthDelta { get; }

        public HeroUpgradeDto(
            int levelDelta,
            int strengthDelta)
        {
            LevelDelta = levelDelta > 0 ? levelDelta : 0;
            StrengthDelta = strengthDelta > 0 ? strengthDelta : 0;
        }
    }
}