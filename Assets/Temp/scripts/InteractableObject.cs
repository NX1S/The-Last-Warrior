using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class InteractableObject : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Outline outline;

    private HashSet<IXRInteractor> hoveringInteractors = new HashSet<IXRInteractor>();

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        outline = GetComponent<Outline>();

        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
        }

        outline.OutlineColor = Color.cyan;
        outline.OutlineWidth = 7.0f;
        outline.enabled = false;
    }

    private void OnEnable()
    {
        grabInteractable.hoverEntered.AddListener(OnHoverEntered);
        grabInteractable.hoverExited.AddListener(OnHoverExited);
        grabInteractable.selectEntered.AddListener(OnSelectEntered);
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    private void OnDisable()
    {
        grabInteractable.hoverEntered.RemoveListener(OnHoverEntered);
        grabInteractable.hoverExited.RemoveListener(OnHoverExited);
        grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        grabInteractable.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        hoveringInteractors.Add(args.interactorObject);
        UpdateOutlineState();
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        hoveringInteractors.Remove(args.interactorObject);
        UpdateOutlineState();
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        UpdateOutlineState();
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        UpdateOutlineState();
    }

    private void UpdateOutlineState()
    {
        if (grabInteractable.isSelected || hoveringInteractors.Count == 0)
        {
            outline.enabled = false;
        }
        else
        {
            outline.enabled = true;
        }
    }
}
