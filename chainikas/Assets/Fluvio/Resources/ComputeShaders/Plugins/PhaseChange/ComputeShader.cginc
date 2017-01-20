typedef struct{float4 emitPosition;float4 emitVelocity;float4 pluginParams;} PhaseChangeData;
#define FLUVIO_PLUGIN_DATA_RW_0 PhaseChangeData
#include "../../Includes/FluvioCompute.cginc"
FLUVIO_KERNEL(OnUpdatePlugin){int particleIndex = get_global_id(0);if (FluvioShouldUpdatePlugin(particleIndex)){FLUVIO_BUFFER_RW(PhaseChangeData) phaseChangeData = FluvioGetPluginBuffer(0);float densityThreshold = phaseChangeData[0].pluginParams.x;float comparisonType = phaseChangeData[0].pluginParams.y;phaseChangeData[particleIndex].emitVelocity.w = -1.0f;float density = solverData_GetDensity(particleIndex);if (comparisonType < 0 && density >= densityThreshold) return;if (comparisonType > 0 && density < densityThreshold) return;float4 position = solverData_GetPosition(particleIndex);float4 velocity = solverData_GetVelocity(particleIndex);float invMass = 1.0f / solverData_GetMass(particleIndex);float4 acceleration = solverData_GetFluid(fluvio_PluginFluidID).gravity + solverData_GetForce(particleIndex)*invMass;float dtIter = fluvio_Time.y;int iterations = fluvio_Time.w;for (int iter = 0; iter < iterations; ++iter){float4 t = dtIter*acceleration;if (dot(t,t) > FLUVIO_MAX_SQR_VELOCITY_CHANGE * fluvio_KernelSize.w * fluvio_KernelSize.w){t *= 0;}velocity += t;}phaseChangeData[particleIndex].emitVelocity = velocity;phaseChangeData[particleIndex].emitPosition = position + velocity * fluvio_Time.x;phaseChangeData[particleIndex].emitVelocity.w = solverData_GetLifetime(particleIndex);solverData_SetLifetime(particleIndex, -1.0f);}}