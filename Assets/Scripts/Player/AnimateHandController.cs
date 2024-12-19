using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class AnimateHandController : MonoBehaviour
{

    public InputActionReference gripInputActionReference;
    public InputActionReference triggerInputActionReference;

    private Animator animator;
    private float gripValue;
    private float triggerValue;
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        gripValue = gripInputActionReference.action.ReadValue<float>();
        triggerValue = triggerInputActionReference.action.ReadValue<float>();
        animator.SetFloat("Grip", gripValue);
        animator.SetFloat("Trigger", triggerValue);
    }
}
