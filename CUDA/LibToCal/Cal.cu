#include "Cal.cuh"
#include <stdio.h> 
#include<math.h>

const int ThreadCount = 1024;
const int MaxValue = 60000 * 1000;

__global__ void getMinStepFF(int length, int segCountPerUnit, int pointCountPerUnit, int* startDic, int* endDic, int* lastFP, int* minStepResult, int* minStepResultOnOff, int* costTime)
{
	//int bx = blockIdx.x;

	//int tx = threadIdx.x;
	//getMinStepF << <this->BlockCount, ThreadCount >> > (this->Length, this->LeftStepWithPass_GPU, this->MinStepResult_GPU);
	int i = blockIdx.x * blockDim.x + threadIdx.x;
	if (i < length) {

		//if(i>=input.)
		int startIndex = startDic[i % segCountPerUnit];
		int endIndex = endDic[i % segCountPerUnit];
		int unitIndex = i / segCountPerUnit;

		//int segIndex=

		if (lastFP[endIndex + unitIndex * pointCountPerUnit] == -1)
		{
			if (lastFP[startIndex + unitIndex * pointCountPerUnit] == -1)
			{
				minStepResult[i] = MaxValue;
				minStepResultOnOff[i] = 0;
			}
			else
			{
				minStepResult[i] = costTime[i];
				minStepResultOnOff[i] = 1;
			}
		}
		else
		{
			minStepResult[i] = MaxValue;
			minStepResultOnOff[i] = 0;
		}
	}
}


__global__ void getReduceF(int length, int segCountPerUnit, int pointCountPerUnit, int* startDic, int* endDic, int* lastFP, int* minStepResult, int* minStepResultOnOff, int* costTime)
{
	int i = blockIdx.x * blockDim.x + threadIdx.x;
	int i = blockIdx.x * blockDim.x + threadIdx.x;
	if (i < length) {
		int startIndex = startDic[i % segCountPerUnit];
		int endIndex = endDic[i % segCountPerUnit];
		int unitIndex = i / segCountPerUnit;
		if (minStepResultOnOff[i] == 1)
		{
			if (costTime[i] - minStepResult[i] * minStepResultOnOff[i] == 0)
			{
				if (lastFP[unitIndex * pointCountPerUnit + endIndex] == -1)
				{
					lastFP[unitIndex * pointCountPerUnit + endIndex] = startIndex;
				}

			}
			else
			{

			}
			costTime[i] -= minStepResult[i] * minStepResultOnOff[i];
		}
	}
}

__global__ void FindMinOfMinStepResult(int length, int segCountPerUnit, int step, int* minStepResult)
{
	int i = blockIdx.x * blockDim.x + threadIdx.x;

	if (i < length)
	{
		int indexOfItem = i % segCountPerUnit;
		int indexOfUnit = i / segCountPerUnit;
		int indexBaseOfUnit = indexOfUnit * segCountPerUnit;



		if ((i - indexBaseOfUnit) % (step << 1) == 0)

			if (i - indexBaseOfUnit < segCountPerUnit) {
				if (i - indexBaseOfUnit + step < indexBaseOfUnit + segCountPerUnit)
				{
					minStepResult[i] =
						minStepResult[i] > minStepResult[i + step] ? minStepResult[i] : minStepResult[i + step];
				}
			}
	}
}


