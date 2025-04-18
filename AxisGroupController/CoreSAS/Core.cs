using System;
using System.Collections.Generic;
using UnityEngine;
using AxisGroupController.Data;

// Written by stal47

namespace AxisGroupController
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class CoreSAS : MonoBehaviour
    {
        public static Vessel ship;
        public static bool ControlledEnabled = false;

        public static float pitch;
        public static float yaw;
        public static float roll;

        public static bool Settings_ControllerEnabled;

        public bool NeedsToEnable = true;
        public void Update()
        {
            ship = FlightGlobals.ActiveVessel;

            if (Settings.IsDebug)
            {
                if (NeedsToEnable)
                {
                    NeedsToEnable = false;
                    EnableController();
                }
            }
            
        }
        public static void ToggleController()
        {
            if (ControlledEnabled)
            {
                DisableController();
            }
            else 
            { 
                EnableController();
            }
        }
        public static void EnableController()
        {
            ship.OnFlyByWire += new FlightInputCallback(SAS_Update);
            UnityEngine.Debug.Log("[AxisGroupController] Enabled Controller");
        }
        public static void DisableController() 
        {
            ship.OnFlyByWire -= new FlightInputCallback(SAS_Update);
            UnityEngine.Debug.Log("[AxisGroupController] Disabled Controller");
        }

        public static void SAS_Update(FlightCtrlState state)
        {
            // FixedUpdate() for SAS
            pitch = state.pitch;
            yaw = state.yaw;
            roll = state.roll;

            AxisGroupsModule axisGroupsModule = ship.FindVesselModuleImplementing<AxisGroupsModule>();


            axisGroupsModule.UpdateAxisGroup(KSPAxisGroup.Pitch, pitch);
            axisGroupsModule.UpdateAxisGroup(KSPAxisGroup.Yaw, yaw);
            axisGroupsModule.UpdateAxisGroup(KSPAxisGroup.Roll, roll);

            if (Settings.IsDebug)
            {
                UnityEngine.Debug.Log("[AxisGroupController:debug] Updating SAS");
            }
        }

        void OnDestroy()
        {
            DisableController();
        }

        

    }
}
