using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; // Required for interacting with UI buttons

public class SkyboxHandlerScript : MonoBehaviour
{
    // Existing serialized fields and variables
    [SerializeField] private GameObject rightHandController;
    [SerializeField] private GameObject activeList;
    public Material entranceSkybox;
    public float raycastDistance = 10f;
    public Material rayMaterial;
    public float rayWidth = 0.01f;

    public LineRenderer rightLineRenderer;
    public InputActionReference rightPrimaryButton;
    public InputActionReference rightSecondaryButton;

    public GameObject eagleStatue;
    public GameObject handStatue;
    public GameObject hammerStatue;
    public GameObject hexagonalPot;
    public GameObject liddedJar;
    public GameObject teapot;

    private GameObject eagleStatueInstance;
    private GameObject handStatueInstance;
    private GameObject hammerStatueInstance;
    private GameObject hexagonalPotInstance;
    private GameObject liddedJarInstance;
    private GameObject teapotInstance;


    private GameObject heldObject = null;
    private RaycastHit currentHit;
    private string currentNode = "Entrance";
    private int clickCount = 0;

    void Start()
    {
        eagleStatueInstance = Instantiate(eagleStatue);
        eagleStatueInstance.SetActive(false);

        handStatueInstance = Instantiate(handStatue);
        handStatueInstance.SetActive(false);

        hammerStatueInstance = Instantiate(hammerStatue);
        hammerStatueInstance.SetActive(false);

        hexagonalPotInstance = Instantiate(hexagonalPot);
        hexagonalPotInstance.SetActive(false);

        liddedJarInstance = Instantiate(liddedJar);
        liddedJarInstance.SetActive(false);

        teapotInstance = Instantiate(teapot);
        teapotInstance.SetActive(false);


        RenderSettings.skybox = entranceSkybox;

        if (rightHandController == null)
            Debug.LogError("RightHandController is not assigned!");
        if (rightLineRenderer == null)
            Debug.LogError("Right LineRenderer is not assigned!");

        if (rayMaterial != null)
        {
            rightLineRenderer.material = rayMaterial;
        }
        rightLineRenderer.startWidth = rayWidth;
        rightLineRenderer.endWidth = rayWidth;

        rightPrimaryButton.action.Enable();
        rightPrimaryButton.action.performed += RightPrimaryButtonAction;

        rightSecondaryButton.action.Enable();
    }

    void Update()
    {
        if (rightHandController != null && rightLineRenderer != null)
            HandleRaycast(rightHandController.transform, rightLineRenderer);

        if (rightSecondaryButton.action.IsPressed())
        {
            HandleObjectSpawn();
        }
        else if (heldObject != null)
        {
            DestroyObject();
        }
    }

    private void RightPrimaryButtonAction(InputAction.CallbackContext context)
    {
        if (currentHit.collider != null)
        {
            // Check if the hit object is a UI button
            Button uiButton = currentHit.collider.gameObject.GetComponent<Button>();
            if (uiButton != null)
            {
                // Trigger the button's onClick event
                uiButton.onClick.Invoke();
                Debug.Log($"Button {uiButton.name} clicked!");
                return;
            }

            // Existing logic for node interaction
            NodeLoadingScript nodeScript = currentHit.collider.gameObject.GetComponent<NodeLoadingScript>();
            currentNode = currentHit.collider.gameObject.name;
            if (nodeScript != null)
            {
                Material theStoredMaterial = nodeScript.myMaterial;
                RenderSettings.skybox = theStoredMaterial;

                GameObject theNodeList = nodeScript.myNodeList;
                if (activeList != null) activeList.SetActive(false);
                theNodeList.SetActive(true);
                activeList = theNodeList;
            }
            else
            {
                Debug.LogWarning("NodeLoadingScript not found on hit object.");
            }
        }
    }

    private void HandleRaycast(Transform controllerTransform, LineRenderer lineRenderer)
    {
        Ray ray = new Ray(controllerTransform.position, controllerTransform.forward);

        if (Physics.Raycast(ray, out currentHit, raycastDistance))
        {
            Debug.Log($"Hit Node: {currentHit.collider.gameObject.name}");
            UpdateLineRenderer(lineRenderer, ray.origin, currentHit.point);
        }
        else
        {
            UpdateLineRenderer(lineRenderer, ray.origin, ray.origin + ray.direction * raycastDistance);
        }
    }

    private void UpdateLineRenderer(LineRenderer lineRenderer, Vector3 startPosition, Vector3 endPosition)
    {
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);
    }

    private void HandleObjectSpawn()
    {
        if (heldObject == null)
        {
            Debug.Log(currentNode);
            if (currentNode.Equals("Eagle Sculpture"))
            {
                SpawnObject(eagleStatueInstance);
            }
            else if (currentNode.Equals("Hand and Hammer"))
            {
                if (clickCount % 2 == 0)
                {
                    SpawnObject(handStatueInstance);
                }
                else
                {
                    SpawnObject(hammerStatueInstance);
                }
                clickCount++;
            }
            else if (currentNode.Equals("Pottery Pieces"))
            {
                if (clickCount % 3 == 0)
                {
                    SpawnObject(hexagonalPotInstance);
                }
                else if (clickCount % 3 == 1)
                {
                    SpawnObject(liddedJarInstance);
                }
                else
                {
                    SpawnObject(teapotInstance);
                }
                clickCount++;
            }
        }
    }

    private void SpawnObject(GameObject objectToSpawn)
    {
        if (objectToSpawn != null && rightHandController != null)
        {
            heldObject = objectToSpawn;

            heldObject.SetActive(true);
            heldObject.transform.position = rightHandController.transform.position;
            heldObject.transform.rotation = rightHandController.transform.rotation;
            heldObject.transform.SetParent(rightHandController.transform);

            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }
    }

    private void DestroyObject()
    {
        if (heldObject != null)
        {
            heldObject.SetActive(false);
            heldObject = null;
        }
    }

    void OnDisable()
    {
        rightPrimaryButton.action.Disable();
        rightSecondaryButton.action.Disable();
    }
}
