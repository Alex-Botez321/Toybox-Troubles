using Cinemachine;
using UnityEngine;

namespace Fungus
{
    public class ChangeCamera : Command
    {
        [Tooltip("New camera to use")]
        [SerializeField] protected CinemachineVirtualCamera newCamera;

        public override void OnEnter()
        {
            base.OnEnter();
            newCamera.Priority = 1000;
        }

        public override void OnExit()
        {
            base.OnExit();
            newCamera.Priority--;
        }
    }
}