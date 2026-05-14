namespace EnventsAndDelegates
{
    public class MailService // responsible for sending an email once video is encoded
    {
        public void OnVideoEncoded(object source, EventArgs e) // handler
        {
            Console.WriteLine("MailService: Sending an email...");

        }

    }
    
}

