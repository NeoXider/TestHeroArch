namespace App.Messages.Dto.HeroUpgrade
{
    public readonly struct UpgradeResultDto
    {
        public bool Success {get; }
        public HeroDto Dto { get; }
        public string? Error { get; }

        public UpgradeResultDto(bool success, HeroDto dto, string? error = null)
        {
            Success = success;
            Dto = dto;
            Error = error;
        }
    }
}