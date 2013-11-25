using System.Collections.Generic;

namespace TestHotKey.STORAGE
{
    public sealed class FunPicCollection
    {
        public List<FunPicModel> PicsList { get; set; }

        public FunPicCollection()
        {
            PicsList=new List<FunPicModel>();
        }
    }
}
