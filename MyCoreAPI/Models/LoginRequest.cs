using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreAPI.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "请输入在账号")]
        public string LoginID { get; set; }
        [Required(ErrorMessage = "请输入在密码")]
        public string LoginPwd { get; set; }
    }
}
