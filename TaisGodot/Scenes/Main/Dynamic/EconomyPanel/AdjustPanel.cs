using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Tais;
using Tais.API;
using Tais.Run;

namespace TaisGodot.Scripts
{
	class AdjustPanel : PanelContainer
	{
		public const string path = "res://Scenes/Main/Dynamic/EconomyPanel/AdjustPanel2.tscn";

		public IAdjust gmObj;

		private Label name;
		private Label value;
		private Slider slider;

		public override void _Ready()
		{
			name = GetNode<Label>("HBoxContainer/Label");
			slider = GetNode<Slider>("HBoxContainer/HSlider");
			value = GetNode<Label>("HBoxContainer/Value");

			name.Text = gmObj.type.ToString();


			gmObj.OBSProperty(x => x.percent).Subscribe(l =>
			{
				slider.Value = l;
			}).EndWith(this);

			if (gmObj.type == ADJUST_TYPE.POP_TAX)
			{
				GMRoot.runner.economy.OBSProperty(x => x.incomes).Subscribe(incomes =>
				{
					value.Text = incomes.SelectMany(income => income[IncomeDetail.TYPE.POP_TAX]).Sum(t => t.value).ToString();
				}).EndWith(this);
			}

			if (gmObj.type == ADJUST_TYPE.CHAOTING_TAX)
			{
				GMRoot.runner.economy.OBSProperty(x => x.outputs).Subscribe(outputs =>
				{
					value.Text = outputs.Select(output => output[OutputDetail.TYPE.CHAOTING_YEAR_TAX]).Sum().ToString();
				}).EndWith(this);
			}
		}

		private void _on_HSlider_value_changed(float value)
		{
			gmObj.percent = (int)value;
		}
	}
}
