using System.Text;

namespace PasswordGenerator.Core.DataModels;

public class Part
{
    public Part() => Data = new StringBuilder(DesiredLength);

    public int StartingBounds { get; set; }
    public int EndingBounds { get; set; }

    public int DesiredLength => EndingBounds - StartingBounds;
    public StringBuilder Data { get; }
}
