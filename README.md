# Case00033267

Reproduction for Case 00033267. A native sender, outside of the teams control, publishes a message to an NServiceBus endpoint. The message is serialized as JSON, but does not include the type information.

## Example

In this example, based on the [Native RabbitMQ sample](https://docs.particular.net/samples/rabbitmq/native-integration/), an incoming message transport mutator is added to add the header for the incoming message.

The message being received, a simplified mockup of the message payload provided, is defined as a .NET class:

https://github.com/BobAtParticular/Case00033267/blob/master/Receiver/MyMessage.cs

And the Newtonsoft JSON serializer is added to the receiver's configuration:

https://github.com/BobAtParticular/Case00033267/blob/master/Receiver/Program.cs#L22

Now we can add an incoming transport mutator:

https://github.com/BobAtParticular/Case00033267/blob/master/Receiver/Program.cs#L35

And register the mutator on the receiver:

https://github.com/BobAtParticular/Case00033267/blob/master/Receiver/Program.cs#L23

Every message that is received will have the type of the message class we defined added with the appropriate header:

https://github.com/BobAtParticular/Case00033267/blob/master/Receiver/Program.cs#L39

## Caveats

This works well if each message type is sent to a different queue. Each input queue in NServiceBus will typically be created as a separate endpoint.

An alternative (not explored in this example) to consolidate all native integration queues into one endpoint would be to create satellites to consume the native integration queues, each setting the appropriate header and forwarding the message to the endpoints input queue.

Documentation for creating satellites can be found here:

https://docs.particular.net/nservicebus/satellites/

If multiple message types are sent via the same native integration queue, the mutator (or satellite) should be modified to inspect the message headers to determine the appropriate message type.
