namespace Taggle.Messages;

/// <summary>
/// Message that can be used to navigate the main window (frame).
/// </summary>
public sealed class MainWindowRouteMessage(Type viewModelBase) : ValueChangedMessage<Type>(viewModelBase);
