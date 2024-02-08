using Microsoft.Xna.Framework;
using System.Threading;

namespace ThreadProject
{
    internal class Structures : GameObject
    {
        static public int level = 1;
        static Semaphore MySemaphore = new Semaphore(level, 5);

        static public void Enter()
        {
            MySemaphore.WaitOne();
            Thread.Sleep(1000);
            MySemaphore.Release(level);
            
        }
    }
}
