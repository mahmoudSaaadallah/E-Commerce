using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DataAccess.Repository.IRepository
{
    // The IRepository is a generic class which accept any type of class.
    /*
     A Repository pattern is a design pattern that mediates data from and to the Domain and Data Access Layers ( like Entity
     Framework Core / Dapper). 
     Repositories are classes that hide the logics required to store or retreive data. Thus, our application will not care about
     what kind of ORM we are using, as everything related to the ORM is handled within a repository layer. This allows you to have
     a cleaner seperation of concerns. Repository pattern is one of the heavily used Design Patterns to build cleaner solutions

     If you want to learn more about the Repository Pattern in ASP.NET Core, I recommend checking out this [*Ultimate Guide*]
     (^1^) by Mukesh Murugan. The guide covers everything you need to know about the Repository Pattern, Generic 
     Repository Patterns, Unit of Work, and related topics. The guide also includes a project implementation of a 
     clean architecture to access data ¹.
    //https://codewithmukesh.com/blog/repository-pattern-in-aspnet-core/
 
     */

    // Benfit of repository pattern
    /*
       Reduces Duplicate Queries
Imagine having to write lines of code to just fetch some data from your datastore. Now what if this set of queries are going to be
 used in multiple places in the application. Not very ideal to write this code over and over again, right? Here is the added 
advantage of Repository Classes. You could write your data access code within the Repository and call it from multiple 
Controllers / Libraries. Get the point?

      De-couples the application from the Data Access Layer
There are quite a lot of ORMs available for ASP.NET Core. Currently the most popular one is Entity Framework Core. But that change
in the upcoming years. To keep in pace with the evolving technologies and to keep our Solutions upto date, it is highly crucial to
build applications that can switch over to a new DataAccessTechnology with minimal impact on our application’s code base.

There can be also cases where you need to use multiple ORMs in a single solution. Probably Dapper to fetch the data and EFCore to 
write the data. This is solely for performance optimizations.

Repository pattern helps us to achieve this by creating an Abstration over the DataAccess Layer. Now, you no longer have to depend
 on EFCore or any other ORM for your application. EFCore becomes one of your options rather than your only option to access data.
     */

    public interface IRepository<T> where T : class
    {
        // This method used to get all the records in the data base.
        IEnumerable<T> GetAll();
        
        // This method used to get a specific record using the specific filter.
        IEnumerable<T> Get(Expression<Func<T, bool>> filter);

        // This method used to get a specifc record using its id.
        T GetById(int id);

        // This method used to add a new record to database.
        void Add(T entity);

        // This method used to add a mulitble records to database.
        void AddRange(IEnumerable<T> entities);

        // This method used to remove a specific record from database.
        void Remove(T entity);

        // This method used to remove mulitble records from database
        void RemoveRange(IEnumerable<T> entities);
        
        
    }
}
