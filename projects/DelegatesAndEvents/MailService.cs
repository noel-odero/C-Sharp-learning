namespace EnventsAndDelegates
{
    public class MailService // responsible for sending an email once video is encoded
    {
        public void OnVideoEncoded(object source, VideoEventArgs e) // handler
        {
            Console.WriteLine("MailService: Sending an email..." + e.Video.Title);

        }

    }
    
}

