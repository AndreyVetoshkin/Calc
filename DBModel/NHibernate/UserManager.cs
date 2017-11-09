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
    public class UserManager
    {
        public void Save(User user)
        {
            using (ISession session = NHHelper.OpenSession())
            {
                using (ITransaction tr = session.BeginTransaction())
                {
                    try
                    {
                        session.SaveOrUpdate(user);
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

        public User Get(string login)
        {
            using (ISession session = NHHelper.OpenSession())
            {
                var user = session.QueryOver<User>()
                    .And(u => u.Login == login)
                    .SingleOrDefault();

                return user;
            }
        }

        public bool Check(string login, string password)
        {
            using (ISession session = NHHelper.OpenSession())
            {
                var criteria = session.CreateCriteria<User>();
                criteria.Add(Restrictions.Eq("Login", login));
                criteria.Add(Restrictions.Eq("Password", password));

                var user = criteria.UniqueResult<User>();
                return user != null;
            }
        }
    }
}
