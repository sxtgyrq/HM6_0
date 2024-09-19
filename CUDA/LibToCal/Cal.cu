#include "Cal.cuh"
#include <stdio.h> 
#include<math.h>

const int ThreadCount = 1024;
const int MaxValue = 60000 * 1000;

/// <summary>
/// 获取最小的前进值。
/// </summary>
/// <param name="length">CostTimeCount</param>
/// <param name="segCountPerUnit"></param>
/// <param name="pointCountPerUnit"></param>
/// <param name="startDic"></param>
/// <param name="endDic"></param>
/// <param name="lastFP"></param>
/// <param name="minStepResult"></param>
/// <param name="minStepResultOnOff"></param>
/// <param name="costTime"></param>
/// <returns></returns>
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
	//int i = blockIdx.x * blockDim.x + threadIdx.x;
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

__global__ void FindMinOfMinTimeStepResult(int length, int segCountPerUnit, int step, int* minStepResult)
{
	int i = blockIdx.x * blockDim.x + threadIdx.x;

	if (i < length)
	{
		int columnIndex = i % segCountPerUnit;
		if (columnIndex % (step << 1) == 0) {
			if (columnIndex + step < segCountPerUnit)
			{
				minStepResult[i] =
					minStepResult[i] < minStepResult[i + step] ? minStepResult[i] : minStepResult[i + step];
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

__global__ void FindMinOfIndexOfFPStepResult(int length, int step, int pointCountPerUnit, int* minStepResultOut)
{
	int i = blockIdx.x * blockDim.x + threadIdx.x;
	if (i < length) {
		int columnIndex = i % pointCountPerUnit;
		if (columnIndex % (step << 1) == 0)
		{
			if (columnIndex + step < pointCountPerUnit)
			{
				minStepResultOut[i] =
					minStepResultOut[i] < minStepResultOut[i + step] ? minStepResultOut[i] : minStepResultOut[i + step];
			}
		}
	}
}

Cal::Cal(int* costTime, int* lastFP, int costTimeCount, int fPCount, int calUnitCount, int* startDic, int* endDic)
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

	cudaError_t cudaStatus = cudaDeviceReset();
	if (cudaStatus != cudaSuccess) {
		fprintf(stderr, "161cudaDeviceReset failed: %s\n", cudaGetErrorString(cudaStatus));
		goto Error;
	}

	/*cudaStatus = cudaGetLastError();
	if (cudaStatus != cudaSuccess) {
		fprintf(stderr, "167--started failed: %s\n", cudaGetErrorString(cudaStatus));
		goto Error;
	}*/
	cudaStatus = cudaSetDevice(0);
	if (cudaStatus != cudaSuccess) {
		fprintf(stderr, "cudaSetDevice failed!  Do you have a CUDA-capable GPU installed?");
		goto Error;
	}
	{
		//cudaError_t cudaStatus = cudaDeviceSynchronize();
		//if (cudaStatus != cudaSuccess) {
		//	fprintf(stderr, "cudaDeviceSynchronize returned error code %d after launching addKernel!\n", cudaStatus);
		//	//goto Error;
		//}
	}
	{
		this->CostTime = costTime;


		this->CostTime_GPU = 0;
		cudaStatus = cudaMalloc((void**)&this->CostTime_GPU, this->CostTimeCount * sizeof(int));
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "CostTime_GPU cudaMalloc failed!\n");
			goto Error;
		}
		/*	cudaStatus = cudaGetLastError();
			if (cudaStatus != cudaSuccess) {
				fprintf(stderr, "176--CostTime_GPU launch failed: %s\n", cudaGetErrorString(cudaStatus));
				goto Error;
			}*/
		cudaStatus = cudaMemcpy(this->CostTime_GPU, this->CostTime, this->CostTimeCount * sizeof(int), cudaMemcpyHostToDevice);
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "CostTime_GPU cudaMemcpy failed!\n");
			goto Error;
		}
		/*	cudaStatus = cudaGetLastError();
			if (cudaStatus != cudaSuccess) {
				fprintf(stderr, "CostTime_GPU cudaMemcpy failed!%s\n", cudaGetErrorString(cudaStatus));
				goto Error;
			}*/
	}
	{
		this->LastFP = lastFP;
		this->LastFP_GPU = 0;
		this->LastFP_Out_GPU = 0;

		cudaStatus = cudaMalloc((void**)&this->LastFP_GPU, this->PointCount * sizeof(int));
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "LastFP_GPU cudaMalloc failed!");
			goto Error;
		}
		/*cudaStatus = cudaGetLastError();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "LastFP_GPU launch failed: %s\n", cudaGetErrorString(cudaStatus));
			goto Error;
		}*/
		cudaStatus = cudaMemcpy(this->LastFP_GPU, this->LastFP, this->PointCount * sizeof(int), cudaMemcpyHostToDevice);
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "LastFP_GPU failed!");
			goto Error;
		}
		/*cudaStatus = cudaGetLastError();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "LastFP_GPU copy failed: %s\n", cudaGetErrorString(cudaStatus));
			goto Error;
		}*/

		cudaStatus = cudaMalloc((void**)&this->LastFP_Out_GPU, this->PointCount * sizeof(int));
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaMalloc failed!");
			goto Error;
		}
		/*cudaStatus = cudaGetLastError();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "LastFP_GPU launch failed: %s\n", cudaGetErrorString(cudaStatus));
			goto Error;
		}*/

	}
	{
		this->StartDic = startDic;
		this->StartDic_GPU = 0;

		cudaStatus = cudaMalloc((void**)&this->StartDic_GPU, this->SegCountPerUnit * sizeof(int));
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaMalloc failed!");
			goto Error;
		}
		/*cudaStatus = cudaGetLastError();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "StartDic_GPU launch failed: %s\n", cudaGetErrorString(cudaStatus));
			goto Error;
		}*/
		cudaStatus = cudaMemcpy(this->StartDic_GPU, this->StartDic, this->SegCountPerUnit * sizeof(int), cudaMemcpyHostToDevice);
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaMalloc failed!");
			goto Error;
		}
		/*cudaStatus = cudaGetLastError();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "StartDic_GPU launch failed: %s\n", cudaGetErrorString(cudaStatus));
			goto Error;
		}*/
	}

	{
		this->EndDic = endDic;
		this->EndDic_GPU = 0;

		cudaStatus = cudaMalloc((void**)&this->EndDic_GPU, this->SegCountPerUnit * sizeof(int));
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaMalloc failed!");
			goto Error;
		}
		/*cudaStatus = cudaGetLastError();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "EndDic_GPU launch failed: %s\n", cudaGetErrorString(cudaStatus));
			goto Error;
		}*/
		cudaStatus = cudaMemcpy(this->EndDic_GPU, this->EndDic, this->SegCountPerUnit * sizeof(int), cudaMemcpyHostToDevice);
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaMalloc failed!");
			goto Error;
		}
		/*cudaStatus = cudaGetLastError();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "EndDic_GPU launch failed: %s\n", cudaGetErrorString(cudaStatus));
			goto Error;
		}*/
	}
	{
		this->MinStepResult_GPU = 0;

		cudaStatus = cudaMalloc((void**)&this->MinStepResult_GPU, this->CostTimeCount * sizeof(int));
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaMalloc failed!");
			goto Error;
		}
		/*	cudaStatus = cudaGetLastError();
			if (cudaStatus != cudaSuccess) {
				fprintf(stderr, "MinStepResult_GPU launch failed: %s\n", cudaGetErrorString(cudaStatus));
				goto Error;
			}*/
		cudaStatus = cudaMemcpy(this->MinStepResult_GPU, this->CostTime, this->CostTimeCount * sizeof(int), cudaMemcpyHostToDevice);
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaMemcpy failed!");
			goto Error;
		}
		/*	cudaStatus = cudaGetLastError();
			if (cudaStatus != cudaSuccess) {
				fprintf(stderr, "MinStepResult_GPU launch failed: %s\n", cudaGetErrorString(cudaStatus));
				goto Error;
			}*/
	}

	/*{
		this->MinStepResult_CALMINVALUE_GPU = 0;

		cudaStatus = cudaMalloc((void**)&this->MinStepResult_CALMINVALUE_GPU, this->CostTimeCount * sizeof(int));
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaMalloc failed!");
			goto Error;
		}
		cudaStatus = cudaGetLastError();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "getReduceF launch failed: %s\n", cudaGetErrorString(cudaStatus));
			goto Error;
		}
		cudaStatus = cudaMemcpy(this->MinStepResult_CALMINVALUE_GPU, this->CostTime, this->CostTimeCount * sizeof(int), cudaMemcpyHostToDevice);
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaMemcpy failed!");
			goto Error;
		}
		cudaStatus = cudaGetLastError();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "getReduceF launch failed: %s\n", cudaGetErrorString(cudaStatus));
			goto Error;
		}
	}*/
	//MinStepResult_CALMINVALUE_GPU
	//MinStepResult_OnOff_GPU

	{
		this->MinStepResult_OnOff_GPU = 0;

		cudaStatus = cudaMalloc((void**)&this->MinStepResult_OnOff_GPU, this->CostTimeCount * sizeof(int));
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaMalloc failed!");
			goto Error;
		}
		/*cudaStatus = cudaGetLastError();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "MinStepResult_OnOff_GPU launch failed: %s\n", cudaGetErrorString(cudaStatus));
			goto Error;
		}*/
		cudaStatus = cudaMemcpy(this->MinStepResult_OnOff_GPU, this->CostTime, this->CostTimeCount * sizeof(int), cudaMemcpyHostToDevice);
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaMemcpy failed!");
			goto Error;
		}
		/*cudaStatus = cudaGetLastError();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "MinStepResult_OnOff_GPU launch failed: %s\n", cudaGetErrorString(cudaStatus));
			goto Error;
		}*/
	}

	do {
		CalculateMinStep();
		Reduce();
		Copy();
	} while (NotFinished());

	Copy();


	{
		cudaStatus = cudaMemcpy(this->LastFP, this->LastFP_GPU, this->PointCount * sizeof(int), cudaMemcpyDeviceToHost);
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaMemcpy failed 377!");
			goto Error;
		}
	}


	cudaFree(this->CostTime_GPU);
	cudaFree(this->LastFP_GPU);
	cudaFree(this->LastFP_Out_GPU);
	cudaFree(this->StartDic_GPU);
	cudaFree(this->EndDic_GPU);
	cudaFree(this->MinStepResult_GPU);
	cudaFree(this->MinStepResult_OnOff_GPU);

	cudaStatus = cudaDeviceReset();
	if (cudaStatus != cudaSuccess) {
		fprintf(stderr, "cudaDeviceReset failed: %s\n", cudaGetErrorString(cudaStatus));
		goto Error;
	}
	this->LastFPResult = this->LastFP;

	return;
