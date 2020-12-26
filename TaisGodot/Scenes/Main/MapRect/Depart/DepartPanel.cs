using Godot;
using System;
using Tais.API;

public class DepartPanel : Panel
{
	public const string path = "res://Scenes/Main/MapRect/Depart/DepartPanel.tscn";

	internal IDepart gmObj;
	internal new Label Name;
	public override void _Ready()
	{
		Name = GetNode<Label>("CenterContainer/PanelContainer/VBoxContainer/Name");

		Name.Text = gmObj.GetType().FullName;
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
