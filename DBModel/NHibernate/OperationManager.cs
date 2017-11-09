using DBModel.Model;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.NHibernate
{
    public class OperationManager
    {
        public void Save(User operation)
        {
            using (ISession session = NHHelper.OpenSession())
            {
                using (ITransaction tr = session.BeginTransaction())
                {
                    try
                    {
                        session.SaveOrUpdate(operation);
                    }
                    catch (Exception ex)
                    {
                        tr.Rollback();
                        return;
                    }
                    tr.Commit();
                }
            }
            return;
        }

        public Operation Get(string operName)
        {
            using (ISession session = NHHelper.OpenSession())
            {
                var operation = session.QueryOver<Operation>()
                    .And(o => o.Name == operName)
                    .SingleOrDefault();

                return operation;
            }
        }
       
    }
}
