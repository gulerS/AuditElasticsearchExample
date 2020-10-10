using AuditExample.Models;

namespace AuditExample.ElasticSearch
{
    public interface IElasticSearch
    {
        public void CheckExistsAndInsert(ChangeLog log);
    }
}