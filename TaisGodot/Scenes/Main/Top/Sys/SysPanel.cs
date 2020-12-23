using Godot;

namespace TaisGodot.Scripts
{
	class SysPanel : Panel
	{
		public const string path = "res://Scenes/Main/Top/Sys/SysPanel.tscn";

		public override void _EnterTree()
		{
			SpeedContrl.Pause();
		}

		public override void _ExitTree()
		{
			SpeedContrl.UnPause();
		}

		private void _on_Button_Quit_pressed()
		{
			GetTree().ChangeScene(StartScene.path);
		}

		private void _on_Button_Save_pressed()
		{
			SaveLoadPanel.Instance(this, false);
		}
		
		private void _on_Button_Cancel_pressed()
		{
			QueueFree();
		}

	}
}