__global__ void CopyMinStepResult(int length, int* minStepResult, int* minStepResultOut)
{
	int i = blockIdx.x * blockDim.x + threadIdx.x;

	if (i < length)
	{
		minStepResultOut[i] = minStepResult[i];
	}
}
Cal::Cal(int* costTime, int* lastFP, int* resultForSave, int costTimeCount, int fPCount, int calUnitCount, int* startDic, int* endDic)
{

	this->CostTimeCount = costTimeCount;
	this->PointCount = fPCount;
	this->UnitCount = calUnitCount;
	this->PointCountPerUnit = fPCount / calUnitCount;
	this->SegCountPerUnit = costTimeCount / calUnitCount;

	if (this->CostTimeCount % ThreadCount == 0)
	{
		this->BlockCount = this->CostTimeCount / ThreadCount;
	}
	else
	{
		this->BlockCount = this->CostTimeCount / ThreadCount + 1;
	}



	//this->MinStepResult = minStepResult;
	//this->MinStepValue = MaxValue;

	//this->Direct_GPU = 0;
	cudaError_t cudaStatus;
	cudaStatus = cudaSetDevice(0);
	if (cudaStatus != cudaSuccess) {
		fprintf(stderr, "cudaSetDevice failed!  Do you have a CUDA-capable GPU installed?");
		goto Error;
	}
	cudaStatus = cudaGetLastError();
	if (cudaStatus != cudaSuccess) {
		fprintf(stderr, "getReduceF launch failed: %s\n", cudaGetErrorString(cudaStatus));
		goto Error;
	}
	{
		this->CostTime = costTime;


		this->CostTime_GPU = 0;
		cudaStatus = cudaMalloc((void**)&this->CostTime_GPU, this->CostTimeCount * sizeof(int));
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaMalloc failed!");
			goto Error;
		}
		cudaStatus = cudaGetLastError();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "getReduceF launch failed: %s\n", cudaGetErrorString(cudaStatus));
			goto Error;
		}
		cudaStatus = cudaMemcpy(this->CostTime_GPU, this->CostTime, this->CostTimeCount * sizeof(int), cudaMemcpyHostToDevice);
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaMemcpy failed!");
			goto Error;
		}
		cudaStatus = cudaGetLastError();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "getReduceF launch failed: %s\n", cudaGetErrorString(cudaStatus));
			goto Error;
		}
	}
	{
		this->LastFP = lastFP;
		this->LastFP_GPU = 0;
		this->LastFP_Out_GPU = 0;

		cudaStatus = cudaMalloc((void**)&this->LastFP_GPU, this->PointCount * sizeof(int));
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaMalloc failed!");
			goto Error;
		}
		cudaStatus = cudaGetLastError();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "getReduceF launch failed: %s\n", cudaGetErrorString(cudaStatus));
			goto Error;
		}
		cudaStatus = cudaMemcpy(this->LastFP_GPU, this->CostTime, this->CostTimeCount * sizeof(int), cudaMemcpyHostToDevice);
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaMemcpy failed!");
			goto Error;
		}
		cudaStatus = cudaGetLastError();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "getReduceF launch failed: %s\n", cudaGetErrorString(cudaStatus));
			goto Error;
		}
	}
	{
		this->StartDic = startDic;
		this->StartDic_GPU = 0;

		cudaStatus = cudaMalloc((void**)&this->StartDic_GPU, this->PointCount / this->UnitCount * sizeof(int));
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaMalloc failed!");
			goto Error;
		}
		cudaStatus = cudaGetLastError();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "getReduceF launch failed: %s\n", cudaGetErrorString(cudaStatus));
			goto Error;
		}
		cudaStatus = cudaMemcpy((void**)&this->StartDic_GPU, this->StartDic, this->PointCount / this->UnitCount * sizeof(int), cudaMemcpyHostToDevice);
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaMalloc failed!");
			goto Error;
		}
		cudaStatus = cudaGetLastError();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "getReduceF launch failed: %s\n", cudaGetErrorString(cudaStatus));
			goto Error;
		}
	}

	{
		this->EndDic = endDic;
		this->EndDic_GPU = 0;

		cudaStatus = cudaMalloc((void**)&this->EndDic_GPU, this->PointCount / this->UnitCount * sizeof(int));
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaMalloc failed!");
			goto Error;
		}
		cudaStatus = cudaGetLastError();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "getReduceF launch failed: %s\n", cudaGetErrorString(cudaStatus));
			goto Error;
		}
		cudaStatus = cudaMemcpy((void**)&this->EndDic_GPU, this->EndDic, this->PointCount / this->UnitCount * sizeof(int), cudaMemcpyHostToDevice);
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaMalloc failed!");
			goto Error;
		}
		cudaStatus = cudaGetLastError();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "getReduceF launch failed: %s\n", cudaGetErrorString(cudaStatus));
			goto Error;
		}
	}
	{
		this->MinStepResult_GPU = 0;

		cudaStatus = cudaMalloc((void**)&this->MinStepResult_GPU, this->CostTimeCount * sizeof(int));
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaMalloc failed!");
			goto Error;
		}
		cudaStatus = cudaGetLastError();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "getReduceF launch failed: %s\n", cudaGetErrorString(cudaStatus));
			goto Error;
		}
		cudaStatus = cudaMemcpy(this->MinStepResult_GPU, this->CostTime, this->CostTimeCount * sizeof(int), cudaMemcpyHostToDevice);
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaMemcpy failed!");
			goto Error;
		}
		cudaStatus = cudaGetLastError();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "getReduceF launch failed: %s\n", cudaGetErrorString(cudaStatus));
			goto Error;
		}
	}
	//MinStepResult_OnOff_GPU

	{
		this->MinStepResult_OnOff_GPU = 0;

		cudaStatus = cudaMalloc((void**)&this->MinStepResult_OnOff_GPU, this->CostTimeCount * sizeof(int));
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaMalloc failed!");
			goto Error;
		}
		cudaStatus = cudaGetLastError();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "getReduceF launch failed: %s\n", cudaGetErrorString(cudaStatus));
			goto Error;
		}
		cudaStatus = cudaMemcpy(this->MinStepResult_OnOff_GPU, this->CostTime, this->CostTimeCount * sizeof(int), cudaMemcpyHostToDevice);
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaMemcpy failed!");
			goto Error;
		}
		cudaStatus = cudaGetLastError();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "getReduceF launch failed: %s\n", cudaGetErrorString(cudaStatus));
			goto Error;
		}
	}

	do {
		CalculateMinStep();
		Reduce();
		Copy();
	} while (NotFinished());

	cudaStatus = cudaMemcpy(this->LastRecord, this->LastRecord_GPU, this->Length * sizeof(int), cudaMemcpyDeviceToHost);
	if (cudaStatus != cudaSuccess) {
		fprintf(stderr, "cudaMemcpy failed!");
		goto Error;
	}
	cudaStatus = cudaDeviceSynchronize();
	if (cudaStatus != cudaSuccess) {
		fprintf(stderr, "copy Result error!\n", cudaStatus);
		goto Error;
	}
	cudaFree(this->Direct_GPU);
	cudaFree(this->IsTargetFP_GPU);
	cudaFree(this->LastRecord_GPU);
	cudaFree(this->LastRecord_Out_GPU);
	cudaFree(this->LeftStepWithPass_GPU);
	cudaFree(this->LeftStepWithPass_Out_GPU);
	cudaFree(this->MinStepResult_GPU);
	cudaFree(this->NotPassedTargetFPStaticc_GPU);
	return;
