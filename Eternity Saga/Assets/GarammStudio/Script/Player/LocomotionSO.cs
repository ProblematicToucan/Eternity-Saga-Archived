using UnityEngine;

[CreateAssetMenu(menuName = "GarammStudio/Player/Locomotion", fileName = "New Locomotion")]
public class LocomotionSO : ScriptableObject
{
    // player
    public float speed;
    public float animationBlend;
    public float targetRotation = 0.0f;
    public float rotationVelocity;
    public float verticalVelocity;
    public float terminalVelocity = 53.0f;
    private CharacterController _controller;
    public CharacterController Controller { get { return _controller; } }
    public void OnStart(PlayerManager manager)
    {
        _controller = manager.GetComponent<CharacterController>();
    }
}
