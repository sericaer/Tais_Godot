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


		public static Node CreateEventDialog<T>(string path, Action<T> act = null) where T : Control
		{
			SpeedContrl.Pause();

			var panel = ResourceLoader.Load<PackedScene>(path).Instance() as T;

			act?.Invoke(panel);

			instance.GetNode<CanvasLayer>("EventLayer").AddChild(panel);

			panel.Connect("tree_exiting", instance, nameof(UnPause));

			return panel;
		}


		private void UnPause()
		{
			SpeedContrl.UnPause();
		}

		private void CreateEventDialog(IEvent gmObj)
		{
			GD.Print("CreateEventDialog 1");

			if (gmObj == null)
			{
				return;
			}

			GD.Print("CreateEventDialog 2");

			if (gmObj is EndEvent)
			{
				CreateEventDialog<EndPanel>(EndPanel.path);
				return;
			}

			GD.Print("CreateEventDialog 3");

			CreateEventDialog<EventDialogPanel>(EventDialogPanel.path, (panel)=>
			{
				panel.gmObj = gmObj;
				panel.gmObj.FinishNotify = async () =>
				{
					await ToSignal(panel, "tree_exiting");
				};
			});
		}

		private void _on_Speed_DaysInc()
		{
			GMRoot.runner.date.Inc();
		}
	}
}


