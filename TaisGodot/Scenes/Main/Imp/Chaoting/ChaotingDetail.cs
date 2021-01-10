using Godot;
using System;
using Tais;
using Tais.Run;

namespace TaisGodot.Scripts
{
	public class ChaotingDetail : Panel
	{
		public const string path = "res://Scenes/Main/Imp/Chaoting/ChaotingDetail.tscn";
		Button btnFullFill;

		IChaoting gmObj;

		Label yearTaxStatus;

		public override void _Ready()
		{
			//btnFullFill = GetNode<Button>("CenterContainer/PanelContainer/HBoxContainer/LeftContainer2/VBoxContainer/VBoxContainer/ButtonContainer/ButtonFullFill");
			//btnFullFill.Connect("pressed", this, nameof(_onButtonFullFillCountryTax));

			yearTaxStatus = GetNode<ReactiveLabel>("CenterContainer/PanelContainer/HBoxContainer/LeftContainer/VBoxContainer/VBoxContainer/GridContainer/TaxLevel/Value");

			gmObj = GMRoot.runner.chaoting;

			gmObj.OBSProperty(x => x.reportYearTax).Subscribe(_ => UpdateYearTaxStatus()).EndWith(this);
			gmObj.OBSProperty(x => x.expectYearTax).Subscribe(_ => UpdateYearTaxStatus()).EndWith(this);

			//UpdateFullFillCountryTax();
		}

		private void UpdateYearTaxStatus()
		{
			yearTaxStatus.Text = (gmObj.reportYearTax - gmObj.expectYearTax).ToString();
		}

		//private void UpdateFullFillCountryTax()
		//{
		//	var lackCountryTax = RunData.Chaoting.inst.expectYearTax.Value - RunData.Chaoting.inst.realYearTax.Value;
		//	if (lackCountryTax == 0)
		//	{
		//		btnFullFill.Disabled = true;
		//		btnFullFill.HintTooltip = TranslateServerEx.Translate("STATIC_COUNTRY_TAX_NOT_LACK");
		//	}
		//	else if (RunData.Economy.inst.curr.Value < lackCountryTax)
		//	{
		//		btnFullFill.Disabled = true;
		//		btnFullFill.HintTooltip = TranslateServerEx.Translate("STATIC_COUNTRY_TAX_LACK_AND_ECONOMY_NOT_SUFFICENT",
		//			lackCountryTax.ToString(),
		//			RunData.Economy.inst.curr.Value.ToString());
		//	}
		//	else
		//	{
		//		btnFullFill.Disabled = false;
		//		btnFullFill.HintTooltip = TranslateServerEx.Translate("STATIC_COUNTRY_TAX_LACK_AND_ECONOMY_SUFFICENT",
		//			lackCountryTax.ToString(),
		//			RunData.Economy.inst.curr.Value.ToString());
		//	}
		//}

		private void _onButtonFullFillCountryTax()
		{
			//var need = RunData.Chaoting.inst.expectYearTax.Value - RunData.Chaoting.inst.realYearTax.Value;

			//RunData.Chaoting.inst.expectYearTax.Value = RunData.Chaoting.inst.realYearTax.Value;
			//RunData.Economy.inst.curr.Value -= need;

			//UpdateFullFillCountryTax();
		}

		//  // Called every frame. 'delta' is the elapsed time since the previous frame.
		//  public override void _Process(float delta)
		//  {
		//      
		//  }
	}

}

