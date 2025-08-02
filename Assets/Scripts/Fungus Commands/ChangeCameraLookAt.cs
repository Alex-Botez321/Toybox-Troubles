// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using Cinemachine;
using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// Changes the camera target
    /// Needs a new target and a reference to the camera
    /// </summary>
    [CommandInfo("Scripting",
                 "Change Camera Target",
                 "Changes the target of the camera")]
    [AddComponentMenu("")]
    public class ChangeCameraLookAt : Command
    {
        [Tooltip("Virtual Camera")]
        [SerializeField] protected CinemachineVirtualCamera Camera;

        [Tooltip("Transform of the new camera target")]
        [SerializeField] protected Transform newTarget;

        [Tooltip("Give in-level player to Lock movement and camera")]
        [SerializeField] protected PlayerController playerController;

        public override void OnEnter()
        {
            if (Camera != null)
            {
                Camera.LookAt = newTarget;

            }

            if (playerController != null)
            {
                playerController.isInteracting = true;
                playerController.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }
}