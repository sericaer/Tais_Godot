using Godot;
using System;
using Tais;

namespace TaisGodot.Scripts
{
	public class Date : Button
	{
		public override void _Ready()
		{
			GetNode<ReactiveLabel>("HBoxContainer/Value").Assoc(GMRoot.runner.date.OBSProperty(x=>x.desc), 
												 (desc)=> TranslateServerEx.Translate("STATIC_DATE_VALUE", desc.Split('-')));
		}
	}

}
