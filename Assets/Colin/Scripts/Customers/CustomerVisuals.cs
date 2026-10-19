using UnityEditor.Animations;
using UnityEngine;

public class CustomerVisuals : MonoBehaviour
{
    AnimatorController controller;

    public void PlayVisual(CustomerSatisfaction satisfaction)
    {
        PlayVFXVisuals(satisfaction);
        PlayAnimation(satisfaction);
    }

    public void PlayVFXVisuals(CustomerSatisfaction satisfaction)
    {
        switch(satisfaction)
        {
            case CustomerSatisfaction.Unsatisfied:
            case CustomerSatisfaction.Mad:
                break;
            case CustomerSatisfaction.Meh:
                break;
            case CustomerSatisfaction.Satisfied:
                break;
        }
    }

    public void PlayAnimation(CustomerSatisfaction satisfaction)
    {
        switch (satisfaction)
        {
            case CustomerSatisfaction.Unsatisfied:
            case CustomerSatisfaction.Mad:

                break;
            case CustomerSatisfaction.Meh:

                break;
            case CustomerSatisfaction.Satisfied:

                break;
        }
    }
}
