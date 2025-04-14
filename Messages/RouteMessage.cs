namespace Taggle.Messages;

public sealed class RouteMessage(Type routeToService) : ValueChangedMessage<Type>(routeToService);
