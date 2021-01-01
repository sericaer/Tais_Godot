using Godot;
using System;
using System.Collections.Generic;

namespace TaisGodot.Scripts
{
	class SaveFileContainer : ScrollContainer
	{
		Container container;

		public override void _Ready()
		{
			container = GetNode<VBoxContainer>("VBoxContainer");

		}

		internal IEnumerable<SaveFileItemPanel> EnumSaveFileItems()
		{
			return container.GetChildren<SaveFileItemPanel>();
		}

		internal void GenerateFileItems(bool enableLoad, Action<string> onTriggerLoad, Action<string> onTriggerDelete)
		{
			foreach (var filePath in System.IO.Directory.EnumerateFiles(GlobalPath.save, "*.save"))
			{
				var fileItem = SaveFileItemPanel.Instance(container, enableLoad, filePath);
				fileItem.actTriggerLoad = onTriggerLoad;
				fileItem.actTriggerDelete = onTriggerDelete;
			}
		}
	}
}
