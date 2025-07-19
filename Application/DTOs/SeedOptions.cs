namespace BookStoreAPI.Application.DTOs;
public class SeedOptions
{
    public int? DefaultBookCount { get; set; } = 10000;
    public int? IntervalInMinutes { get; set; } = 60;
    public string? JobKey { get; set; }
    public string? Identity { get; set; }
    public int FuzzScore { get; set; } = 90;
}