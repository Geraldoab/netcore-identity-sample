using System.Text;

namespace EmailMessageConsumer.Domain.Messages
{
    public class EmailMessage
    {
        public string Subject { get; set; }

        public string Body { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("-----------------New message found----------------");
            sb.AppendLine($"From: {From}");
            sb.AppendLine($"To: {To}");
            sb.AppendLine($"Subject: {Subject}");
            sb.AppendLine($"Body: {Body}");
            sb.AppendLine("--------------------------------------------------");

            return sb.ToString();
        }
    }
}
