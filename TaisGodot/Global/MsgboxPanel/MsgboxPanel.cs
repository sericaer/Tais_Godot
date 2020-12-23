using Godot;
using System;

namespace TaisGodot.Scripts
{
	class MsgboxPanel : Panel
	{
		public string desc;
		public Action action;

		public override void _Ready()
		{
			GetNode<RichTextLabel>("CenterContainer/PanelContainer/VBoxContainer/Desc").Text = desc;
		}

		internal static MsgboxPanel Instance(Node parent, string desc, Action action)
		{
			var panel = (MsgboxPanel)ResourceLoader.Load<PackedScene>("res://Global/MsgboxPanel/MsgboxPanel.tscn").Instance();
			panel.desc = desc;
			panel.action = action;

			parent.AddChild(panel);

			return panel;
		}

		private void _on_ButtonConfirm_pressed()
		{
			action?.Invoke();
			QueueFree();
		}

		private void _on_ButtonCancel_pressed()
		{
			QueueFree();
		}
	}
}


