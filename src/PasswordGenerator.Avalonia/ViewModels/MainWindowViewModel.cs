using Avalonia;
using PasswordGenerator.Core;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reflection;
using System.Threading.Tasks;

namespace PasswordGenerator.Avalonia.ViewModels;

public class MainWindowViewModel : ReactiveObject
{
    private string _password = string.Empty;
    private bool _isActive = false;

    public MainWindowViewModel()
    {
        Version = $"v{Assembly.GetEntryAssembly()?.GetName().Version?.ToString(3) ?? "?.?.?"}";

        GenerateCommand = ReactiveCommand.CreateFromTask(GenerateAsync);
        CopyCommand = ReactiveCommand.Create(Copy);
    }

    public string Version { get; }

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
        get => string.IsNullOrEmpty(_password) || _password.Length <= 100 ? _password : _password.Substring(0, 100);
        set
        {
            this.RaiseAndSetIfChanged(ref _password, value);
            IsActive = false;
        }
    }

    public ReactiveCommand<Unit, Unit> GenerateCommand { get; }
    public ReactiveCommand<Unit, Unit> CopyCommand { get; }

    private async Task GenerateAsync()
    {
        IsActive = true;
        Password = string.Empty;
        
        // This is done to clear up the previously generated password.
        GC.Collect();
        
        Password = await Task.Run(() => Generator.GeneratePassword(Length, Symbols, Numbers));
        
        // This is to clear up any allocations left over from the generator.
        GC.Collect();
    }

    private void Copy() => Application.Current?.Clipboard?.SetTextAsync(_password);
}
