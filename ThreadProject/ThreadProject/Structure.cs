using System.Threading;
using ThreadProject;

/// <summary>
/// Den her klasse opretter semaphoren, som bliver brugt til at tillade indgang i minen
/// </summary>
internal class Structure : GameObject
{

    static public int level = 1;

    /// <summary>
    /// Semaphore bruges til at styre adgangen til en delt ressource, der tillader op til 5 tråde at komme ind på én gang
    /// </summary>
    static Semaphore MySemaphore = new Semaphore(1, 5);

    /// <summary>
    /// Metode til at simulere indgang i structure
    /// </summary>
    static public void Enter()
    {
        // Venter på at semaphoren er tilgængelig
        MySemaphore.WaitOne();


        Thread.Sleep(5000);

        // Frigiver semaphoren for at tillade en anden tråd at indtaste
        MySemaphore.Release(1);
    }

    /// <summary>
    /// Metode til at opgradere strukturen, med en begrænsning på 4 opgraderinger
    /// </summary>
    static public void UpgradeMine()
    {
        if (level <= 4)
        {
            MySemaphore.Release(1);

            level++;
        }
        else
        {
            return;
        }
    }
}
