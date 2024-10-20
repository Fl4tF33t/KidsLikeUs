using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    private Entity entityBrain;

    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private TextMeshProUGUI dialogueText;

    private bool isActive = false;
    private void Awake()
    {
        entityBrain = GetComponent<Entity>();
    }

    private void OnEnable()
    {
        entityBrain.OnStartDialogue += StartDialogue;
    }
    private void OnDisable()
    {
        entityBrain.OnStartDialogue -= StartDialogue;
    }
    private void LateUpdate()
    {
        if(isActive)
            transform.LookAt(Camera.main.transform);
    }

    private void StartDialogue(string dialogue)
    {
        isActive = true;
        dialogueText.text = dialogue;
        canvas.SetActive(true);
    }
}
