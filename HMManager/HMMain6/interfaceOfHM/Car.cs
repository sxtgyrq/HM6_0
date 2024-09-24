namespace HMMain6.interfaceOfHM
{
    public interface Car
    {
        void SendStateOfCar(HMMain6.Player player, HMMain6.Car car, ref List<string> notifyMsg);
        void SetAnimateChanged(HMMain6.Player player, HMMain6.Car car, ref List<string> notifyMsg);


    }
}
