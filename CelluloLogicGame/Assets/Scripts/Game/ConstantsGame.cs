using UnityEngine;

public class ConstantsGame : MonoBehaviour {

    public static InputKeyboard trueInput = InputKeyboard.arrows;
    public static InputKeyboard falseInput = InputKeyboard.wasd;
    public static float maxDistPushCellulo = 4.5f;
    public static float maxDistDrawCellulo = 8f;
    public static float maxDistStartDrawingCellulo = 6f;
    public static bool gameIsRunning = false;
    public static int level1Score = 100;
    public static float level1Time = 120f;
}