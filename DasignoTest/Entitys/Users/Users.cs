using System.ComponentModel.DataAnnotations;
using System.Net;

namespace DasignoTest.Entitys.Users
{
    public class Users
    {
        [Key]
        public int userId { get; set; }
        public string name { get; set; }
        public string secondName { get; set; }
        public string surName {  get; set; }
        public string secondSurName { get; set; }
        public DateTime birthDate { get; set; }
        public int salary { get; set; }

        public DateTime creationDate { get; set; }
        public DateTime modifiedDate { get; set; }

    }
}
