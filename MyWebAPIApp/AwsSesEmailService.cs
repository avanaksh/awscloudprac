public class AwsSesEmailService : IEmailService
{
public async Task Send(string emailAddress, string body)
{
using var emailClient = new AmazonSimpleEmailServiceClient(
RegionEndpoint.EUWest1);
await emailClient.SendEmailAsync(new SendEmailRequest
{
Source = "from@example.com",
Destination = new Destination
{
ToAddresses = new List<string> { emailAddress }
},
Message = new Message
{
Subject = new Content("Email Subject"),
Body = new Body { Text = new Content(body) }
}
});
}
}