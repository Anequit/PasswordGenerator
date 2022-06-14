using PasswordGenerator.Core;

namespace PasswordGenerator.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseSwagger();

        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.MapGet("/", () => 
        {
            Results.Redirect("/swagger/index.html");
        });

        app.MapGet("/generate", (int length, bool? containsSymbols, bool? containsNumbers) =>
        {
            if(length > 2048)
                return "Length is capped at 2048";

            else if(length <= 0)
                return "Length must be at least 1";

            bool symbols = containsSymbols ?? true;
            bool numbers = containsNumbers ?? true;

            return Generator.GeneratePassword(length, symbols, numbers);
        });

        app.Run();
    }
}