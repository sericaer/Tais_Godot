using Godot;
using System;
using Tais;

namespace TaisGodot.Scripts
{
	public class Date : Button
	{
		public override void _Ready()
		{
			GetNode<ReactiveLabel>("HBoxContainer/Value").Assoc(GMRoot.runner.date.OBSProperty(x=>x.value), 
												 (value)=> TranslateServerEx.Translate("STATIC_DATE_VALUE", value.y.ToString(), value.m.ToString(), value.d.ToString()));
		}
	}

}
