namespace EnventsAndDelegates
{
    public class VideoEncoder
    {
        // 1- Define a delegate
        // 2 - define an event based in the delegate
        // 3- Raise the event / publish the event - to raise an event, we need a method responsible for it

        public delegate void VideoEncodedHandler(object source, EventArgs args);

        public event VideoEncoderHandler VideoEncoded;
        public void Encode(Video video )
        {
            Console.WriteLine("Encoding video...");
            Thread.Sleep(3000);

            OnVideoEncoded();
        }

        protected virtual void OnVideoEncoded()
        {
            if(VideoEncoded != null)
            {
                VideoEncoded(this, EventArgs.Empty);
            }
            
        }
    }
}