using System;
using UnityEngine;
using UnityEngine.UI;

namespace Pokemon
{
    public partial class TitlePanel
    {
		private Button StartButton;
		private Button SettingButton;
		private Button ExitButton;

        
        protected override void OnPartialBind()
        {
			StartButton = transform.Find("Buttons/StartButton/").GetComponent<Button>();
			SettingButton = transform.Find("Buttons/SettingButton/").GetComponent<Button>();
			ExitButton = transform.Find("Buttons/ExitButton/").GetComponent<Button>();

        }
    }
}