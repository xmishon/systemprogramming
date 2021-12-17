﻿using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace lessonTwo
{
    public struct GravitationJob : IJobParallelFor
    {
        #region publicVariables

        [ReadOnly] public NativeArray<Vector3> Positions;
        [ReadOnly] public NativeArray<Vector3> Velocities;
        public NativeArray<Vector3> Accelerations;
        [ReadOnly] public NativeArray<float> Masses;
        [ReadOnly] public float GravitationModifier;
        [ReadOnly] public float DeltaTime;

        #endregion

        public void Execute(int index)
        {
            for (int i = 0; i < Positions.Length; i++)
            {
                if (i == index) continue;

                float distance = Vector3.Distance(Positions[i], Positions[index]);
                Vector3 direction = Positions[i] - Positions[index];
                Vector3 gravitation = (direction * Masses[i] * GravitationModifier) / (Masses[index] * Mathf.Pow(distance, 2));
                Accelerations[index] += gravitation * DeltaTime;
            }
        }
    }
}
