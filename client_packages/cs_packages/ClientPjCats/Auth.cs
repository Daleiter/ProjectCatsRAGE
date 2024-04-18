using RAGE;
using RAGE.Elements;
using RAGE.Game;
using RAGE.Ui;
using System;
using System.Collections.Generic;
using System.Text;

public class Auth : Events.Script
{
    public static HtmlWindow AuthWindow;
    public static string  name, passhash;
    public static void AutoAuth(object[] args)
    {
        name = args[0].ToString();
        passhash = args[1].ToString();
    }
    public static void CreateAuthWindow(Events.CancelEventArgs cancel)
    {
        Ui.DisplayRadar(false);
        Chat.Activate(false);
        AuthWindow = new HtmlWindow("package://cef/auth/index.html");
        AuthWindow.Active = true;
        Cursor.ShowCursor(true, true);
        if (!string.IsNullOrEmpty(passhash))
        {
            AuthWindow.ExecuteJs($"document.dispatchEvent(new CustomEvent('AutoAuth', {{ detail: {{ name: '{name}', passhash: '{passhash}' }} }}));");
        }
    }
    public static void CloseAuth(object[] args)
    {
        Main.authorized = true;
        AuthWindow.Destroy();
        Cursor.ShowCursor(false, false);
        Ui.DisplayRadar(true);
        Chat.Activate(true);
        HUD.CreateHudWindow();
        RAGE.Elements.Player.LocalPlayer.SetData<int>("PlayerID", (int)args[0]);
    }
    public static void FailedAuth(object[] args)
    {
        AuthWindow.ExecuteJs("document.dispatchEvent(new Event('FailedAuth'))");
    }
    public static void SendDataLoginToServer(object[] args)
    {
        string login = args[0].ToString();
        string password = args[1].ToString();
        string ispasswordhash = args[2].ToString();
        Events.CallRemote("CLIENT:SERVER::SendLoginData", login, password, ispasswordhash);
    }
    public static void closeAuthWindow(object[] args)
    {
        AuthWindow.Destroy();
        Cursor.ShowCursor(false, false);
    }
}
