using Godot;
using System;
using System.Linq;
using Tais;
using Tais.API;

namespace TaisGodot.Scripts
{
	public class InitScene : Control
	{
		internal static string path = "res://Scenes/Init/InitScene.tscn";

		public override void _Ready()
		{
			var initNameAgePanel = InitNameAgePanel.Instance();
			AddChild(initNameAgePanel);

			initNameAgePanel.Connect("Finish", this, nameof(_on_SelectNameAgeFinish_Signal));
		}

		private void _on_SelectNameAgeFinish_Signal()
		{
			var firstInitSelect = GMRoot.modder.initSelects.Single(x => x.IsFirst);

			CreateInitSelectPanel(firstInitSelect);
		}

		private void CreateInitSelectPanel(InitSelect initSelect)
		{
			var initSelectPanel = InitSelectPanel.Instance();

			initSelectPanel.gmObj = initSelect;

			initSelectPanel.Connect("SelectNext", this, nameof(_on_SelectNext_Signal));

			AddChild(initSelectPanel);
		}

		private void _on_SelectNext_Signal(InitSelect nextSelect)
		{
			if (nextSelect == null)
			{
				//GMRoot.runner = GMData.Run.Runner.Generate();
				//GetTree().ChangeScene(MainScene.path);
				return;
			}
			CreateInitSelectPanel(nextSelect);
		}
	}
}

