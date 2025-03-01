

namespace Infarstuructre.BL
{


    public interface IICustomerMessage
    {
        List<TBCustomerMessage> GetAll();
        TBCustomerMessage GetById(int IdCustomerMessage);
        bool saveData(TBCustomerMessage savee);
        bool UpdateData(TBCustomerMessage updatss);
        bool deleteData(int IdCustomerMessage);
        List<TBCustomerMessage> GetAllv(int IdCustomerMessage);
    }
    public class CLSTBCustomerMessage: IICustomerMessage
    {
        MasterDbcontext dbcontext;
        public CLSTBCustomerMessage(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }

        public List<TBCustomerMessage> GetAll()
        {
            List<TBCustomerMessage> MySlider = dbcontext.TBCustomerMessages.OrderByDescending(n => n.IdCustomerMessage).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBCustomerMessage GetById(int IdCustomerMessage)
        {
            TBCustomerMessage sslid = dbcontext.TBCustomerMessages.FirstOrDefault(a => a.IdCustomerMessage == IdCustomerMessage);
            return sslid;
        }
        public bool saveData(TBCustomerMessage savee)
        {
            try
            {
                dbcontext.Add<TBCustomerMessage>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBCustomerMessage updatss)
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
        public bool deleteData(int IdCustomerMessage)
        {
            try
            {
                var catr = GetById(IdCustomerMessage);
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
        public List<TBCustomerMessage> GetAllv(int IdCustomerMessage)
        {
            List<TBCustomerMessage> MySlider = dbcontext.TBCustomerMessages.OrderByDescending(n => n.IdCustomerMessage == IdCustomerMessage).Where(a => a.IdCustomerMessage == IdCustomerMessage).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

    }
}