Error:
	cudaFree(this->CostTime_GPU);
	cudaFree(this->LastFP_GPU);
	cudaFree(this->LastFP_Out_GPU);
	cudaFree(this->StartDic_GPU);
	cudaFree(this->EndDic_GPU);
	cudaFree(this->MinStepResult_GPU);
	cudaFree(this->MinStepResult_OnOff_GPU);

	cudaStatus = cudaDeviceReset();
	if (cudaStatus != cudaSuccess) {
		fprintf(stderr, "cudaDeviceReset Error failed: %s\n", cudaGetErrorString(cudaStatus));
		goto Error;
	}

	return;
}


//int length, int segCountPerUnit, int pointCountPerUnit, int* startDic, int* endDic, int* lastFP, int* minStepResult, int* minStepResultOnOff, int* costTime
cudaError_t Cal::CalculateMinStep()
{
	cudaError_t cudaStatus;
	// this->CostTime_GPU
			// Launch a kernel on the GPU with one thread for each element.

	getMinStepFF << <this->BlockCount, ThreadCount >> > (this->CostTimeCount, this->SegCountPerUnit, this->PointCountPerUnit, this->StartDic_GPU, this->EndDic_GPU, this->LastFP_GPU, this->MinStepResult_GPU, this->MinStepResult_OnOff_GPU, this->CostTime_GPU);
	//addKernel(dev_c, dev_a, dev_b);
	// Check for any errors launching the kernel


	cudaStatus = cudaGetLastError();
	//If you look in the programming guide you will see that the maximum amount of threads per block is 512
	if (cudaStatus != cudaSuccess) {
		fprintf(stderr, "getMinStepFF launch failed: %s\n", cudaGetErrorString(cudaStatus));
		goto Error;
	}

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
		//FindMinOfMinTimeStepResult(int length, int segCountPerUnit, int step, int* minStepResult)

		FindMinOfMinTimeStepResult << <this->BlockCount, ThreadCount >> > (this->CostTimeCount, this->SegCountPerUnit, step, this->MinStepResult_GPU);
		//FindMinOfMinStepResult << <this->BlockCount, ThreadCount >> > (this->CostTimeCount, this->SegCountPerUnit, step, this->MinStepResult_GPU);
		//cudaError_t cudaStatus;
		cudaStatus = cudaGetLastError();
		//If you look in the programming guide you will see that the maximum amount of threads per block is 512
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "FindMinOfMinStepResult launch failed: %s\n", cudaGetErrorString(cudaStatus));
			goto Error;
		}
		cudaStatus = cudaDeviceSynchronize();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaDeviceSynchronize returned error code %d after launching addKernel!\n", cudaStatus);
			goto Error;
		}
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
	//int length, int segCountPerUnit, int pointCountPerUnit, int* startDic, int* endDic, int* lastFP, int* minStepResult, int* minStepResultOnOff, int* costTime
	getReduceF << <this->BlockCount, ThreadCount >> > (
		this->CostTimeCount,
		this->SegCountPerUnit,
		this->PointCountPerUnit,
		this->StartDic_GPU,
		this->EndDic_GPU,
		this->LastFP_GPU,
		this->MinStepResult_GPU,
		this->MinStepResult_OnOff_GPU,
		this->CostTime_GPU
		);
	cudaStatus = cudaGetLastError();
	if (cudaStatus != cudaSuccess) {
		fprintf(stderr, "getReduceFun launch failed: %s\n", cudaGetErrorString(cudaStatus));
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

cudaError_t  Cal::Copy()
{
	cudaError_t cudaStatus;
	{
		CopyMinStepResult << <this->BlockCount, ThreadCount >> > (this->PointCount, this->LastFP_GPU, this->LastFP_Out_GPU);
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

		//CopyMinStepResult << <this->BlockCount, ThreadCount >> >（this
	}
	return cudaStatus;
Error:
	return cudaStatus;
}

bool Cal::NotFinished()
{
	int step = 1;

	while (step < this->PointCountPerUnit)
	{
		//nt length, int step, int* minStepResultOut
	//	this->MinStepResult_GPU  

		FindMinOfIndexOfFPStepResult << <this->BlockCount, ThreadCount >> > (this->PointCount, step, this->PointCountPerUnit, this->LastFP_Out_GPU);

		cudaError_t	cudaStatus = cudaGetLastError();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "MinStepResult_OnOff_GPU launch failed: %s\n", cudaGetErrorString(cudaStatus));

		}
		cudaStatus = cudaDeviceSynchronize();
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaDeviceSynchronize returned error code %d after launching addKernel!\n", cudaStatus);
			//	goto Error;
		}

		step = step << 1;
	}

	//	this->PointCount
	//int h_data[this->PointCount];
	int* h_data = new int[this->PointCount];
	cudaError_t cudaStatus = cudaMemcpy(h_data, this->LastFP_Out_GPU, this->PointCount * sizeof(int), cudaMemcpyDeviceToHost);
	if (cudaStatus != cudaSuccess) {
		fprintf(stderr, "cudaDeviceSynchronize returned error code %d after launching addKernel!\n", cudaStatus);
		//goto Error;
	}
	cudaStatus = cudaDeviceSynchronize();
	if (cudaStatus != cudaSuccess) {
		fprintf(stderr, "cudaDeviceSynchronize returned error code %d after launching addKernel!\n", cudaStatus);
		//	goto Error;
	}
	for (int i = 0; i < this->PointCount; i++)
	{
		fprintf(stderr, "%d ，", h_data[i]);
	}
	fprintf(stderr, "\n");
	//cudaError_t cudaStatus = cudaDeviceSynchronize();
	//if (cudaStatus != cudaSuccess) {
	//	fprintf(stderr, "cudaDeviceSynchronize returned error code %d after launching addKernel!\n", cudaStatus);
	//	//goto Error;
	//}
	bool notFinished = false;
	for (int i = 0; i < this->PointCount; i += this->PointCountPerUnit)
	{
		notFinished = notFinished || (h_data[i] == -1);
		if (notFinished)
		{
			break;
		}
	}
	return notFinished;
}

