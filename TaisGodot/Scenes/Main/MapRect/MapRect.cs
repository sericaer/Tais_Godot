using Godot;
using Tais;
using System.Linq;

namespace TaisGodot.Scripts
{
	public class MapRect : TextureRect
	{
		//[Signal]
		//public delegate void MapClickSignal(int r, int g, int b);

		public MapRect()
		{
			Texture = ResourceLoader.Load<Texture>("res://Resources/image/map/depart.png");
		}
		
		// Declare member variables here. Examples:
		// private int a = 2;
		// private string b = "text";

		public override void _Ready()
		{

		}

		

		private void _on_MapRect_gui_input(object @event)
		{
			Color color;
			if (GetMousePointColor(@event, out color))
			{
				var depart = GMRoot.runner.departs.SingleOrDefault(x => x.color.Same((color.r8, color.g8, color.b8)));
				if(depart != null)
				{
					var panel = ResourceLoader.Load<PackedScene>(DepartPanel.path).Instance() as DepartPanel;
					panel.gmObj = depart;
					MainScene.instance.AddChild(panel);
				}

				//GD.Print(color.r8, color.g8, color.b8);
				//EmitSignal(nameof(MapClickSignal), color.r8, color.g8, color.b8);
			}

			// Replace with function body.

			//if (@event is InputEventMouseButton eventMouseButton && eventMouseButton.IsPressed())
			//{
			//	GD.Print("Mouse Click/Unclick at: ", eventMouseButton.Position);

			//	var pos = eventMouseButton.Position;

			//	var image = Texture.GetData();
			//	image.Lock();
			//	var color = image.GetPixel((int)pos.x, (int)pos.y);
			//	image.Unlock();

			//	GD.Print($"({color.r8}, {color.g8}, {color.b8})");
			//}

			//else if (@event is InputEventMouseMotion eventMouseMotion)
			//	GD.Print("Mouse Motion at: ", eventMouseMotion.Position);
		}

		private bool GetMousePointColor(object @event, out Color color)
		{
			if (@event is InputEventMouseButton eventMouseButton && eventMouseButton.IsPressed())
			{
				var pos = eventMouseButton.Position;

				var image = Texture.GetData();
				image.Lock();
				color = image.GetPixel((int)pos.x, (int)pos.y);
				image.Unlock();

				return true;
			}

			color = new Color();
			return false;
		}


		//  // Called every frame. 'delta' is the elapsed time since the previous frame.
		//  public override void _Process(float delta)
		//  {
		//      
		//  }
	}
}
