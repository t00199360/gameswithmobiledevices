# gameswithmobiledevices
Games with mobile devices assignment

Accelerometer/gyroscope is quite sensitive to use with some of the other features, possibly disable this to show other features

There is a plane instanciated through code for demonstration purposes.

Selected object is indicated by the object turning red when it is being interacted with

two finger rotation is shown on the cube

Camera zoom is carried out with a two finger guesture by calculating the distances between two input.touch points and adjusting the zoom based on the change in distance between these points
Camera zoom was initially carried out by adjusting the camera.fov but changing the fov was not the goal although it provided a similar effect. This method remains commented out in the body of the code

The objects cannot pass out the plane. This is carried out by performing a raycast from the objects to the plane, and ensuring the distance between the object and the plane remains.
This is demonstrated in the "SphereTwo" gameobject.

Scaling is performed using a similar method to the camera zoom but is only performed on the gameobject "Sphere". 

GameObject "Cylinder" uses transform.rotation.up to maintain distance from the camera.

TouchPhases are used to determine the type of touch that is carried out in the project. (touchphase.began, touchphase.moved, touchphase.ended)
