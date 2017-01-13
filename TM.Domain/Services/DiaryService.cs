using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Models;
using TM.Domain.ViewModels;

namespace TM.Domain.Services
{
    public class DiaryService
    {
        private TMDbContext _db;

        public DiaryService()
        {
            _db = new TMDbContext();
        }

        public Diary Find(int id)
        {
            return _db.Diaries.Where(x => x.DiaryId == id).FirstOrDefault();
        }

        public List<Diary> FindByUserId(int userId)
        {
           return _db.Diaries.Where(x => x.UserId == userId).ToList();
        }

        public Item FindItem(int id)
        {
            return _db.Items.Where(x => x.ItemId == id).FirstOrDefault();
        }

        public IPagedList<IGrouping<DateTime,Diary>> FindByUserId(string searchDate, int userId, int currentPage, int pageSize)
        {
            currentPage = currentPage < 1 ? 1 : currentPage;

            var diaries = _db.Diaries.AsQueryable();

            if (userId > 0)
            {
                diaries = diaries.Where(x => x.UserId == userId);
            }

            if (!string.IsNullOrWhiteSpace(searchDate))
            {
                if (searchDate.Contains("-"))
                {
                    string[] Dates = searchDate.Split('-');
                    DateTime sDate = DateTime.Parse(Dates[0]);
                    DateTime eDate = DateTime.Parse(Dates[1]);

                    diaries = diaries.Where(x => x.WorkDate >= sDate && x.WorkDate <= eDate);
                }
                else
                {
                    DateTime sDate = DateTime.Parse(searchDate);

                    diaries = diaries.Where(x => x.WorkDate == sDate);
                }
            }

            var diariesGroups = from d in diaries
                                group d by d.WorkDate into grp
                                orderby grp.Key descending
                                select grp;

            return diariesGroups.ToPagedList(currentPage, pageSize);
        }

        public IPagedList<DiaryGroup> FindGroupByUserId(string searchDate,string employeeId,int userId, int currentPage, int pageSize)
        {
            currentPage = currentPage < 1 ? 1 : currentPage;

            var diaries = _db.Diaries.AsQueryable();

            if(userId > 0)
            {
                diaries = diaries.Where(x => x.UserId == userId);
            }

            if (!string.IsNullOrWhiteSpace( searchDate ))
            {
                if(searchDate.Contains("-"))
                {
                    string[] Dates = searchDate.Split('-');
                    DateTime sDate = DateTime.Parse(Dates[0]);
                    DateTime eDate = DateTime.Parse(Dates[1]);

                    diaries = diaries.Where(x => x.WorkDate >= sDate && x.WorkDate <= eDate);
                }
                else
                {
                    DateTime sDate = DateTime.Parse(searchDate);

                    diaries = diaries.Where(x => x.WorkDate == sDate);
                }
            }

            if(!String.IsNullOrWhiteSpace(employeeId))
            {
                diaries = diaries.Where(x => x.User.EmployeeId == employeeId);
            }

            var diariesGroups = from d in diaries
                                group d by d.WorkDate into grp
                                orderby grp.Key descending
                                select new DiaryGroup
                                {
                                    WorkDate = grp.Key,
                                    Diaries = (from k in grp
                                               group k by k.User.EmployeeId into grp2
                                               select grp2).ToList()
                                };

            return diariesGroups.ToPagedList(currentPage, pageSize);
        }

        //工作統計圖表
        public JobWeightChart FindJobWeightData(string year, string month, int userId)
        {
            JobWeightChart chart = new JobWeightChart();

            List<Diary> diaryLogs = FindByMonth(year, month, userId);
            var diariesGroup = from q in diaryLogs
                                group q by q.Item into g
                                select new
                                {
                                    Item = g.Key,
                                    ItemSum = g.Select(x => x.Hours).Sum()
                                };

            chart.Legend = diariesGroup.Select(x => x.Item).ToList();
            chart.Series = diariesGroup.Select(x => new Series
            {
                value = x.ItemSum,
                name = x.Item
            }).ToList();

            return chart;
        }

        public List<Diary> FindByMonth(string year, string month, int userId)
        {
            List<Diary> diaries = FindByUserId(userId);
            diaries = diaries.Where(x => x.WorkDate.Year == int.Parse(year) && x.WorkDate.Month == int.Parse(month)).ToList();

            return diaries;
        }

        public int Create(Diary diary)
        {
            try
            {
                _db.Diaries.Add(diary);
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Modify(Diary diary)
        {
            try
            {
                _db.Entry(diary).State = EntityState.Modified;
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
                Diary diary = _db.Diaries.Where(x => x.DiaryId == id).FirstOrDefault();
                _db.Diaries.Remove(diary);
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
