using UnityEngine;
using Simple.Behaviour;

public class CharacterAnimation : MonoBehaviour 
{
    private PlayerGame _player = null;
    public PlayerGame player { get { return _player; } set { _player = value; } }

    public void Shoot()
    {
		player.ConsumeAmmo();
    }
}
