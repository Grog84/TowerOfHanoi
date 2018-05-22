/*
 * CLOUD
 * 
 * Script controlling the clouds spawning behaviour
 * It follows an object pooling pattern, creating a number of clouds
 * equal to the maximum number of clouds allowed on the screen
 * 
 * */

using System.Collections;
using UnityEngine;
using HanoiTower.Setting.Cloud;

namespace HanoiTower.Setting
{

    public class CloudSpawner : MonoBehaviour {

        public GameObject Cloud;

        // Tool Section

        [Header("Clouds Count Control")]
        [Space(5)]
        public int maxActiveCloudsCount = 4;
        [Range(0,1)]
        public float spawnChance;
        [Range(0, 1)]
        public float spawnWaitTime;

        [Space(10)]

        [Header("Cloud Description")]
        [Space(5)]
        [Range(0.5f, 5f)]
        public float minSpeed;
        [Range(0.5f, 5f)]
        public float maxSpeed;

        [Space(5)]

        [Range(0.2f, 2f)]
        public float minScale;
        [Range(0.2f, 2f)]
        public float maxScale;

        [Space(5)]
        public Sprite[] sprites;

        // Internal Variables

        int activeCloudCount = 0;  
        float maxSpawnY;
        float maxSpawnX;

        CloudController[] allClouds;

        public void Init()
        {
            maxSpawnY = Camera.main.orthographicSize;  // Half of the full screen height

            maxSpawnX = Camera.main.aspect * Camera.main.orthographicSize;

            allClouds = new CloudController[maxActiveCloudsCount];
            for (int i = 0; i < maxActiveCloudsCount; i++)
            {
                allClouds[i] = Instantiate(Cloud).GetComponent<CloudController>();
                allClouds[i].Init(this);
            }

            activeCloudCount = Random.Range(1, maxActiveCloudsCount + 1);

            for (int i = 0; i < activeCloudCount; i++)
            {
                GenerateCloud(i, false);
            }

            StartCoroutine(SpawnCO());

        }

        void GenerateCloud( int idx, bool inGameSpawn = true )
        {
            CloudDescription cloudDescription = GenerateRandomDescription( idx );

            allClouds[idx].MorphAs(cloudDescription);
            allClouds[idx].SetPosition(GenerateRandomPosition(inGameSpawn), inGameSpawn);

        }

        CloudDescription GenerateRandomDescription( int ID )
        {
            CloudDescription cloudDescription;

            cloudDescription.speed = Random.Range(minSpeed, maxSpeed);

            cloudDescription.sprite = sprites[Random.Range(0, sprites.Length)];

            cloudDescription.scale = Random.Range(minScale, maxScale);

            cloudDescription.ID = ID;

            return cloudDescription;

        }


        Vector2 GenerateRandomPosition( bool inGameSpawn = true )
        {
            if (inGameSpawn)
            {
                return new Vector2(-maxSpawnX, Random.Range(0, maxSpawnY));
            }

            return new Vector2(Random.Range(-maxSpawnX, maxSpawnX), Random.Range(0, maxSpawnY));
        }


        public float GetXMax()
        {
            return maxSpawnX;
        }

        IEnumerator SpawnCO()
        {
            while (true)
            {
                if (activeCloudCount < maxActiveCloudsCount)
                {
                    if (Random.value <= spawnChance)
                    {
                        for (int i = 0; i < maxActiveCloudsCount; i++)
                        {
                            if (!allClouds[i].GetActiveStatus())
                            {
                                GenerateCloud(i);
                                break;
                            }
                        }
                    }
                }

                yield return new WaitForSeconds(spawnWaitTime);
            }

        }

        private void OnValidate()
        {
            if (minSpeed > maxSpeed)
            {
                maxSpeed = minSpeed;
            }
            else if (maxSpeed < minSpeed)
            {
                minSpeed = maxSpeed;
            }

            if (minScale > maxScale)
            {
                maxScale = minScale;
            }
            else if (maxScale < minScale)
            {
                minScale = maxScale;
            }
        }

    }

}
