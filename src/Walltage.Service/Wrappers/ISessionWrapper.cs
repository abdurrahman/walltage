namespace Walltage.Service.Wrappers
{
    public interface ISessionWrapper
    {
        int UserId { get; set; }
        string UserName { get; set; }
        string UserEmail { get; set; }

        void StartSession(int userId, string userName, string userEmail);
        void EndSession();
    }
}