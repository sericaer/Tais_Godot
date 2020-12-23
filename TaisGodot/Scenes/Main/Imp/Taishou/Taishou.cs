using Godot;
using System;


namespace TaisGodot.Scripts
{
	public class Taishou : Button
	{
		public override void _Ready()
		{
			
		}

		private void _on_ButtonTaishou_pressed()
		{
			var panel = ResourceLoader.Load<PackedScene>(TaishouDetail.path).Instance() as TaishouDetail;
			MainScene.instance.AddChild(panel);
		}
	}
}
