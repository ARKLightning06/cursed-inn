using System.Threading.Tasks;
using UnityEngine;

public class WeaponAnimationController : MonoBehaviour

{
    private Animator _WeaponAnimationController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _WeaponAnimationController = GetComponent<Animator>();
    }

    //activates downward swing animation
    public async Task SwingSword()
    {
        _WeaponAnimationController.SetBool("Swing", true);
        await Task.Delay(200); //waits for 0.2 seconds
        _WeaponAnimationController.SetBool("Swing", false);
    }

}