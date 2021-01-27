using Godot;
using System;
using Tais;

namespace TaisGodot.Scripts
{
	public class EndPanel : Panel
	{
		public const string path = "res://Scenes/Main/Dynamic/EndPanel/EndPanel.tscn";


		private void _on_Button_pressed()
		{
			QueueFree();

			SpeedContrl.UnPause();

			GetTree().ChangeScene(StartScene.path);
		}
	}
}


