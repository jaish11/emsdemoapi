using emsdemoapi.Data.Entities;
using emsdemoapi.Data.Interfaces;

namespace emsdemoapi.Data.Services
{
    public class BranchService : IBranch
    {
        private readonly AppDbContext _context;
        private readonly RedisCacheService _redis;
        public BranchService(AppDbContext context, RedisCacheService redis)
        {
            _context = context;
            _redis = redis;
        }

        public List<Branch> GetAllBranches()
        {
            const string cacheKey = "all_branches";
            var cachedBranches = _redis.GetAsync<List<Branch>>(cacheKey).Result;
            if (cachedBranches != null)
            {
                Console.WriteLine("✅ REDIS CACHE HIT");
                return cachedBranches;
            }

            Console.WriteLine("❌ REDIS CACHE MISS – Fetching from DB");
            var branches = _context.Branches.ToList();
            _redis.SetAsync(cacheKey, branches, 10).Wait();
            return branches;
            //return _context.Branches.ToList();
        }

        public Branch GetBranchById(int id)
        {
            string cacheKey = $"branch_{id}";
            var cached = _redis.GetAsync<Branch>(cacheKey).Result;
            if (cached != null)
            {
                return cached;
            }
            var branch = _context.Branches.Find(id);
            if (branch != null)
            {
                _redis.SetAsync(cacheKey, branch, 10).Wait();
            }
            return branch;
            //return _context.Branches.Find(id);
        }

        public bool AddBranch(Branch branch)
        {
            _context.Branches.Add(branch);
            _context.SaveChanges();
            _redis.RemoveAsync("all_branches").Wait();
            return true;
        }

        public bool DeleteBranch(int id)
        {
            Branch branch = _context.Branches.Find(id);
            if (branch == null)
            {
                return false;
            }
            _context.Branches.Remove(branch);
            _context.SaveChanges();
            _redis.RemoveAsync("all_branches").Wait();
            _redis.RemoveAsync($"branch_{id}").Wait();
            return true;
        }

        public bool UpdateBranch(Branch branch)
        {
            _context.Branches.Update(branch);
            _context.SaveChanges();
            _redis.RemoveAsync("all_branches").Wait();
            _redis.RemoveAsync($"branch_{branch.Id}").Wait();
            return true;
        }
    }
}
