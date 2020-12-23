using Godot;
using System;
using System.Collections.Generic;

namespace TaisGodot.Scripts
{
	class SaveFileContainer : ScrollContainer
	{
		internal IEnumerable<SaveFileItemPanel> EnumSaveFileItems()
		{
			return GetNode<VBoxContainer>("VBoxContainer").GetChildren<SaveFileItemPanel>();
		}

		internal void GenerateFileItems(bool enableLoad, Action<string> onTriggerLoad, Action<string> onTriggerDelete)
		{
			foreach (var filePath in System.IO.Directory.EnumerateFiles(GlobalPath.save, "*.save"))
			{
				var fileItem = SaveFileItemPanel.Instance(this, enableLoad, filePath);
				fileItem.actTriggerLoad = onTriggerLoad;
				fileItem.actTriggerDelete = onTriggerDelete;
			}
		}
	}
}
