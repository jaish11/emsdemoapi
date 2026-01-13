using emsdemoapi.Data.Entities;

namespace emsdemoapi.Data.Interfaces
{
    public interface IState
    {
        List<State> GetAllState();
        State GetStateById(int id);
        bool AddState(State state);
        bool UpdateState(State state);
        bool DeleteState(int id);
    }
}
