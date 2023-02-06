using UnityEngine;

namespace Mirror
{
    /// <summary>Start position for player spawning, automatically registers itself in the NetworkManager.</summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Network/Network Start Position")]
    [HelpURL("https://mirror-networking.gitbook.io/docs/components/network-start-position")]
    public class NetworkStartPosition : MonoBehaviour
    {
        bool enabled;
        public void Awake()
        {
            NetworkManager.RegisterStartPosition(transform);
            enabled =true;
        }

        public void OnEnable(){
            if (!enabled) {
                NetworkManager.RegisterStartPosition(transform);
                enabled = true;    
            }
        }

        public void OnDisable(){
            NetworkManager.UnRegisterStartPosition(transform);
            enabled = false;
        }

        public void OnDestroy()
        {
            NetworkManager.UnRegisterStartPosition(transform);
        }
    }
}
