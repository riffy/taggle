namespace Taggle.Messages;

public sealed class MainWindowRouteMessage(Type viewModelBase) : ValueChangedMessage<Type>(viewModelBase);
