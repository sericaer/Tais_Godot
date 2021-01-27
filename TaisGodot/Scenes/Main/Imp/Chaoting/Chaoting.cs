using Godot;
using System;
using System.Reactive.Linq;
using Tais;
using Tais.API;
using Tais.Run;

namespace TaisGodot.Scripts
{
	public class Chaoting : Button
	{
		IChaoting gmObj;

		decimal power;

		PowerStatus powerStatus = null;

		public override void _Ready()
		{
			gmObj = GMRoot.runner.chaoting;

			gmObj.OBSProperty(x => x.power).Skip(1).Subscribe(x => UpdatePower(x)).EndWith(this);
		}


		private void UpdatePower(decimal power)
        {
			if(powerStatus != null && gmObj.powerStatus != powerStatus)
            {
				MainScene.CreateEventDialog(this.power > power ? ChaotingPowerStatusIncPanel.path : ChaotingPowerStatusDecPanel.path);
			}

			this.powerStatus = gmObj.powerStatus;
			this.power = power;

		}

		private void _on_ButtonChaoting_pressed()
		{
			var panel = ResourceLoader.Load<PackedScene>(ChaotingDetail.path).Instance() as ChaotingDetail;
			MainScene.instance.AddChild(panel);
		}
	}

}



