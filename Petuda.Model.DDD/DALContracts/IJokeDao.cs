namespace Petuda.Model.DDD.DALContracts
{
    public interface IJokeDao: IPetudaDAO<Joke>
    {
        int GetJokesCount();
    }
}