using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject Player;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - Player.transform.position;
    }
    // зміна розташування камери після переміщення
    void LateUpdate()
    {
        transform.position = Player.transform.position + offset;
    }
}
