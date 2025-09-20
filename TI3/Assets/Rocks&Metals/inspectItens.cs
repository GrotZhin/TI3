using Sfx;
using UnityEngine;
using UnityEngine.InputSystem;

public class inspectItens : MonoBehaviour
{
    public Camera inspCam;
    public float spd = 100f;
    public float SfxSence = 250; 
    public float SpkCooldown=0.5f;
    float SpkTimer;

    void Update()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = inspCam.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane));
        Vector3 direction = mouseWorldPosition - transform.position;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRotation * Quaternion.Euler(0, -180f, 0), spd * Time.deltaTime);
            float Dif = Quaternion.Angle(transform.rotation, newRotation);
            if (Dif / Time.deltaTime > SfxSence)
            {
                if (SpkTimer > 0)
        {
            SpkTimer -= Time.deltaTime;
        }
                if (SpkTimer <= 0)
                {
                    SpkTimer = SpkCooldown;
                    soundManager.PlaySound(SoundType.Sparkles);
                
                }
                
               
            }

            transform.rotation = newRotation;
        }
    }
}
    




