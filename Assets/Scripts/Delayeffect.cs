using UnityEngine;

public class Delayeffect : MonoBehaviour
{
    //FPS KIT [www.armedunity.com]

    public float amount = 0.02f;
    public float maxAmount = 0.03f;
    public float smooth = 3f;
    private Vector3 def;

    void Start()
	{
        def = transform.localPosition;
    }

    void Update()
    {
        if (Cursor.lockState == CursorLockMode.None) return;

        float factorX = -Input.GetAxis("Mouse X") * amount;
        float factorY = -Input.GetAxis("Mouse Y") * amount;
		
        factorX = Mathf.Clamp(factorX, -maxAmount, maxAmount);
        factorY = Mathf.Clamp(factorY, -maxAmount, maxAmount);

        Vector3 target = new Vector3(def.x + factorX, def.y + factorY, def.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * smooth);
    }
}