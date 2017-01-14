using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Models;

namespace TM.Domain.Services
{
    public class SlotFuncAuthRecordService
    {
        private TMDbContext _db;

        public SlotFuncAuthRecordService()
        {
            _db = new TMDbContext();
        }

        public SlotFuncAuthRecord Find(int id)
        {
            return _db.SlotFuncAuthRecords.Where(x => x.RecordId == id).FirstOrDefault();
        }

        public IEnumerable<SlotFuncAuthRecord> FindAll()
        {
            return _db.SlotFuncAuthRecords.AsEnumerable();
        }

        public IPagedList<SlotFuncAuthRecord> FindByPageds(string employeeId,string employeeName,string isCompleted, int currentPage, int pageSize)
        {
            currentPage = currentPage < 1 ? 1 : currentPage;

            var records = _db.SlotFuncAuthRecords.AsQueryable();

            if(!string.IsNullOrWhiteSpace(employeeId))
            {
                records = records.Where(x => x.EmployeeId.Contains(employeeId));
            }

            if (!string.IsNullOrWhiteSpace(employeeName))
            {
                records = records.Where(x => x.EmployeeName.Contains(employeeName));
            }

            if (!string.IsNullOrWhiteSpace(isCompleted))
            {
                records = records.Where(x => x.IsCompleted.Contains(isCompleted));
            }

            records = records.OrderByDescending(x => x.CreateDateTime);

            return records.ToPagedList(currentPage, pageSize);
        }

        public int Create(SlotFuncAuthRecord slotFuncAuthRecord)
        {
            try
            {
                _db.SlotFuncAuthRecords.Add(slotFuncAuthRecord);
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Modify(SlotFuncAuthRecord slotFuncAuthRecord)
        {
            try
            {
                _db.Entry(slotFuncAuthRecord).State = EntityState.Modified;
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int id)
        {
            try
            {
                SlotFuncAuthRecord slotFuncAuthRecord = _db.SlotFuncAuthRecords.Where(x => x.RecordId == id).FirstOrDefault();
                _db.SlotFuncAuthRecords.Remove(slotFuncAuthRecord);

                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
