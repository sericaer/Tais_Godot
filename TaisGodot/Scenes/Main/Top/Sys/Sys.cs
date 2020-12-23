using Godot;
using System;

namespace TaisGodot.Scripts
{
	public class Sys : Button
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{

		}

		private void _on_Sys_pressed()
		{
			var panel = ResourceLoader.Load<PackedScene>(SysPanel.path).Instance();
			MainScene.instance.AddChild(panel);
		}
	}
}



