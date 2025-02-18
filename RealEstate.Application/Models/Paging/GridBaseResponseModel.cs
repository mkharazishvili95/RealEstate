namespace RealEstate.Application.Models.Paging
{
    public class GridBaseResponseModel<T> : ResponseBaseModel
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        //public int CurrentElementStart { get; set; }
        //public int CurrentElementEnd { get; set; }

        public GridBaseResponseModel()
        {
            Items = new List<T>();
        }
    }
}
