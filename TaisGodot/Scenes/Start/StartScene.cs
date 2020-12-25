using Godot;

using System.Linq;
using Tais;
using Tais.Mod;

namespace TaisGodot.Scripts
{
	public class StartScene : Panel
	{
		public const string path = "res://Scenes/Start/StartScene.tscn";

		static StartScene()
		{
			UserSetting.lang = "zh";

			GMRoot.logger = (objs) => GD.Print(objs);
			GMRoot.modder = Modder.Load(GlobalPath.mod);

			foreach (var pair in GMRoot.modder.languages)
			{
				TranslateServerEx.AddTranslate(pair.Key, pair.Value);
			}


			//TranslateServerEx.SetLocale("zh");

			//Directory.CreateDirectory(GlobalPath.save);
		}

		private void _on_Button_Start_pressed()
		{
			GetTree().ChangeScene(InitScene.path);
		}

		private void _on_Button_Load_pressed()
		{
			var loadPanel = SaveLoadPanel.Instance(this, true);
			loadPanel.Connect("LoadSaveFile", this, nameof(_on_LoadSaveFile_Signed));
		}

		private void _on_Button_Quit_pressed()
		{
			GetTree().Quit();
		}

		private void _on_LoadSaveFile_Signed(string path)
		{
			var content = System.IO.File.ReadAllText(path);
			GMRoot.runner = Tais.Run.Runner.Deserialize(content);

			GetTree().ChangeScene(MainScene.path);
		}
	}
}
