using UnityEngine;

public class PendulumMass : MonoBehaviour
{
    public Rigidbody2D rodRb;           // Arraste a referência do Rigidbody2D da barra (pendulum rod) no Inspector.
    public Vector2 connectionPoint;     // Posição local no corpo da barra onde o peso será conectado.
    
    void Start()
    {
        FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
        joint.connectedBody = rodRb;
        // O anchor do peso é o ponto de origem do seu próprio transform.
        joint.anchor = Vector2.zero;
        // O connectedAnchor é o ponto local, na barra, onde o peso estará conectado.
        joint.connectedAnchor = connectionPoint;
    }
}
