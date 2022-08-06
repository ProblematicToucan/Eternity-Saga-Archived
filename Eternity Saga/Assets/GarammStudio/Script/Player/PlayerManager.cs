using UnityEngine;

public class PlayerManager : Character
{
    public GameObject mainCamera;
    [SerializeField] private InputReader inputReader;
    public InputReader InputReader { get { return inputReader; } }
    [Expandable]
    public CharacterStat stat;
    [Expandable]
    public PlayerProp prop;
    [Expandable]
    public StateControllerSO stateController;
    [field: SerializeField] public LocomotionSO Locomotion { get; private set; }
    public AnimatorController AnimatorController { get; private set; }
    [SerializeField] private GroundedCheckSO groundedCheck;


    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main!.gameObject;
        AnimatorController = GetComponent<AnimatorController>();
    }
    private void Start()
    {
        groundedCheck.OnStart(this);
        Locomotion.OnStart(this);
        stateController.OnStart(this);
    }

    private void Update()
    {
        groundedCheck.OnUpdate();
        stateController.OnUpdate();
    }

    private void FixedUpdate()
    {
        stateController.OnFixedUpdate();
    }

    private void OnDisable()
    {
        stateController.OnExit();
    }

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        Gizmos.color = prop.Grounded ? transparentGreen : transparentRed;
        var position = transform.position;
        Gizmos.DrawSphere(
            new Vector3(position.x, position.y - prop.GroundedOffset, position.z),
            prop.GroundedRadius);
    }
}