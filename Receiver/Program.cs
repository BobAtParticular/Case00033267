using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.MessageMutator;
using NServiceBus.Pipeline;

class Program
{
    static async Task Main()
    {
        Console.Title = "Case00033267.Receiver";

        var endpointConfiguration = new EndpointConfiguration("Case00033267.Receiver");
        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.ConnectionString("host=localhost");

        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.EnableInstallers();
        endpointConfiguration.UsePersistence<InMemoryPersistence>();

#region Case00033267 Customization
        endpointConfiguration.UseSerialization<NewtonsoftSerializer>(); //add json serializer
        endpointConfiguration.RegisterMessageMutator(new AddMessageTypeHeaderMutator()); //mutator to add type to header
#endregion

        var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }
}

public class AddMessageTypeHeaderMutator : IMutateIncomingTransportMessages
{
    public Task MutateIncoming(MutateIncomingTransportMessageContext context)
    {
        context.Headers.Add(Headers.EnclosedMessageTypes, typeof(MyMessage).AssemblyQualifiedName);
        return Task.CompletedTask;
    }
}
