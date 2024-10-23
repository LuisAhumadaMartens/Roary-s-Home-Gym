using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Windmill_Action_Evaluator : MonoBehaviour
{

    public GameObject test_object;
    public GameObject head;
    public GameObject left_hand;
    public GameObject right_hand;
    private double shoulder_height;
    private LineRenderer line;
    private float last_angle = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.line = this.head.AddComponent<LineRenderer>();
        this.shoulder_height = this.head.transform.position[1];
        Debug.Log(this.shoulder_height);

        var line = this.line;
        var segments = 360;
        float radius = 10;
        line.useWorldSpace = false;
        line.startWidth = 0.2f;
        line.endWidth = 0.2f;
        line.positionCount = segments + 1;

        var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, Mathf.Cos(rad) * radius, 0);
        }

        line.SetPositions(points);
    }



    // Update is called once per frame
    void Update()
    {
        var lpos = this.left_hand.transform.position - this.head.transform.position;
        var nplane = this.left_hand.transform.position - this.right_hand.transform.position;
        var response = Vector3.ProjectOnPlane(lpos, nplane);
        float angle = Vector3.Angle(response,Vector3.up);
        Debug.Log(angle);

        float speed = ((angle - last_angle)/Time.deltaTime)/10000;
        CharacterController controller = gameObject.GetComponent<CharacterController>();
        controller.Move( new Vector3(0,0,-1 * speed) );
        this.last_angle = angle;
    }
}
