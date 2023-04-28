using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts
{
    public class TilesGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject startTile;
        [SerializeField] private List<GameObject> tiles;
        [SerializeField] private GameObject endTile;
        [SerializeField] private float tileLenght;
        [SerializeField] private int tilesCount;

        private float _spawnPosZ = 0f;
        private void Start()
        {
            GenerateTiles();
        }

        private void GenerateTiles()
        {
            var currentPosZ = _spawnPosZ;
            if (startTile)
            {
                Vector3 posToSpawn = new Vector3(0f, 0f, currentPosZ);
                Instantiate(startTile, posToSpawn, Quaternion.identity, transform);
                currentPosZ += tileLenght;
            }
            if (tiles.Any())
            {
                for (int i = 0; i < tilesCount; i++)
                {
                    Vector3 posToSpawn = new Vector3(0f, 0f, currentPosZ);
                    Instantiate(tiles[Random.Range(0, tiles.Count)], posToSpawn, Quaternion.identity, transform);
                    currentPosZ += tileLenght;
                }
            }
            if (endTile)
            {
                Vector3 posToSpawn = new Vector3(0f, 0f, currentPosZ);
                Instantiate(endTile, posToSpawn, Quaternion.identity, transform);
                currentPosZ += tileLenght;
            }
        }
    }
}