Error:
	/*cudaFree(LeftStep_Input);
	cudaFree(MinStep_Output);*/
	cudaFree(this->Direct_GPU);
	cudaFree(this->IsTargetFP_GPU);
	cudaFree(this->LastRecord_GPU);
	cudaFree(this->LastRecord_Out_GPU);
	cudaFree(this->LeftStepWithPass_GPU);
	cudaFree(this->LeftStepWithPass_Out_GPU);
	cudaFree(this->MinStepResult_GPU);
	cudaFree(this->NotPassedTargetFPStaticc_GPU);
	return;
}



cudaError_t Cal::CalculateMinStep()
{
	cudaError_t cudaStatus;
	// this->CostTime_GPU
			// Launch a kernel on the GPU with one thread for each element.
	getMinStepFF << <this->BlockCount, ThreadCount >> > (this->CostTimeCount, this->SegCountPerUnit, this->PointCountPerUnit, this->StartDic_GPU, this->EndDic_GPU, this->LastFP_GPU, this->MinStepResult_GPU, this->CostTime_GPU);
	//addKernel(dev_c, dev_a, dev_b);
	// Check for any errors launching the kernel
	cudaStatus = cudaGetLastError();
	//If you look in the programming guide you will see that the maximum amount of threads per block is 512
	if (cudaStatus != cudaSuccess) {
		fprintf(stderr, "getMinStepFF launch failed: %s\n", cudaGetErrorString(cudaStatus));
		goto Error;
	}

	// cudaDeviceSynchronize waits for the kernel to finish, and returns
	// any errors encountered during the launch.
	cudaStatus = cudaDeviceSynchronize();
	if (cudaStatus != cudaSuccess) {
		fprintf(stderr, "cudaDeviceSynchronize returned error code %d after launching addKernel!\n", cudaStatus);
		goto Error;
	}

	FindMin();
	return cudaStatus;
Error:
	/*cudaFree(LeftStep_Input);
	cudaFree(MinStep_Output);*/
	return cudaStatus;
}

