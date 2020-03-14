using Entities;
using UnityEngine;

namespace DataPresets
{
    [CreateAssetMenu(fileName = "NewGameEntitiesPreset",menuName = "GameEntitiesPreset",order = 4)]
    public class GameEntitiesPreset : ScriptableObject
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject circlePrefab;
        [SerializeField] private Player playerPrefab;
        [SerializeField] private Obstacle[] obstaclesPrefabs;

        public GameObject CirclePrefab => circlePrefab;
        public float CircleSize => circlePrefab.GetComponent<MeshRenderer>().bounds.size.z;
        public Player PlayerPrefab => playerPrefab;
        public Obstacle ObstaclePrefabs => obstaclesPrefabs[Random.Range(0,obstaclesPrefabs.Length)];
    }
}
