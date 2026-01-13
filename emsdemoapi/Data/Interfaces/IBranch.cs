using emsdemoapi.Data.Entities;

namespace emsdemoapi.Data.Interfaces
{
    public interface IBranch
    {
        List<Branch> GetAllBranches();
        Branch GetBranchById(int id);
        bool AddBranch(Branch branch);
        bool DeleteBranch(int id);
        bool UpdateBranch(Branch branch);
    }
}
