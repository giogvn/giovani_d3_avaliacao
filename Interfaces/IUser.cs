using giovani_d3_avaliacao.Models;

namespace giovani_d3_avaliacao.Interfaces
{
    public interface IUser
    {
        int Create(User newUser);

        int Update(User User, string field, string newValue);

        int Delete(string idUser, string pwd);

        bool Access(string id, string email, string pswd);

        void Cancel();

        void LogOut();

        void ShutDown();
    }
}