using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))] // Custom editor for the FieldOfView script
public class FOVEditor : Editor
{
    void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target; // Get the target FieldOfView script
        Handles.color = Color.red; // Set the color for the handles
        Handles.DrawWireArc(
            fov.transform.position,
            Vector3.up,
            Vector3.forward,
            360,
            fov.viewDistance // Draw a wire arc representing the field of view
        );

        Vector3 angleA = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.viewAngle / 2); // Calculate the left angle direction
        Vector3 angleB = DirectionFromAngle(fov.transform.eulerAngles.y, fov.viewAngle / 2); // Calculate the right angle direction

        Handles.color = Color.green / 2f; // Set the color for the angle lines
        // Draw The filled up circle of the field of view
        Handles.DrawSolidArc(
            fov.transform.position,
            Vector3.up,
            angleA,
            fov.viewAngle,
            fov.viewDistance // Draw a solid arc representing the field of view angle
        );
        Handles.DrawLine(
            fov.transform.position,
            fov.transform.position + angleA * fov.viewDistance
        ); // Draw a line for the left angle direction
        Handles.DrawLine(
            fov.transform.position,
            fov.transform.position + angleB * fov.viewDistance
        ); // Draw a line for the right angle direction

        if (fov.seeSuspicous) // If the enemy can see suspicious objects
        {
            Handles.color = Color.yellow; // Set the color for the suspicious object line
            Handles.DrawLine(
                fov.transform.position,
                fov.player.transform.position // Draw a line to the player object
            );
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY; // Adjust the angle based on the eulerY rotation
        return new Vector3(
            Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),
            0,
            Mathf.Cos(angleInDegrees * Mathf.Deg2Rad)
        ); // Return the direction vector based on the angle
    }
}
