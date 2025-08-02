// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// Changes the camera target
    /// Needs a new target and a reference to the camera
    /// </summary>
    [CommandInfo("Scripting",
                 "Player Interact On",
                 "Locks player movement and shows cursor")]
    [AddComponentMenu("")]
    public class PlayerInteractOn : Command
    {
        [Tooltip("Give in-level player to Lock movement and camera")]
        [SerializeField] protected GameObject player;

        public override void OnEnter()
        {
            if (player != null)
            {
                player.GetComponent<PlayerController>().InteractOn();
            }

            Continue();
        }


    }
}
