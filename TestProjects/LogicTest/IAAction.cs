using System.Threading.Tasks;

namespace LogicTest
{ 
    public interface IAAction
    {
        Task<bool> Execute();

    }
}