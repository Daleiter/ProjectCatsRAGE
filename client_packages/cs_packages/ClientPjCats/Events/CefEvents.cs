using ClientPjCats;
using RAGE;
using RAGE.Ui;
using System;
using System.Collections.Generic;
using System.Text;

public class CefEvents : Events.Script
{
    public CefEvents() 
    {
        Events.Add("closeAuth", Auth.SendDataLoginToServer);
        Events.Add("closeHair", PersonChange.closeHairWindow);
        Events.Add("chosehair", PersonChange.chosehair);
        Events.Add("chosehaircolor", PersonChange.chosehaircolor);
        Events.Add("saveHair", PersonChange.saveHair);
        Events.Add("TaxiGoTo", PersonChange.taxigoto); 
        Events.Add("spawnCar", Cars.spawnCar); 
        Events.Add("closeSalon", CarShop.ExitCarShop);
        Events.Add("changeCarInCarShop", CarShop.ChangeCarInCarShop);
        Events.Add("buyCar", CarShop.BuyCar);
        Events.Add("changeCarColor1InCarShop", CarShop.changeCarColor1InCarShop); 
        Events.Add("changeCarColor2InCarShop", CarShop.changeCarColor2InCarShop);
        Events.Add("closeBussines", Bussines.closeBussines); 
        Events.Add("buyBussines", Bussines.buyBussines); 
        Events.Add("sellBussines", Bussines.sellBussines);
        Events.Add("addmoneyBussines", Bussines.addmoneyBussines); 
        Events.Add("closeDelivery", Delivery.closeDelivery);
        //Events.Add("cancelDelivery", Delivery.cancelDelivery);
        Events.Add("takeDeliveryOrder", Delivery.takeDeliveryOrder);
    }
    
    
    
    
}