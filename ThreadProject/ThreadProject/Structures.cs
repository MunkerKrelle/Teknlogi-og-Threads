using Microsoft.Xna.Framework;
using System.Threading;

namespace ThreadProject
{
    internal class Structures : GameObject
    {
        static Semaphore MySemaphore = new Semaphore(1, 5);

        static void main() 
        {
            //for (int i = 0; i < 10; i++)
            //{
            //    new Thread(Enter(position)).Start(i);
            //}
            Thread.Sleep(500);
            MySemaphore.Release(1);
        }

        static void Enter(Vector2 workerPos)
        {
            MySemaphore.WaitOne();
            Thread.Sleep(5000);
            MySemaphore.Release(1);
        }

    }
}
