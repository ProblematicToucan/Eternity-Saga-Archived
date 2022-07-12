using UnityEngine;

public class PlayerManager : Character
{
    public GameObject mainCamera;
    public InputReader inputReader;
    [Expandable]
    public CharacterStat stat;
    [Expandable]
    public PlayerProp prop;
    public StateController stateController;
    private PlayerAnimator playerAnimator;
    public PlayerAnimator PlayerAnimator { get { return playerAnimator; } }


    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main.gameObject;
        playerAnimator = new PlayerAnimator(this);
        stateController = new StateController(this);
    }
    private void Start()
    {
        playerAnimator.OnStart();
        stateController.OnStart();
    }

    private void Update()
    {
        stateController.OnUpdate();
    }

    private void FixedUpdate()
    {
        stateController.OnFixedUpdate();
    }

    private void OnDisable()
    {
        stateController.OnDisable();
    }

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (prop.Grounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(
            new Vector3(transform.position.x, transform.position.y - prop.GroundedOffset, transform.position.z),
            prop.GroundedRadius);
    }
}