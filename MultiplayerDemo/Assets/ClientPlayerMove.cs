using Unity.Netcode;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
public class ClientPlayerMove : NetworkBehaviour
{
    [SerializeField] private CinemachineCamera m_camera;
    [SerializeField] private PlayerInput m_playerInput;
    [SerializeField] private StarterAssetsInputs m_starterAssetsInput;
    [SerializeField] private ThirdPersonController m_thirdPersonController;
    private void Awake()
    {
        m_camera.enabled = false;
        m_playerInput.enabled = false;
        m_starterAssetsInput.enabled = false;
        m_thirdPersonController.enabled = false;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsOwner)
        {
            m_playerInput.enabled = true;
            m_starterAssetsInput.enabled = true;
            m_camera.enabled = true;
        }
        if (IsServer)
        {
            m_thirdPersonController.enabled = true;
        }
    }
    [Rpc(target:SendTo.Server)]
    private void UpdateInputServerRpc(Vector2 move, Vector2 look, bool jump, bool sprint)
    {
        m_starterAssetsInput.MoveInput(move);
        m_starterAssetsInput.LookInput(look);
        m_starterAssetsInput.JumpInput(jump);
        m_starterAssetsInput.SprintInput(sprint);
    }

    private void LateUpdate()
    {
        if (!IsOwner)
        {
            return;
        }
        UpdateInputServerRpc(m_starterAssetsInput.move, m_starterAssetsInput.look, m_starterAssetsInput.jump, m_starterAssetsInput.sprint);
    }
}
