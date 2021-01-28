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
			GD.Print("CreateEventDialog<> 1");

			SpeedContrl.Pause();

			GD.Print("CreateEventDialog<> 2");

			var panel = ResourceLoader.Load<PackedScene>(path).Instance() as T;

			GD.Print("CreateEventDialog<> 3");

			act?.Invoke(panel);

			GD.Print("CreateEventDialog<> 4");

			instance.GetNode<CanvasLayer>("EventLayer").AddChild(panel);

			GD.Print("CreateEventDialog<> 5");

			panel.Connect("tree_exiting", instance, nameof(UnPause));

			GD.Print("CreateEventDialog<> 6");
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
				GD.Print("CreateEventDialog ACT 1");

				panel.gmObj = gmObj;
				panel.gmObj.FinishNotify = async () =>
				{
					GD.Print("CreateEventDialog AWAIT 1");
					await ToSignal(panel, "tree_exiting");
					GD.Print("CreateEventDialog AWAIT 2");
				};

				GD.Print("CreateEventDialog ACT 2");
			});
		}

		private void _on_Speed_DaysInc()
		{
			GMRoot.runner.date.Inc();
		}
	}
}


