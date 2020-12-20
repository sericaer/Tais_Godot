using Godot;
//using GMData;
//using GMData.Mod;
using Directory = System.IO.Directory;
using File = System.IO.File;
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
			GD.Print(GlobalPath.mod);

			GMRoot.logger = (objs) => GD.Print(objs);
			GMRoot.modder = Modder.Load(GlobalPath.mod);

			//GMRoot.logger = GD.Print;
			//GMRoot.modder = new Modder(GlobalPath.mod);

			foreach (var pair in GMRoot.modder.languages)
			{
				TranslateServerEx.AddTranslate(pair.Key, pair.Value);
			}

			TranslationServer.SetLocale("zh");

			//Directory.CreateDirectory(GlobalPath.save);
		}

		private void _on_Button_Start_button_up()
		{
			//GetTree().ChangeScene(InitScene.path);
		}

		private void _on_Button_Load_pressed()
		{
			//var loadPanel = SaveLoadPanel.Instance(this, true);
			//loadPanel.Connect("LoadSaveFile", this, nameof(_on_LoadSaveFile_Signed));
		}

		private void _on_Button_Quit_pressed()
		{
			GetTree().Quit();
		}

		private void _on_LoadSaveFile_Signed(string path)
		{
			//var content = File.ReadAllText(path);
			//GMRoot.runner = GMData.Run.Runner.Deserialize(content);

			//GetTree().ChangeScene(MainScene.path);
		}
	}
}
