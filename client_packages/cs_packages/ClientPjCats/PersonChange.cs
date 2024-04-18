using RAGE;
using RAGE.Ui;
using System;
using System.Collections.Generic;
using System.Text;

public class PersonChange
{
    public static HtmlWindow HairWindow; 
    public static void taxigoto(object[] args)
    {
        Events.CallRemote("CLIENT:SERVER::taxigoto", Waypointto4ka.positionWaypoint.X, Waypointto4ka.positionWaypoint.Y, Waypointto4ka.positionWaypoint.Z);
        KeyHandler.OpenPhone();
    }
    public static void SendServerDate(object[] args)
    {
        string serverDate = (string)args[0];
        RAGE.Elements.Player.LocalPlayer.SetData("SERVER_DATE", serverDate);
        HUD.UpdateDataHUD();
    }
    public static void SendCountPlayers(object[] args)
    {
        string countplayers = (string)args[0];
        RAGE.Elements.Player.LocalPlayer.SetData("PLAYERS_COUNT", countplayers);
        HUD.UpdateDataHUD();
    }
    public static void SendMoney(object[] args)
    {
        string money = (string)args[0];
        RAGE.Elements.Player.LocalPlayer.SetData("PLAYER_MONEY", money);
        HUD.UpdateDataHUD();
    }
    public static void OnChekpoint(object[] args)
    {
        Cursor.ShowCursor(true, true);
        HairWindow = new HtmlWindow("package://cef/hair/index.html");
        HairWindow.Active = true;
    }
    public static void closeHairWindow(object[] args)
    {
        HairWindow.Destroy();
        Cursor.ShowCursor(false, false);
    }
    public static void chosehair(object[] args)
    {
        Events.CallRemote("CLIENT:SERVER::ChoseHair", args[0].ToString());
    }
    public static void chosehaircolor(object[] args)
    {
        RAGE.Elements.Player.LocalPlayer.SetHairColor((int)args[0], (int)args[1]);
        Events.CallRemote("CLIENT:SERVER::ChoseHairColor", args[0].ToString());
    }
    public static void saveHair(object[] args)
    {
        Events.CallRemote("CLIENT:SERVER::saveHair", args[0].ToString(), args[1].ToString());

    }
}
