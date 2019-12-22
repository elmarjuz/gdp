// Title:    ClickToMove.js
// Usage:    Moves the object that script is attatched to towards the mouse position on left mouse click or hold
// Author:   George Coope
// Date:     10/02/13
 
var moveSpeed:float = 20;
var velocity:float = 1;
 
private var targetPosition:Vector3;
public var targetDistance:float;
public var motherShipDistance:float;
var motherShipPlatform:Transform;
var isParked:boolean;

private var virtualThing : Transform;
 
/*function Start(){
	transform.position.x=15;
	transform.position.y=15;
} */


function Start() {


isParked=true;


targetPosition = motherShipPlatform.position;
transform.LookAt(targetPosition);

}




function Update () {
	if(Time.timeScale!=0){
		motherShipDistance = Vector3.Distance(motherShipPlatform.position, transform.position);
		targetDistance = Vector3.Distance(targetPosition, transform.position);
		
		if(motherShipDistance<2 && targetDistance < 0.2){
			targetPosition = motherShipPlatform.position;
			isParked=true;
	
		} else {
			isParked=false;
		}
		if(targetDistance < 2){ // prevents shaking when it reaches location
			moveSpeed -= moveSpeed / (6*velocity);
			if(moveSpeed<8){
				moveSpeed = 0;
			
			}
		}
		else if(targetDistance > 2){
			moveSpeed = 20 * velocity;
		}
		
		
		
		
		if(Input.GetKey(KeyCode.Mouse0)) // Allows for click and hold, use GetKeyDown for single click
		{
			var playerPlane = new Plane(Vector3.forward, transform.position);
			var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			var hitdist:float = 0.0;
	 
			if (playerPlane.Raycast (ray, hitdist)) {
				targetPosition = ray.GetPoint(hitdist);
				
			}
			
			if(targetDistance > 2){
				velocity = velocity + 0.01;
			} else {
				if (velocity!=1){
					velocity = velocity - 0.01;
					if(targetDistance < 0.005 || velocity < 1){
						velocity = 1;
					}
				}	
			}
		} else {
			if (velocity!=1){
				velocity = velocity - 0.01;
				if(targetDistance < 0.005 || velocity < 1){
					velocity = 1;
				}
			}		
		}
		
		
		if(isParked){
		
			transform.position = motherShipPlatform.position;
			
		} else {
	 
		if(targetDistance > 0){ // Prevents code running when it doesn't need to
			//virtualThing.LookAt(targetPosition);
			var currentRot = transform.rotation;
			transform.LookAt(targetPosition);
			
			var targetRot = transform.rotation;
			
			transform.rotation = currentRot;
			
			transform.rotation.x = 	targetRot.z;
			
			//var targetPos = targetPosition;
	 		//targetPos.x = targetPosition.z;
	 		//targetPos.z = transform.position.x;
	 		
	 		
			//transform.LookAt(targetPosition);	
			
			//float angle = Vector3.Angle(targetPosition-transform.position, transform.position);
			//transform.rotation.x = angle;
			
			
			
			transform.position += (targetPosition - transform.position).normalized * moveSpeed * Time.deltaTime;
			
			}
		}
		transform.position.z = 0;
	}
}