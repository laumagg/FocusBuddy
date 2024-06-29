using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tauchgang.XR
{
    public class MR_RoomSpawner : MonoBehaviour
    {
        [SerializeField]
        private Transform ObjectToSpawn;
        public SpawnLocation SpawnLocations = SpawnLocation.Floating;
        [SerializeField, Tooltip("When using surface spawning, use this to filter which anchor labels should be included. Eg, spawn only on TABLE or OTHER.")]
        public MRUKAnchor.SceneLabels Labels = ~(MRUKAnchor.SceneLabels)0;
        [SerializeField, Tooltip("Set the layer(s) for the physics bounding box checks, collisions will be avoided with these layers.")]
        public LayerMask LayerMask = -1;
        [SerializeField, Tooltip("If enabled then the spawn position will be checked to make sure there is no overlap with physics colliders including themselves.")]
        public bool CheckOverlaps = true;

        public void StartSpawn()
        {
            foreach (MRUKRoom room in MRUK.Instance.Rooms)
            {
                StartSpawn(room);
            }
        }

        public void StartSpawn(MRUKRoom room)
        {
            var prefabBounds = Utilities.GetPrefabBounds(ObjectToSpawn.gameObject);
            float minRadius = 0.0f;
            const float clearanceDistance = 0.01f;
            Bounds adjustedBounds = new();

            if (prefabBounds.HasValue)
            {
                minRadius = Mathf.Min(-prefabBounds.Value.min.x, -prefabBounds.Value.min.z, prefabBounds.Value.max.x, prefabBounds.Value.max.z);
                if (minRadius < 0f)
                {
                    minRadius = 0f;
                }
                var min = prefabBounds.Value.min;
                var max = prefabBounds.Value.max;
                min.y += clearanceDistance;
                if (max.y < min.y)
                {
                    max.y = min.y;
                }
                adjustedBounds.SetMinMax(min, max);
            }
            int SpawnAmount = 1; // for test
            for (int i = 0; i < SpawnAmount; ++i)
            {
                int MaxIterations = 1000; // for test
                for (int j = 0; j < MaxIterations; ++j)
                {
                    Vector3 spawnPosition = Vector3.zero;
                    Vector3 spawnNormal = Vector3.zero;


                    MRUK.SurfaceType surfaceType = 0;
                    switch (SpawnLocations)
                    {
                        case SpawnLocation.AnySurface:
                            surfaceType |= MRUK.SurfaceType.FACING_UP;
                            surfaceType |= MRUK.SurfaceType.VERTICAL;
                            surfaceType |= MRUK.SurfaceType.FACING_DOWN;
                            break;
                        case SpawnLocation.VerticalSurfaces:
                            surfaceType |= MRUK.SurfaceType.VERTICAL;
                            break;
                        case SpawnLocation.OnTopOfSurfaces:
                            surfaceType |= MRUK.SurfaceType.FACING_UP;
                            break;
                        case SpawnLocation.HangingDown:
                            surfaceType |= MRUK.SurfaceType.FACING_DOWN;
                            break;
                    }
                    if (room.GenerateRandomPositionOnSurface(surfaceType, minRadius, LabelFilter.FromEnum(Labels), out var pos, out var normal))
                    {
                        spawnPosition = pos + normal;
                        spawnNormal = normal;
                        Vector3 center = spawnPosition + normal;
                        // In some cases, surfaces may protrude through walls and end up outside the room
                        // check to make sure the center of the prefab will spawn inside the room
                        if (!room.IsPositionInRoom(center))
                        {
                            continue;
                        }
                        // Ensure the center of the prefab will not spawn inside a scene volume
                        if (room.IsPositionInSceneVolume(center))
                        {
                            continue;
                        }
                        // Also make sure there is nothing close to the surface that would obstruct it
                        if (room.Raycast(new Ray(pos, normal), 0.1f, out _))
                        {
                            continue;
                        }
                    }


                    Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, spawnNormal);
                    if (CheckOverlaps && prefabBounds.HasValue)
                    {
                        if (Physics.CheckBox(spawnPosition + spawnRotation * adjustedBounds.center, adjustedBounds.extents, spawnRotation, LayerMask, QueryTriggerInteraction.Ignore))
                        {
                            continue;
                        }
                    }

                    if (ObjectToSpawn.gameObject.scene.path == null)
                    {
                        Instantiate(ObjectToSpawn, spawnPosition, spawnRotation, transform);
                    }
                    else
                    {
                        ObjectToSpawn.transform.position = spawnPosition;
                        ObjectToSpawn.transform.rotation = spawnRotation;
                        return; // ignore SpawnAmount once we have a successful move of existing object in the scene
                    }
                    break;
                }
            }
        }
    }

    public enum SpawnLocation
    {
        Floating,           // Spawn somewhere floating in the free space within the room
        AnySurface,         // Spawn on any surface (i.e. a combination of all 3 options below)
        VerticalSurfaces,   // Spawn only on vertical surfaces such as walls, windows, wall art, doors, etc...
        OnTopOfSurfaces,    // Spawn on surfaces facing upwards such as ground, top of tables, beds, couches, etc...
        HangingDown         // Spawn on surfaces facing downwards such as the ceiling
    }
}