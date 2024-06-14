using UnityEngine;
using UnityEngine.UI;

public class UIMaskController : MonoBehaviour
{
    public Image image;
    public float revealSpeed;


    public Material material;
    private float cutoff;

    private void Start()
    {
        if (revealSpeed ==null)
            revealSpeed = 1.0f;
        if (image != null)
        {
            material = image.material;
            cutoff = material.GetFloat("_Cutoff");
        }
    }

    private void Update()
    {
        if (material != null)
        {
            cutoff += revealSpeed * Time.deltaTime;
            cutoff = Mathf.Clamp(cutoff, 0, 1);
            material.SetFloat("_Cutoff", cutoff);
        }
    }
}
