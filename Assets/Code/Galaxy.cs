using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace lessonTwo
{
    public class Galaxy : MonoBehaviour
    {
        #region privateFields

        [SerializeField] private int numberOfEntities;
        [SerializeField] private GameObject celestialBodyPrefab;
        [SerializeField] private float maxStartDistance;
        [SerializeField] private float maxStartVelocity;
        [SerializeField] private float maxStartMass;
        [SerializeField] private float gravitationModifier;

        private NativeArray<Vector3> positions;
        private NativeArray<Vector3> velocities;
        private NativeArray<Vector3> accelerations;
        private NativeArray<float> masses;

        private TransformAccessArray transformAccessArray;

        #endregion


        #region unityMethods

        private void Start()
        {
            positions = new NativeArray<Vector3>(numberOfEntities, Allocator.Persistent);
            velocities = new NativeArray<Vector3>(numberOfEntities, Allocator.Persistent);
            accelerations = new NativeArray<Vector3>(numberOfEntities, Allocator.Persistent);
            masses = new NativeArray<float>(numberOfEntities, Allocator.Persistent);

            Transform[] transforms = new Transform[numberOfEntities];
            for (int i = 0; i < numberOfEntities; i++)
            {
                positions[i] = Random.insideUnitSphere * Random.Range(0, maxStartDistance);
                velocities[i] = Random.insideUnitSphere * Random.Range(0, maxStartVelocity);
                accelerations[i] = new Vector3();
                masses[i] = Random.Range(1, maxStartMass);
                transforms[i] = Instantiate(celestialBodyPrefab, positions[i], Quaternion.identity).transform;
            }

            transformAccessArray = new TransformAccessArray(transforms);
        }

        private void Update()
        {
            GravitationJob gravitationJob = new GravitationJob()
            {
                Positions = positions,
                Velocities = velocities,
                Accelerations = accelerations,
                Masses = masses,
                GravitationModifier = gravitationModifier,
                DeltaTime = Time.deltaTime
            };
            JobHandle gravitationHandle = gravitationJob.Schedule(numberOfEntities, 0);

            MoveJob moveJob = new MoveJob()
            {
                Positions = positions,
                Velocities = velocities,
                Accelerations = accelerations,
                DeltaTime = Time.deltaTime
            };
            JobHandle moveHandle = moveJob.Schedule(transformAccessArray, gravitationHandle);
            moveHandle.Complete();
        }

        private void OnDestroy()
        {
            positions.Dispose();
            velocities.Dispose();
            accelerations.Dispose();
            masses.Dispose();
            transformAccessArray.Dispose();
        }

        #endregion
    }

}
