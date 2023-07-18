using Nico;
using UnityEngine;

namespace OneButtonGame
{
    public class PlayerModel : IModel
    {
        // public BindableProperty<float> exp;

        public void OnRegister()
        {
            // float value = PlayerPrefs.GetFloat("nameofexp", 0);
            // exp = new BindableProperty<float>(value);
            // exp.OnValueChanged += (previous, current) =>
            // {
            //     Debug.Log($"exp changed from {previous} to {current}");
            //     EventManager.Send(new ExpChange()
            //     {
            //         previousExp = previous,
            //         currentExp = current
            //     });
            // };
        }

        public void OnSave()
        {
            // PlayerPrefs.SetFloat("nameofexp", exp.Value);
        }
    }
}