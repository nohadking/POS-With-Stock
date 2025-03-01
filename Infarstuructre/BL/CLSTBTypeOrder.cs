
namespace Infarstuructre.BL
{
    public interface IITypeOrder
    {
        List<TBTypeOrder> GetAll();
        TBTypeOrder GetById(int IdTypeOrder);
        bool saveData(TBTypeOrder savee);
        bool UpdateData(TBTypeOrder updatss);
        bool deleteData(int IdTypeOrder);
        List<TBTypeOrder> GetAllv(int IdTypeOrder);
    }
    public class CLSTBTypeOrder: IITypeOrder
    {
        MasterDbcontext dbcontext;
        public CLSTBTypeOrder(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }
        public List<TBTypeOrder> GetAll()
        {
            List<TBTypeOrder> MySlider = dbcontext.TBTypeOrders.OrderByDescending(n => n.IdTypeOrder).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBTypeOrder GetById(int IdTypeOrder)
        {
            TBTypeOrder sslid = dbcontext.TBTypeOrders.FirstOrDefault(a => a.IdTypeOrder == IdTypeOrder);
            return sslid;
        }
        public bool saveData(TBTypeOrder savee)
        {
            try
            {
                dbcontext.Add<TBTypeOrder>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBTypeOrder updatss)
        {
            try
            {
                dbcontext.Entry(updatss).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool deleteData(int IdTypeOrder)
        {
            try
            {
                var catr = GetById(IdTypeOrder);
                catr.CurrentState = false;
                //TbSubCateegoory dele = dbcontex.TbSubCateegoorys.Where(a => a.IdBrand == IdBrand).FirstOrDefault();
                //dbcontex.TbSubCateegoorys.Remove(dele);
                dbcontext.Entry(catr).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public List<TBTypeOrder> GetAllv(int IdTypeOrder)
        {
            List<TBTypeOrder> MySlider = dbcontext.TBTypeOrders.OrderByDescending(n => n.IdTypeOrder == IdTypeOrder).Where(a => a.IdTypeOrder == IdTypeOrder).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
    }
}
