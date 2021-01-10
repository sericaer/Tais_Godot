using Godot;
using System;

namespace TaisGodot.Scripts
{
	public class Chaoting : Button
	{
		// Declare member variables here. Examples:
		// private int a = 2;
		// private string b = "text";

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{

		}

		private void _on_ButtonChaoting_pressed()
		{
			var panel = ResourceLoader.Load<PackedScene>(ChaotingDetail.path).Instance() as ChaotingDetail;
			MainScene.instance.AddChild(panel);
		}

		//  // Called every frame. 'delta' is the elapsed time since the previous frame.
		//  public override void _Process(float delta)
		//  {
		//      
		//  }
	}

}



