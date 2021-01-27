using Godot;
using System;
using Tais;
using Tais.Run;

namespace TaisGodot.Scripts
{
	public class ChaotingPowerStatusIncPanel : Panel
	{
		public const string path = "res://Scenes/Main/Dynamic/ChaotingPowerStatusChangedPanel/ChaotingPowerStatusIncPanel.tscn";

		IChaoting gmObj;

		RichTextLabel content;

		public override void _Ready()
		{
			gmObj = GMRoot.runner.chaoting;

			content = GetNode<RichTextLabel>("");

			content.Text = TranslateServerEx.Translate("CHAOTING_POWER_STATUS_DEC_DESC", gmObj.powerStatus.desc);

		}

		private void _on_Button_pressed()
		{
			SpeedContrl.UnPause();
		}
	}
}


