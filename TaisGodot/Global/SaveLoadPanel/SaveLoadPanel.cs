using Godot;
using Tais;

namespace TaisGodot.Scripts
{
	class SaveLoadPanel : Panel
	{
		public static string path = "res://Global/SaveLoadPanel/SaveLoadPanel.tscn";

		[Signal]
		internal delegate void LoadSaveFile(string name);

		internal bool enableLoad;

		private NewSaveContainer newSaveContainter;
		private SaveFileContainer saveFileContainer;

		internal static SaveLoadPanel Instance(Node parent, bool enableLoad)
		{
			var saveLoadPanel = ResourceLoader.Load<PackedScene>(path).Instance() as SaveLoadPanel;
			saveLoadPanel.enableLoad = enableLoad;

			parent.AddChild(saveLoadPanel);

			return saveLoadPanel;
		}

		public override void _Ready()
		{
			newSaveContainter = GetNode<NewSaveContainer>("CenterContainer/PanelContainer/VBoxContainer/NewSaveContainer");
			saveFileContainer = GetNode<SaveFileContainer>("CenterContainer/PanelContainer/VBoxContainer/SaveFileContainer");

			newSaveContainter.Visible = !enableLoad;
			newSaveContainter.buttonConfirm.Connect("pressed", this, nameof(onTriggerSave));

			saveFileContainer.GenerateFileItems(enableLoad, onTriggerLoad, onTriggerDelete);
		}

		private void _on_ButtonCancel_pressed()
		{
			GD.Print("_on_ButtonCancel_pressed");
			QueueFree();
		}

		private void onTriggerSave()
		{
			var filePath = GlobalPath.save + newSaveContainter.fileNameEdit.Text + ".save";

			if (System.IO.File.Exists(filePath))
			{
				MsgboxPanel.Instance(this, "STATIC_SAVE_FILE_EXIT", () =>
				{
					System.IO.File.WriteAllText(filePath, GMRoot.runner.Serialize());
					QueueFree();
				});
				return;
			}

			System.IO.File.WriteAllText(filePath, GMRoot.runner.Serialize());
			QueueFree();
		}

		private void onTriggerLoad(string path)
		{
			EmitSignal(nameof(LoadSaveFile), path);
		}

		private void onTriggerDelete(string path)
		{
			System.IO.File.Delete(path);
		}
	}
}
