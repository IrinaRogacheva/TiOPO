namespace APITesting
{
    public class CreateProductDTO
    {
        public long id { get; set; }
        public long category_id { get; set; }
        public string title { get; set; }
        public string alias { get; set; }
        public string content { get; set; }
        public long price { get; set; }
        public long old_price { get; set; }
        public long status { get; set; }
        public object keywords { get; set; }
        public object description { get; set; }
        public long hit { get; set; }
    }
}
