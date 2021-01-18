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

			SpeedContrl.Pause();

			if (gmObj is EndEvent)
			{
				GD.Print("EndEvent");

				var pEndanel = ResourceLoader.Load<PackedScene>(EndPanel.path).Instance() as EndPanel;

				AddChild(pEndanel);
				return;
			}

			var panel = ResourceLoader.Load<PackedScene>(EventDialogPanel.path).Instance() as EventDialogPanel;
			panel.gmObj = gmObj;
			panel.gmObj.FinishNotify  = async () =>
			{
				await ToSignal(panel, "tree_exiting");
				SpeedContrl.UnPause();
			};

			AddChild(panel);
		}

		private void _on_Speed_DaysInc()
		{
			GMRoot.runner.date.Inc();
		}
	}
}


