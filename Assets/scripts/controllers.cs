using UnityEngine;

public class controllers : MonoBehaviour
{
    public int score = 0;
    public float Speed = 10f;
    public float JumpForce = 1f;
    private bool _isGrounded;
    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        MovementLogic();
        JumpLogic();
    }
    private void MovementLogic()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        _rb.AddForce(movement * Speed);
    }
    private void JumpLogic()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            if (_isGrounded)
            {
                _rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            }
        }
    }
    private bool checkGround()
    {
        //check ground with raycast, but take in account size of object (player is sphere)
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f
        + transform.localScale.y / 2))
        {
            return true;
        }
        return false;
    }
    private void IsGroundedUpate(Collision collision, bool value)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            print("------start");
            foreach (ContactPoint contact in collision.contacts)
            {
                print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
                // Visualize the contact point
                Debug.DrawRay(contact.point, contact.normal, Color.white);
            }
            print($"checkground - {checkGround()}");
            print($"expected value - {value}");
            print("------end");
            //if(collision.contacts[0].normal.y>0.5f)
            _isGrounded = value;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        IsGroundedUpate(collision, true);
    }
    void OnCollisionExit(Collision collision)
    {
        IsGroundedUpate(collision, false);
    }

    private void OnGUI()
    {
        GUIStyle gUIStyle = new GUIStyle();
        gUIStyle.normal.textColor = Color.green;
        gUIStyle.fontStyle = FontStyle.Bold;
        gUIStyle.fontSize = 25;
        GUI.Label(new Rect(500, 100, 1000, 1000), score.ToString(), gUIStyle);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            Destroy(other.gameObject);
            score++;
        }
    }
}
