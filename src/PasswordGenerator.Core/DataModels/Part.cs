namespace PasswordGenerator.Core.DataModels;

public class Part
{
    public int StartingBounds { get; set; }
    public int EndingBounds { get; set; }
    public string Data { get; set; } = string.Empty;

    public int GetDesiredLength() => EndingBounds - StartingBounds;
}
