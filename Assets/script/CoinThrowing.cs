using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CoinThrowing : MonoBehaviour
{
    [SerializeField, Tooltip("“ŠŽË‚·‚é‚à‚Ì")]
    private GameObject ThrowingCoin;

    [SerializeField,Tooltip("‘_‚¤ˆÊ’u")]
    private GameObject TargetCoin;

    [SerializeField, Range(0f, 90f), Tooltip("Šp“x")]
    private float ThrowingAngle;
    [SerializeField] private LineRenderer trajectoryLine;
    [SerializeField] private int lineSegmentCount = 30;

    public GameObject CoinParent;


    // Start is called before the first frame update
    void Start()
    {
        Collider collider = GetComponent<Collider>();
        if(collider != null)
        {
            collider.isTrigger = true;
        }
    }

    [SerializeField, Tooltip("target‚ÌˆÚ“®‘¬“x")]
    private float moveSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        if (GManager.instance.Possession > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _CoinThrow();
            }
        }
        else
        {
            return;
        }

        if(TargetCoin != null)
        {
            Vector3 moveDir = Vector3.zero;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                moveDir = Vector3.left;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                moveDir = Vector3.right;
            }

            TargetCoin.transform.position += moveDir * moveSpeed * Time.deltaTime;

            Vector3 pos = TargetCoin.transform.position;
            pos.x = Mathf.Clamp(pos.x, -4f, 4f);
            TargetCoin.transform.position = pos;
        }
        if (trajectoryLine != null && ThrowingCoin != null && TargetCoin != null)
        {
            Vector3 velocity = CalculateVelocity(transform.position, TargetCoin.transform.position, ThrowingAngle);
            DrawTrajectory(transform.position, velocity);
        }
    }

    private void _CoinThrow()
    {
        if (ThrowingCoin != null && TargetCoin != null)
        {
            GameObject Coin = Instantiate(ThrowingCoin, this.transform.position, Quaternion.identity);


            if(CoinParent != null)
            {
                Coin.transform.SetParent(CoinParent.transform);
            }

            Vector3 TargetPosition = TargetCoin.transform.position;

            float angle = ThrowingAngle;

            Vector3 velocity = CalculateVelocity(this.transform.position, TargetPosition, angle);

            Rigidbody rid = Coin.GetComponent<Rigidbody>();
            rid.AddForce(velocity * rid.mass, ForceMode.Impulse);

            GManager.instance.Possession--;
        }
    }

    private Vector3 CalculateVelocity(Vector3 pointA, Vector3 pointB,float angle)
    {
        float rad = angle * Mathf.PI / 180;

        float x = Vector2.Distance(new Vector2(pointA.x, pointA.z), new Vector2(pointB.x, pointB.z));

        float y = pointA.y - pointB.y;

        float speed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(x, 2) / (2 * Mathf.Pow(Mathf.Cos(rad), 2) * (x * Mathf.Tan(rad) + y)));

        if (float.IsNaN(speed))
        {
            return Vector3.zero;
        }
        else
        {
            return (new Vector3(pointB.x - pointA.x, x * Mathf.Tan(rad), pointB.z - pointA.z).normalized * speed); 
        }
    }

    private void DrawTrajectory(Vector3 startPos, Vector3 startVelocity)
    {
        Vector3[] points = new Vector3[lineSegmentCount];
        for (int i = 0; i < lineSegmentCount; i++)
        {
            float t = i * 0.1f;
            Vector3 point = startPos + startVelocity * t + 0.5f * Physics.gravity * t * t;
            points[i] = point;
        }

        trajectoryLine.positionCount = lineSegmentCount;
        trajectoryLine.SetPositions(points);
    }
}
