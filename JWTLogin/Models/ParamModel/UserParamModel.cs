using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWTLogin.Models.ViewModel
{
    public class UserParamModel
    {
        [Required(ErrorMessage = "帳號為必填欄位"), MaxLength(32), MinLength(3, ErrorMessage = "帳號長度要大於3")]
        public string Username { get; set; }

        [Required(ErrorMessage = "密碼為必填欄位"), DataType(DataType.Password), MaxLength(32), MinLength(3, ErrorMessage = "密碼長度要大於3")]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "密碼不同")]
        public string ConfirmPassword { get; set; }
    }
}
