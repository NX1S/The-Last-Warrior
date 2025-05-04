using UnityEngine.XR.Interaction.Toolkit;

public class CustomRayInteractor : XRRayInteractor
{
    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        // If the object is already selected, make this interactor ignore it
        if (interactable.isSelected)
        {
            return IsSelecting(interactable); // Only allow if this interactor is already selecting it
        }

        return base.CanSelect(interactable);
    }

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        // Check if the interactable can be cast to IXRSelectInteractable
        if (interactable is IXRSelectInteractable selectInteractable)
        {
            // Prevent hovering if the object is selected by another interactor
            if (selectInteractable.isSelected && !IsSelecting(selectInteractable))
            {
                return false; // Ignore the object entirely for other interactors
            }
        }

        return base.CanHover(interactable);
    }
}