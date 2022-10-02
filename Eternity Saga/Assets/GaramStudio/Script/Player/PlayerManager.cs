using UnityEngine;

public class PlayerManager : Character
{
    public GameObject mainCamera;
    [field: SerializeField] public InputReaderSO InputReader { get; private set; }
    [field: SerializeField, Expandable] public CharacterStatSO Stat { get; private set; }
    [field: SerializeField, Expandable] public PlayerPropSO Prop { get; private set; }
    [field: SerializeField, Expandable] public StateControllerSO StateController { get; private set; }
    [field: SerializeField] public LocomotionSO Locomotion { get; private set; }
    public AnimatorController AnimatorController { get; private set; }
    [field: SerializeField] public GroundedCheckSO GroundedCheck { get; private set; }
    [field: SerializeField, Expandable] public InventorySO Inventory { get; private set; }

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main!.gameObject;
        AnimatorController = GetComponent<AnimatorController>();
        Inventory?.RegisterEvent();
    }
    private void Start()
    {
        GroundedCheck.OnStart(this);
        Locomotion.OnStart(this);
        StateController.OnStart(this);
    }

    private void Update()
    {
        GroundedCheck.OnUpdate();
        StateController.OnUpdate();
    }

    private void FixedUpdate()
    {
        StateController.OnFixedUpdate();
    }

    private void OnDisable()
    {
        StateController.OnExit();
        Inventory?.UnRegisterEvent();
    }

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        Gizmos.color = Prop.Grounded ? transparentGreen : transparentRed;
        var position = transform.position;
        Gizmos.DrawSphere(
            new Vector3(position.x, position.y - Prop.GroundedOffset, position.z),
            Prop.GroundedRadius);
    }
}