using UnityEngine;

public class PendulumSetup : MonoBehaviour
{
    public Rigidbody2D pendulumRb; // Rigidbody da barra
    public Rigidbody2D baseRb;     // Rigidbody da base

    void Start()
    {
        HingeJoint2D hinge = pendulumRb.gameObject.AddComponent<HingeJoint2D>();
        hinge.connectedBody = baseRb;
        hinge.autoConfigureConnectedAnchor = false;
        // Configure o ponto de ancoragem de acordo com o sprite,
        // por exemplo, se a extremidade superior for o ponto de rotação:
        hinge.anchor = new Vector2(0f, 1f);
        hinge.connectedAnchor = Vector2.zero;
    }
}