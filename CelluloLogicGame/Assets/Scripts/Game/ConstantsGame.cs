using UnityEngine;

public class ConstantsGame : MonoBehaviour {

    public static InputKeyboard trueInput = InputKeyboard.arrows;
    public static InputKeyboard falseInput = InputKeyboard.wasd;
    public static float maxDistPushCellulo = 4.25f;
    public static float maxDistDrawCellulo = 8f;
    public static float maxDistStartDrawingCellulo = 6f;
    public static bool gameIsRunning = false;
    public static int level1Score = 100;
    public static float level1Time = 120f;

    //Gardian
    public static float minX = 32f;
    public static float maxX = 53.3f;
    public static float minZ = -16.7f;
    public static float maxZ = -2.9f;
}