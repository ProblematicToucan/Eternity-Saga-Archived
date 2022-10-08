using UnityEngine;

[CreateAssetMenu(menuName = "GaramStudio/Player/Locomotion", fileName = "New Locomotion")]
public class LocomotionSO : ScriptableObject
{
    // player
    [ReadOnly] public float speed;
    [ReadOnly] public float animationBlend;
    [ReadOnly] public float targetRotation = 0.0f;
    [ReadOnly] public float rotationVelocity;
    [ReadOnly] public float verticalVelocity;
    [ReadOnly] public float terminalVelocity = 53.0f;
    private CharacterController _controller;
    public CharacterController Controller { get { return _controller; } }
    public void OnStart(PlayerManager manager)
    {
        _controller = manager.GetComponent<CharacterController>();
    }
}
