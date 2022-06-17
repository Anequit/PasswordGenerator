using Avalonia;
using PasswordGenerator.Core;
using ReactiveUI;
using System.Net.Http;
using System.Reactive;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordGenerator.Avalonia.ViewModels;

public class MainWindowViewModel : ReactiveObject
{
    private string _password = string.Empty;
    private bool _isActive = false;
    private HttpClient _client;

    public MainWindowViewModel()
    {
        Version = $"v{Assembly.GetEntryAssembly()?.GetName()?.Version?.ToString(3) ?? "?.?.?"}";

        GenerateCommand = ReactiveCommand.Create(Generate);
        CopyCommand = ReactiveCommand.Create(Copy);

        _client = new HttpClient();
    }

    public string Version { get; } = string.Empty;

    public bool Symbols { get; set; } = true;

    public bool Numbers { get; set; } = true;

    public int Length { get; set; } = 1;

    public bool IsActive
    {
        get => _isActive;
        set => this.RaiseAndSetIfChanged(ref _isActive, value);
    }

    public string Password
    {
        get => _password;
        set
        {
            this.RaiseAndSetIfChanged(ref _password, value);
            IsActive = false;
        }
    }

    public ReactiveCommand<Unit, Unit> GenerateCommand { get; }
    public ReactiveCommand<Unit, Unit> CopyCommand { get; }

    private async void Generate()
    {
        IsActive = true;
        Password = await Task.Run(() => Generator.GeneratePassword(Length, Symbols, Numbers));
    }

    private void Copy() => Application.Current?.Clipboard?.SetTextAsync(_password);
}
