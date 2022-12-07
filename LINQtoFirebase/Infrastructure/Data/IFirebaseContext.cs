using System.Threading.Tasks;

namespace WebTech.L2F.Infrastructure
{
    public interface IFirebaseContext
    {
        public IRealtimeDatabaseRecord Retrieve(string jsonPath);
        public void SignInUser(string uid);
    }
}