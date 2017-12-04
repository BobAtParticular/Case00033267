using System;
using NServiceBus;

#region DefineNSBMessage

public class MyMessage :
    IMessage
{
    public string exampleId { get; set; }

    public DateTime eventDate { get; set; }
}

#endregion