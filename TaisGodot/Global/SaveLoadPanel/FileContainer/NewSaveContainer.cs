using Godot;
using System;
namespace TaisGodot.Scripts
{
	class NewSaveContainer : HBoxContainer
	{
		internal Button buttonConfirm
		{
			get
			{
				return GetNode<Button>("ButtonConfirm");
			}
		}

		internal TextEdit fileNameEdit
		{
			get
			{
				return GetNode<TextEdit>("TextEdit");
			}
		}

		private void _on_FileNameEdit_Text_Changed()
		{
			buttonConfirm.Disabled = fileNameEdit.Text.Length == 0;
		}
	}
}
