namespace Taggle.Messages;

public sealed class RouteMessage(Type viewModelBase) : ValueChangedMessage<Type>(viewModelBase);
