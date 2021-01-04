using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Tais;
using Tais.Run;
using Tais.API;

namespace TaisGodot.Scripts
{
	class EconomyPanel : Panel
	{
		public const string path = "res://Scenes/Main/Dynamic/EconomyPanel/EconomyPanel.tscn";

		private Container incomeAdjustContainer;
		private Container outputAdjustContainer;

		private ReactiveLabel incomeTotal;

		public override void _Ready()
		{
			incomeAdjustContainer = GetNode<Container>("CenterContainer/PanelContainer/AdjustContainer/AdjustInputContainer/VBoxContainer");
			incomeTotal = GetNode<ReactiveLabel>("CenterContainer/PanelContainer/AdjustContainer/AdjustInputContainer/VBoxContainer/HBoxContainer/Value");

			incomeTotal.Assoc(GMRoot.runner.economy.OBSProperty(x=>x.incomeTotal));

			var incomeObj = GMRoot.runner.adjusts[ADJUST_TYPE.POP_TAX];
			var panel = ResourceLoader.Load<PackedScene>(AdjustPanel.path).Instance() as AdjustPanel;
			panel.gmObj = incomeObj;
			incomeAdjustContainer.AddChild(panel);
		}
	}
}
