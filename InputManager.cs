using UnityEngine;


public class InputManager : MonoBehaviour
{
    [SerializeField]
    private float distanceBetweenBallAndMouseClickLimit = 1.5f; 

    private float distanceBetweenBallAndMouseClick;             
    private bool canRotate = false;                             

    
    void Update()
    {
        if (GameManager.singleton.gameStatus != GameStatus.Playing) return; 
        if (Input.GetMouseButtonDown(0) && !canRotate)          
        {
            GetDistance();                                      
            canRotate = true;                                  

           
            if (distanceBetweenBallAndMouseClick <= distanceBetweenBallAndMouseClickLimit)
            {
                BallControl.instance.MouseDownMethod();        
            }
        }

        if (canRotate)                                          
        {
            if (Input.GetMouseButton(0))                        
            {                                                   
                if (distanceBetweenBallAndMouseClick <= distanceBetweenBallAndMouseClickLimit)
                {
                    BallControl.instance.MouseNormalMethod();   
                }
                else
                {                                             
                    CameraRotation.instance.RotateCamera(Input.GetAxis("Mouse X"));
                }
            }

            if (Input.GetMouseButtonUp(0))                     
            {
                canRotate = false;                             
                if (distanceBetweenBallAndMouseClick <= distanceBetweenBallAndMouseClickLimit)
                {
                    BallControl.instance.MouseUpMethod();      
                }
            }
        }
    }

    void GetDistance()
    {
        
        var plane = new Plane(Camera.main.transform.forward, BallControl.instance.transform.position);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
        float dist;                                                     
        if (plane.Raycast(ray, out dist))
        {
            var v3Pos = ray.GetPoint(dist);                             
            
            distanceBetweenBallAndMouseClick = Vector3.Distance(v3Pos, BallControl.instance.transform.position);
        }
    }

}
