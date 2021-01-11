using Godot;
using System;
using Tais;
using Tais.API;
using Tais.Run;

namespace TaisGodot.Scripts
{
	public class MainScene : Panel
	{
		internal static readonly string path = "res://Scenes/Main/MainScene.tscn";

		internal static MainScene instance;

		public override void _Ready()
		{
			instance = this;

			GMRoot.runner.eventMgr.OBSProperty(x => x.currEvent).Subscribe(e => CreateEventDialog(e));
		}

		private void CreateEventDialog(IEvent gmObj)
		{
			if (gmObj == null)
			{
				return;
			}

			var panel = ResourceLoader.Load<PackedScene>(EventDialogPanel.path).Instance() as EventDialogPanel;
			panel.gmObj = gmObj;
			panel.gmObj.FinishNotify  = async () =>
			{
				await ToSignal(panel, "tree_exited");
				SpeedContrl.UnPause();
			};

			SpeedContrl.Pause();

			AddChild(panel);
		}

		private void _on_Speed_DaysInc()
		{
			GMRoot.runner.date.Inc();
		}
	}
}