Cal::~Cal()
{
	cudaFree(this->CostTime_GPU);
	cudaFree(this->LastFP_GPU);
	cudaFree(this->LastFP_Out_GPU);
	cudaFree(this->StartDic_GPU);
	cudaFree(this->EndDic_GPU);
	cudaFree(this->MinStepResult_GPU);
	//cudaFree(this->MinStepResult_CALMINVALUE_GPU);
	cudaFree(this->MinStepResult_OnOff_GPU);
	cudaError_t	cudaStatus = cudaDeviceReset();
	if (cudaStatus != cudaSuccess) {
		fprintf(stderr, "cudaDeviceReset failed!");
	}
	else
	{
		//fprintf(stderr, "~Cal() cudaDeviceReset success!");
	}
}

extern "C" __declspec(dllexport) Cal * MCal_Create(int* costTime, int* lastFP, int costTimeCount, int fPCount, int calUnitCount, int* startDic, int* endDic)
{
	Cal* rc = new Cal(costTime, lastFP, costTimeCount, fPCount, calUnitCount, startDic, endDic);
	return rc;
}

extern "C" __declspec(dllexport) void  MCal_Delete(Cal * cal)
{
	cal->~Cal();
	//pFoo->~PolygonExtrude(); 
	delete cal;
}
extern "C" __declspec(dllexport) int* MCal_LastFPResult(Cal * cal)
{
	return	cal->LastFPResult;
}