using UnityEngine;
using System.Collections;
public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    { //оголошуємо структуру enum, що буде співставляти імена
      //з параметрами
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxes axes = RotationAxes.MouseXAndY; //загальнодоступна змінна
    public float sensHor = 9.0f;
    public float sensVer = 9.0f;

    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;
    private float _rotationX = 0; // закрита змінна для кута повороту по вертикалі
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.freezeRotation = true;
        }
    }
    void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            // поворот в горизонтальній площині
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensHor, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensVer; // збільшуємо кут
                                                              //повороту по вертикалі у відповідності до до вказівника миші
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert); /*фіксуємо
кут повороту по вертикалі в діапазоні, що представлений мінімальним та максимальним
значенням*/
            float rotationY = transform.localEulerAngles.y; //зберігаємо однаковий кут
                                                            //навколо осі у (тобто поворот в горизонтальній площині відсутній)
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
        else
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensVer;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
            float delta = Input.GetAxis("Mouse X") * sensHor; //приріст кута повороту
                                                                     //через значення delta
            float rotationY = transform.localEulerAngles.y + delta; // значення delta – це
                                                                    //величина зміни кута повороту
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
    }
}