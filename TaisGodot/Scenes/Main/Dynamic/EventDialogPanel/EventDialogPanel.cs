﻿using System;
using Godot;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais.Run;

namespace TaisGodot.Scripts
{
    class EventDialogPanel : Panel
    {
        internal const string path = "res://Scenes/Main/Dynamic/EventDialogPanel/EventDialogPanel.tscn";
        internal IEvent gmObj;

        Label title;
        Label content;

        public override void _Ready()
        {
            title = GetNode<Label>("CenterContainer/PanelContainer/VBoxContainer/Title");

            
            title.Text = TranslateServerEx.Translate(gmObj.title_format, gmObj.title_objs);

            AddOptions();
        }

        private void AddOptions()
        {
            List<Button> btns = GenerateButtons();
                
            for(int i=0; i< btns.Count; i++)
            {
                AssocOption(btns[i], gmObj.options[i]);
            }
        }

        private void AssocOption(Button button, IOption option)
        {
            button.Text = TranslateServerEx.Translate(option.desc_format, option.desc_objs);
            button.Connect("pressed", this, nameof(QueueFree));
        }

        private List<Button> GenerateButtons()
        {
            var rslt = new List<Button>();
            var btnContainer = GetNode<Container>("");
            var btn = btnContainer.GetChild<Button>(0);
            rslt.Add(btn);

            for (int i = 1; i < gmObj.options.Length; i++)
            {
                var newBtn = btn.Duplicate() as Button;
                btnContainer.AddChild(newBtn);

                rslt.Add(newBtn);
            }

            return rslt;
        }
    }
}
