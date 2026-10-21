using UnityEngine;

public class CustomerVisuals : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject happyVFX, mehVFX, madVFX;

    public void Start()
    {

    }

    public void PlayVisual(CustomerSatisfaction satisfaction)
    {
        PlayVFXVisuals(satisfaction);
        PlayAnimation(satisfaction);
    }

    public void PlayVFXVisuals(CustomerSatisfaction satisfaction)
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);

        switch(satisfaction)
        {
            case CustomerSatisfaction.Unsatisfied:
            case CustomerSatisfaction.Mad:
                Instantiate(madVFX, newPosition, transform.rotation);
                break;
            case CustomerSatisfaction.Meh:
                Instantiate(mehVFX, newPosition, transform.rotation);
                break;
            case CustomerSatisfaction.Satisfied:
                Instantiate(happyVFX, newPosition, transform.rotation);
                break;
        }
    }

    public void PlayAnimation(CustomerSatisfaction satisfaction)
    {
        if (!animator)
        {
            print("Bad");
            return;
        }

        switch (satisfaction)
        {
            case CustomerSatisfaction.Unsatisfied:
            case CustomerSatisfaction.Mad:
                animator.SetTrigger("WrongOrder");
                break;
            case CustomerSatisfaction.Meh:
                animator.SetTrigger("LateOrder");
                break;
            case CustomerSatisfaction.Satisfied:
                animator.SetTrigger("CorrectOrder");
                break;
        }
    }
}
