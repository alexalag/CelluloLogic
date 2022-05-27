using UnityEngine;

public class ConstantsGame : MonoBehaviour {

    public static InputKeyboard trueInput = InputKeyboard.arrows;
    public static InputKeyboard falseInput = InputKeyboard.wasd;
    public const float maxDistPushCellulo = 4.25f;
    public const float maxDistDrawCellulo = 8f;
    public const float maxDistStartDrawingCellulo = 6f;
    public static bool gameIsRunning = false;
    public const int level1Score = 100;
    public const float level1Time = 120f;

    //Gardian
    public const float minX = 32f;
    public const float maxX = 53.3f;
    public const float minZ = -16.7f;
    public const float maxZ = -2.9f;
    public const float ANGLE = 48f;
    public const float rotationSpeed = 3f;
}