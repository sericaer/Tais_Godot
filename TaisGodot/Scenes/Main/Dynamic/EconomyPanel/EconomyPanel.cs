using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Tais;
using Tais.API;

namespace TaisGodot.Scripts
{
    class EconomyPanel : Panel
    {
        public const string path = "res://Scenes/Main/Dynamic/EconomyPanel/EconomyPanel.tscn";

        private Container incomeAdjustContainer;
        private Container outputAdjustContainer;

        public override void _Ready()
        {
            var incomeObjs = GMRoot.runner.adjusts.Where(x => x.IsDefType<AdjustTaxDef>());

            foreach(var elem in incomeObjs)
            {
                var panel = ResourceLoader.Load<PackedScene>(AdjustPanel.path).Instance() as AdjustPanel;
                panel.gmObj = elem;

                MainScene.instance.AddChild(panel);
            }
        }
    }
}
