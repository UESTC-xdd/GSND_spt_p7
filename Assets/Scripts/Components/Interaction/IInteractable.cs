using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IInteractable : MonoBehaviour
{
    public bool Interactable;
    public bool InteractOnce;
    public UnityEvent InteractEvt;

    public bool CanInteract { get; set; }
    public bool IsPlayerInRange { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            IsPlayerInRange = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            IsPlayerInRange = false;

        }
    }

    public virtual void OnInteract()
    {
        Debug.Log("Interact: " + gameObject.name);
        InteractEvt?.Invoke();
        if (InteractOnce)
        {
            Interactable = false;
            IsPlayerInRange = false;
        }
    }
}
