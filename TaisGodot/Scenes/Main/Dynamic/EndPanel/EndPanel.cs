using Godot;
using System;
using Tais;

namespace TaisGodot.Scripts
{
	public class EndPanel : Panel
	{
		public const string path = "res://Scenes/Main/Dynamic/EventDialogPanel/EndPanel.tscn";

		public override void _ExitTree()
		{
			GMRoot.runner = null;

			GetTree().ChangeScene(StartScene.path);
		}

		private void _on_Button_pressed()
		{
			QueueFree();
		}
	}
}


