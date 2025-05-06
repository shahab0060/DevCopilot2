namespace DevCopilot2.Domain.DTOs.Common
{
    public class BasePaginationDto
    {
        public BasePaginationDto(int takeCount, int skipCount)
        {
            TakeCount = takeCount;
            SkipCount = skipCount;
        }

        public int TakeCount { get; private set; } = 10;
        public int SkipCount { get; private set; } = 0;
    }
}
