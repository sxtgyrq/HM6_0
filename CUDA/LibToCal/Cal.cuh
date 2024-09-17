#include "cuda_runtime.h"
#include "device_launch_parameters.h"

class Cal
{
public:
	Cal(int* costTime, int* lastFP, int* resultForSave, int costTimeCount, int fPCount, int calUnitCount, int* startDic, int* endDic);

	//int[] costTime, int[] lastFP, int[] resultForSave, int costTimeCount, int FPCount, int[] startDic, int[] endDic
	//int* Cal();
	~Cal();
private:
	int CostTimeCount;

	int PointCount;

	int UnitCount;

	int PointCountPerUnit;
	int SegCountPerUnit;

	int BlockCount;//��ʼ����

	int* CostTime;
	int* CostTime_GPU;

	int* LastFP;
	int* LastFP_GPU = 0;
	int* LastFP_Out_GPU = 0;

	int* StartDic;
	int* StartDic_GPU;

	int* EndDic;
	int* EndDic_GPU;

	int* MinStepResult_GPU;//��ʼ����

	int* MinStepResult_OnOff_GPU;//��ʼ����
	//int* MinStepResult;//��ʼ����

	cudaError_t CalculateMinStep();
	cudaError_t FindMin();
	cudaError_t Reduce();
	cudaError_t	Copy();
	bool NotFinished();
};