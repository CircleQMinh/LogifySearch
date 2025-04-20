using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVTQ.LogifySearch.Domain.Setting
{
    public class DefaultUserSetting
    {
        public List<CreateUserModelSetting> Admins { get; set; }
        public List<CreateUserModelSetting> Managers { get; set; }
        public List<CreateUserModelSetting> Users { get; set; }
    }

    public class CreateUserModelSetting
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
