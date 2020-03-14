using System;
using DataPresets;
using Entities;
using EventDispatchers;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private DifficultySettingsPreset difficultySettingsPreset;
    [SerializeField] private GameEntitiesPreset gameEntitiesPreset;

    private int currentLevel;
    private int passedLevels;
    private int currentCircle;

    private int minLevelWeight;
    private int maxLevelWeight;

    private GameObject level;
    private Entity player;
    private Transform[] circles;

    public static event Action OnNextLevel;

    private void OnEnable()
    {
        PlayerCollisionEventsDispatcher.OnCircleTransition += ToNextCircle;
        ViewController.OnClick += GenerateLevel;
    }

    private void OnDisable()
    {
        PlayerCollisionEventsDispatcher.OnCircleTransition -= ToNextCircle;
        ViewController.OnClick -= GenerateLevel;
    }

    private void Awake()
    {
        LoadProgress();
        GenerateLevel();
    }

    private void LoadProgress()
    {
        currentLevel = PlayerPrefs.GetInt("level", 1);
        passedLevels = currentLevel - 1;
    }
    
    private void ToNextCircle()
    {
        if (currentCircle == circles.Length - 1)
            NextLevel();
        else
        {
            currentCircle++;
            player.Locate(MoveDirectionOnThisCircle(currentCircle), circles[currentCircle].position,
                GetPointOnACircle(gameEntitiesPreset.CircleSize / 2, false));
        }
    }

    private void NextLevel()
    {
        currentLevel++;
        PlayerPrefs.SetInt("level", currentLevel);
        passedLevels++;

        Destroy(player.gameObject);
        OnNextLevel?.Invoke();
    }
    
    private void GenerateLevel()
    {
        currentCircle = 0;
        difficultySettingsPreset.CalculateLevelWeights(passedLevels, out minLevelWeight, out maxLevelWeight);

        print($"min level weight: {minLevelWeight}\nmax level weight: {maxLevelWeight}");

        if (level)
        {
            Destroy(level);
        }

        level = new GameObject($"Level {currentLevel}");
        GenerateCircles();
        player = GenerateEntity(gameEntitiesPreset.PlayerPrefab, MoveDirectionOnThisCircle(0), circles[0], false);
    }
    
    private int MoveDirectionOnThisCircle(int i) => i % 2 == 0 ? 1 : -1;
    
    private void GenerateCircles()
    {
        float circleSize = gameEntitiesPreset.CircleSize;
        circles = new Transform[difficultySettingsPreset.CirclesCount];

        Vector3 spawnPoint = Vector3.zero;

        for (int i = 0; i < difficultySettingsPreset.CirclesCount; i++)
        {
            circles[i] = Instantiate(gameEntitiesPreset.CirclePrefab, level.transform).transform;
            circles[i].position = spawnPoint;
            circles[i].name = $"Circle {i + 1}";

            if (i >= 1)
                PopulateWithObstacles(circles[i], MoveDirectionOnThisCircle(i));

            spawnPoint += Vector3.forward * circleSize;
        }
    }
    
    private void PopulateWithObstacles(Transform circle, int moveDirectionOnThisCircle)
    {
        int maxAttemptsToPick = 10;
        int circleWeight = Random.Range(minLevelWeight, maxLevelWeight + 1);

        while (circleWeight > 0 && maxAttemptsToPick > 0)
        {
            Obstacle obstaclePrefab = gameEntitiesPreset.ObstaclePrefabs;

            if (circleWeight - obstaclePrefab.Weight >= 0)
            {
                GenerateEntity(obstaclePrefab, moveDirectionOnThisCircle, circle);
                circleWeight -= obstaclePrefab.Weight;
            }
            else
            {
                maxAttemptsToPick--;
            }
        }
    }
    
    private Entity GenerateEntity(Entity entityPrefab, int moveDirectionOnThisCircle, Transform circle,
        bool randomOffset = true)
    {
        Entity entity = Instantiate(entityPrefab);
        entity.transform.SetParent(circle);

        float radius = gameEntitiesPreset.CircleSize / 2;
        entity.Locate(moveDirectionOnThisCircle, circle.position, GetPointOnACircle(radius, randomOffset));
        return entity;
    }

    private Vector3 GetPointOnACircle(float radius, bool random)
    {
        if (!random)
            return new Vector3(0, 1, -1 * radius);

        //two opposite angle ranges from -60 to 60 degrees
        float angle1 = Random.Range(-Mathf.PI/3,Mathf.PI/3);
        float angle2 = Random.Range(2 * Mathf.PI / 3, 4 * Mathf.PI / 3);

        float angle = Random.value >= 0.5f ? angle2 : angle1;

        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;
        return new Vector3(x, 1, z);
    }
}