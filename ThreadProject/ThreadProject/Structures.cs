using Microsoft.Xna.Framework;
using System.Threading;

namespace ThreadProject
{
    internal class Structures : GameObject
    {
        static Semaphore MySemaphore = new Semaphore(1, 5);

        static public void Enter()
        {
            MySemaphore.WaitOne();
            Thread.Sleep(5000);
            MySemaphore.Release(1);
        }

    }
}
