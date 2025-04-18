using System;
using System.Collections.Generic;
using UnityEngine;

namespace AxisGroupController.Data
{
    public class Settings : GameParameters.CustomParameterNode
    {
        private static Settings instance;
        public static Settings Instance
        {
            get
            {
                if (instance == null)
                {
                    if (HighLogic.CurrentGame != null)
                    {
                        instance = HighLogic.CurrentGame.Parameters.CustomParams<Settings>();
                    }
                }
                return instance;
            }
        }
        public override string Section => "AgC";
        public override string DisplaySection => "AxisGroupController";
        public override int SectionOrder => 0;  
        public override string Title => "Axis Group Controller";
        public override GameParameters.GameMode GameMode => GameParameters.GameMode.ANY;
        public override bool HasPresets => false;

        public static bool EnableController => Instance.enableController;

        [GameParameters.CustomParameterUI("Enable Controller In Flight", toolTip = "Enable the controller in flight")]
        public bool enableController = true;

        public static bool IsDebug => Instance.isDebug;

        [GameParameters.CustomParameterUI("Debug Mode", toolTip = "Special logging (May lag game)")]
        public bool isDebug = false;
    }
}
