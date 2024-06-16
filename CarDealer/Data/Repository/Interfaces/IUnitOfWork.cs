namespace CarDealer.Data.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IMakeRepository Make { get; }
        IVehicleModelRepository VehicleModel { get; }
        IVehicleRepository Vehicle { get; }
        void Save();

    }

}
