using RAGE;
using RAGE.Game;
using RAGE.Ui;
using System;
using System.Collections.Generic;
using System.Text;

public class ServerEvents : Events.Script
{
    public ServerEvents() 
    {
        Events.Add("SERVER:CLIENT::LOGINSUCCESSFUL", Auth.CloseAuth);
        Events.Add("SERVER:CLIENT::LOGINFAILED", Auth.FailedAuth);
        Events.Add("SERVER:CLIENT::SendMoney", PersonChange.SendMoney);
        Events.Add("SERVER:CLIENT::SendCountPlayers", PersonChange.SendCountPlayers);
        Events.Add("SERVER:CLIENT::SendServerDate", PersonChange.SendServerDate);
        Events.Add("SERVER:CLIENT::SendCarList", Cars.SendCarList); 
        Events.Add("SERVER:CLIENT::PassHashToAuth", Auth.AutoAuth);
        Events.Add("SERVER:CLIENT::OnChekpoint", PersonChange.OnChekpoint); 
        Events.Add("SERVER:CLIENT::OnChekpointAutoSail", CarShop.EnterCarShop);
        Events.Add("SERVER:CLIENT::OnChekpointBussines", Bussines.EnterBussines); 
        Events.Add("SERVER:CLIENT::CloseBussines", Bussines.closeBussines);
        Events.Add("SERVER:CLIENT::OnChekpointDelivery", Delivery.OnChekpointDelivery);
    }

    
}