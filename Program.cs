using System.Threading;
using static System.Console;
using static System.Threading.Thread;
namespace AddWithThreads
{
    class Program
    {
        private static readonly AutoResetEvent waitHandle = new AutoResetEvent(false);

        static void Main()
        {
            WriteLine("***** Adding with Thread objects *****");
            WriteLine($"ID of thread in Main(): {CurrentThread.ManagedThreadId}");

            AddParams ap = new AddParams(10, 10);
            Thread t = new Thread(new ParameterizedThreadStart(Add));
            t.Start(ap);

            // Wait here until you are notified         
            waitHandle.WaitOne();

            WriteLine("Other thread is done!");
            ReadLine();
        }

        static void Add(object data)
        {
            if (data is AddParams)
            {
                WriteLine($"ID of thread in Add(): {CurrentThread.ManagedThreadId}");

                AddParams ap = (AddParams)data;
                WriteLine($"{ap.a} + {ap.b} is {ap.a + ap.b}");

                // Tell other thread we are done.
                waitHandle.Set();
            }
        }
    }
}