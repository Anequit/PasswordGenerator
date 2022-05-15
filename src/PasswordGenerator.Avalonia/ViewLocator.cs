using Avalonia.Controls;
using Avalonia.Controls.Templates;
using ReactiveUI;
using System;

namespace PasswordGenerator.Avalonia;

public class ViewLocator : IDataTemplate
{
    public IControl Build(object data)
    {
        string? name = data.GetType().FullName!.Replace("ViewModel", "View");
        Type? type = Type.GetType(name);

        return type != null ? (Control)Activator.CreateInstance(type)! : (IControl)new TextBlock { Text = "Not Found: " + name };
    }

    public bool Match(object data) => data is ReactiveObject;
}
