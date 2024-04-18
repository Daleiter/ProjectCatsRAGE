using ClientPjCats;
using RAGE;
using RAGE.Ui;
using System;
using System.Collections.Generic;
using System.Text;

public class LocalEvents : Events.Script
{
    public LocalEvents()
    {
        Events.Tick += Cars.UpdateSpeedVehicle;
        Events.OnPlayerEnterVehicle += Cars.EnterVehicle;
        Events.OnPlayerLeaveVehicle += Cars.LeaveVehicle;
    }




}