cudaError_t Cal::FindMin()
{
	cudaError_t cudaStatus;
	int step = 1;
	while (step < this->SegCountPerUnit)
	{

		FindMinOfMinStepResult << <this->BlockCount, ThreadCount >> > (this->CostTimeCount, this->SegCountPerUnit, step, this->MinStepResult_GPU);
		//cudaError_t cudaStatus;
		cudaStatus = cudaGetLastError();
		//If you look in the programming guide you will see that the maximum amount of threads per block is 512
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "FindMinOfMinStepResult launch failed: %s\n", cudaGetErrorString(cudaStatus));
			goto Error;
		}
		/*	cudaStatus = cudaDeviceSynchronize();
			if (cudaStatus != cudaSuccess) {
				fprintf(stderr, "cudaDeviceSynchronize returned error code %d after launching addKernel!\n", cudaStatus);
				goto Error;
			}*/
		step = step << 1;
	}
	return cudaStatus;
Error:
	/*cudaFree(LeftStep_Input);
	cudaFree(MinStep_Output);*/
	return cudaStatus;
	//return cudaError_t();
}

cudaError_t Cal::Reduce()
{
	//	this->MinStepResult_GPU
	cudaError_t cudaStatus;

	getReduceF << <this->BlockCount, ThreadCount >> > (this->Length, this->MinStepResult_GPU, this->LeftStepWithPass_GPU, this->LeftStepWithPass_Out_GPU, this->LastRecord_GPU, this->LastRecord_Out_GPU, this->Direct_GPU);
	cudaStatus = cudaGetLastError();
	if (cudaStatus != cudaSuccess) {
		fprintf(stderr, "getReduceF launch failed: %s\n", cudaGetErrorString(cudaStatus));
		goto Error;
	}
	cudaStatus = cudaDeviceSynchronize();
	if (cudaStatus != cudaSuccess) {
		fprintf(stderr, "cudaDeviceSynchronize returned error code %d after launching addKernel!\n", cudaStatus);
		goto Error;
	}
	return cudaStatus;
Error:
	/*cudaFree(LeftStep_Input);
	cudaFree(MinStep_Output);*/
	return cudaStatus;
}

cudaError_t Copy()
{
	cudaError_t cudaStatus;
	{
		CopyMinStepResult << <this->BlockCount, ThreadCount >> > (this->Length, this->LeftStepWithPass_GPU, this->LeftStepWithPass_Out_GPU);
		cudaStatus = cudaGetLastError();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "CopyLeftStepWithPass launch failed: %s\n", cudaGetErrorString(cudaStatus));
			goto Error;
		}
		cudaStatus = cudaDeviceSynchronize();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaDeviceSynchronize returned error code %d after launching addKernel!\n", cudaStatus);
			goto Error;
		}
	} 
	return cudaStatus;
Error:
	return cudaStatus;
}