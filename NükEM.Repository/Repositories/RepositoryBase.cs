using NükEM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NükEM.Repository
{
    public class RepositoryBase<TT> : IRepository<TT> where TT:class
    {
        private static NükEMEntities context;

        public NükEMEntities Context
        {
            get
            {
                //if (context==null)
                //{
                //    context = new NorthwindEntities();
                //}
                context = context ?? new NükEMEntities();//yukarıdaki ile aynı şey
                return context;
            }
            set
            {
                context = value;
            }
        }

        public bool Adding(TT entity)
        {
            //Set<TT>: Context in TT tipini algılamasını sağlar
            Context.Set<TT>().Add(entity);
            try
            {
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
               
                return false;
            }
        }

        public bool Deleting(TT entity)
        {
            Context.Set<TT>().Remove(entity);
            try
            {
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        //public decimal GetConstantValue()
        //{
            
        //    return ;
        //}

        public List<TT> Listing()
        {
            return Context.Set<TT>().ToList();
        }

        public bool Updating(TT entity)
        {
            try
            {
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
