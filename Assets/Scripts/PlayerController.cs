using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform go_baseRotation;
    public Transform go_GunBody;
    public Transform go_barrel;

    public float barrelRotationSpeed;
    float currentRotationSpeed;


    public ParticleSystem muzzelFlash;

    bool canFire = false;
    Transform go_target;


    void Update()
    {
        AimAndFire();        
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            go_target = other.transform;
            canFire = true;
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            canFire = false;
        }
    }

    void AimAndFire()
    {
        go_barrel.transform.Rotate(0, 0, currentRotationSpeed * Time.deltaTime);

        if (canFire && go_target != null)
        {
            currentRotationSpeed = barrelRotationSpeed;

            Vector3 baseTargetPostition = new Vector3(go_target.position.x, this.transform.position.y, go_target.position.z);
            Vector3 gunBodyTargetPostition = new Vector3(go_target.position.x, go_target.position.y, go_target.position.z);

            go_baseRotation.transform.LookAt(baseTargetPostition);
            go_GunBody.transform.LookAt(gunBodyTargetPostition);

            if (!muzzelFlash.isPlaying)
            {
                muzzelFlash.Play();
            }
        }
        else
        {
            currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, 0, 10 * Time.deltaTime);

            if (muzzelFlash.isPlaying)
            {
                muzzelFlash.Stop();
            }
        }
    }
   
}
