using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EC1ContinuousAssessment_2005734
{
    public class AdagioDBContext : IdentityDbContext<AdagioUser>
    {
        public AdagioDBContext() : base("Data Source=OMERTA\\SQLEXPRESS;Initial Catalog=AdagioMgmt;Integrated Security=True;", throwIfV1Schema: false)
        {
            
        }

        public static AdagioDBContext Create()
        {
            return new AdagioDBContext();
        }
    }
}