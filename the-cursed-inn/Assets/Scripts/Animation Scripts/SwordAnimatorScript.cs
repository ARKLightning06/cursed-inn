using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Animator _swordAnimationController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _swordAnimationController = GetComponent<Animator>();
    }

    public void PlaySwordSwingAnimation()
    {
        _swordAnimationController.SetTrigger("SwordSwing");
    }
}
