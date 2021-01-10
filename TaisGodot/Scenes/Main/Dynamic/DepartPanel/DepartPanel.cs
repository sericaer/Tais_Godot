using Godot;
using System;
using System.Collections.Generic;
using Tais;
using Tais.Run;

namespace TaisGodot.Scripts
{
	public class DepartPanel : Panel
	{
		public const string path = "res://Scenes/Main/Dynamic/DepartPanel/DepartPanel.tscn";

		internal Depart gmObj;

		private Label name;
		private ReactiveLabel popNum;
		private ReactiveLabel tax;

		private Container popContainer;

		public override void _Ready()
		{
			name = GetNode<Label>("CenterContainer/PanelContainer/VBoxContainer/Name");
			popNum = GetNode<ReactiveLabel>("CenterContainer/PanelContainer/VBoxContainer/StatisticContainer/GridContainer/PopNum/Value");
			tax = GetNode<ReactiveLabel>("CenterContainer/PanelContainer/VBoxContainer/StatisticContainer/GridContainer/Tax/Value");
			popContainer = GetNode<Container>("CenterContainer/PanelContainer/VBoxContainer/PopContainer/GridContainer");
			
			name.Text = gmObj.name;

			popNum.Assoc(gmObj.OBSProperty(x => x.popNum));
			tax.Assoc(gmObj.OBSProperty(x => x.incomeDetail.value));

			ShowPops(gmObj.pops);
		}

		private void ShowPops(IEnumerable<IPop> pops)
		{
			foreach(var pop in pops)
			{
				var panel = ResourceLoader.Load<PackedScene>(PopBox.path).Instance() as PopBox;
				panel.gmObj = pop;
				popContainer.AddChild(panel);
			}
		}
	}
}

