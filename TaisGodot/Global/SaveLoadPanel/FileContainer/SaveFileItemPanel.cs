using System;
using Godot;

namespace TaisGodot.Scripts
{
	class SaveFileItemPanel : PanelContainer
	{
		public const string panelPath = "res://Global/SaveLoadPanel/FileContainer/SaveFileItem.tscn";

		internal string filePath;

		internal Action<string> actTriggerLoad;
		internal Action<string> actTriggerDelete;

		internal static SaveFileItemPanel Instance(Node parent, bool enableLoad, string filePath)
		{
			var panel = (SaveFileItemPanel)ResourceLoader.Load<PackedScene>(panelPath).Instance();
			panel.filePath = filePath;
			parent.AddChild(panel);

			return panel;
		}

		public override void _Ready()
		{
			GetNode<Label>("HBoxContainer/Label").Text = System.IO.Path.GetFileNameWithoutExtension(filePath);

			var buttonLoad = GetNode<Button>("HBoxContainer/ButtonLoad");

			buttonLoad.Visible = this.GetParentRecursion<SaveLoadPanel>().enableLoad;
		}

		private void _on_ButtonDelete_pressed()
		{
			actTriggerDelete(filePath);
			QueueFree();
		}

		private void _on_ButtonLoad_pressed()
		{
			actTriggerLoad(filePath);
		}
	}
}
