using RAGE;
using RAGE.Game;
using RAGE.Ui;

public class Main : Events.Script
    {
    public static bool authorized = false;
    public Main()
    {
        Events.OnPlayerReady += OnPlayerReady;
        Events.OnPlayerSpawn += OnPlayerSpawn;
    }

    
    public void OnPlayerReady()
    {
        Chat.Output("darova zaebal");
    }

    public void OnPlayerSpawn(Events.CancelEventArgs cancel)
    {
        if (!authorized) { 
        Auth.CreateAuthWindow(cancel);
        }
    }


}
