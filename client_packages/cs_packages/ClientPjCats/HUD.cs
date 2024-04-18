using RAGE;
using RAGE.Elements;
using RAGE.Game;
using RAGE.Ui;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

public class HUD : Events.Script
{

    public static HtmlWindow HUDWindow;
    private int tickCounter = 0;
    public HUD()
    {
        Events.Tick += OnTick; // Додати метод OnTick до події Tick
    }

    private void OnTick(List<Events.TickNametagData> nametags)
    {
        if (HUDWindow != null) { 
        tickCounter++;
        if (tickCounter == 600)
        {
            Events.CallRemote("CLIENT:SERVER::CheckHUDData");
            tickCounter = 0;
        }
            //throw new NotImplementedException();
        }
    }

    public static void UpdateDataHUD()
    {
        try {
            SendCountPlayerToHUD();
            SendDataTimeToHUD();
            SendMoneyToHUD();
        }
        catch {
        }
    }
        public static void SendCountPlayerToHUD()
    {
        string countplayers = RAGE.Elements.Player.LocalPlayer.GetData<string>("PLAYERS_COUNT");
        string script = $"document.dispatchEvent(new CustomEvent('SendCountPlayerToHUD', {{ detail: {{ countplayers: '{countplayers}' }} }}));";
        HUDWindow.ExecuteJs(script);
    }
    public static void SendDataTimeToHUD()
    {
        //DateTime currentDate = DateTime.Now;
        string currentDate = RAGE.Elements.Player.LocalPlayer.GetData<string>("SERVER_DATE");
        string script = $"document.dispatchEvent(new CustomEvent('SendDataTimeToHUD', {{ detail: {{ data: '{currentDate}' }} }}));";
        HUDWindow.ExecuteJs(script);
    } 
    public static void SendMoneyToHUD()
    {
        string money = RAGE.Elements.Player.LocalPlayer.GetData<string>("PLAYER_MONEY");
        string script = $"document.dispatchEvent(new CustomEvent('SendMoneyToHUD', {{ detail: {{ moneyfromclient: '{money}' }} }}));";
        HUDWindow.ExecuteJs(script);
    }
    public static void CreateHudWindow()
    {
        HUDWindow = new HtmlWindow("package://cef/HUD/index.html");
        HUDWindow.Active = true;
        UpdateDataHUD();
    }
    public static void ChangeHUDVisibility()
    {
        HUDWindow.Active = true;
    }
}