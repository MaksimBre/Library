using Library.BusinessLogicLayer.Models;

namespace Library.PresentationLayer.Web.Models
{
    public class RoleModel
    {
        public RoleModel() { }
        public RoleModel(string name)
        {
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsChecked { get; set; }

        public static implicit operator Role(RoleModel rm)
        {
            Role genre = new Role(rm.Name)
            {
                Id = rm.Id
            };

            return genre;
        }

        public static implicit operator RoleModel(Role r)
        {
            RoleModel role = new RoleModel(r.Name)
            {
                Id = r.Id
            };

            return role;
        }
    }
}