using IdentityFrame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityFrame.Repository
{
    public class StudioRepository
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();


        public IEnumerable<StudioModel> GetStudios()
        {
            return dbContext.Studios.ToList();
        }


    }
}