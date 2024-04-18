using RAGE;
using RAGE.Elements;
using RAGE.Game;
using RAGE.Ui;
using System;
using System.Collections.Generic;
using System.Text;

public class Bussines : Events.Script
{

    public static HtmlWindow BussinesWindow;
    public static int enteridbussines;
    public static void EnterBussines(object[] args)
    {
        Ui.DisplayRadar(false);
        Chat.Activate(false);
        Cursor.ShowCursor(true, true);
        string Name = (string)args[0];
        int Owner = (int)args[1];
        int Price = (int)args[2];
        int Balance = (int)args[3];
        int MoneyGive = (int)args[4];
        int Storage = (int)args[5];
        enteridbussines = (int)args[6];

        BussinesWindow = new HtmlWindow("package://cef/busines/index.html");
        BussinesWindow.Active = true;
        string script = $"document.dispatchEvent(new CustomEvent('SendDataToBussines', {{ detail: {{ Name: '{Name}', Owner: '{Owner}', Price: '{Price}', Balance: '{Balance}', MoneyGive: '{MoneyGive}', Storage: '{Storage}', PlayerID: '{RAGE.Elements.Player.LocalPlayer.GetData<int>("PlayerID")}' }} }}));";
        BussinesWindow.ExecuteJs(script);
    }
        public static void closeBussines(object[] args)
    {
        BussinesWindow.Destroy();
        Ui.DisplayRadar(true);
        Chat.Activate(true);
        Cursor.ShowCursor(false, false);
    }
    public static void buyBussines(object[] args)
    {
        Events.CallRemote("CLIENT:SERVER::buyBussines", enteridbussines);
    }
    public static void sellBussines(object[] args)
    {
        Events.CallRemote("CLIENT:SERVER::sellBussines", enteridbussines);
    }
public static void addmoneyBussines(object[] args)
    {
        Events.CallRemote("CLIENT:SERVER::addmoneyBussines", enteridbussines);
    }
    //public static void createwaypointtobiz()
    //{
        
    //}
}
