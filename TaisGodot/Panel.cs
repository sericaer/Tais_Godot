using Godot;
using System;
using System.Linq;
using System.Reflection;
using Tais;

public class Panel : Godot.Panel
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GMRoot.logger = (object[] objs) => GD.Print(objs);
		GMRoot.runner = new Tais.Run.Runner();
		GMRoot.runner.Test();
		var label = GetNode<Label>("Label");
		label.Text = GMRoot.runner.a.ToString();
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }
}
