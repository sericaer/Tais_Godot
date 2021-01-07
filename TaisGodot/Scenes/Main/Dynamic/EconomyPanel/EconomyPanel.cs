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
		private ReactiveLabel outputTotal;

		public override void _Ready()
		{
			incomeAdjustContainer = GetNode<Container>("CenterContainer/PanelContainer/AdjustContainer/AdjustInputContainer/VBoxContainer");
			outputAdjustContainer = GetNode<Container>("CenterContainer/PanelContainer/AdjustContainer/AdjustOutputContainer/VBoxContainer");

			incomeTotal = GetNode<ReactiveLabel>("CenterContainer/PanelContainer/AdjustContainer/AdjustInputContainer/VBoxContainer/HBoxContainer/Value");
			outputTotal = GetNode<ReactiveLabel>("CenterContainer/PanelContainer/AdjustContainer/AdjustOutputContainer/VBoxContainer/HBoxContainer/Value");

			incomeTotal.Assoc(GMRoot.runner.economy.OBSProperty(x => x.incomeTotal));

			outputTotal.Assoc(GMRoot.runner.economy.OBSProperty(x => x.outputTotal));

			AddIncomeAdjust(ADJUST_TYPE.POP_TAX);
			AddOutputAdjust(ADJUST_TYPE.CHAOTING_TAX);

		}

		private void AddOutputAdjust(ADJUST_TYPE type)
		{
			var incomeObj = GMRoot.runner.adjusts[type];
			var panel = ResourceLoader.Load<PackedScene>(AdjustPanel.path).Instance() as AdjustPanel;
			panel.gmObj = incomeObj;
			outputAdjustContainer.AddChild(panel);
		}

		private void AddIncomeAdjust(ADJUST_TYPE type)
		{
			var incomeObj = GMRoot.runner.adjusts[type];
			var panel = ResourceLoader.Load<PackedScene>(AdjustPanel.path).Instance() as AdjustPanel;
			panel.gmObj = incomeObj;
			incomeAdjustContainer.AddChild(panel);
		}
	}
}
