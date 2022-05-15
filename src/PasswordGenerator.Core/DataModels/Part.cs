using System.Text;

namespace PasswordGenerator.Core.DataModels;

public class Part
{
    public Part(int buffer) => Data = new StringBuilder(buffer);

    public int StartingBounds { get; set; }
    public int EndingBounds { get; set; }

    public int DesiredLength => EndingBounds - StartingBounds;
    public int ActualLength => Data?.Length ?? 0;

    public StringBuilder Data { get; set; }
}
