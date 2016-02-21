using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Simple.Behaviour;
using Simple.CustomType;
using Simple.Manager;

public class CharacterAnimation : MonoBehaviour 
{
    private PlayerGame _player = null;
    public PlayerGame player { get { return _player; } set { _player = value; } }

    public void ConsumeRound()
    {
        if (--_player.currentWeapon.ammo <= 0)
        {
            _player.Shoot(false);
        }

        _player.UpdateGUIWeaponAmmo();
    }
}
