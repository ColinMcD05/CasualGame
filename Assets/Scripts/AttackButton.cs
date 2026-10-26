using UnityEngine;

public class AttackButton : MonoBehaviour
{
    [SerializeField] private int element;

    // 2. Expose it via a public getter with no external setter
    public int GetElement()
    {
        return element;
    }
}
