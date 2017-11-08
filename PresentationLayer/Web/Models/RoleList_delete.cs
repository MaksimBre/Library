using System.Collections.Generic;

namespace Library.PresentationLayer.Web.Models
{
    public class RoleList
    {
        public RoleList() {
            Roles = new List<RoleCboxModel>();
        }



        public List<RoleCboxModel> Roles { get; set; }

        public void AddRoleItem(RoleCboxModel model)
        {
            Roles.Add(model);
        }
    }
}