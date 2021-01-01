using Godot;
using System;
using Tais;
using Tais.Run;

namespace TaisGodot.Scripts
{
	class PopPanel : Panel
	{
		public const string path = "res://Scenes/Main/Dynamic/PopPanel/PopPanel.tscn";

		internal IPop gmObj;

		private Label name;
		private ReactiveLabel num;
		private ReactiveLabel tax;
		public override void _Ready()
		{
			name = GetNode<Label>("CenterContainer/PanelContainer/VBoxContainer/Name");

			num = GetNode<ReactiveLabel>("CenterContainer/PanelContainer/VBoxContainer/StatisticContainer/GridContainer/PopNum/Value");
			tax = GetNode<ReactiveLabel>("CenterContainer/PanelContainer/VBoxContainer/StatisticContainer/GridContainer/Tax/Value");

			name.Text = gmObj.name;

			num.Assoc(gmObj.OBSProperty(x => x.num));
			tax.Assoc(gmObj.OBSProperty(x => x.tax.value));
		}
	}
}
