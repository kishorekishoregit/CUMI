using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CUMI.Models
{
   
 
public class UserContext: DbContext  
{
    public UserContext() : base("DBConnectionForMembership")  
    {}  
    public DbSet < UserProfile > UserRoles  
    {  
        get;  
        set;  
    }  

    public void get_ObjectContext()
    {

    }
}   
}