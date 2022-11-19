using PublishRabbitMQ;

#region direct message
/*
Directmessages directmessages = new Directmessages();
for (int i = 0; i < 20; i++)
{
    directmessages.SendMessage($"Direct message {i}");
}
*/
#endregion

#region topic exchange
/*
Topicmessages topicmessages = new Topicmessages();
topicmessages.SendMessage();
*/
#endregion


#region fanout exchange
/*
Fanoutmessages fanoutmessages = new Fanoutmessages();
fanoutmessages.SendMessage();
*/
#endregion

#region headers exchange
/*
Headersmessages headersmessages = new Headersmessages();
headersmessages.SendMessage();
*/
#endregion
MessageTTL ttlMessages = new MessageTTL();
ttlMessages.SendMessage("Message with ttl 10000");

Console.ReadLine();