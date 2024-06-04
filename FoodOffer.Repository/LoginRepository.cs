using FoodOffer.Infrastructure;
using FoodOffer.Model.Models;
using clasificados.Infraestructure.DbContextConfig.DbModels;
using AutoMapper;
using FoodOffer.Model.Services;

namespace FoodOffer.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbConecction _session;
        private readonly IMapper _mapper;
        
        public LoginRepository(IDbConecction dbConecction, ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _session = dbConecction ?? throw new ArgumentNullException(nameof(dbConecction));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public LoginData GetAccess(int userId)      
        {
            var result = _context.user_login_data.Where(u => u.uld_usr_id == userId).First();

            return _mapper.Map<LoginData>(result);

        }


        public bool CreateLoginData(User user, string salt)
        {
            var data = new Db_User_Login_Data();
            data.uld_usr_id = user.Id_User;
            data.uld_email = user.Email;
            data.uld_pwd = user.Password;
            data.uld_salt = salt;

            var result = _context.user_login_data.Add(data);
            _context.SaveChanges();

            return true;
        }

        public bool UpdateLoginData(User user)
        {
            var flag = false;
            var data = new Db_User_Login_Data();
            data.uld_email = user.Email;
            data.uld_pwd = user.Password;

            var existingUser = _context.user_login_data.Where(log => log.uld_usr_id == user.Id_User);

            if (existingUser != null)
            {
                _mapper.Map(data, existingUser);
                _context.SaveChanges();
                flag = true;
            }
            else
            {
                throw new Exception("User not found.");
            }

            return flag;
        }

        public void DeleteLoginData(int userId)
        {
            var dataToDelete = _context.user_login_data.Find(userId);

            if (dataToDelete != null)
            {
                _context.user_login_data.Remove(dataToDelete);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("User not found.");
            }
        }


    }
}