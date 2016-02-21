using UnityEngine;
using System.Collections;
using GamepadInput;

namespace Simple.Behaviour
{
    public class PlayerSelection : AbstractPlayer 
    {
        public void SaveCharacterType()
        {
            Character character = _root.GetComponent<Character>();
            string key = string.Format(PREF_KEY_ROOT, _id) + PREF_KEY_CHARACTER;
            PlayerPrefs.SetInt(key, (int)character.type);
        }
    }
}