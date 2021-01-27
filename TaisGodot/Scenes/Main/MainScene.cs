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


		public static Node CreateEventDialog(string path)
		{
			SpeedContrl.Pause();

			var panel = ResourceLoader.Load<PackedScene>(path).Instance() as Control;
			panel.GrabFocus();

			instance.GetNode<CanvasLayer>("EventLayer").AddChild(panel);

			return panel;
		}

		private void CreateEventDialog(IEvent gmObj)
		{
			if (gmObj == null)
			{
				return;
			}

			if (gmObj is EndEvent)
			{
				CreateEventDialog(EndPanel.path);
				return;
			}

			var panel = CreateEventDialog(EventDialogPanel.path) as EventDialogPanel;
			panel.gmObj = gmObj;
			panel.gmObj.FinishNotify  = async () =>
			{
				await ToSignal(panel, "tree_exiting");
				SpeedContrl.UnPause();
			};
		}

		private void _on_Speed_DaysInc()
		{
			GMRoot.runner.date.Inc();
		}
	}
}


