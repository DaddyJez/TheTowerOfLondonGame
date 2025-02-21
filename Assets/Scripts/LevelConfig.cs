using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Game/Level Config")]
public class LevelConfig : ScriptableObject
{
    [Header("Целевая конфигурация")]
    public TargetRod[] targetRods;

    [Header("Ограничения")]
    public int maxMoves = 10;

    [System.Serializable]
    public class TargetRod
    {
        public int rodIndex;
        public int[] ringSizes;
    }
}