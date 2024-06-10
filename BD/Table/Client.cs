using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.Table
{
    [Table("Users")]
    public class Client
    {
        [Key]
        [Column("ClientID")]
        public int ClientID { get; set; }

        [Column("Login", TypeName = "varchar(150)")]
        public string Login { get; set; } = string.Empty;

        [Column("Password", TypeName = "varchar(65535)")]
        public string Password { get; set; } = string.Empty;

        [Column("Email", TypeName = "varchar(255)")]
        public string Email { get; set; } = string.Empty;

    }
}
