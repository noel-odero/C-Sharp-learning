namespace Interfaces
{


    public interface IMyInterface
    {
        void TestFunction();
    }

    public class Interfacepractice : IMyInterface
    {
        public void TestFunction()
        {
            Console.WriteLine("This is an interface");
            
        }
    }
}