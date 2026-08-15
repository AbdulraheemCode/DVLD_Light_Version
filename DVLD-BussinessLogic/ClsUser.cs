using System.Data;
using DVLD_DataAccess;

namespace DVLD_BussinessLogic
{
    public class ClsUser
    {
        public int UserId { get; set; }
        public int PersonId { get; set; }
        public ClsPerson PersonInfo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }


        public ClsUser()
        {
            UserId = -1;
            PersonId = -1;
            UserName = "";
            Password = "";
            IsActive = false;
        }

        private ClsUser(int userId, int personId, string userName, string password, bool isActive)
        {
            this.UserId = userId;
            this.PersonId = personId;
            this.UserName = userName;
            this.Password = password;
            this.IsActive = isActive;
        }

        public static DataTable GetAllUsers()
        {
            return ClsUserData.GetAllUsers();
        }
    }
}